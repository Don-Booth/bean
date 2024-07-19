using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bean.Resources.Database
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.2", @"Data\");
            //Options.UseSqlite($"Data Source=Database.sqlite");
            Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");
        }
    }
}