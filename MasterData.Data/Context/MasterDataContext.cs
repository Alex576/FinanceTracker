using System;
using System.Collections.Generic;
using MasterData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Data.Context;

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
                .HasDefaultValueSql("(NEXT VALUE FOR [md].[SQ_Capital])")
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
                .HasDefaultValueSql("(NEXT VALUE FOR [md].[SQ_Finances])")
                .HasColumnName("id");
            entity.Property(e => e.CapitalId).HasColumnName("capital_id");
            entity.Property(e => e.DateFrom).HasColumnName("date_from");
            entity.Property(e => e.DateTo).HasColumnName("date_to");
            entity.Property(e => e.LastModifiedUser).HasColumnName("last_modified_user");
            entity.Property(e => e.LastUpdate).HasColumnName("last_update");
            entity.Property(e => e.OptionsJson).HasColumnName("options_json");

            entity.HasOne(d => d.Capital).WithMany(p => p.Finances)
                .HasForeignKey(d => d.CapitalId)
                .HasConstraintName("FK__Finances__capita__534D60F1");
        });
        modelBuilder.HasSequence("SQ_Capital", "md");
        modelBuilder.HasSequence("SQ_Finances", "md");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
