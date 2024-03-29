﻿using System;
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

            builder.Entity<MenuItem>()
                .HasOne(x => x.ParentMenuItem)
                .WithMany(x => x.ChildMenuItems)
                .HasForeignKey(x => x.ParentMenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuItem>()
                .HasOne(x => x.ParentMenuItem)
                .WithMany(x => x.ChildMenuItems)
                .HasForeignKey(x => x.ParentMenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuItem>()
                .HasOne(x => x.Post)
                .WithMany(x => x.MenuItems)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            //Indexes

            builder.Entity<ExternalFile>()
                .HasIndex(x => x.PostId);
            builder.Entity<ExternalFile>().HasIndex(entity => entity.PostId);
            builder.Entity<ExternalFile>().HasIndex(entity => entity.PostedTime);

            builder.Entity<Post>().HasIndex(entity => entity.Published);
            builder.Entity<Post>().HasIndex(entity => entity.List);
            builder.Entity<Post>().HasIndex(entity => entity.AddedTime);
            builder.Entity<Post>().HasIndex(entity => entity.FirstPublished);
            builder.Entity<Post>().HasIndex(entity => entity.Alias).IsUnique();

            builder.Entity<PostVersion>().HasIndex(entity => entity.PostId);
            builder.Entity<PostVersion>().HasIndex(entity => entity.PostedDate);

            builder.Entity<MenuItem>().HasIndex(entity => entity.Priority);

            //Auth
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.ProfilePicture).HasMaxLength(191));
            builder.Entity<ExternalFile>(entity => entity.Property(m => m.FileName).HasMaxLength(191));
            builder.Entity<ExternalFile>(entity => entity.Property(m => m.MIMEType).HasMaxLength(128));
            builder.Entity<Post>(entity => entity.Property(m => m.AuthorId).HasMaxLength(128));
            builder.Entity<PostTag>(entity => entity.Property(m => m.TagId).HasMaxLength(64));
            builder.Entity<Tag>(entity => entity.Property(m => m.TagId).HasMaxLength(64));
            builder.Entity<PostVersion>(entity => entity.Property(m => m.Title).HasMaxLength(191));
            builder.Entity<PostVersion>(entity => entity.Property(m => m.PreviewText).HasMaxLength(2047));

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
        public DbSet<RawFile> RawFiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<PostVersion> PostVersions { get; set; }
        public DbSet<RawText> RawTexts { get; set; }

    }
}
