using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.businesslayer.Services.Interfaces
{
    public interface ICatalogService
    {
        List<Device> GetDevices();
        List<OS> GetOs();
        List<FrameWork> GetFrameworks();
        Device GetDeviceById(int id);
        OS GetOsById(int id);
        FrameWork GetFrameWorkById(int id);
        void InsertDevice(Device device);
        void UpdatePicture(int device, HttpPostedFileBase image);

        void UpdateFavorite(int id, bool favorite);

        List<Device> GetFavoriteDevices();
        void RefreshDevices();
    }
}
