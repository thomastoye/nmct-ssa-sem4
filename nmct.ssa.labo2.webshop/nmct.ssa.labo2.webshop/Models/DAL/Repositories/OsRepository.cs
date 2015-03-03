using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Models.DAL.Repositories
{
    public class OsRepository : GenericRepository<OS>, IOsRepository
    {
        public OsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public OsRepository()
        {

        }

        public override IEnumerable<OS> All()
        {
            return this.context.OSes.ToList();
        }

    }
}