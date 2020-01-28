using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().ToTable("Posts");

            var userEntity = modelBuilder.Entity<User>();
            userEntity.HasMany(u => u.Posts)
                .WithOne(p => p.Author);           
        }
    }
}
