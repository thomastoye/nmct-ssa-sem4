using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using nmct.ssa.labo2.webshop.Helper;
using nmct.ssa.labo2.webshop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Tests.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(ApplicationRoles.ADMINISTRATOR))
            {
                roleResult = roleManager.Create(new IdentityRole(ApplicationRoles.ADMINISTRATOR));
            }

            if (!context.Users.Any(u => u.Email.Equals("admin@dev.null")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Administrator",
                    FirstName = "Root",
                    Email = "admin@dev.null",
                    UserName = "admin@dev.null",
                    Address = "Graaf Karel De Goedlaan 1",
                    City = "Kortrijk",
                    ZipCode = "8500"
                };

                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, ApplicationRoles.ADMINISTRATOR);
            }



            // import
            var path = ConfigurationManager.AppSettings["CsvFileLocation"];

            if (context.OSes.Count() == 0)
            {
                using (StreamReader sr = new StreamReader(String.Format("{0}{1}", path, "Os.txt")))
                {
                    while (sr.Peek() > 0)
                    {
                        string line = sr.ReadLine();
                        context.OSes.Add(new OS() { Name = line.Split(';')[1] });
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
                        context.ProgrammingFrameworks.Add(new ProgrammingFramework() { Name = line.Split(';')[1] });
                    }
                }
                context.SaveChanges();
            }

            if (context.Devices.Count() == 0)
            {
                using (StreamReader sr = new StreamReader(String.Format("{0}{1}", path, "Devices.txt")))
                {
                    while (sr.Peek() > 0)
                    {
                        string line = sr.ReadLine();
                        line = sr.ReadLine(); // ignore first line
                        var split = line.Split(';');

                        if (split.Length != 9) continue;

                        // ID;Name;Price;RentPrice;Stock;Picture;OS;Framework;Description
                        var name = split[1];
                        var price = double.Parse(split[2]);
                        var rentPrice = double.Parse(split[3]);
                        var stock = int.Parse(split[4]);
                        var picture = split[5];
                        var osIds = split[6].Split('-');
                        var frameworks = split[7].Split('-');
                        var desc = split[8];

                        Device newDevice = new Device()
                        {
                            Name = name,
                            Price = price,
                            Stock = stock,
                            Picture = picture,
                            Description = desc
                        };

                        newDevice.Frameworks = new List<ProgrammingFramework>();

                        foreach (string frameworkIdString in frameworks)
                        {
                            int id = int.Parse(frameworkIdString);
                            newDevice.Frameworks.Add(context.ProgrammingFrameworks.Where(o => o.ID == id).Single<ProgrammingFramework>());
                        }

                        context.Devices.Add(newDevice);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}