using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ColoShop.Models
{
    public partial class EcommerceShopContext : DbContext
    {
        public EcommerceShopContext()
        {
        }

        public EcommerceShopContext(DbContextOptions<EcommerceShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Adv> Advs { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<New> News { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductOrder> ProductOrders { get; set; } = null!;
        public virtual DbSet<Statistic> Statistics { get; set; } = null!;
        public virtual DbSet<Subscribe> Subscribes { get; set; } = null!;
        public virtual DbSet<SystemSetting> SystemSettings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=EcommerceShop; User Id=sa; Password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Adv>(entity =>
            {
                entity.ToTable("Adv");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .HasColumnName("image");

                entity.Property(e => e.Link)
                    .HasMaxLength(500)
                    .HasColumnName("link");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(150)
                    .HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedDate");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .HasColumnName("message");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(150)
                    .HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<New>(entity =>
            {
                entity.ToTable("New");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .HasColumnName("description");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .HasColumnName("image");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title")
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'pending')");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_Order_Users");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.IdCategoryProduct, "IX_Product_IdCategoryProduct");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsFeature)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsHome)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsHot)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsSale)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductionCode).HasMaxLength(150);

                entity.Property(e => e.ViewCount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdCategoryProductNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategoryProduct)
                    .HasConstraintName("FK_tb_Product_tb_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.ToTable("Product_Order");

                entity.HasIndex(e => e.IdOrder, "IX_Product_Order_IdOrder");

                entity.HasIndex(e => e.IdProduct, "IX_Product_Order_IdProduct");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_tb_Product_Order_tb_Order");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_tb_Product_Order_tb_Product");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.ToTable("Statistic");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<Subscribe>(entity =>
            {
                entity.ToTable("Subscribe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasKey(e => e.SettingKey)
                    .HasName("PK_tb_SystemSetting");

                entity.ToTable("SystemSetting");

                entity.Property(e => e.SettingKey)
                    .HasMaxLength(50)
                    .HasColumnName("settingKey");

                entity.Property(e => e.SettingDescription)
                    .HasMaxLength(250)
                    .HasColumnName("settingDescription");

                entity.Property(e => e.SettingValue).HasColumnName("settingValue");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
