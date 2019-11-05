using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VyFaq.Models
{
    public class faqhjelp
    {
        [Key]
        public int id { get; set; }
        public string spml { get; set; }
        public string svar { get; set; }
        public int like { get; set; }
        public int unlike { get; set; }
    }
}