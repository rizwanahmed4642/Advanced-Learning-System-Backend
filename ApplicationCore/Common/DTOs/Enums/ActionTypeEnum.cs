using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.Enums
{
    public enum ActionTypeEnum
    {
       Create = 1,
       Edit = 2,
       Deleted = 3,
       BeforeDelete = 4,
       AfterUpdate = 5,
    }


    public enum SpecialityRunningMode
    {
        Both_OnlineOffline = 1,
        Online = 2,
        Offline = 3,
    }
}
