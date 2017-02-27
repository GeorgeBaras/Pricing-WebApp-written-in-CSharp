using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    [Serializable]
    [Table("Vehicles")]
    public class Vehicle
    { 
        public int id { get; set; }
        public String make { get; set; }
        public String model { get; set; }
        public String derivative { get; set; }
        public String lookupCode { get; set; }
        public int mileage { get; set; }
        public decimal value { get; set; }

        public Vehicle() { }
        public Vehicle(String make, String model, String derivative, String lookupCode, int mileage, decimal value)
        {
            this.make = make;
            this.model = model;
            this.derivative = derivative;
            this.lookupCode = lookupCode;
            this.mileage = mileage;
            this.value = value;
        }

        
    }
}