using MasterData.Data.DBModels;
using MasterData.Data.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MasterData.Data.DBContext
{
    public partial class MasterDataContext : DbContext
    {
        private readonly ILogger<MasterDataContext> m_Logger;
        private readonly IMasterDataCache m_Cache;
        private readonly Dictionary<Type, string> EntityToTableName = new Dictionary<Type, string>();

        [ActivatorUtilitiesConstructor]
        public MasterDataContext(DbContextOptions<MasterDataContext> options, ILogger<MasterDataContext> logger, IMasterDataCache cache) : this(options)
        {
            m_Logger = logger;
            m_Cache = cache;
        }

        public string GetTableName(Type type) => EntityToTableName[type];

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinanceItem>(entity =>
            {
                entity.Ignore(x => x.OptionsJson);
                entity.OwnsOne(
                    o => o.OptionsJson,
                    builder =>
                    {
                        builder.ToJson();
                        builder.OwnsMany(c => c.Attributes);
                    });
            });

            EntityToTableName.Add(typeof(ObjectEntity), modelBuilder.Entity<ObjectEntity>().Metadata.GetTableName() ?? ThrowFailException());

            string ThrowFailException()
            {
                throw new Exception($"Failed to get table name for {typeof(ObjectEntity)}");
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var strategy = Database.CreateExecutionStrategy();
            try
            {

                var isObjectChanged = ChangeTracker.Entries<ObjectEntity>().Any(e => e.State == EntityState.Deleted || e.State == EntityState.Modified || e.State == EntityState.Added);
                return await strategy.ExecuteAsync(async () =>
                {
                    int result;
                    using (var transaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken))
                    {
                        try
                        {
                            result = await base.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }

                    }
                    if (isObjectChanged)
                    {
                        await m_Cache.GetObjectStorage().LoadAsync(this);
                    }
                    return result;
                });
            }
            catch (RetryLimitExceededException ex)
            {
                m_Logger.LogError(ex, ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                m_Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
