using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationPOE.Models
{
    public class MonetaryDonations
    {
        //Declaring Variables For User Input

        [Key]
        public int MonetaryID { get; set; }

        [DataType(DataType.Date)]
       
        //Date Format
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [DisplayName ("Donation Date")]        
        public DateTime DonationDate { get; set; }

        [DataType (DataType.Currency)]
        [Column(TypeName = "Money")]
        [DisplayName("Donation Amount")]
        public decimal DonationAmount { get; set; }

        [DisplayFormat(NullDisplayText = "Anonymous")]
        [DisplayName("Donor Name")]
        public string DonorName { get; set; } 


    }
}
