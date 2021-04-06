using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Common;
using Common.Models;

namespace Logic
{
    class CliverrContext : DbContext
    {

        public DbSet<Posts> Posts { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<OrderPurchase> OrderPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.Posts)
            .WithOne(a => a.Account)
            .HasForeignKey<Posts>(c => c.PostAuthor);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
            options.UseSqlite("Data Source=./bin/Debug/netcoreapp3.1/cliverr.db");

        }

    }
}

