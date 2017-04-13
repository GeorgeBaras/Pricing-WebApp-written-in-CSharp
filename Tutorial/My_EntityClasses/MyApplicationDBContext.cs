using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext
{
    public class appDBContext : DbContext
    {
        public appDBContext(): base("sqlExpressConnection")
        {

        }
        public DbSet<Vehicle> Vehicles { get; set;}
        public DbSet<PriceBand> PriceBands { get; set; }
        public DbSet<PriceRecord> PriceRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceBand>()
            .HasRequired(t => t.PriceRecord)
            .WithMany(t => t.PriceBands)
            .HasForeignKey(d => d.PriceRecordId)
            .WillCascadeOnDelete(true);
        }

    }
}