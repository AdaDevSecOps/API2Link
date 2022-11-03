using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2Link.Class.Standard
{
    public class cMS
    {
        //Response Code
        public readonly string tMS_RespCode001 = "001";
        public readonly string tMS_RespCode700 = "700";
        public readonly string tMS_RespCode701 = "701";
        public readonly string tMS_RespCode800 = "800";
        public readonly string tMS_RespCode900 = "900";
        public readonly string tMS_RespCode904 = "904";
        public readonly string tMS_RespCode907 = "907";

        //Response Desc
        public readonly string tMS_RespDesc001 = "success.";
        public readonly string tMS_RespDesc700 = "all parameter is null.";
        public readonly string tMS_RespDesc701 = "validate parameter model false.|";
        public readonly string tMS_RespDesc800 = "data not found.";
        public readonly string tMS_RespDesc900 = "service process false.";
        public readonly string tMS_RespDesc904 = "key not allowed to use method.";
        public readonly string tMS_RespDesc907 = "cannot connect server MQ";
    }
}