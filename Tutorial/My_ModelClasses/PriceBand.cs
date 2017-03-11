using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Tutorial.MyClasses
{
    [Serializable]
    [Table("PriceBands")]
    [XmlRoot("PriceBand")]
    public class PriceBand : IPriceBand
    {
        [XmlAttribute("id")]
        public int PriceBandId { get; set; }
        [XmlElement("mileage")]
        public int Mileage { get; set; }
        [XmlElement("valuation")]
        public decimal Valuation { get; set; }

        [XmlElement("priceRecordId")]
        public int PriceRecordId { get; set; }
        public virtual PriceRecord PriceRecord { get; set; }


        public PriceBand() { }
        public PriceBand(int mileage, decimal valuation) {
            this.Mileage = mileage;
            this.Valuation = valuation;
        }

        public int getMileage()
        {
            return this.Mileage;
        }

        public decimal getValuation()
        {
            return this.Valuation;
        }

        public bool Equals(PriceBand priceBand)
        {
           if (priceBand == null || GetType() != priceBand.GetType())
            {
                return false;
            }

            if (!this.PriceBandId.Equals(priceBand.PriceBandId))
            {
                return false;
            }
            else if (!this.Mileage.Equals(priceBand.Mileage))
            {
                return false;
            }
            else if (!this.Valuation.Equals(priceBand.Valuation))
            {
                return false;
            }
            return true;
        }
    }
}