using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Bean.Data;

namespace Bean.Resources.Database
{
    public class SQLiteDbContext : DbContext
    {
        #region Tables
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<HeartBeat> HeartBeats { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            //string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.2", @"Data\");
            Options.UseSqlite($"Data Source={Data.General.ConnectionString}");
            //Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");
        }
    }
}