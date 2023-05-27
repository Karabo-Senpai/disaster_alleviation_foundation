using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace DisasterAlleviationPOE.Models
{
    public class Disasters
    {
        //Declaring Variables For User Input

        //Primary key
        [Key]
        public int DisasterID { get; set; }

        //Date Format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        //Date Format
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [DisplayName("Required Aid Type ID")]
        public int RequiredAidTypeID { get; set; }

        //foriegn key
        [DisplayName("Required Aid Type")]
        public RequiredAidType RequiredAidType { get; set; }

        public ICollection<GoodsDonations> Goods { get; set; }



    }
}
