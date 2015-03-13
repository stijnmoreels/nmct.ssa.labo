using nmct.ssa.labo.webshop.DatabaseHelper;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;


namespace nmct.ssa.labo.webshop.Tests.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            
            DatabaseSeed seed = new DatabaseSeed();
            seed.InsertOs(context);
            seed.InsertFrameWork(context);
            seed.InsertDevice(context);
        }
    }
}
