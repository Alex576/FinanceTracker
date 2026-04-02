using MasterData.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Data.DBContext
{
    public partial class MasterDataContext : DbContext
    {
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
        }
    }
}
