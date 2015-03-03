using System;
using System.Collections.Generic;
namespace nmct.ssa.labo2.webshop.Models.DAL.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Device GetDevice(int id);
        List<Device> GetDevices();
    }
}
