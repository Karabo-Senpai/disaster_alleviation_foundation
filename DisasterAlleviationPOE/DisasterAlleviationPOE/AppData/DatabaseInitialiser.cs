using DisasterAlleviationPOE.Models;
using System;
using System.Linq;

namespace DisasterAlleviationPOE.AppData
{
    static class DatabaseInitialiser
    {

        public static void DBInitialise(DisasterAlleviationContext context)
        {
            //Ensuiring that the database is created
            context.Database.EnsureCreated();

            if (context.MonetaryDonations.Any())
            {
                return;
            }

            //Inserting Data Into MonetaryDonations Table
            var monetary = new MonetaryDonations[]
            {
                new MonetaryDonations { DonationDate=DateTime.Parse("2022-12-05"), DonationAmount = 850000, DonorName = "Bridget"},
                new MonetaryDonations { DonationDate=DateTime.Parse("2022-12-10"), DonationAmount = 40000, DonorName = "Luvhengo"},
                new MonetaryDonations { DonationDate=DateTime.Parse("2022-12-10"), DonationAmount = 200000, DonorName = "Greg"},
                new MonetaryDonations { DonationDate=DateTime.Parse("2022-12-10"), DonationAmount = 300000, DonorName = ""}
            };

            //Adding Data To The Monetary Donations Table
            foreach(MonetaryDonations monetaryDonation in monetary)
            {
                context.MonetaryDonations.Add(monetaryDonation);
            }
            //Saving Changes
            context.SaveChanges();


            //Inserting Into Category Table
            var categories = new Category[] { 
            
            new Category { CategoryName = "Clothes"},
            new Category { CategoryName = "Non-Perishble Goods"},
            new Category { CategoryName = "Perishble Goods"},
            new Category { CategoryName = "Food"},
            new Category { CategoryName = "Adult Clothes"},
            new Category { CategoryName = "Blankets"}
            
            };

            foreach (Category category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();


            //Inserting into required aid table
            var requiredaidtypes = new RequiredAidType[] { 
            
            new RequiredAidType{ RequiredAidName = "Shelter"},
            new RequiredAidType{ RequiredAidName = "Food Provisions"},
            new RequiredAidType{ RequiredAidName = "Clothing and Blankets"},
            new RequiredAidType{ RequiredAidName = "Water Provisions"} 
            };

            foreach (RequiredAidType requiredAidType in requiredaidtypes)
            {
                context.RequiredAidTypes.Add(requiredAidType);
            }

            context.SaveChanges();

            //Inserting data into Disaster Table
            var disasters = new Disasters[] { 
            
             
                new Disasters{ RequiredAidTypeID = 2,StartDate=DateTime.Parse("2023-01-19"), EndDate=DateTime.Parse("2023-01-23"),Description="Earthquake",Location="Canada" },
                new Disasters{ RequiredAidTypeID = 1,StartDate=DateTime.Parse("2023-01-15"), EndDate=DateTime.Parse("2023-01-22"),Description="Floods",Location="Kwa-Zulu Natal"}

            };

            foreach (Disasters disaster in disasters)
            {
                context.Disasters.Add(disaster);
            }

            context.SaveChanges();



            //Iserting data Into GoodsDonations Table
            var goods = new GoodsDonations[] { 
            
                new GoodsDonations{CategoryID = 2, DisasterID = 2, DonationDate=DateTime.Parse("2022-12-20"), NumberOfItems = 20, Description="Canned Foods", DonorName=""},
                new GoodsDonations{CategoryID = 2, DisasterID = 1, DonationDate=DateTime.Parse("2023-01-05"), NumberOfItems = 100, Description="Water Bottles", DonorName="Bridget"},
                new GoodsDonations{CategoryID = 4, DisasterID = 2, DonationDate=DateTime.Parse("2023-01-11"), NumberOfItems = 1200, Description="Food Parcels", DonorName="Greg"}
             
            };

            foreach (GoodsDonations goodsDonations in goods)
            {
                context.GoodsDonations.Add(goodsDonations);
            }
            context.SaveChanges();
        }

    }
}
