using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext_Repositories
{
    public class VehicleRepository
    {
        public List<Vehicle> GetVehicles() {
            MyApplicationDBContext myApplicationDBContext = new MyApplicationDBContext();
            return myApplicationDBContext.Vehicles.ToList();
        }
    }
}