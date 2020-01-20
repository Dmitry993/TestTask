using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Context
{
    public class NewsPortalDbContext : DbContext
    {
        public NewsPortalDbContext(DbContextOptions<NewsPortalDbContext> options)
            : base(options: options)
        { }
        public DbSet<ApplicationUserDb> Users { get; set; }
    }
}
