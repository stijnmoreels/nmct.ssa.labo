using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        void InsertDevice(Device device);
        void UpdatePicture(Device device);
        List<Device> GetFavoriteDevices();
        void UpdateFavorite(int id, bool favorite);
    }
}
