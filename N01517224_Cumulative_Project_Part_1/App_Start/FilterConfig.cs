﻿using System.Web;
using System.Web.Mvc;

namespace N01517224_Cumulative_Project_Part_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
