using ConAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models
{
    public class AppDbContext: IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions options): base(options) { }

        public DbSet<SessionToken> SessionTokens { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 1 to many
            builder.Entity<User>().HasMany(u => u.Projects).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            // many to many
            // setam cheia compusa
            builder.Entity<Offer>().HasKey(off => new { off.ConstructorId, off.ProjectId });

            builder.Entity<Offer>().HasOne(off => off.User).WithMany(u => u.Offers)
                .HasForeignKey(off => off.ConstructorId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Offer>().HasOne(off => off.Project).WithMany(p => p.Offers)
                .HasForeignKey(off => off.ProjectId);

            builder.Entity<Rating>().HasOne(r => r.User).WithOne(u => u.Rating);

            base.OnModelCreating(builder);

            builder.Entity<UserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });
                ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });


        }
    }
}
