using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Tutorial.MyClasses
{
    [Serializable]
    [Table("Vehicles")]
    [XmlRoot("vehicle")]
    public class Vehicle
    { 
        [DataMember]
        [XmlAttribute("id")]
        public int id { get; set; }
        [DataMember]
        [XmlElement("make")]
        public String make { get; set; }
        [DataMember]
        [XmlElement("model")]
        public String model { get; set; }
        [DataMember]
        [XmlElement("derivative")]
        public String derivative { get; set; }
        [DataMember]
        [XmlElement("lookupCode")]
        public String lookupCode { get; set; }
        [DataMember]
        [XmlElement("mileage")]
        public int mileage { get; set; }
        [DataMember]
        [XmlElement("value")]
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

        public bool Equals(Vehicle vehicle)
        {
            if (vehicle == null || GetType() != vehicle.GetType())
            {
                return false;
            }

            if (!this.id.Equals(vehicle.id)) {
                return false;
            }
            else if (!this.make.Equals(vehicle.make))
            {
                return false;
            }
            else if (!this.model.Equals(vehicle.model))
            {
                return false;
            }
            else if (!this.derivative.Equals(vehicle.derivative))
            {
                return false;
            }
            else if (!this.lookupCode.Equals(vehicle.lookupCode))
            {
                return false;
            }
            else if (!this.mileage.Equals(vehicle.mileage))
            {
                return false;
            }
            else if (!this.value.Equals(vehicle.value))
            {
                return false;
            }
            return true;
        }
        
    }
}