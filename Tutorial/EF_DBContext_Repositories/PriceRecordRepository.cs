using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;

namespace Tutorial.EF_DBContext_Repositories
{
    public class PriceRecordRepository : IRepository
    {

        private MyApplicationDBContext db;
        public PriceRecordRepository(MyApplicationDBContext db)
        {
            this.db = db;
        }
        public bool dbIsEmpty()
        {
            throw new NotImplementedException();
        }

        public bool deleteAllEntries()
        {
            throw new NotImplementedException();
        }
    }
}