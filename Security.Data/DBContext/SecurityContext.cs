using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Security.Data.DBModels;

namespace Security.Data.DBContext;

public partial class SecurityContext : DbContext
{
    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83FE3E4A363");

            entity.ToTable("Roles", "sc");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [sc].[SQ_Role])", "DF__Roles__id__440B1D61")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F098F2E16");

            entity.ToTable("Users", "sc");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [sc].[SQ_User])", "DF__Users__id__403A8C7D")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true, "DF__Users__active__01142BA1")
                .HasColumnName("active");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("lastLogin");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OptionsJson).HasColumnName("optionsJson");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .HasColumnName("password");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(1000)
                .HasColumnName("refreshToken");
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users_Ro__3213E83F0D1EAC68");

            entity.ToTable("Users_Roles", "sc");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [sc].[SQ_Users_Roles])", "DF__Users_Roles__id__47DBAE45")
                .HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Role).WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Rol__role___49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Rol__user___48CFD27E");
        });
        modelBuilder.HasSequence("SQ_Role", "sc");
        modelBuilder.HasSequence("SQ_User", "sc");
        modelBuilder.HasSequence("SQ_Users_Roles", "sc");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
