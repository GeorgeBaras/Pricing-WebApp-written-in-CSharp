using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext_Repositories
{
    public class PriceBandRepository : IRepository
    {
        private MyApplicationDBContext db;
        public PriceBandRepository(MyApplicationDBContext db)
        {
            this.db = db;
        }

        public List<PriceBand> getAllEntries()
        {
            MyApplicationDBContext myApplicationDBContext = new MyApplicationDBContext();
            return myApplicationDBContext.PriceBands.ToList();
        }

        public PriceBand getPriceBandById(int id) {
            return null;
        }

        public Boolean deletePriceBandById(int Id) {
            return false;
        }

        public Boolean updateMileageById() {
            return false;
        }

        public Boolean updateValuationById()
        {
            return false;
        }

        
        public Boolean deletePriceBandById() {
            return false;
        }

        public Boolean deleteAllEntries() {
            return false;
        }

        public Boolean dbIsEmpty() {
            return false;
        }
    }
}