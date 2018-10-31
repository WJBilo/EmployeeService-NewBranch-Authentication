using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace ElevService
{
   
    public static class WebApiConfig
    {

        
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Følgende sørger for at den rå json data som klienten modtager er ordentligt indented. 
            // Og har camelcase isdedet for pascalcase 
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Gør sådan at serveren som standard formatere dataen i JSON-format 
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("text/html")); 

            // Https configuration

            // Følgende gør at man skal benytte https protokollen i hele Web api applikationen (For alle controllere og action methods). 
            config.Filters.Add(new RequireHttpsAttribute());

            // Gør at man i hele Web api applikationen skal være logget ind, får at kunne udstede HTTP Requests til denne Rest service. 
            config.Filters.Add(new BasicAuthenticationAttribute()); 

            // Slår Cross origin sharing til som gør det muligt at sende Get request afsted til denne Rest service ved hjælp af JQuery AJAX på tværs af domæner.  
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "GET");
            config.EnableCors(cors); 

           

        }
    }
}
