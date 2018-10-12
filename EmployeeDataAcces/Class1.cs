using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EmployeeDataAcces
{



    public partial class EleverEntitiesFiltered : DbContext
    {
        public EleverEntitiesFiltered()
            : base("name=EleverEntitiesFiltered")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilderFiltered)
        {
            throw new UnintentionalCodeFirstException();
        }


        public virtual DbSet<ElevTable> ElevTablesFiltered { get; set; }

    }
}
