using System.ComponentModel.DataAnnotations;
using System;

namespace DisasterAlleviationPOE.Models
{
    public class DisasterStats
    {

        [DataType(DataType.Date)]
        public DateTime? Active { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ActiveDate { get; set; }

        public int DisastersCount { get; set; }

        public string Description { get; set; }


    }
}
