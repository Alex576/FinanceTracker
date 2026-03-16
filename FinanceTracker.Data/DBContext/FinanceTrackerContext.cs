using System;
using System.Collections.Generic;
using FinanceTracker.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data.DBContext;

public partial class FinanceTrackerContext : DbContext
{
    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Layout> Layouts { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Tile> Tiles { get; set; }

    public virtual DbSet<TileItem> TileItems { get; set; }

    public virtual DbSet<TileType> TileTypes { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    public virtual DbSet<UserSetting> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Layout>(entity =>
        {
            entity.ToTable("Layout");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[SQ_Layout])", "id")
                .HasColumnName("id");
            entity.Property(e => e.LayoutJson).HasColumnName("layoutJson");
            entity.Property(e => e.TileCode).HasColumnName("tileCode");

            entity.HasOne(d => d.TileCodeNavigation).WithMany(p => p.Layouts)
                .HasForeignKey(d => d.TileCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Layout__tile_id__628FA481");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuItem__3213E83F19C7DECD");

            entity.ToTable("MenuItem");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentMenuCode).HasColumnName("parentMenuCode");
            entity.Property(e => e.ToolCode).HasColumnName("toolCode");

            entity.HasOne(d => d.ParentMenuCodeNavigation).WithMany(p => p.InverseParentMenuCodeNavigation)
                .HasForeignKey(d => d.ParentMenuCode)
                .HasConstraintName("FK__MenuItem__parent__339FAB6E");

            entity.HasOne(d => d.ToolCodeNavigation).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.ToolCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MenuItem__toolCo__3587F3E0");
        });

        modelBuilder.Entity<Tile>(entity =>
        {
            entity.HasKey(e => e.TileCode).HasName("PK__Tiles__3213E83F646A63D6");

            entity.Property(e => e.TileCode)
                .ValueGeneratedNever()
                .HasColumnName("tileCode");
            entity.Property(e => e.Hierarchy)
                .HasMaxLength(4000)
                .HasComputedColumnSql("([hierarchyPath].[ToString]())", false)
                .HasColumnName("hierarchy");
            entity.Property(e => e.HierarchyPath).HasColumnName("hierarchyPath");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.ParentTileCode).HasColumnName("parentTileCode");
            entity.Property(e => e.ToolCode).HasColumnName("toolCode");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.ToolCodeNavigation).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.ToolCode)
                .HasConstraintName("FK__Tiles__tool_id__7B5B524B");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tiles__type__797309D9");
        });

        modelBuilder.Entity<TileItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TileItem__3213E83FB919BC31");

            entity.ToTable("TileItem");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[SQ_TileItem])")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TileType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tile_Typ__3213E83FFA46817B");

            entity.ToTable("TileType");

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
            entity.Property(e => e.ParentToolCode).HasColumnName("parentToolCode");

            entity.HasOne(d => d.ParentToolCodeNavigation).WithMany(p => p.InverseParentToolCodeNavigation)
                .HasForeignKey(d => d.ParentToolCode)
                .HasConstraintName("FK__ToolCode__tool_c__656C112C");
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSett__3213E83FA3CFE7E7");

            entity.HasIndex(e => e.Path, "IX_Path");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ParentSettingCode).HasColumnName("parentSettingCode");
            entity.Property(e => e.Path)
                .IsUnicode(false)
                .UseCollation("Latin1_General_BIN")
                .HasColumnName("path");
            entity.Property(e => e.SettingCode).HasColumnName("settingCode");
            entity.Property(e => e.SettingsJson).HasColumnName("settingsJson");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });
        modelBuilder.HasSequence("SQ_Layout").StartsAt(1000L);
        modelBuilder.HasSequence("SQ_Tile_Code");
        modelBuilder.HasSequence("SQ_TileItem").StartsAt(10000L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
