using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ListOfTenants.Models;

namespace ListOfTenants.Data
{
    public partial class wesidentityContext : IdentityDbContext<Aspnetuser>
    {


        public wesidentityContext(DbContextOptions<wesidentityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aspnetrole> Aspnetroles { get; set; } = null!;
        public virtual DbSet<Aspnetuser> Aspnetusers { get; set; } = null!;
        public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=wes-identity;user=root;password=Omnia@2703", ServerVersion.Parse("8.0.38-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Aspnetrole>(entity =>
            {
                entity.ToTable("aspnetroles");

                entity.HasIndex(e => e.TenantId, "IX_AspNetRoles_TenantId");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Aspnetroles)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_AspNetRoles_Tenant_TenantId");

                entity.HasMany(d => d.UsersNavigation)
                    .WithMany(p => p.RolesNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "RoleuserDictionaryStringObject",
                        l => l.HasOne<Aspnetuser>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK_RoleUser (Dictionary<string, object>)_AspNetUsers_UsersId"),
                        r => r.HasOne<Aspnetrole>().WithMany().HasForeignKey("RolesId").HasConstraintName("FK_RoleUser (Dictionary<string, object>)_AspNetRoles_RolesId"),
                        j =>
                        {
                            j.HasKey("RolesId", "UsersId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("roleuser (dictionary<string, object>)");

                            j.HasIndex(new[] { "UsersId" }, "IX_RoleUser (Dictionary<string, object>)_UsersId");
                        });
            });

            modelBuilder.Entity<Aspnetuser>(entity =>
            {
                entity.ToTable("aspnetusers");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.OwnerId, "IX_AspNetUsers_OwnerId");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LastLoginTime).HasMaxLength(6);

                entity.Property(e => e.LockoutEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LockoutEnd).HasMaxLength(6);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.RegistrationDate).HasMaxLength(6);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Aspnetusers)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_AspNetUsers_Owner_OwnerId");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "Aspnetuserrole",
                        l => l.HasOne<Aspnetrole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                        r => r.HasOne<Aspnetuser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("aspnetuserroles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<Aspnetusertoken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("aspnetusertokens");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Aspnetusertokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owner");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Permissions)
                    .UsingEntity<Dictionary<string, object>>(
                        "PermissionroleDictionaryStringObject",
                        l => l.HasOne<Aspnetrole>().WithMany().HasForeignKey("RolesId").HasConstraintName("FK_PermissionRole (Dictionary<string, object>)_AspNetRoles_Role~"),
                        r => r.HasOne<Permission>().WithMany().HasForeignKey("PermissionsId").HasConstraintName("FK_PermissionRole (Dictionary<string, object>)_Permission_Permi~"),
                        j =>
                        {
                            j.HasKey("PermissionsId", "RolesId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("permissionrole (dictionary<string, object>)");

                            j.HasIndex(new[] { "RolesId" }, "IX_PermissionRole (Dictionary<string, object>)_RolesId");
                        });
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("tenant");

                entity.HasIndex(e => e.OwnerId, "IX_Tenant_OwnerId");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Tenant_Owner_OwnerId");
            });

            modelBuilder.Entity<RoleUser>().ToTable("RoleUsers");
            modelBuilder.Entity<RoleUser>().HasKey(entity => new
            {
                entity.UsersId,
                entity.RolesId
            });
            base.OnModelCreating(modelBuilder);

        }


       
    }
}
