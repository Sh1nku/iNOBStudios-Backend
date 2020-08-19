using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using iNOBStudios.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace iNOBStudios.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>().
                HasKey(x => new { x.PostId, x.TagId });
            builder.Entity<PostTag>()
                .HasOne(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId);

            builder.Entity<PostTag>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.TagId);

            builder.Entity<PostVersion>()
                .HasOne(x => x.Post)
                .WithOne(x => x.CurrentVersion)
                .HasForeignKey<PostVersion>(x => x.PostVersionId);

            //Indexes

            builder.Entity<ExternalFile>()
                .HasIndex(x => x.PostId);

            //Auth
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.ProfilePicture).HasMaxLength(191));
            builder.Entity<ExternalFile>(entity => entity.Property(m => m.FileName).HasMaxLength(191));
            builder.Entity<ExternalFile>(entity => entity.Property(m => m.MIMEType).HasMaxLength(128));
            builder.Entity<Post>(entity => entity.Property(m => m.AuthorId).HasMaxLength(128));
            builder.Entity<PostTag>(entity => entity.Property(m => m.TagId).HasMaxLength(64));
            builder.Entity<Tag>(entity => entity.Property(m => m.TagId).HasMaxLength(64));
            builder.Entity<PostVersion>(entity => entity.Property(m => m.Title).HasMaxLength(191));

            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(128));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.UserName).HasMaxLength(128));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Email).HasMaxLength(128));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(128));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(128));

            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(128));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Name).HasMaxLength(128));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(128));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(128));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));

            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(128));

            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(128));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(128));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(128));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(128));

        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<ExternalFile> ExternalFiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostVersion> PostVersions { get; set; }

    }
}
