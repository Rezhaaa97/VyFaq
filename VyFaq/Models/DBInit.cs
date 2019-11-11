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
                svar = "Du kan kjøpe billett fra:" + Environment.NewLine +
                " Vy.no, " + 
                " Appen, \r\n" +
                " Om bord i toget, \r\n"+"\n Betalingsmetoder: \r\n" +
                " Kort (Visa, MasterCard, Diners), \r\n" +
                " Vipps, \r\n" + "Faktura. \r\n",
                like = 0,
                unlike = 0
            };

            var spm2 = new faqhjelp
            {
                spml = "Hvordan henter jeg billetter kjøpt på vy.no?",
                svar = "Det enkleste er å hente ut billetten via appen. Den kan også lastes ned som PDF eller hentes ut om bord, på billettautomat, på betjent stasjon og hos enkelte Narvesen-kiosker.",
                like = 0,
                unlike = 0
            };

            var spm3 = new faqhjelp
            {
                spml = "Hvordan endrer jeg navn på billetten?",
                svar = "Når du kjøper billetter så er det navnet til den som bestiller som står på billettene. Det er altså ikke noe i veien for at det står samme navn på flere billetter, så lenge personen som bestilte billettene skal være med på reisen."+
                "Hvis ikke personen som står oppført på billettene blir med på reisen, ring eller chat med kundeservice for å endre navn.",
                like = 0,
                unlike = 0
            };

            var spm4 = new faqhjelp
            {
                spml = "Hva er reisekort?",
                svar = "Med reisekort reiser du med elektronisk billett hos Vy og Ruter, som samarbeider om kortsystemet elektronisk reisekort. Reisekortet gjelder i Oslo og Akershus.",
                like = 0,
                unlike = 0
            };

            var spm5 = new faqhjelp
            {
                spml = "Jeg vil reise rundt i Norge i ferien. Finnes det en billett for dette?",
                svar = "Vi tilbyr ikke en fleksibel billett som kan brukes på flere avganger rundt omkring i Norge. Det er heller ikke mulig å reise med Interrailbillett i eget land. For å reise rundt i Norge med toget, må du dermed bestille enkeltbilletter for de avgangene du ønsker."+ "Hvis du er interessert i en norgesferie med transport, hotell og aktiviteter inkludert, så kan du se om noen av turene til vår samarbeidspartner, Fjord Tours, passer deg.",
                like = 0,
                unlike = 0
            };

            var spm6 = new faqhjelp
            {
                spml = "Hvor lenge før avreise kan jeg kjøpe togbillett?",
                svar = "Billetter legges ut for salg 90 dager før avgang. Det kan være kortere salgsperiode ved planlagte arbeider på strekningen eller i forbindelse med ruteendringer i juni og desember.",
                like = 0,
                unlike = 0
            };

            var spm7 = new faqhjelp
            {
                spml = "Jeg vil betale for togbilletten med faktura – hvordan gjør jeg det?",
                svar = "Privatpersoner kan velge faktura ved betaling på vy.no. Minstebeløpet er 500 kr og du må betale fakturagebyr på 40 kr."+ "Fakturabetaling via kundeservice og betjent stasjon er forbeholdt firmaer, reisebyråer, skoler, institusjoner, foreninger, lag og grupper, samt privatpersoner som kjøper årskort.",
                like = 0,
                unlike = 0
            };
            var spm8 = new faqhjelp
            {
                spml = "Hva skjer hvis jeg ikke rekker toget?",
                svar = "Enkeltbilletter er gyldig til angitt avgang, det vil si at du ikke kan benytte billetten på andre avganger. Du må dermed kjøpe en ny billett hvis du vil reise med neste avgang."+ "Dersom du bruker Vy-appen til å kjøpe Ruter-billett, er det Ruters bestemmelser for gyldighetstid som gjelder. ",
                like = 0,
                unlike = 0
            };
            var spm9 = new faqhjelp
            {
                spml = "Det står at det er utsolgt. Kan jeg bli med likevel?",
                svar = "Det er ikke mulig å kjøpe billetter til utsolgte tog. Du kan møte opp og ta sjansen på at det er kapasitet. Konduktøren gjør da en vurdering rett før avgang. Dersom det ikke er kapasitet om bord vil du ikke kunne bli med toget.",
                like = 0,
                unlike = 0
            };
            var spm10 = new faqhjelp
            {
                spml = "Hvordan kjøper jeg gavekort, og hvordan betaler jeg med det?",
                svar = "Du kan ikke lenger kjøpe gavekort, men kunder kan få gavekort av oss i ulike sammenhenger."+ "Verdikoden på gavekortet kan brukes til kjøp av enkeltbilletter eller periodebilletter til Vys tog via vy.no, kundeservice og betjente stasjoner.",
                like = 0,
                unlike = 0
            };




            context.Kunder.Add(melding1);
            context.Kunder.Add(melding2);
            context.Spm.Add(spm1);
            context.Spm.Add(spm2);
            context.Spm.Add(spm3);
            context.Spm.Add(spm4);
            context.Spm.Add(spm5);
            context.Spm.Add(spm6);
            context.Spm.Add(spm7);
            context.Spm.Add(spm8);
            context.Spm.Add(spm9);
            context.Spm.Add(spm10);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}