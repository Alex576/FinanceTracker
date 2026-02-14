using System;
using System.Collections.Generic;
using MasterData.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Data.DBContext;

public partial class MasterDataContext : DbContext
{
    public MasterDataContext(DbContextOptions<MasterDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Capital> Capitals { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Capital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Capitals__3213E83FB262F327");

            entity.ToTable("Capitals", "md");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [md].[SQ_Capital])", "DF__Capitals__id__4D94879B")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Finances__3213E83F27F42E06");

            entity.ToTable("Finances", "md");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [md].[SQ_Finances])", "DF__Finances__id__5165187F")
                .HasColumnName("id");
            entity.Property(e => e.CapitalId).HasColumnName("capitalId");
            entity.Property(e => e.DateFrom).HasColumnName("dateFrom");
            entity.Property(e => e.DateTo).HasColumnName("dateTo");
            entity.Property(e => e.LastModifiedUser).HasColumnName("lastModifiedUser");
            entity.Property(e => e.LastUpdate).HasColumnName("lastUpdate");
            entity.Property(e => e.OptionsJson).HasColumnName("optionsJson");

            entity.HasOne(d => d.Capital).WithMany(p => p.Finances)
                .HasForeignKey(d => d.CapitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Finances__capita__534D60F1");
        });
        modelBuilder.HasSequence("SQ_Capital", "md");
        modelBuilder.HasSequence("SQ_Finances", "md");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
