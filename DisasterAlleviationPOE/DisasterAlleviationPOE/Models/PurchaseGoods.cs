using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DisasterAlleviationPOE.Models
{
    public class PurchaseGoods
    {
        //Declaring Variables For User Input


        //Primary key
        [Key]
        public int GoodsPurchaseID { get; set; } 

        public int DisasterID { get; set; }

        public int MonetaryID { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        [DisplayName("Purchase Amount")]
        public decimal PurchaseAmount { get; set; }

        public Disasters Disasters { get; set; }
        public MonetaryDonations MonetaryDonations { get; set; }


    }
}
