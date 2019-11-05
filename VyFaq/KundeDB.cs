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
        KundeContext db = new KundeContext();
        
        public List<kunde> hentAlleKunder()
        {
            List<kunde> alleKunder = db.Kunder.Select(k=> new kunde()
                                      {
                                          id = k.id,
                                          fornavn = k.fornavn,
                                          etternavn = k.etternavn,
                                          adresse = k.adresse,
                                          postnr = k.postnr,
                                          poststed = k.poststed.poststed
                                      }).ToList();
            return alleKunder;
        }
        
        public kunde hentEnKunde(int id)
        {
            Kunde enDBKunde = db.Kunder.Find(id); 

            var enKunde = new kunde()
            {
                id = enDBKunde.id,
                fornavn = enDBKunde.fornavn,
                etternavn = enDBKunde.etternavn,
                adresse = enDBKunde.adresse,
                postnr = enDBKunde.postnr,
                poststed = enDBKunde.poststed.poststed
            };
            return enKunde;
        }

        public bool lagreEnKunde(kunde innKunde)
        {
            var nyKunde = new Kunde
            {
                fornavn = innKunde.fornavn,
                etternavn = innKunde.etternavn,
                adresse = innKunde.adresse,
                postnr = innKunde.postnr
            };

            Poststed funnetPoststed = db.Poststeder.Find(innKunde.postnr);
            if (funnetPoststed == null)
            {
                // lag poststedet
                var nyttPoststed = new Poststed
                {
                    postnr = innKunde.postnr,
                    poststed = innKunde.poststed
                };
                // legg det inn i den nye kunden
                nyKunde.poststed = nyttPoststed;

            }
            try
            {
                // lagre kunden
                db.Kunder.Add(nyKunde);
                db.SaveChanges();
            }
            catch(Exception feil)
            {
                return false;
            }
            return true;
        }
        public bool endreEnKunde(int id, kunde innKunde)
        {
            // finn kunden
            Kunde funnetKunde = db.Kunder.FirstOrDefault(k => k.id == id);
            if (funnetKunde == null)
            {
                return false;
            }
            // legg inn ny verdier i denne fra innKunde
            funnetKunde.fornavn = innKunde.fornavn;
            funnetKunde.etternavn = innKunde.etternavn;
            funnetKunde.adresse = innKunde.adresse;
            funnetKunde.postnr = innKunde.postnr;

            // finn ut om postnummer finnes fra før
            Poststed funnetPoststed = db.Poststeder.Find(innKunde.postnr);
            if(funnetPoststed==null)
            {
                // lag poststedet
                var nyttPoststed = new Poststed
                {
                    postnr = innKunde.postnr,
                    poststed = innKunde.poststed
                };
                // legg det inn i kunden
                funnetKunde.poststed = nyttPoststed;
            }
            try
            {
                // lagre kunden
                db.SaveChanges();
            }
            catch(Exception feil)
            {
                return false;
            }
            return true;
        }
        
        public bool slettEnKunde(int id)
        {
            try
            {
                Kunde finnKunde = db.Kunder.Find(id);
                db.Kunder.Remove(finnKunde);
                db.SaveChanges();
            }
            catch(Exception feil)
            {
                return false;
            }
            return true;
        }
    }
}