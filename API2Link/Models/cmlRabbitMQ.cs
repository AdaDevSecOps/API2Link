using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2Link.Models
{
    public class cmlRabbitMQ
    {
        /// <summary>
        /// Host name.
        /// </summary>
        public string tMQHostName { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string tMQUserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string tMQPassword { get; set; }

        /// <summary>
        /// Virtual host.
        /// </summary>
        public string tMQVirtualHost { get; set; }

        /// <summary>
        /// List queue name. ex. SALEPOS,CONSOLIDATE,FCBCH2HQ
        /// </summary>
        public string tMQListQueue { get; set; }
        public Int32 nMQPort { get; set; }
    }
}