using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VIcocaApplication.UI
{    
    public class Enum
    {
        public enum EmailType
        {
            None,
            Invitation,
            Survey,
            TFO = 5,
            RIP = 6,
            DNA = 7,
        }       
    }
}