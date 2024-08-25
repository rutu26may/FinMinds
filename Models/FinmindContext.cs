using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace finminds.Models;

public partial class FinmindContext : DbContext
{
    public FinmindContext()
    {
    }

    public FinmindContext(DbContextOptions<FinmindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }

    public virtual DbSet<Eodpublisher> Eodpublishers { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<TradeFlow> TradeFlows { get; set; }

    public virtual DbSet<TradingBook> TradingBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("finmind");

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.HasKey(e => e.SystemInformationId).HasName("PK__BuildVer__35E58ECA52804FDA");

            entity.ToTable("BuildVersion");

            entity.Property(e => e.SystemInformationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SystemInformationID");
            entity.Property(e => e.DatabaseVersion)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("Database Version");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VersionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CustCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("custCode");
            entity.Property(e => e.CustLivePosition).HasColumnName("cust_Live_Position");
            entity.Property(e => e.CustStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("custStatus");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Eodpublisher>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EODPublisher");

            entity.Property(e => e.EodDate)
                .HasColumnType("date")
                .HasColumnName("eodDate");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PubStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pubStatus");
            entity.Property(e => e.SystemName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("systemName");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK_ErrorLog_ErrorLogID");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");
            entity.Property(e => e.ErrorMessage)
                .IsRequired()
                .HasMaxLength(4000);
            entity.Property(e => e.ErrorProcedure).HasMaxLength(126);
            entity.Property(e => e.ErrorTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(128);
        });

        modelBuilder.Entity<TradeFlow>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TradeFlow");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.LoadDate)
                .HasColumnType("date")
                .HasColumnName("loadDate");
            entity.Property(e => e.LoadStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("loadStatus");
            entity.Property(e => e.TradeId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("trade_id");
        });

        modelBuilder.Entity<TradingBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TradingBook");

            entity.Property(e => e.BookLivePosition).HasColumnName("book_Live_Position");
            entity.Property(e => e.BookName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bookName");
            entity.Property(e => e.BookStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("bookStatus");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
