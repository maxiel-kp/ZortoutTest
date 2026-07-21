using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZortouTest.Models.Entities;

namespace ZortouTest.Data;

public partial class ZortExamDbContext : DbContext
{
    public ZortExamDbContext(DbContextOptions<ZortExamDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderDetail");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderProduct");

            entity.Property(e => e.PricePerUnit).HasColumnType("money");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Product");

            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
