using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using nmct.ssa.labo.webshop.businesslayer.Context;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class DeviceRepository : GenericRepository<Device, ApplicationDbContext>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void InsertDevice(Device device)
        {
            foreach (OS os in device.OS)
                context.Entry(os).State = System.Data.Entity.EntityState.Unchanged;
            foreach (FrameWork frame in device.FrameWorks)
                context.Entry(frame).State = System.Data.Entity.EntityState.Unchanged;
            context.Device.Add(device);
            context.SaveChanges();
            
        }

        public void UpdatePicture(Device device)
        {
            context.Device.AddOrUpdate<Device>(device);
            context.SaveChanges();
        }
    }
}