using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUKAT.Core;
using TSUKAT.Core.DbModels;

namespace TSUKAT.Infrastructure
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>().HasData(new Group
            {
                Id = 1,
                GroupName = "Ungroupped"
            });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Administrator"
                },
                new Role
                {
                    Id = 2,
                    Name = "User"
                });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "Admin",
                UserName = "Admin",
                Password = "root",
                AccessToken = "123456789",
                GroupId = 1
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                UserId = 1,
                RoleId = 1
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(40);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(50);
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.AccessToken).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.AccessToken).IsUnique();
                entity.Property(e => e.GroupId).HasDefaultValue(1);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey(e => new { e.RoleId, e.UserId });
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                entity.HasOne(ur => ur.Role).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GroupName).HasMaxLength(50);
                entity.HasMany(us => us.Users).WithOne(u => u.Group).HasForeignKey(gi => gi.GroupId);
            });
        }
    }
}
