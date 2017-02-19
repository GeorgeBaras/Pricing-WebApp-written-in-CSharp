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
    public VehicleRepository(){}

        // get all vehicles

        // delete all vehicles

        // add a vehicle

        // get a vehicle

        // delete a vehicle

        // add list of vehicles

        // delete list of vehicles  

        // update vehicle
        public List<Vehicle> GetVehicles() {
            MyApplicationDBContext myApplicationDBContext = new MyApplicationDBContext();
            return myApplicationDBContext.Vehicles.ToList();
        }
    }
}