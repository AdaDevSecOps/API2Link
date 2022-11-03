using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2Link.Models.Webservice.StockTransfer
{
    public class cmlReqStkTFToBK
    {
        // ชื่อ Function
        public string ptFunction { get; set; }
        // ต้นทาง
        public string ptSource { get; set; }
        // ปลายทาง
        public string ptDest { get; set; }
        // ข้อมูล
        public cmlData ptData { get; set; }
    }
    public class cmlData
    {
        // รหัสสาขาตัวเอง
        public string ptFilter { get; set; }
        // จากวันที่ๆต้องการค้นหา
        public string ptDateFrm { get; set; }
        // ถึงวันที่ๆต้องการค้นหา
        public string ptDateTo { get; set; }
    }

}