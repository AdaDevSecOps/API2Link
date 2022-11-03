using RabbitMQ.Client;
using System;
using System.Text;

namespace API2Link.Class.Standard
{
    public class cRabbitMQ
    {
        public static string tC_HostName { get; set; }
        public static string tC_UsrName { get; set; }
        public static string tC_Pwd { get; set; }
        public static string tC_VirtualHost { get; set; }
        public static int nC_Port { get; set; }
        public static string tC_QueueName { get; set; }
        public static string tC_QueueName_StockTransfer { get; set; }

        //*Ton 64-05-24
        public bool C_GETbLoadConfigMQ()
        {
            try
            {
                tC_HostName = cAppSetting.Default.tRQHost;
                tC_UsrName = cAppSetting.Default.tRQUsr;
                tC_Pwd = cSP.SP_DATtTripleDESDecryptData(cAppSetting.Default.tRQPwd, cCS.tCS_Key);
                tC_VirtualHost = cAppSetting.Default.tRQVirtual;
                nC_Port = Convert.ToInt32(cAppSetting.Default.tRQPort);
                tC_QueueName_StockTransfer = "LK_QPdtStkTnf";
                return true;
            }
            catch (Exception oEx)
            {
                cLog.C_PRCxLog("cRabbitMQ", "C_GETbLoadConfigMQ : Error/" + oEx.Message.ToString()); //*Arm 63-09-08
                return false;
            }
        }


        //Comment by Ton 64-05-24
        //public bool C_GETbLoadConfigMQ()
        //{
        //    try
        //    {
        //        NameValueCollection oCfgDabtabase;
        //        oCfgDabtabase = (NameValueCollection)ConfigurationManager.GetSection("RabbitMQ");

        //        //cVB.oVB_RabbitMQ = new cmlRabbitMQ();
        //        //cVB.oVB_RabbitMQ.tMQHostName = oCfgDabtabase["MQHost"].Trim();
        //        //cVB.oVB_RabbitMQ.tMQUserName = oCfgDabtabase["MQUser"].Trim();
        //        //cVB.oVB_RabbitMQ.tMQPassword = oCfgDabtabase["MQPwd"].Trim();
        //        //cVB.oVB_RabbitMQ.tMQVirtualHost = oCfgDabtabase["MQVH"].Trim();
        //        //cVB.oVB_RabbitMQ.tMQListQueue = oCfgDabtabase["MQQueue"].Trim();
        //        //cVB.oVB_RabbitMQ.nMQPort = Convert.ToInt32(oCfgDabtabase["MQPort"].Trim());

        //        //*Arm 63-08-23
        //        tC_HostName = oCfgDabtabase["MQHost"].Trim();
        //        tC_UsrName = oCfgDabtabase["MQUser"].Trim();
        //        tC_Pwd = cSP.SP_DATtTripleDESDecryptData(oCfgDabtabase["MQPwd"].Trim(), cCS.tCS_Key);
        //        tC_VirtualHost = oCfgDabtabase["MQVH"].Trim();
        //        nC_Port = Convert.ToInt32(oCfgDabtabase["MQPort"].Trim());
        //        tC_QueueName_StockTransfer = "LK_QPdtStkTnf";
        //        return true;
        //    }
        //    catch(Exception oEx)
        //    {
        //        cLog.C_PRCxLog("cRabbitMQ", "C_GETbLoadConfigMQ : Error/" + oEx.Message.ToString()); //*Arm 63-09-08
        //        return false;
        //    }
        //}

        /// <summary>
        /// Send Message to Server RabbitMQ
        /// </summary>
        /// <param name="ptMsg"></param>
        /// <returns></returns>
        public bool C_PRCbSendData2Srv(string ptMsg, string ptQueue)
        {
            try
            {
                ConnectionFactory oFactory = new ConnectionFactory();
                oFactory.HostName = tC_HostName;
                oFactory.UserName = tC_UsrName;
                oFactory.Password = tC_Pwd;
                oFactory.VirtualHost = tC_VirtualHost;
                oFactory.Port = nC_Port;
                using (IConnection oConn = oFactory.CreateConnection())
                {
                    using (IModel oChannel = oConn.CreateModel())
                    {
                        oChannel.QueueDeclare(ptQueue, false, false, false, null);
                        var oBody = Encoding.UTF8.GetBytes(ptMsg);
                        oChannel.BasicPublish("", ptQueue, false, null, oBody);
                    }
                }
                return true;
            }
            catch(Exception oEx)
            {
                oEx.Message.ToString();
                cLog.C_PRCxLog("cRabbitMQ", "C_PRCbSendData2Srv : Error/" + oEx.Message.ToString()); //*Arm 63-09-08
                return false;
            }
        }

        //public bool C_bCHKMQConnection()
        //{
        //    try
        //    {
        //        ConnectionFactory oFactory = new ConnectionFactory();
        //        oFactory.HostName = cVB.oVB_RabbitMQ.tMQHostName;
        //        oFactory.UserName = cVB.oVB_RabbitMQ.tMQUserName;
        //        oFactory.Password = cVB.oVB_RabbitMQ.tMQPassword;
        //        oFactory.VirtualHost = cVB.oVB_RabbitMQ.tMQVirtualHost;
        //        oFactory.Port = cVB.oVB_RabbitMQ.nMQPort;
        //        using (IConnection oConn = oFactory.CreateConnection())
        //        {
        //            using (IModel oChannel = oConn.CreateModel())
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch { }
        //    return false;
        //}

        public bool C_PRCxMQPublish(string ptQueueName,  string ptMessage)
        {
            ConnectionFactory oFactory;
            //cFunction oFunc = new cFunction();
            string tQueueName = ptQueueName;
            bool bStatus = false;
            try
            {
                oFactory = new ConnectionFactory();
                oFactory.HostName = cVB.oVB_RabbitMQ.tMQHostName;
                oFactory.UserName = cVB.oVB_RabbitMQ.tMQUserName;
                oFactory.Password = cVB.oVB_RabbitMQ.tMQPassword;
                oFactory.VirtualHost = cVB.oVB_RabbitMQ.tMQVirtualHost;
                oFactory.Port = cVB.oVB_RabbitMQ.nMQPort;
                using (var oConn = oFactory.CreateConnection())
                {
                    using (var oChannel = oConn.CreateModel())
                    {
                        var body = Encoding.UTF8.GetBytes(ptMessage);
                        oChannel.QueueDeclare(tQueueName, false, false, false, null);
                        oChannel.BasicPublish("", tQueueName, false, null, body);
                        bStatus = true;
                        //ptErrMsg = "";
                    }
                }
            }
            catch (Exception oEx)
            {
                //ptErrMsg = oEx.Message.ToString();
                cLog.C_PRCxLog("cRabbitMQ","C_PRCxMQPublish : Error/" + oEx.Message.ToString()); //*Arm 63-09-08
            }
            finally
            {
                oFactory = null;
            }
            return bStatus;
        }
    }
}