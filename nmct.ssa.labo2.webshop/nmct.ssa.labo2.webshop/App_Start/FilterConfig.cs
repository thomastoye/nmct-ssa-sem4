﻿using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo2.webshop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
