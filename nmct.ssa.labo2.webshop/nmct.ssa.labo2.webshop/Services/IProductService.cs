using nmct.ssa.labo2.webshop.Models;
using System;
using System.Collections.Generic;
namespace nmct.ssa.labo2.webshop.Services
{
    public interface IProductService
    {
        Device AddDevice(Device dev);
        Device GetDevice(int id);
        List<ProgrammingFramework> GetProgrammingFrameworks();

        IEnumerable<Device> GetAllDevices();
    }
}
