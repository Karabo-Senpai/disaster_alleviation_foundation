using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationPOE.Models
{
    public class RequiredAidType
    //Declaring Variables For User Input
    {

        [Key]
        [DisplayName("Required Aid Type ID")]
        public int RequiredAidTypeID { get; set; }

        [DisplayName("Required Aid Type")]
        public string RequiredAidName { get; set; }

        public ICollection<Disasters> Disasters { get; set; }



    }
}
