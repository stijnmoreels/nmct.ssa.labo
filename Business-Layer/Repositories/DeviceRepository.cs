using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using nmct.ssa.labo.webshop.businesslayer.Context;
using System.Data.Entity.Validation;
using System.Diagnostics;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using System.Globalization;
using System.Data.Entity.Migrations;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class DeviceRepository : GenericRepository<Device, ApplicationDbContext>, IDeviceRepository
    {
        private CultureInfo culture = CultureInfo.CurrentCulture;

        public override IEnumerable<Device> All()
        {
            return base.All();
        }

        public List<TranslatedDevice> AllTranslatedDevices(string name)
        {
            string filter = !name.Equals(string.Empty) ? name : culture.Name;
            return context.TranslatedDevice
                .Include("Device")
                .Where(t => t.Culture.Name.Equals(filter))
                .ToList<TranslatedDevice>();
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
            try
            {
                context.Device.AddOrUpdate<Device>(device);
                context.Entry<Device>(device).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public List<Device> GetFavoriteDevices()
        {
            return context.Device
                .Where(d => d.Favourite == true)
                .ToList<Device>();
        }

        public void UpdateFavorite(int id, bool favorite)
        {
            Device device = context.Device.Where(d => d.Id == id).SingleOrDefault<Device>();
            device.Favourite = favorite;
            context.Entry<Device>(device).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}