using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Models
{
    public class OS
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Device> MyProperty { get; set; }
    }
}