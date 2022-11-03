using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2Link.Class.Standard
{
    public class cCS
    {
        // Version Main API.
        public const string tCS_APIVer = "V4";

        // Key
        public const string tCS_Key = "Sk$4dF#z"; //"SOFTXada";

        // Message attribute model.
        public const string tCS_MsgAtrRequired = "The {0} field is required.";
        public const string tCS_MsgAtrMaxLength = "{0} cannot be greater than {1}.";
        public const string tCS_MsgAtrArrayMaxLength = "{0} size cannot be greater than {1}.";

        // Default configuration database.
        public const int nCS_ConTme = 30;
        public const int nCS_CmdTme = 30;
        public const int nCS_BcpTme = 60;
    }
}