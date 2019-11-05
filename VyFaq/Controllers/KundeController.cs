using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Net.Http.Formatting;
using System.Data.Common;

using VyFaq.Models;

namespace VyFaq
{
    public class KundeController : ApiController
    {
        KundeDB kundeDb = new KundeDB();

        // GET api/Kunde
        public HttpResponseMessage Get()
        {
            List<kunde> alleKunder = kundeDb.hentAlleKunder();
            
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleKunder);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };

            /* alternativ til return-koden over - for å forklare dette bedre :
            
            var respons = new HttpResponseMessage();
            respons.Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            respons.StatusCode = HttpStatusCode.OK;
            return respons;
            
             */

        }

        // GET api/Kunde/5
        public HttpResponseMessage Get(int id)
        {
            kunde enKunde = kundeDb.hentEnKunde(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(enKunde);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/Kunde
        [HttpPost]
        public HttpResponseMessage Post([FromBody]kunde innKunde)
        {
            
            if (ModelState.IsValid)
            {
                bool OK = kundeDb.lagreEnKunde(innKunde);
                if (OK)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                    
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Kunne ikke sette inn kunden i DB")
            };
        }

        // PUT api/Kunde/5
        public HttpResponseMessage Put(int id, [FromBody]kunde innKunde)
        {
            if(ModelState.IsValid)
            {
                bool OK = kundeDb.endreEnKunde(id, innKunde);
                if (OK)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke endre kunden i DB")
            };
            
        }

        // DELETE api/Kunde/5
        public HttpResponseMessage Delete(int id)
        {
            bool OK = kundeDb.slettEnKunde(id);
            if (!OK)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette kunden i DB")
                };
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}