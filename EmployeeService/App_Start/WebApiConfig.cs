using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace EmployeeService
{

    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {

        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); 
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json"); 
        }
    }


    public static class WebApiConfig
    {

        // WebApiConfig.Register() method includes a parameter of HttpConfiguration type which is then used to configure the Web API
        // Web api configuration http://www.tutorialsteacher.com/webapi/configure-web-api  WebApiConfig.Register() 
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
            // Følgende sørger for at den rå json data som brugeren får når de benytter Get metoden er ordentligt indented. 
            // Og har camelcase isdedet for pascalcase 
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            config.Formatters.Add(new CustomJsonFormatter());


            // http://www.tutorialsteacher.com/webapi/web-api-formatters Media type formatters 
            // Web api configuration http://www.tutorialsteacher.com/webapi/configure-web-api  

            // Jsonp (json with padding): 
            // Web browser tilladder ikke at ren json kan blive consumed med mindre man benytter noget som jsonp
            // AJAX (XMLHttpRequest) doesn’t allow cross domain communication due to security reasons. https://www.codeproject.com/Tips/631685/JSONP-in-ASP-NET-Web-API-Quick-Get-Started
            // Web browsers tillader at javascript kode kan blive 'consumed' så derfor 'wrapper' vi alt vores json data ind i en javascript funktion ved hjælp fra jsonp

            /* JsonpMediaTypeFormatter is a JSONP MediaTypeFormatter implementation for ASP.NET Web API.
i added it to my Web API solution, by running Install-Package WebApiContrib.Formatting.Jsonp from your NuGet Package Manager console in Visual Studio. */



            // Https configuration
            // Følgende gør at man kan benytte https protokollen i hele Web api applikationen (For alle controllere og action methods). 
          config.Filters.Add(new RequireHttpsAttribute());

            // Gør at man i hele Web api applikationen skal være logget ind
        config.Filters.Add(new BasicAuthenticationAttribute());

         config.EnableCors(); 

        }
    }
}
