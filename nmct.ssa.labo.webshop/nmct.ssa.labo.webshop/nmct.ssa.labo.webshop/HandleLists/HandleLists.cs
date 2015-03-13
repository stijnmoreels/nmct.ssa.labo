using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.Controllers.ControllerInterface;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.HandleLists
{
    public class HandleLists : IControllerService
    {
        public ICatalogService Service { get; set; }

        public HandleLists(ICatalogService service)
        {
            this.Service = service;
        }

        public List<FrameWork> MakeListFrame(FrameWork frame, string hidden)
        {
            List<FrameWork> list = new List<FrameWork>();
            if (frame != null)
                list.Add(frame);

            if (hidden != null)
                foreach (string id in hidden.Split(';'))
                    if (id != string.Empty)
                        list.Add(Service.GetFrameWorkById(int.Parse(id)));

            return list;
        }

        public List<OS> MakeListOS(OS os, string hidden)
        {
            List<OS> list = new List<OS>();
            if (os != null)
                list.Add(os);

            if (hidden != null)
                foreach (string id in hidden.Split(';'))
                    if (id != string.Empty)
                        list.Add(Service.GetOsById(int.Parse(id)));

            return list;
        }
    }
}