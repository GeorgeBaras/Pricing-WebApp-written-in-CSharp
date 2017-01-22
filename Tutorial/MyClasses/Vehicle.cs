using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class Vehicle
    {
        private String make;
        private String model;
        private String derivative;
        private String lookupCode;
        private int mileage;
        private decimal value;

        public Vehicle(String make, String model, String derivative, String lookupCode, int mileage, decimal value)
        {
            this.Make = make;
            this.Model = model;
            this.Derivative = derivative;
            this.LookupCode = lookupCode;
            this.Mileage = mileage;
            this.Value = value;
        }

        public string Make
        {
            get
            {
                return make;
            }

            set
            {
                make = value;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public string Derivative
        {
            get
            {
                return derivative;
            }

            set
            {
                derivative = value;
            }
        }

        public string LookupCode
        {
            get
            {
                return lookupCode;
            }

            set
            {
                lookupCode = value;
            }
        }

        public int Mileage
        {
            get
            {
                return mileage;
            }

            set
            {
                mileage = value;
            }
        }

        public decimal Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        
    }
}