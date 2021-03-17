using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fncPar.Models
{
    class Aleatorio
    {
        [Key]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public int ValorRandom { get; set; }
    }
}
