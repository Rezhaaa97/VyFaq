using VyFaq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace VyFaq.Controllers
{
    public class FaqhjelpController : ApiController
    {

        KundeDB kundeDb = new KundeDB();


        public HttpResponseMessage Get()
        {
            List<faqhjelp> alleSpm = kundeDb.AlleSpmOgSvar();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleSpm);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]faqhjelp innSpm)
        {
            if (ModelState.IsValid)
            {
                bool OK = kundeDb.registrerspørsmål(innSpm);
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
                Content = new StringContent("Feil i innsetting av DB" + "Spørsmål: " + innSpm.spml
                + ", Svar: " + innSpm.svar)
            };
        }

        public HttpResponseMessage Put(int id, [FromBody]faqhjelp faq)
        {
            bool OK = kundeDb.oppdaterLikeUnlike(id, faq);
            if (OK)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke endre like/unlike i DB")
            };
            
        }
    }
}
