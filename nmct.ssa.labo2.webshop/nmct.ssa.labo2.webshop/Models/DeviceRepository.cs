using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Models
{
    public class DeviceRepository
    {

        public List<Device> GetDevices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from c in context.Devices select c);
                return query.ToList<Device>();
            }
        }
        
        public Device GetDevice(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from c in context.Devices where c.ID == id select c);
                return query.Single<Device>();
            }
        }

        public int InsertDevice(Device dev)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Device returned = context.Devices.Add(dev);
                context.SaveChanges();
                return returned.ID;
            }
        }
    }
}