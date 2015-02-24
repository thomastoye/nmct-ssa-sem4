using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Models
{
    public class Device
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Picture { get; set; }
        public List<OS> OSes { get; set; }
        public List<ProgrammingFramework> Frameworks { get; set; }
        public string Description { get; set; }
    }
}