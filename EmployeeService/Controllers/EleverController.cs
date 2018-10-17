


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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EleverController : ApiController
    {
        // private object entities;



        //// [BasicAuthentication] Jeg tror jeg har tilføjet dette inde i WebApi.config for alle sider... Undersøg det.
        //// Følgende Get udskriver en liste med alle Eleverne
        //// Denne metode kommer til at svare på et Get request.
        public IEnumerable<ElevTable> Get()
        {


            // Authentication code kudvenkat ep 18--- Denne kommentar Kan godt slettes benyttes ikke    

            // her retriver vi den authenticatede user's navn, som er logget ind
            //  string username = Thread.CurrentPrincipal.Identity.Name; 

            // ---Authentication code slut

            /*
            *
            Årsagen til brugen af using er at det sikre, at objectet bliver bortskaffet,
            så snart den er out of scope (Færdig med dens opgave).
            https://stackoverflow.com/questions/75401/what-are-the-uses-of-using-in-c-sharp
            */
            // Vi laver en instance af klassen entities da denne klasse hjælper os med at forbinde til databasen og hente elev enheder
            using (EleverEntities entities = new EleverEntities()) 
            {
                // Sætter FilteredData til IEnumerable (AsEnumerable), frem for IQueryAble, da IQueryAble eksekverer filter logikken på databsesiden hvilket vil skabe konflikt.
                var FilteredData = from a in entities.ElevTables.AsEnumerable()
                          select new ElevTable() { bemning = a.bemning, efternavn = a.efternavn, fornavn = a.fornavn, userID = a.userID};

                

                return FilteredData.ToList();
                
                // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne


                // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne
                //  return entities.ElevTables.ToList()



                //var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(test);
                //return test;


            }

        }

        // Følgende Get udskriver en bestemt elev
        //public ElevTable Get(int id)
        //{
        //    using (EleverEntities entities = new EleverEntities())
        //    {
        //        // Retunere eleven med id'et som blev specificeret i Get Parameteren.
        //        return entities.ElevTables.FirstOrDefault(elev => elev.userID == id);

        //    }

        //}

    }
}














//// [BasicAuthentication] Jeg tror jeg har tilføjet dette inde i WebApi.config for alle sider... Undersøg det.
//// Følgende Get udskriver en liste med alle Eleverne
//// Denne metode kommer til at svare på et Get request.
//public IEnumerable<ElevTable>
//    Get()
//{


//    // Authentication code kudvenkat ep 18--- Kan godt slettes benyttes ikke

//    // her retriver vi den authenticatede user's navn, som er logget ind
//    //  string username = Thread.CurrentPrincipal.Identity.Name;

//    // ---Authentication code slut

//    /*
//    *
//    Årsagen til brugen af using er at det sikre, at objectet bliver bortskaffet,
//    så snart den er out of scope (Færdig med dens opgave).
//    https://stackoverflow.com/questions/75401/what-are-the-uses-of-using-in-c-sharp
//    */
//    // Vi laver en instance af klassen entities da denne klasse hjælper os med at forbinde til databasen og hente elev enheder
//    using (EleverEntities entities = new EleverEntities())
//    {

//        //var test = from a in entities.ElevTables
//        //         select new ElevTableFilter { bemning = a.bemning, efternavn = a.efternavn, fornavn = a.fornavn };

//        // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne


//        // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne
//        return entities.ElevTables.ToList();

//    }

//}










//    ElevFilter:



//            public IEnumerable<ElevTableFilter>
//            Get()
//{


//    // Authentication code kudvenkat ep 18--- Kan godt slettes benyttes ikke

//    // her retriver vi den authenticatede user's navn, som er logget ind
//    //  string username = Thread.CurrentPrincipal.Identity.Name;

//    // ---Authentication code slut

//    /*
//    *
//    Årsagen til brugen af using er at det sikre, at objectet bliver bortskaffet,
//    så snart den er out of scope (Færdig med dens opgave).
//    https://stackoverflow.com/questions/75401/what-are-the-uses-of-using-in-c-sharp
//    */
//    // Vi laver en instance af klassen entities da denne klasse hjælper os med at forbinde til databasen og hente elev enheder
//    using (EleverEntitiesFiltered entities = new EleverEntitiesFiltered())
//    {

//        //var test = from a in entities.ElevTables
//        //         select new ElevTableFilter { bemning = a.bemning, efternavn = a.efternavn, fornavn = a.fornavn };

//        // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne


//        // Følgende er en collection property (ElevTables) der kommer til at retunere en liste over eleverne
//        return entities.ElevTablesFiltered.ToList();

//    }

//}
