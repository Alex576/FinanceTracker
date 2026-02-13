using System;
using System.Collections.Generic;
using FinanceTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data.Context;

public partial class FinanceTrackerContext : DbContext
{
    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Layout> Layouts { get; set; }

    public virtual DbSet<Tile> Tiles { get; set; }

    public virtual DbSet<TileType> TileTypes { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Layout>(entity =>
        {
            entity.ToTable("Layout");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[SQ_Layout])", "id")
                .HasColumnName("id");
            entity.Property(e => e.LayoutJson).HasColumnName("layout_json");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TileId).HasColumnName("tile_id");

            entity.HasOne(d => d.Tile).WithMany(p => p.Layouts)
                .HasForeignKey(d => d.TileId)
                .HasConstraintName("FK__Layout__tile_id__628FA481");
        });

        modelBuilder.Entity<Tile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tiles__3213E83F646A63D6");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[SQ_Tile_Code])", "DF__Tiles__id__619B8048")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.ParentTileId).HasColumnName("parent_tile_id");
            entity.Property(e => e.TileId).HasColumnName("tile_id");
            entity.Property(e => e.ToolId).HasColumnName("tool_id");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.ParentTile).WithMany(p => p.InverseParentTile)
                .HasForeignKey(d => d.ParentTileId)
                .HasConstraintName("FK__Tiles__tile_code__6477ECF3");

            entity.HasOne(d => d.Tool).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.ToolId)
                .HasConstraintName("FK__Tiles__tool_id__7B5B524B");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK__Tiles__type__797309D9");
        });

        modelBuilder.Entity<TileType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tile_Typ__3213E83FFA46817B");

            entity.ToTable("Tile_Type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToolCode__3213E83F5D638763");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ToolCode).HasColumnName("tool_code");

            entity.HasOne(d => d.ToolCodeNavigation).WithMany(p => p.InverseToolCodeNavigation)
                .HasForeignKey(d => d.ToolCode)
                .HasConstraintName("FK__ToolCode__tool_c__656C112C");
        });
        modelBuilder.HasSequence("SQ_Layout").StartsAt(1000L);
        modelBuilder.HasSequence("SQ_Tile_Code");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
