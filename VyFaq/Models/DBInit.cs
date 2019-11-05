using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VyFaq.Models
{
    public class DBInit: DropCreateDatabaseAlways<FaqContext>
    {
        protected override void Seed(FaqContext context)
        {

            var melding1 = new kunde
            {
                fornavn = "Ole",
                etternavn = "Hansen",
                epost = "OleHansen@gmail.com",
                spm = "Er det Mulig å bestille seter på forhånd?"
            };

            var melding2 = new kunde
            {
                fornavn = "Rolf",
                etternavn = "Andersen",
                epost = "RolfAndersen@gmail.com",
                spm = "Er det mulig å få familierabatt ved å kjøpe flere billetter"
            };




            var spm1 = new faqhjelp
            {
                spml = "Hvordan kjøper jeg billett?",
                svar = "Du kan kjøpe billett fra:" +
                "Vy.no" +
                "Appen" +
                "Om bord i toget" + "Betalingsmetoder:" +
                "Kort (Visa, MasterCard, Diners)" +
                "Vipps" + "Faktura",
                like = 0,
                unlike = 0
            };

          


            context.Kunder.Add(melding1);
            context.Kunder.Add(melding2);
            context.Spm.Add(spm1);
           
            context.SaveChanges();
            base.Seed(context);
        }
    }
}