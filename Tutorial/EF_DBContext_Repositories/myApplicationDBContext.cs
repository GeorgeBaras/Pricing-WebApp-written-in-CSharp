using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext
{
    public class MyApplicationDBContext : DbContext
    {
        public MyApplicationDBContext(): base()
        {

        }
        public DbSet<Vehicle> Vehicles  { get; set;}
        public DbSet<PriceBandImp> PriceBands { get; set; }
        public DbSet<PriceRecordImp> PriceRecords { get; set; }

    }
}