using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace DisasterAlleviationPOE.Models
{
    public class GoodsDonations
    {
        //Declaring Variables For User Input
        [Key]
        public int GoodsDonationID { get; set; }

        //Date Format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Donation Date")]
        public DateTime DonationDate { get; set; }

        public int NumberOfItems { get; set; }

        public string Description { get; set; }

        [DisplayFormat(NullDisplayText = "Anonymous")]
        [DisplayName("Donor Name")]
        public string DonorName { get; set; }

        public int CategoryID { get; set; }
        public int DisasterID { get; set; }


        public Category Category { get; set; }

        public Disasters Disaster { get; set; }

    }
}
