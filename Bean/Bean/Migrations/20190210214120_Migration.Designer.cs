﻿// <auto-generated />
using Bean.Resources.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bean.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    [Migration("20190210214120_Migration")]
    partial class Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("Bean.Resources.Database.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DateAdded");

                    b.Property<string>("QuoteAuthor");

                    b.Property<string>("QuoteContributor");

                    b.Property<string>("QuoteDate");

                    b.Property<string>("QuoteSource");

                    b.Property<string>("QuoteText");

                    b.HasKey("QuoteId");

                    b.ToTable("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}