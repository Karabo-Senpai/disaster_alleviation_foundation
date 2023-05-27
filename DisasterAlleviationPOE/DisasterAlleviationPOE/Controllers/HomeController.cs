using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationPOE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DisasterAlleviationContext alleviationContext;


        //Declaring global variables
        public decimal totalMonetaryDonations {get; set; }
        public decimal totalGoodsDonations {get; set; }
        public decimal totalFisasters {get; set; }

        public HomeController(ILogger<HomeController> logger , DisasterAlleviationContext disasterAlleviation)
        {
            _logger = logger;
            alleviationContext = disasterAlleviation;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GoodsDonationsStats()
        {
            ViewBag.totalGoods = getTotalGoods();
        
            IQueryable<GoodDonationStats> data =
                from good in alleviationContext.GoodsDonations
                group good by good.DonationDate into
                dateGroup
                select new GoodDonationStats()
                {
                    DonationDate = dateGroup.Key,
                    GoodsDonationCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        //Getting Total Number Of Goods Donated
        public decimal getTotalGoods()
        {
            totalGoodsDonations = alleviationContext.GoodsDonations
                .Sum(x => x.NumberOfItems);
            return totalGoodsDonations;
        }


        public async Task<ActionResult> MonetaryDonationsStats()
        {
            ViewBag.totalMonetaryDonations = getTotalMonetaryDonation().ToString("C0");

            IQueryable<MonetaryDonationsStats> data =
                from monetary in alleviationContext.MonetaryDonations
                group monetary by monetary.DonationDate into
                dateGroup
                select new MonetaryDonationsStats()
                {
                    DonationDate = dateGroup.Key,
                    MonetaryDonationsCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }


        public decimal getTotalMonetaryDonation()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations
                .Sum(v => v.DonationAmount);
            return totalMonetaryDonations;
        }


        public async Task<ActionResult> AllDisastersStats()
        {
            ViewBag.totalDisasters = getTotalActiveDisasters();
            

            var disasterViewModel = from disaster in alleviationContext.Disasters
                                    join goods in alleviationContext.GoodsDonations on disaster.DisasterID equals goods.DisasterID into disasterGood
                                    from goods in disasterGood.DefaultIfEmpty()

                                    join category in alleviationContext.Categories on goods.CategoryID equals category.CategoryID into categoryGood
                                    from category in categoryGood.DefaultIfEmpty()

                                    join goodspurchase in alleviationContext.PurchaseGoods on disaster.DisasterID equals goodspurchase.DisasterID into disasterFund
                                    from goodspurchase in disasterFund.DefaultIfEmpty()
                                    select new DisasterDetailsStats {DisastersM = disaster, GoodsDonationsM = goods, CategoryM = category, PurchaseGoodsM = goodspurchase };

            return View(await disasterViewModel.AsNoTracking().ToListAsync());
        }



        public decimal getTotalActiveDisasters()
        {
            totalFisasters = alleviationContext.Disasters.Where(p => p.StartDate >= DateTime.Today).Count();
            return totalFisasters;
        }

        //

        protected override void Dispose(bool disposing)
        {
            alleviationContext.Dispose();
            base.Dispose(disposing);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
