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

        public HttpResponseMessage Get()
        {
            List<kunde> alleKunder = kundeDb.alleSpm();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleKunder);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };


        }

       
        [HttpPost]
        public HttpResponseMessage Post([FromBody]kunde innKunde)
        {

            if (ModelState.IsValid)
            {
                bool OK = kundeDb.registrerKunde(innKunde);
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
                Content = new StringContent("Kunne ikke sette inn kunden i DB" + "Fornavn: " + innKunde.fornavn
                + ", Etternavn: " + innKunde.etternavn + ", epost: " + innKunde.epost + ", spm: " + innKunde.spm)
            };
        }
    }
}