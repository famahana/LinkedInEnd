using LinkedIn.Domain.Entities;
using LinkedIn.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Infrastructure.Data
{
        public class LinkedInDbContext : DbContext
        {
            public DbSet<UserEntity> Users { get; set; }
            public DbSet<CommentEntity> Comments { get; set; }
            public DbSet<LikeEntity> Likes { get; set; }
            public DbSet<ProfileEntity> Profiles { get; set; }
            public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
            public DbSet<PostEntity> Posts { get; set; }
            public LinkedInDbContext(DbContextOptions<LinkedInDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {                        
                    modelBuilder.Entity<UserEntity>()
                        .Property(u => u.CreatedAt)
                        .HasDefaultValueSql("SYSDATETIME()");
                    modelBuilder.Entity<UserEntity>()
                        .Property(u => u.Role)
                        .HasConversion<int>()
                        .IsRequired();
                    modelBuilder.Entity<UserEntity>()
                        .ToTable(t => t.HasCheckConstraint(
                             "CK_User_Role",
                             $"Role IN ({string.Join(",", Enum.GetValues(typeof(UserRole)).Cast<int>())})"));
                    modelBuilder.Entity<CommentEntity>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                    modelBuilder.Entity<LikeEntity>()
                        .HasOne(l => l.User)
                        .WithMany(u => u.Likes)
                        .HasForeignKey(l => l.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                    modelBuilder.Entity<LikeEntity>()
                        .HasOne(l => l.Post)
                        .WithMany(p => p.Likes)
                        .HasForeignKey(l => l.PostId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();
            }
        }
}
