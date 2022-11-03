using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API2Link.Class.Standard
{
    public class cSP
    {
        /// <summary>
        ///     Validate model.
        /// </summary>
        /// 
        /// <param name="ptModelErr">ref Error message.</param>
        /// <param name="poModelState">Parameter model.</param>
        /// 
        /// <returns>
        ///     true : validate pass.<br/>
        ///     false : validate false.
        /// </returns>
        public static bool SP_CHKbParaModel(out string ptModelErr, ModelStateDictionary poModelState)
        {
            try
            {
                if (poModelState.IsValid)
                {
                    // Validate pass.
                    ptModelErr = "";
                    return true;
                }
                else
                {
                    // Validate false.
                    IEnumerable<string> atErrList = from oState in poModelState.Values
                                                    from oError in oState.Errors
                                                    where !string.IsNullOrEmpty(oError.ErrorMessage)
                                                    select oError.ErrorMessage;

                    ptModelErr = string.Join("|", atErrList);
                    return false;
                }
            }
            catch (Exception)
            {

            }

            ptModelErr = "";
            return false;
        }

        //*Ton 64-05-24
        public static bool SP_GETtHeader(HttpRequest poRequest)
        {
            bool bStatus = false;
            try
            {
                string tXKey = poRequest.Headers["X-Key"].FirstOrDefault();
                if (!string.IsNullOrEmpty(tXKey))
                {
                    if (tXKey == "524aa1ebc230770ed5553bf0a50008449e734fd3f1010ac5c56b5b3003ff8e39")
                    {
                        bStatus = true;
                    }
                }
                return bStatus;
            }
            catch (Exception oEx)
            {
                //throw new HttpResponseException(HttpStatusCode.Forbidden);
                throw new Exception(oEx.Message);
            }
        }

        //Comment by ton 64-05-24
        //public static bool SP_GETtHeader(HttpRequestMessage poRequest)
        //{
        //    bool bStatus = false;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(poRequest.Headers.GetValues("X-Key").FirstOrDefault()))
        //        {
        //            string tXKey = poRequest.Headers.GetValues("X-Key").FirstOrDefault();
        //            if (tXKey == "524aa1ebc230770ed5553bf0a50008449e734fd3f1010ac5c56b5b3003ff8e39")
        //            {
        //                bStatus = true;
        //            }
        //        }
        //        return bStatus;
        //    }
        //    catch (Exception oEx)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.Forbidden);
        //    }
        //}

        public static string SP_DATtTripleDESDecryptData(string ptEncryptedData, string ptKey)
        {
            TripleDESCryptoServiceProvider oTripleDESSrvPvd;
            MemoryStream oMemStm;
            CryptoStream oDecStm;
            byte[] anEncryptedBytes;
            string tStrDec;

            try
            {
                // Initialize the crypto provider.
                oTripleDESSrvPvd = new TripleDESCryptoServiceProvider();
                oTripleDESSrvPvd.Key = SP_DATaSHA1EncryptData(ptKey, (int)Math.Floor(oTripleDESSrvPvd.KeySize / 8.0));
                oTripleDESSrvPvd.IV = SP_DATaSHA1EncryptData("", (int)Math.Floor(oTripleDESSrvPvd.BlockSize / 8.0));

                // Convert the encrypted text string to a byte array.
                anEncryptedBytes = Convert.FromBase64String(ptEncryptedData);

                // Create the stream.
                oMemStm = new MemoryStream();

                // Create the encoder to write to the stream.
                oDecStm = new CryptoStream(oMemStm, oTripleDESSrvPvd.CreateDecryptor(), CryptoStreamMode.Write);

                // Use the crypto stream to write the byte array to the stream.
                oDecStm.Write(anEncryptedBytes, 0, anEncryptedBytes.Length);
                oDecStm.FlushFinalBlock();

                // Convert the encrypted stream to a printable string.
                tStrDec = Encoding.Unicode.GetString(oMemStm.ToArray());
                return tStrDec;
            }
            catch (Exception)
            {

            }
            finally
            {
                oTripleDESSrvPvd = null;
                oMemStm = null;
                oDecStm = null;
                anEncryptedBytes = null;
                tStrDec = null;
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptPlaintext"></param>
        /// <param name="pnHashSize"></param>
        /// <returns></returns>
        public static byte[] SP_DATaSHA1EncryptData(string ptPlaintext, int pnHashSize)
        {
            SHA1CryptoServiceProvider oSHA1SrvPvd;
            byte[] anPlaintextBytes, anHash, anTmpHash;

            try
            {
                // Hash the key.
                oSHA1SrvPvd = new SHA1CryptoServiceProvider();
                anPlaintextBytes = Encoding.Unicode.GetBytes(ptPlaintext);
                anHash = oSHA1SrvPvd.ComputeHash(anPlaintextBytes);

                // Truncate or pad the hash.
                anTmpHash = anHash;
                anHash = new byte[pnHashSize];
                Array.Copy(anTmpHash, anHash, Math.Min(anHash.Length, anTmpHash.Length));

                return anHash;
            }
            catch (Exception)
            {

            }
            finally
            {
                oSHA1SrvPvd = null;
                anPlaintextBytes = null;
                anHash = null;
                anTmpHash = null;
            }

            return null;
        }
    }
}