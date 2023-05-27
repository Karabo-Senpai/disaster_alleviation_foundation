using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using DisasterAlleviationPOE.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace DisasterAlleviationUnitTests
{
    [TestClass]
    public class LogicUnitTests
    {

        //Declaring Global Variables 

         private readonly DbContextOptions<DisasterAlleviationContext>disasterAlleviationContext ;
        private IConfigurationRoot configuration;

        public LogicUnitTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            configuration = builder.Build();
            disasterAlleviationContext = new DbContextOptionsBuilder<DisasterAlleviationContext>()
                .UseSqlServer(configuration.GetConnectionString("DAFDB"))
                .Options;
        }


        //Method To Test Monetary Donations
        [TestMethod]
        public void TestMonetaryDonations()
        {
            using (var context = new DisasterAlleviationContext(disasterAlleviationContext))
            {
                context.Database.EnsureCreated();

                var monetaryDonations = new MonetaryDonations()
                {
                    DonationDate = DateTime.Parse("2023-01-29"),
                    DonationAmount = 50000,
                    DonorName = "Bongani"
                };

                context.MonetaryDonations.AddRange(monetaryDonations);
               context.SaveChanges();

            }
        }

        //Method To Test Goods Donations
        [TestMethod]
        public void TestGoodsDonations()
        {
            using (var context = new DisasterAlleviationContext(disasterAlleviationContext))
            {
                context.Database.EnsureCreated();

                var goodsDonations = new GoodsDonations()
                {

                    CategoryID = 1,
                    DisasterID = 3,
                    DonationDate = DateTime.Parse("2023-01-05"),
                    NumberOfItems = 65,
                    Description = "Blankets",
                    DonorName = "Anonymous"
                };

                context.GoodsDonations.AddRange(goodsDonations);
                context.SaveChanges();

            }
        }


        //Method To Test Natural Disasters
        [TestMethod]
        public void TestDisasters()
        {
            using (var context = new DisasterAlleviationContext(disasterAlleviationContext))
            {
                context.Database.EnsureCreated();

                var disasters = new Disasters()
                {

                    RequiredAidTypeID = 1,
                    StartDate = DateTime.Parse("2023-01-15"),
                    EndDate = DateTime.Parse("2023-01-22"),
                    Description = "Floods",
                    Location = "Kwa-Zulu Natal"
                };

                context.Disasters.AddRange(disasters);
                context.SaveChanges();

            }
        }


        //Method To Test Monetary Allocations Via Goods Purchase
        [TestMethod]
        public void TestMonetaryAllocations()
        {
            using (var context = new DisasterAlleviationContext(disasterAlleviationContext))
            {
                context.Database.EnsureCreated();

                var purchaseGoods = new PurchaseGoods()
                {

                    DisasterID = 2,
                    MonetaryID = 4,
                    Description = "Bottled Water ",
                    PurchaseAmount = 65000
                };

                context.PurchaseGoods.AddRange(purchaseGoods);
                context.SaveChanges();

            }
        }


    }
}
