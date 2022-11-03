using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API2Link.Class.Standard
{
    public class cLog
    {
        /// <summary>
        /// log file
        /// </summary>
        /// <param name="ptForm"></param>
        /// <param name="ptFunction"></param>
        public static void C_PRCxLog(string ptForm, string ptFunction)
        {
            string tPath;
            string tFileName;
            try
            {
                #region Check Directory Log
                tPath = AppDomain.CurrentDomain.BaseDirectory + "Log";
                if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                #endregion

                #region Create File Name Log
                tFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                tPath += @"\" + tFileName;

                #endregion

                #region Check File in Log

                if (!File.Exists(tPath))
                {
                    File.Create(tPath).Dispose();
                }
                    
                #endregion

                #region Write Data To File Log

                using (System.IO.StreamWriter oOutputFile = new System.IO.StreamWriter(tPath, true))
                {
                    oOutputFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " : Form (" + ptForm + ") > " + ptFunction);
                    oOutputFile.Dispose();
                }

                #endregion

            }
            catch (Exception oEx) { System.Diagnostics.Debug.WriteLine(oEx.ToString()); }
            finally
            {
                tPath = null;
            }
        }


    }
}