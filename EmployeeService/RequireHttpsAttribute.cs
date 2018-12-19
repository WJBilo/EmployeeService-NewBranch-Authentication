using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ElevService
{
    // AuthorizationFilterAttribute klassen indeholder en metode kaldet "OnAuthorization", 
    // hvor vi kan ’inject’ en ny respons, hvis anmodningen (http-Request’et) ikke er udført med HTTPS.
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {


        // Følgende metode har en parameter actionContext som provider os med adgang til dens request og response properties.  
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Hvis browser request ikke er issued med https protokol så redirect the request, så det benytter https
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                // Her sætter vi respons beskeden, som vi gerne vil sende tilbage til klienten,
                // lig med http status koden 302, som indikere at den efterspurgte information er fundet. 
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                // Her sætter vi indholdet af den respons besked, 
                // som vi gerne vil sende tilbage til klienten, 
                // lig med en ny instans af StringContent, og constructoren forsyner os
                // med en måde at skabe en HTML streng, som vi kan levere til klienten som beskedindhold. 
                actionContext.Response.Content = new StringContent("<p> Benyt HTTPS istedetfor HTTP </p>", Encoding.UTF8, "text/html"); // text/html gør at Content-typen ændres til html 
               
                // Her redirecter vi automatisk til https ved hjælp af UriBuilder klassen. 
                // Og vi bygger URI'en fra request objektet 
                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                if (uriBuilder.Host.Equals("localhost"))
                {
                    // URI'en skal have https som scheme 
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 44373;
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 443;
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }


            }
            else
            {   
                base.OnAuthorization(actionContext);
            }

        }

    }
}







































//    using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Web;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

//namespace ElevService
//{
//    public class RequireHttpsAttribute : AuthorizationFilterAttribute
//    {
//        // Følgende funktion har en parameter actionContext som provider os med adgang til både the request og response objekt. 
//        // Find ud af hvad HttpActionContext klassen gør 
//        public override void OnAuthorization(HttpActionContext actionContext)
//        {
//            // Hvis browser request ikke er issued med https protokol så redirect the request, så det benytter https

//            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
//            {
//                // Response er en instance af HttpResponseMessage klassen: Defination af HttpResponseMessage:  https://www.oreilly.com/library/view/tcpip-guide/9781593270476/ch81s03.html 
//                // https://www.techopedia.com/definition/27178/http-header 
//                // CreateResponse: Creates an HttpResponseMessage wired up to the associated HttpRequestMessage.  https://msdn.microsoft.com/en-us/library/hh969014(v=vs.118).aspx 
//                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
//                actionContext.Response.Content = new StringContent("<p> Benyt HTTPS istedetfor HTTP </p>", Encoding.UTF8, "text/html"); // text/html gør at Content-typen ændres til html 
//                // Her redirecter vi automatisk til https ved hjælp af UriBuilder klassen. 
//                // Og vi bygger URI'en fra request objektet 
//                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);



//                // URI'en skal have https som scheme 
//                uriBuilder.Scheme = Uri.UriSchemeHttps;
//                uriBuilder.Port = 44362;
//                // SLet dette kommentar det er port til anden app uriBuilder.Port = 44322;

//                actionContext.Response.Headers.Location = uriBuilder.Uri;
//            }
//            else
//            {   // Hvis det dog ikke er tilfældet vil vi bare udføre base klassens OnAuthorization metode
//                base.OnAuthorization(actionContext);
//            }

//        }

//    }
//}