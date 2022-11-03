using API2Link.Class.Standard;
using API2Link.Models.Other;
using API2Link.Models.Webservice.Request.StockTransfer;
using API2Link.Models.Webservice.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading;

namespace API2Link.Controllers
{
    ///<summary>
    /// ใบโอนสินค้าระหว่างคลัง
    ///</summary>
    [ApiController]///
    [Route(cCS.tCS_APIVer)]
    public class cStockTransferController : ControllerBase
    {
        ///<summary>
        /// ใบโอนสินค้าระหว่างคลัง
        ///</summary>
        ///<param name="poData"></param>
        ///<returns>
        ///&#8195;     001 : Success.<br/>
        ///&#8195;     700 : all parameter is null.<br/>
        ///&#8195;     800 : Data not found.<br/>
        ///&#8195;     900 : Service process false.<br/>
        ///&#8195;     904 : Key not allowed to use method.<br/>
        ///&#8195;     907 : cannot connect server MQ.<br/>
        /// </returns>

        [Route("StockTransfer")]
        [HttpPost]
        
        public cmlResBase C_POSoStockTransfer(cmlReqStockTnf poData)
        {
            cmlResBase oResult = new cmlResBase();
            cmlRcvMQ oRcv;
            cRabbitMQ oRQ;
            cMS oMsg = new cMS();
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                
                oMsg = new cMS();
                
                if (poData == null)
                {
                    oResult.ErrCode = oMsg.tMS_RespCode700;
                    oResult.ErrDesc = oMsg.tMS_RespDesc700;
                    cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode700 + ":" + oMsg.tMS_RespDesc700); //*Arm 63-09-08
                    return oResult;
                }

                if (poData.data == null)
                {
                    oResult.ErrCode = oMsg.tMS_RespCode700;
                    oResult.ErrDesc = oMsg.tMS_RespDesc700;
                    cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode700 + ":" + oMsg.tMS_RespDesc700); //*Arm 63-09-08
                    return oResult;
                }
                cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Request/" + JsonConvert.SerializeObject(poData)); //*Arm 63-09-08

                //Check X-Key
                //*Ton 64-05-24
                //if (cSP.SP_GETtHeader(Request) == false)
                if (cSP.SP_GETtHeader(HttpContext.Request) == false)
                {
                    oResult.ErrCode = oMsg.tMS_RespCode904;
                    oResult.ErrDesc = oMsg.tMS_RespDesc904;
                    cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode904 + ":" + oMsg.tMS_RespDesc904); //*Arm 63-09-08
                    return oResult;
                }

                oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueName_StockTransfer))
                    {
                        oRcv = new cmlRcvMQ();
                        oRcv.ptFunction = "StockTransfer";
                        oRcv.ptSource = "API2Link";
                        oRcv.ptDest = "MQAdaLink";
                        oRcv.ptData = JsonConvert.SerializeObject(poData);
                        string tMsgJson = Newtonsoft.Json.JsonConvert.SerializeObject(oRcv);
                        cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Response/" + tMsgJson); //*Arm 63-09-08

                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueName_StockTransfer))
                        {
                            oResult.ErrCode = oMsg.tMS_RespCode001;
                            oResult.ErrDesc = oMsg.tMS_RespDesc001;
                            cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : " + oMsg.tMS_RespCode001 + ":" + oMsg.tMS_RespDesc001); //*Arm 63-09-08
                        }
                        else
                        {
                            oResult.ErrCode = oMsg.tMS_RespCode907;
                            oResult.ErrDesc = oMsg.tMS_RespDesc907;
                            cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode907 + ":" + oMsg.tMS_RespDesc907); //*Arm 63-09-08
                        }
                    }
                    else
                    {
                        oResult.ErrCode = oMsg.tMS_RespCode907;
                        oResult.ErrDesc = oMsg.tMS_RespDesc907;
                        cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode907 + ":" + oMsg.tMS_RespDesc907); //*Arm 63-09-08
                    }
                }
                else
                {
                    oResult.ErrCode = oMsg.tMS_RespCode907;
                    oResult.ErrDesc = oMsg.tMS_RespDesc907;
                    cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/" + oMsg.tMS_RespCode907 + ":" + oMsg.tMS_RespDesc907); //*Arm 63-09-08
                }
                return oResult;
            }
            catch (Exception oEx)
            {
                oResult.ErrCode = oMsg.tMS_RespCode900;
                oResult.ErrDesc = oMsg.tMS_RespCode900 + " : " + oEx.Message.ToString();
                cLog.C_PRCxLog("cStockTransferController", "C_POSoStockTransfer : Error/"+ oMsg.tMS_RespCode900 + ":"+ oMsg.tMS_RespCode900 + " " + oEx.Message.ToString()); //*Arm 63-09-08
                return oResult;
            }
            finally
            {
                oResult = null;
                oRcv = null;
                oMsg = null;
                oRQ = null;
            }
        }
    }
}