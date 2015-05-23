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
using nmct.ssa.labo.webshop.businesslayer.Caching;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Caching.Interfaces;
using System.Globalization;
using System.Diagnostics;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class CatalogService : ICatalogService
    {
        private IGenericRepository<OS> genericOS = null;
        private IGenericRepository<FrameWork> genericFramework = null;
        private IDeviceRepository deviceRepository = null;
        private IWebshopCache cache = null;
        private CultureInfo culture = CultureInfo.CurrentCulture;

        public CatalogService(IDeviceRepository deviceRepository, IGenericRepository<OS> genericOS,  
            IGenericRepository<FrameWork> genericFramework, IWebshopCache cache)
        {
            this.deviceRepository = deviceRepository;
            this.genericOS = genericOS;
            this.genericFramework = genericFramework;
            this.cache = cache;
        }

        public List<Device> GetDevices()
        {
            /*if (cache.CacheCheck(CacheNames.CACHE_DEVICES))
                cache.RefreshCache<Device>(deviceRepository.All() ,CacheNames.CACHE_DEVICES);
            return cache.GetItemsFromCache<Device>(CacheNames.CACHE_DEVICES).ToList<Device>();*/
            
            
            return deviceRepository.All().ToList<Device>();
        }

        public List<TranslatedDevice> GetTranslatedDevices(string name)
        {
            return deviceRepository.AllTranslatedDevices(name);
        }

        public Device GetDeviceById(int id)
        {
            return deviceRepository.GetItemByID(id);
        }

        public List<OS> GetOs()
        {
            if (cache.CacheCheck(CacheNames.CACHE_OS))
                cache.RefreshCache<OS>(genericOS.All().ToList<OS>(), CacheNames.CACHE_OS);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<OS> os = cache.GetItemsFromCache<OS>(CacheNames.CACHE_OS).ToList<OS>();
            //List<OS> os = cache.TestGetOs(CacheNames.CACHE_OS).ToList<OS>();
            //List<OS> os = genericOS.All().ToList<OS>();

            stopwatch.Stop();
            return os;
        }

        public List<FrameWork> GetFrameworks()
        {
            //return cache.GetItemsFromCache<FrameWork>(CacheNames.CACHE_FRAMEWORK).ToList<FrameWork>();
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
            RefreshDevices();
        }

        public void RefreshDevices()
        {
            List<Device> devices = deviceRepository.All().ToList<Device>();
            //cache.RefreshCache<Device>(devices, CacheNames.CACHE_DEVICES);
        }

        //private void RefreshCache()
        //{
        //    List<OS> os = genericOS.All().ToList<OS>();
        //    List<FrameWork> frameworks = genericFramework.All().ToList<FrameWork>();
        //    List<Device> devices = deviceRepository.All().ToList<Device>();
        //    WriteToCache(os, frameworks, devices);
        //}

        //private void WriteToCache(List<OS> os, List<FrameWork> frameworks, List<Device> devices)
        //{
        //    cache.RefreshCache<OS>(os, CacheNames.CACHE_OS);
        //    cache.RefreshCache<FrameWork>(frameworks, CacheNames.CACHE_FRAMEWORK);
        //    cache.RefreshCache<Device>(devices, CacheNames.CACHE_DEVICES);
        //}

        public void UpdatePicture(int device, System.Web.HttpPostedFileBase image)
        {
            SetAzureBlobContainer(image);

            Device d = GetDeviceById(device);
            d.Image = image.FileName;
            deviceRepository.UpdatePicture(d);

            RefreshDevices();
        }

        private static void SetAzureBlobContainer(HttpPostedFileBase image)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("images");
            CloudBlockBlob blob = container.GetBlockBlobReference(image.FileName);
            blob.UploadFromStream(image.InputStream);
        }

        public void UpdateFavorite(int id, bool favorite)
        {
            deviceRepository.UpdateFavorite(id, favorite);
        }

        public List<Device> GetFavoriteDevices()
        {
            return deviceRepository.GetFavoriteDevices();
        }
    }
}