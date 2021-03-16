using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiDoble.Models
{
    public class Aleatorio
    {
        [Key]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public int ValorRandom { get; set; }
    }
}
