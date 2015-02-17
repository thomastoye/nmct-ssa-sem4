using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Models
{
    public class Device
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double RentPrice { get; set; }
        public int Stock { get; set; }
        public string Picture { get; set; }
        public List<OS> OS { get; set; }
        public List<ProgrammingFramework> Framework { get; set; }
        public string Description { get; set; }
    }
}