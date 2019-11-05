using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VyFaq.Models
{
    public class kunde
    {
        [Key]
        public int id { get; set; }

        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,30}$")]
        public string fornavn { get; set; }

        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ. \\-]{2,30}$")]
        public string etternavn { get; set; }

        [Required]
        [EmailAddress]
        public string epost { get; set; }

        [Required]
        [RegularExpression("^[0-9a-zA-ZøæåØÆÅ?.,!\\-. ]{2,300}$")]
        public string spm { get; set; }
    }
}