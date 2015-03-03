using nmct.ssa.labo2.webshop.Models;
using nmct.ssa.labo2.webshop.Models.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo2.webshop.Services
{
    public class ProductService
    {
        private IGenericRepository<OS> repoOS;
        private IGenericRepository<ProgrammingFramework> repoFramework;
        private IDeviceRepository repoDevice;

        public ProductService(
            IGenericRepository<OS> os, IGenericRepository<ProgrammingFramework> framework, IDeviceRepository dev)
        {
            repoOS = os;
            repoFramework = framework;
            repoDevice = dev;
        }


        public Device AddDevice(Device dev) {
            return repoDevice.Insert(dev);
        }

        public Device GetDevice(int id) {
            return repoDevice.GetByID(id);
        }

        public List<ProgrammingFramework> GetProgrammingFrameworks()
        {
            return repoFramework.All().ToList();
        }


    }
}