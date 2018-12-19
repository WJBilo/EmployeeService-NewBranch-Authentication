
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Mvc;
    using ElevDataAcces; 
    
 
namespace ElevService.Controllers
{
    public class EleverController : ApiController
    {

        // Følgende Get returne en liste med alle Elevernes informationer
        public IEnumerable<ElevTable> Get()
        {

            // Vi laver en instance af klassen entities da denne klasse hjælper os med at 
            // forbinde til databasen og hente elev enheder
            using (EleverEntities entities = new EleverEntities()) 
            {
                 // Jeg filtrere listen så kun elevernes fornavn, efternavn, bruger id og bemærkning (status vedrørende om de er tilstede eller), bliver udleveret, 
                 // når man sender et Get-request til serveren. Jeg har filtreret listen på denne måde, 
                 // da det kun er disse informationer der er nødvendig for klienten i denne løsning og også af sikkerhedsårsager. Dette bliver gjort med følgende LINQ Query udtryk: 
                var filteredStudentList = from elever in entities.ElevTable.AsEnumerable()
                          select new ElevTable() { bemning = elever.bemning, efternavn = elever.efternavn, fornavn = elever.fornavn, userID = elever.userID};
             
                // Efter det returnere jeg en liste med elevernes informationer.
                return filteredStudentList.ToList();
               
            }

        }

    }
}

