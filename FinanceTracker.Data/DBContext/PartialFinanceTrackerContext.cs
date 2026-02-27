using FinanceTracker.Data.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.DBContext
{
    public partial class FinanceTrackerContext : DbContext
    {
        private readonly ILogger<FinanceTrackerContext> m_Logger;

        [ActivatorUtilitiesConstructor]
        public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options, ILogger<FinanceTrackerContext> logger)
         : this(options)
        {
            m_Logger = logger;
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tile>(entity =>
            {
                entity.Ignore(e => e.Hierarchy);
            });
            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.Ignore(e => e.Hierarchy);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var strategy = Database.CreateExecutionStrategy();
            try
            {
                return await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken))
                    {
                        try
                        {
                            await SetTileHierarchyPath(cancellationToken);

                            var result = await base.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync();
                            return result;
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
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

        private async Task SetTileHierarchyPath(CancellationToken cancellationToken)
        {

            await SetHierarchyPathForAdded(cancellationToken);
            await SetHierarchyPathForUpdated(cancellationToken);
        }

        private async Task SetHierarchyPathForUpdated(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker.Entries<Tile>()
                              .Where(e => e.State == EntityState.Modified)
                              .ToList();
            foreach (var entry in entries)
            {
                var newParent = entry.Entity.ParentTileCode;
                var oldParent = entry.Property(x => x.ParentTileCode).OriginalValue;

                if (newParent != oldParent && newParent != null)
                {
                    var oldPath = entry.Entity.HierarchyPath;

                    var newParentPath = await Tiles
                       .Where(e => e.Id == newParent)
                       .Select(e => e.HierarchyPath)
                       .FirstOrDefaultAsync(cancellationToken) ?? HierarchyId.GetRoot();

                    var lastChildPath = await Tiles
                      .Where(e => e.HierarchyPath.GetAncestor(1) == newParentPath)
                      .OrderByDescending(e => e.HierarchyPath)
                      .Select(e => e.HierarchyPath)
                      .FirstOrDefaultAsync(cancellationToken);

                    var newNodePath = newParentPath.GetDescendant(lastChildPath, null);
                    entry.Entity.HierarchyPath = newNodePath;

                    await Database.ExecuteSqlInterpolatedAsync($@"
                            UPDATE dbo.Tiles 
                            SET HierarchyPath = HierarchyPath.GetReparentedValue({oldPath}, {newNodePath})
                            WHERE HierarchyPath.IsDescendantOf({oldPath}) AND Id <> {entry.Entity.Id}",
                        cancellationToken);
                }
            }
        }

        private async Task SetHierarchyPathForAdded(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker.Entries<Tile>()
                                .Where(e => e.State == EntityState.Added && e.Entity.ParentTileCode != null)
                                .ToList();

            foreach (var entry in entries)
            {
                var tile = entry.Entity;

                var parentPath = await Tiles
                    .Where(e => e.Id == tile.ParentTileCode)
                    .Select(e => e.HierarchyPath)
                    .FirstOrDefaultAsync(cancellationToken) ?? HierarchyId.GetRoot();

                var lastChildPath = await Tiles
                    .Where(e => e.HierarchyPath.GetAncestor(1) == parentPath)
                    .OrderByDescending(e => e.HierarchyPath)
                    .Select(e => e.HierarchyPath)
                    .FirstOrDefaultAsync(cancellationToken);

                tile.HierarchyPath = parentPath.GetDescendant(lastChildPath, null);
            }
        }
    }
}
