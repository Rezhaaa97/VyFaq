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
        public string adresse { get; set; }
        public string postnr { get; set; }

        public virtual Poststed poststed { get; set; }
    }

    public class Poststed
    {
        [Key]
        public string postnr { get; set; }
        public string poststed { get; set; }

        public virtual List<Kunde> kunder { get; set; }
    }

    public class KundeContext : DbContext
    {
        public KundeContext()
          : base("name=Kunde")
        {
            Database.CreateIfNotExists();
        }

        // konstruktøren under brukes kun under test!
        public KundeContext(DbConnection connection)
                : base(connection,true)
        {
        }
      
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Poststed> Poststeder { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}