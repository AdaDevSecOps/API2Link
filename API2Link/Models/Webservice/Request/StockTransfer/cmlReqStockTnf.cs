using API2Link.Class.Standard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API2Link.Models.Webservice.Request.StockTransfer
{
    public class cmlReqStockTnf
    {
        /// <summary>
        /// Data(List)
        /// </summary>
        public List<cmlTxnStockData> data { get; set; }
    }

    public class cmlTxnStockData
    {
        /// <summary>
        /// Material document
        /// </summary>
        public string MatDocNo { get; set; }

        /// <summary>
        /// Material document year
        /// </summary>
        public string MatDocYear { get; set; }

        /// <summary>
        /// Material document date
        /// </summary>
        public string MatDocDate { get; set; }

        /// <summary>
        /// Plant
        /// </summary>
        public string Plant { get; set; }

        /// <summary>
        /// Receiving Plant
        /// </summary>
        public string PlantReceived { get; set; }

        /// <summary>
        /// Storage location
        /// </summary>
        public string Sloc { get; set; }

        /// <summary>
        /// Receiving stor. Loc.
        /// </summary>
        public string SlocReceived { get; set; }

        /// <summary>
        /// Material document item
        /// </summary>
        public string MatDocItem { get; set; }

        /// <summary>
        /// Receiving Material
        /// </summary>
        public string MaterialReceived { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public Nullable<decimal> Qty { get; set; }
    }
}