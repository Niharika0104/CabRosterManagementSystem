using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Enums
{
    public class StatusEnums
    {
        public enum Status
        {
            Pending = 1,
            Approved,
            Rejected,
            NotAvailed,
            PendingForRejection,
            Completed,
           OnGoing
        }
    }
}
