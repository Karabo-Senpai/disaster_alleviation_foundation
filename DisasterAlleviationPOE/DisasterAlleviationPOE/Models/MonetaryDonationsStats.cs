using System.ComponentModel.DataAnnotations;
using System;

namespace DisasterAlleviationPOE.Models
{
    public class MonetaryDonationsStats
    {

        [DataType(DataType.Date)]
        public DateTime? DonationDate { get; set; }

        public int MonetaryDonationsCount { get; set; }


    }
}
