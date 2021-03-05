using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProductAssortmentServiceApi.Models
{
    public partial class COOP_PAMContext : DbContext
    {
       

        public COOP_PAMContext(DbContextOptions<COOP_PAMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assortment> Assortments { get; set; }
        public virtual DbSet<AssortmentProduct> AssortmentProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Assortment>(entity =>
            {
                entity.HasKey(e => e.AssrtmntId);

                entity.ToTable("ASSORTMENTS");

                entity.Property(e => e.AssrtmntId).HasColumnName("ASSRTMNT_ID");

                entity.Property(e => e.AssrtmntActiveFrom)
                    .HasColumnType("datetime")
                    .HasColumnName("ASSRTMNT_ACTIVE_FROM");

                entity.Property(e => e.AssrtmntActiveTo)
                    .HasColumnType("datetime")
                    .HasColumnName("ASSRTMNT_ACTIVE_TO");

                entity.Property(e => e.AssrtmntName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ASSRTMNT_NAME")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AssortmentProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("ASSORTMENT_PRODUCTS");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.AssrtmntId).HasColumnName("ASSRTMNT_ID");

                entity.HasOne(d => d.Assrtmnt)
                    .WithMany(p => p.AssortmentProducts)
                    .HasForeignKey(d => d.AssrtmntId)
                    .HasConstraintName("FK_ASSORTMENT_PRODUCTS_ASSORTMENT");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.AssortmentProduct)
                    .HasForeignKey<AssortmentProduct>(d => d.ProductId)
                    .HasConstraintName("FK_ASSORTMENT_PRODUCTS_PRODUCT");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

                entity.Property(e => e.ProductEancode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("PRODUCT_EANCODE")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("PRODUCT_NAME")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
