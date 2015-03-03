using System;
namespace nmct.ssa.labo2.webshop.Models.DAL.Repositories
{
    interface IOsRepository : IGenericRepository<OS>
    {
        System.Collections.Generic.IEnumerable<nmct.ssa.labo2.webshop.Models.OS> All();
    }
}
