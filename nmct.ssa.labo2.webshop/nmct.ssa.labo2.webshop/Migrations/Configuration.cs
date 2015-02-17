namespace nmct.ssa.labo2.webshop.Migrations
{
    using nmct.ssa.labo2.webshop.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ssa.labo2.webshop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(nmct.ssa.labo2.webshop.Models.ApplicationDbContext context)
        {
            var path = ConfigurationManager.AppSettings["CsvFileLocation"];

            if (context.OSes.Count() == 0)
            {
                using (StreamReader sr = new StreamReader(String.Format("{0}{1}", path, "Os.txt")))
                {
                    while (sr.Peek() > 0)
                    {
                        string line = sr.ReadLine();
                        context.OSes.AddOrUpdate<OS>(o => o.Name, new OS() { Name = line.Split(';')[1] });
                    }
                }
                context.SaveChanges();
            }


            if (context.ProgrammingFrameworks.Count() == 0)
            {
                using (StreamReader sr = new StreamReader(String.Format("{0}{1}", path, "ProgrammingFramework.txt")))
                {
                    while (sr.Peek() > 0)
                    {
                        string line = sr.ReadLine();
                        context.ProgrammingFrameworks.AddOrUpdate<ProgrammingFramework>(o => o.Name, new ProgrammingFramework() { Name = line.Split(';')[1] });
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
