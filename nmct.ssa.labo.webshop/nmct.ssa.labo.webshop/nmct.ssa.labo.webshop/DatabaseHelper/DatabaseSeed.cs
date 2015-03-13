using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.businesslayer.Context;

namespace nmct.ssa.labo.webshop.DatabaseHelper
{
    public class DatabaseSeed
    {
        private string _url = AppDomain.CurrentDomain.BaseDirectory;

        public void InsertDevice(ApplicationDbContext context)
        {
            using (StreamReader reader = new StreamReader(_url + @"\Devices.txt"))
            {
                reader.ReadLine();
                while (reader.Peek() > -1)
                {
                    Device device = MakeDevice(reader.ReadLine(), context);
                    if (device != null)
                        context.Device.AddOrUpdate<Device>(device);
                }
                context.SaveChanges();
            }
        }

        public Device MakeDevice(string line, ApplicationDbContext context)
        {
            string[] array = line.Split(';');
            return new Device()
            {
                Id = int.Parse(array[0]),
                Name = array[1],
                BuyPrice = double.Parse(array[2]),
                RentPrice = double.Parse(array[3]),
                Stock = int.Parse(array[4]),
                Image = array[5],
                OS = SetOperatingSystems(array[6], context),
                FrameWorks = SetFrameWorks(array[7], context),
                Description = array[8]
            };
        }

        public List<OS> SetOperatingSystems(string line, ApplicationDbContext context)
        {
            List<OS> osen = new List<OS>();
            string[] array = line.Split('-');

            foreach (string a in array)
            {
                int id = int.Parse(a);
                OS os = context.OS.Where(o => o.Id == id).Select(o => o).SingleOrDefault<OS>();
                context.Entry<OS>(os).State = EntityState.Unchanged;
                osen.Add(os);
            }

            return osen;
        }

        public List<FrameWork> SetFrameWorks(string line, ApplicationDbContext context)
        {
            List<FrameWork> frames = new List<FrameWork>();
            string[] array = line.Split('-');

            foreach (string a in array)
            {
                int id = int.Parse(a);
                FrameWork frame = context.FrameWork.Where(f => f.Id == id).Select(f => f).SingleOrDefault<FrameWork>();
                context.Entry<FrameWork>(frame).State = EntityState.Unchanged;
                frames.Add(frame);
            }

            return frames;
        }

        public void InsertFrameWork(ApplicationDbContext context)
        {
            using (StreamReader reader = new StreamReader(_url + @"\ProgrammingFramework.txt"))
            {
                while (reader.Peek() > -1)
                {
                    FrameWork framework = MakeFrameWork(reader.ReadLine());
                    if (framework != null)
                        context.FrameWork.AddOrUpdate<FrameWork>(framework);
                }
                context.SaveChanges();
            }
        }

        public FrameWork MakeFrameWork(string line)
        {
            string[] array = line.Split(';');
            return new FrameWork()
            {
                Id = int.Parse(array[0]),
                Name = array[1]
            };
        }

        public void InsertOs(ApplicationDbContext context)
        {
            using (StreamReader reader = new StreamReader(_url + @"\Os.txt"))
            {
                while (reader.Peek() > -1)
                {
                    OS os = MakeOS(reader.ReadLine());
                    if (os != null)
                        context.OS.AddOrUpdate<OS>(os);
                }
                context.SaveChanges();
            }
        }

        private OS MakeOS(string line)
        {
            int id;
            string[] array = line.Split(';');
            if (int.TryParse(array[0], out id))
                return new OS() { Id = id, Name = array[1] };
            else
                return null;
        }
    }
}