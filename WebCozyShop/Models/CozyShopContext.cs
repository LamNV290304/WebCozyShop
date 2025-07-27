using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

public partial class CozyShopContext : DbContext
{
    public CozyShopContext()
    {
    }

    public CozyShopContext(DbContextOptions<CozyShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BECBAAF5E");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD594F5D28A");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Invoices__UserId__5165187F");

        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F1578F1C086A37A");

            entity.Property(e => e.InvoiceDetailId).HasColumnName("InvoiceDetailID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VariantId).HasColumnName("VariantID");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__InvoiceDe__Invoi__5441852A");

            entity.HasOne(d => d.Variant).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__InvoiceDe__Varia__5535A963");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED6E62057C");

            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Catego__403A8C7D");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("PK__ProductV__0EA233E4ED1A3582");

            entity.HasIndex(e => e.Sku, "UQ__ProductV__CA1ECF0D9E9E2EFE").IsUnique();

            entity.Property(e => e.VariantId).HasColumnName("VariantID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SKU");
            entity.Property(e => e.StockQuantity).HasDefaultValue(0);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProductVa__Produ__45F365D3");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Tokens__A9D105358A2F811B");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.Token1)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Token");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACAD7DFC1C");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4D6799292").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347DB0543C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
