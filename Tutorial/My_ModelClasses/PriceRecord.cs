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
    [Table("PriceRecords")]
    [XmlRoot("PriceRecord")]
    public class PriceRecord : IPriceRecord
    {
        [XmlAttribute("id")]
        public int PriceRecordId { get; set; }
        [XmlElement("LookupCode")]
        public String LookupCode { get; set; }
        [XmlArray("PriceBands")]
        public virtual List<PriceBand> PriceBands { get; set; }
        public PriceRecord() {
            PriceBands = new List<PriceBand>();
        }
        public PriceRecord(String lookupCode, List<PriceBand> priceBands) {
            PriceBands = new List<PriceBand>();
            LookupCode = lookupCode;
            PriceBands = priceBands;
        }

        public PriceRecord(String lookupCode, PriceBand priceBand)
        {
            PriceBands = new List<PriceBand>();
            LookupCode = lookupCode;
            PriceBands.Add(priceBand);
        }

        public string getLookupCode()
        {
            return LookupCode;
        }

        public List<PriceBand> getPriceBands()
        {
            return PriceBands;
        }

        public bool Equals(PriceRecord priceRecord)
        {
            if (priceRecord == null || GetType() != priceRecord.GetType())
            {
                return false;
            }
            if (!this.LookupCode.Equals(priceRecord.LookupCode))
            {
                return false;
            }
            for (int i= 0; i < this.PriceBands.Count; i++) {
                if (!this.PriceBands[i].Equals(priceRecord.PriceBands[i])) {
                    return false;
                }
            }

            return true;
        }

        
    }
}