using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.Models
{
    public class LookUpCodesViewModel
    {
       public List<String> LookupCodes { get; set; }
        public LookUpCodesViewModel(List<String> lookupCodes) {
            this.LookupCodes = lookupCodes;
        }
    }
}