using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using System.Data.Entity.Core.EntityClient;
using System.Data.Common;

namespace VyFaq.Models
{
    public class Kunde
    {
        [Key]
        public int id { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string epost { get; set; }
        public string spml { get; set; }

   }

  

    public class FaqContext : DbContext
    {
        public FaqContext()
          : base("name=Faq")
        {
            Database.SetInitializer(new DBInit());
        }

        // konstruktøren under brukes kun under test!
      /*  public FaqContext(DbConnection connection)
                : base(connection,true)
        {
        }*/
      
        public DbSet<kunde> Kunder { get; set; }
        public DbSet<faqhjelp> Spm { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}