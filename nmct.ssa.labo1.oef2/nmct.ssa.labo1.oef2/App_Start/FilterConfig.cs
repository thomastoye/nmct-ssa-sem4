﻿using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo1.oef2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
