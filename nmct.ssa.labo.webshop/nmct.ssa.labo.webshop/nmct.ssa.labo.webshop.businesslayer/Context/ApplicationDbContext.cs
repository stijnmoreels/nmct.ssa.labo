using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace nmct.ssa.labo.webshop.businesslayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public ApplicationDbContext(string conectionstring)
            : base(conectionstring, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Device> Device { get; set; }
        public DbSet<OS> OS { get; set; }
        public DbSet<FrameWork> FrameWork { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> Orderline { get; set; }
    }
}