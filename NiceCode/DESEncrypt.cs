using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace NiceCode
{
    /// <summary>
    /// DES加密类
    /// </summary>
    public class DESEncrypt
    {
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string KEY = "DJs0ajd#%^j1l$";//密钥
        private static byte[] sKey;
        private static byte[] sIV;
        
        public DESEncrypt()
        {

        }
        /// <summary>
        /// 加密方法 
        /// </summary>
        /// <param name="pToEncrypt">加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt, string keyStr = "")
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StringBuilder ret = null;
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                if (keyStr == null || keyStr == "")
                    keyStr = DESEncrypt.KEY;
                //把字符串放到byte数组中   
                //原来使用的UTF8编码，我改成Unicode编码了，不行   
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                sKey = new byte[8];
                sIV = new byte[8];

                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];
                des.Key = sKey;
                des.IV = sIV;

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (null != cs) cs.Close();
                if (null != ms) ms.Close();
            }
            return ret.ToString();
        }

        /// <summary>
        /// 解密方法  
        /// </summary>
        /// <param name="pToDecrypt">解密字符串</param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt, string keyStr = "")
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            sKey = new byte[8];
            sIV = new byte[8];
            string values = "";

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            if (keyStr == null || keyStr == "")
                keyStr = DESEncrypt.KEY;

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            try
            {
                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];
                des.Key = sKey;
                des.IV = sIV;
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象   
                values = System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
            }
            //finally
            //{
            //    cs.Close();
            //    ms.Close();
            //}
            return values;
        }
    }
}
