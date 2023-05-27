using DisasterAlleviationPOE.Models;
using Microsoft.EntityFrameworkCore;


namespace DisasterAlleviationPOE.AppData
{
    public class DisasterAlleviationContext : DbContext
    {

        public DisasterAlleviationContext() { }

        public DisasterAlleviationContext(DbContextOptions<DisasterAlleviationContext> options) : base(options)
        {

        }
        public DbSet<MonetaryDonations> MonetaryDonations { get; set; }
        public DbSet<GoodsDonations> GoodsDonations { get; set; }
        public DbSet<Disasters>Disasters { get; set; }
        
        public DbSet<Category>Categories { get; set; }

        public DbSet<RequiredAidType> RequiredAidTypes { get; set; }

        public DbSet<PurchaseGoods> PurchaseGoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonetaryDonations>().ToTable("MonetaryDonation");
            modelBuilder.Entity<GoodsDonations>().ToTable("GoodsDonations");
            modelBuilder.Entity<Disasters>().ToTable("Disasters");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<PurchaseGoods>().ToTable("PurchaseGoods");

            base.OnModelCreating(modelBuilder);

        }


    }
}
