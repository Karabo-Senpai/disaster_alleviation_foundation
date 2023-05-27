using System.ComponentModel.DataAnnotations;
using System;

namespace DisasterAlleviationPOE.Models
{
    public class GoodDonationStats
    {

        [DataType(DataType.Date)]
        public DateTime? DonationDate { get; set; }

        public int GoodsDonationCount { get; set; }

    }
}
