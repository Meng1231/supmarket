namespace Supermarket.EneityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SupermarketDB : DbContext
    {
        public SupermarketDB()
            : base("name=SupermarketDBConnString")
        {
        }

        public virtual DbSet<AdminInfo> AdminInfo { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<DetailePurchases> DetailePurchases { get; set; }
        public virtual DbSet<DetailOrder> DetailOrder { get; set; }
        public virtual DbSet<LogDic> LogDic { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<ReturnGoods> ReturnGoods { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminInfo>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<AdminInfo>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<AdminInfo>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<AdminInfo>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.AdminInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdminInfo>()
                .HasMany(e => e.SysLog)
                .WithOptional(e => e.AdminInfo)
                .HasForeignKey(e => e.FK_AdminID);

            modelBuilder.Entity<Card>()
                .Property(e => e.TotalCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetailePurchases>()
                .Property(e => e.ProductAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DetailePurchases>()
                .Property(e => e.Reamrk)
                .IsUnicode(false);

            modelBuilder.Entity<DetailOrder>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DetailOrder>()
                .Property(e => e.subtotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LogDic>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<LogDic>()
                .HasMany(e => e.SysLog)
                .WithOptional(e => e.LogDic)
                .HasForeignKey(e => e.FK_TypeID);

            modelBuilder.Entity<Orders>()
                .Property(e => e.SunPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Way)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.DetailOrder)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.BarCode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CommodityDepict)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CommodityPriceOut)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.CommodityPriceIn)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DetailePurchases)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DetailOrder)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ReturnGoods)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Stock)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchases>()
                .Property(e => e.PurchasesTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchases>()
                .Property(e => e.Reamrk)
                .IsUnicode(false);

            modelBuilder.Entity<Purchases>()
                .HasMany(e => e.DetailePurchases)
                .WithRequired(e => e.Purchases)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReturnGoods>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ReturnGoods>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.AdminInfo)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.CommodityStockAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Stock>()
                .Property(e => e.StockUp)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Stock>()
                .Property(e => e.StockDown)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Behavior)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Parameters)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Exception)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CustomsID)
                .WillCascadeOnDelete(false);
        }
    }
}
