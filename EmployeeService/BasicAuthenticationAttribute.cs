
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

namespace ElevService
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        // her overrider vi en method som er i AuthorizationFilterAttribute base klassen.
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // The HTTP Authorization request header contains the credentials to authenticate a user agent with a server
            // Hvis følgende == null så har brugeren ikke sendt de rigtige credentials. 
            // Og så vil vi sende en unauthorized HTTP Response message. 

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // authenticationToken kommer til at være base 64 encoded 
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                // Her decoder vi authenticationToken 
                // Får at få fat  i den decodede string skal vi benytte Encoding klassen og gøre følgende. 
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                // Selve vores authenticationToken ser sådan her ud: username:password, når den er decoded
                // Så vi skal splitte den ved kolon 
                // Følgende komemer til at retunere et string array der kommer til at indeholde [0]brugernavnet og [1]passwordet. 
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string brugernavn = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];


                // Her checker vi
                if (!ElevSecurity.Login(brugernavn, password))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}





