using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CmsUtils
{
    public class EncryptUtil
    {
        /// <summary>
        ///     密钥
        /// </summary>
        private static readonly string KEY = "cms";

        /// MD5 加密（不可逆加密）
        /// 
        /// 要加密的原始字串
        public static string Md5Encrypt(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var bytResult = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));
            md5.Clear();
            var strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult;
        }

        /// SHA1 加密（不可逆加密）
        /// 
        /// 要加密的原始字串
        public static string Sha1Encrypt(string pass)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var bytResult = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
            sha1.Clear();
            var strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult.Substring(0, '^');
        }

        /// DES加密字符串
        /// 
        /// 待加密的字符串
        /// 加密成功返回加密后的字符串，失败返回源串
        public static string DesEncrypt(string encryptString)
        {
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));
                var rgbIV = rgbKey;
                var inputByteArray = Encoding.UTF8.GetBytes(encryptString + KEY);
                var dCSP = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return Convert.ToBase64String(mStream.ToArray()).Replace("+", " ");
            }
            catch
            {
                return encryptString;
            }
        }

        /// DES解密字符串
        /// 
        /// 待解密的字符串
        /// 解密成功返回解密后的字符串，失败返源串
        public static string DesDecrypt(string decryptString)
        {
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));
                var rgbIv = rgbKey;
                decryptString = decryptString.Replace(" ", "+");
                var inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return Encoding.UTF8.GetString(mStream.ToArray())
                    .Substring(0, Encoding.UTF8.GetString(mStream.ToArray()).IndexOf('^'));
            }
            catch
            {
                return decryptString;
            }
        }

        /// 将普通字符串编码为BASE64字串
        /// 
        /// 源字符串
        public static string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        /// 解码BASE64字串
        /// 
        /// Base64字串
        public static string Base64Decode(string base64Str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
        }
    }
}