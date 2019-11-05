using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VyFaq.Models
{
    public class kunde
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,30}$")]
        public string fornavn { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ. \\-]{2,30}$")]
        public string etternavn { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ.0-9 \\-]{2,30}$")]
        public string adresse { get; set; }
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        public string postnr { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ. \\-]{2,30}$")]
        public string poststed { get; set; }
    }
}