using nmct.ssa.labo.webshop.DatabaseHelper;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using nmct.ssa.labo.webshop.businesslayer.Context;

namespace nmct.ssa.labo.webshop.test.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            DatabaseSeed seed = new DatabaseSeed();
            seed.InsertFrameWork(context);
            seed.InsertOs(context);
            seed.InsertDevice(context);
        }
    }
}
