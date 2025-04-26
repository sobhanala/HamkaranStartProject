using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnbarDomain.Orders
{
    public enum Status
    {
        Draft=0,
        Confirmed=1,
        Processing=2,
        Shipped=3,
        Delivered=4,
        Cancelled=5
    }
}
