using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;
using System.IO;
using nmct.ssa.labo.webshop.businesslayer.Repositories;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class CatalogService : ICatalogService
    {
        private IGenericRepository<OS> genericOS = null;
        private IGenericRepository<FrameWork> genericFramework = null;
        private IDeviceRepository deviceRepository = null;

        public CatalogService(IDeviceRepository deviceRepository, IGenericRepository<OS> genericOS,  IGenericRepository<FrameWork> genericFramework)
        {
            this.deviceRepository = deviceRepository;
            this.genericOS = genericOS;
            this.genericFramework = genericFramework;
        }

        public List<Device> GetDevices()
        {
            return deviceRepository.All().ToList<Device>();
        }

        public Device GetDeviceById(int id)
        {
            return deviceRepository.GetItemByID(id);
        }

        public List<OS> GetOs()
        {
            return genericOS.All().ToList<OS>();
        }

        public List<FrameWork> GetFrameworks()
        {
            return genericFramework.All().ToList<FrameWork>();
        }

        public OS GetOsById(int id)
        {
            return genericOS.GetItemByID(id);
        }

        public FrameWork GetFrameWorkById(int id)
        {
            return genericFramework.GetItemByID(id);
        }

        public void InsertDevice(Device device)
        {
            deviceRepository.InsertDevice(device);
        }

        public void UpdatePicture(int device, System.Web.HttpPostedFileBase image)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("images");
            CloudBlockBlob blob = container.GetBlockBlobReference(image.FileName);
            blob.UploadFromStream(image.InputStream);

            Device d = GetDeviceById(device);
            d.Image = image.FileName;
            deviceRepository.UpdatePicture(d);
        }
    }
}