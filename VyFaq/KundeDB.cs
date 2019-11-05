using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using VyFaq.Models;

namespace VyFaq
{
    public class KundeDB 
    {
        FaqContext db = new FaqContext();

        public bool registrerKunde(kunde spm)
        {
            using (var db = new FaqContext())
            {
                try
                {
                    var nyttSpm = new kunde()
                    {
                        id = spm.id,
                        fornavn = spm.fornavn,
                        etternavn = spm.etternavn,
                        epost = spm.epost,
                        spm = spm.spm
                    };

                    db.Kunder.Add(nyttSpm);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public List<kunde> alleSpm()
        {
            var db = new FaqContext();
            var liste = new List<kunde>();

            var allespm = db.Kunder.ToList();
            foreach (var spm in allespm)
            {
                kunde k = new kunde
                {
                    id = spm.id,
                    fornavn = spm.fornavn,
                    etternavn = spm.etternavn,
                    epost = spm.epost,
                    spm = spm.spm
                };
                liste.Add(k);
            };

            return liste;
        }

        public bool registrerspørsmål(faqhjelp spml)
        {
            using (var db = new FaqContext())
            {
                try
                {
                    var nyttSpml = new faqhjelp()
                    {
                        id = spml.id,
                        spml = spml.spml,
                        svar = spml.svar,
                        like = spml.like,
                        unlike = spml.unlike

                    };

                    db.Spm.Add(nyttSpml);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public List<faqhjelp> AlleSpmOgSvar()
        {
            var db = new FaqContext();
            var liste = new List<faqhjelp>();

            var AlleSpmSvarListe = db.Spm.ToList();
            foreach (var spmsvar in AlleSpmSvarListe)
            {
                faqhjelp k = new faqhjelp
                {
                    id = spmsvar.id,
                    spml = spmsvar.spml,
                    svar = spmsvar.svar,
                    like = spmsvar.like,
                    unlike = spmsvar.unlike
                };
                liste.Add(k);
            };

            return liste;
        }

    }
}