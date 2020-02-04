using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NewsPortal.Data.Model;
using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Context
{
    public class NewsPortalDbContext : DbContext
    {
        public NewsPortalDbContext(DbContextOptions<NewsPortalDbContext> options)
            : base(options: options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Posts)
                .WithOne(post => post.Author);

            modelBuilder.Entity<Post>()
                .HasMany<Comment>()
                .WithOne()
                .HasForeignKey(comment => comment.PostId);

            modelBuilder.Entity<Comment>()
                .HasOne<Comment>()
                .WithMany()
                .HasForeignKey(comment => comment.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
