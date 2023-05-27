using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationPOE.Models
{
    public class Category
    {
        //Declaring Variables For User Input
        [Key]
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        //Creating Collection To Store Categories And New Categories To Be Created
        public ICollection<GoodsDonations> Goods { get; set; }
    }
}
