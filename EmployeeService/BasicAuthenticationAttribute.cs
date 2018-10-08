using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace EmployeeService
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        // her overrider vi en method som er i AuthorizationFilterAttribute base klassen.
       // [EnableCors(origins: "", headers: "", methods: "*", SupportsCredentials = true)]
       // [RequireHttps]
        // [EnableCorsAttribute("*", "*", "*")]
      [EnableCors(origins: "*", headers: "*", methods: "*")]
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // The HTTP Authorization request header contains the credentials to authenticate a user agent with a server
            // Hvis følgende == null så har brugeren ikke sendt de rigtige credentials. 
            // Og så vil vi sende en unauthorized HTTP Response message. 
            var header = actionContext.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization"));
         
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // authenticationToken kommer til at være base 64 encoded 
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                // Her decoder vi the authenticationToken 
                // Får at få fat  i den decodede string skal vi benytte Encoding klassen og gøre følgende. 
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                // Selve vores authenticationToken ser sådan her ud: username:password, når den er decoded
                // Så vi skal splitte den ved kolon 
                // Følgende komemer til at retunere et string array der kommer til at indeholde [0]brugernavnet og [1]passwordet. 
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string brugernavn = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];


                // Følgende er true hvis vi har et brugernavn og password som svare til det som brugeren har indtastet. 
                if(EmployeeSecurity.Login(brugernavn, password))
                {
                    
                    // Thread.CurrentPrincipal is the way .NET applications represent the identity of the user or service account running the process. 
                    // https://stackoverflow.com/questions/34954577/what-is-thread-currentprincipal-and-what-does-it-do 
                    // Setting CurrentPrincipal to the current Thread is valuable in situations where the principal must be validated several times and or it must be validated by other code running in your application
                    // GenericIdentity skaber en ny identitet ved navn 'username' 
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(brugernavn), null); // Sætter roles tíl null
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}