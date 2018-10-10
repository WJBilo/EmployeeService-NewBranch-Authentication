using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAcces; // Vi skal benytte de 'entities' som vi har i vores EmployeeDataAcces projekt. 
namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string kodeord)
        {
            using(EleverEntities entities = new EleverEntities())
            {

          

                // Her cheker vi om password og brugernavn matcher 
                return entities.ElevTables.Any(user => user.brugernavn.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.password == kodeord && user.user_level.Equals("1")); // StringComparison.OrdinalIgnoreCase specificere at vi er ligeglad med at checke om brugernavns bogstaverne er uppercase eller lowercase

            }

        }
    }
}