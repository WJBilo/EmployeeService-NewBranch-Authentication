using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElevDataAcces; 
namespace ElevService
{
    public class ElevSecurity
    {
        // Denne metode kommer til at returnere true eller false, alt efter om det angivende brugernavn og kodeord, er gyldig.  
        public static bool Login(string username, string kodeord)
        {
            //Efter det laver jeg en instans af klassen EleverEntities 
            // der nedarver fra DbContext klassen. EleverEntities klassen håndtere forbindelsen til databasen og skaffer enheder fra databasen: 
            using (EleverEntities entities = new EleverEntities())
            {

                // Her cheker vi om password, brugernavn og brugerrettigheder matcher med en bruger i tabellen og hvis det er tilfældet, 
                // så retunere vi true

                return entities.ElevTable.Any(user => user.brugernavn.Equals(username,
                StringComparison.OrdinalIgnoreCase) && user.password == kodeord && user.user_level.Equals("1")); // StringComparison.OrdinalIgnoreCase specificere at vi er ligeglad med at checke om brugernavns bogstaverne er uppercase eller lowercase

            }
        }
    }
}


