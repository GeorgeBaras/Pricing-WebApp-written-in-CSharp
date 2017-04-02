using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.MyClasses;

namespace Tutorial.Models
{
    public class VehicleList
    {
        public List<Vehicle> vehicleList { get; set; }
        public VehicleList(List<Vehicle> vehicleList) {
            this.vehicleList = vehicleList;
        }
    }
}