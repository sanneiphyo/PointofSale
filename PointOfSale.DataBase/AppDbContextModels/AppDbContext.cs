using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PointOfSale.DataBase.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductCategory> TblProductCategories { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=POSDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07376C5FFE");

            entity.ToTable("TblProduct");

            entity.HasIndex(e => e.ProductCode, "UQ__Product__2F4E024FB45C063F").IsUnique();

            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ProductCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductCategoryCodeNavigation).WithMany(p => p.TblProducts)
                .HasPrincipalKey(p => p.ProductCategoryCode)
                .HasForeignKey(d => d.ProductCategoryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Product__5EBF139D");
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECCE5BC6E96A");

            entity.ToTable("TblProductCategory");

            entity.HasIndex(e => e.ProductCategoryCode, "UQ__ProductC__A6C3D9BCD12173B5").IsUnique();

            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C3FFA00E612D");

            entity.ToTable("TblSale");

            entity.HasIndex(e => e.VoucherNo, "UQ__Sale__3AD31D6F8D8CEAFE").IsUnique();

            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblSaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("PK__SaleDeta__70DB14FE03474087");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.VoucherNoNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.VoucherNo)
                .HasForeignKey(d => d.VoucherNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleDetai__Vouch__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
