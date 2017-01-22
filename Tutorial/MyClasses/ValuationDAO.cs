using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial.MyClasses
{
    public interface ValuationDAO
    {
        PriceRecord getPriceRecord(String lookupCode);
    }
}
