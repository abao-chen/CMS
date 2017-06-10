using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CmsUtils
{
    public static class SecurityUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText">需要加密的字符串</param>
        /// <returns>返回加密后的结果</returns>
        public static string Md5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="randomModel">生成模式，1：纯数字，2：纯字母；3：数字加字母</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomCode(int randomModel, int length)
        {
            List<string> strList = new List<string>()
            {
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
            List<string> intList = new List<string>()
            {
               "0","1","2","3","4","5","6","7","8","9"
            };
            List<string> ranList = new List<string>();
            switch (randomModel)
            {
                case 1:
                    ranList.AddRange(intList);
                    break;
                case 2:
                    ranList.AddRange(strList);
                    break;
                default:
                    ranList.AddRange(intList);
                    ranList.AddRange(strList);
                    break;
            }

            StringBuilder newRandom = new StringBuilder(36);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(ranList[rd.Next(36)]);
            }

            return newRandom.ToString();
        }
    }
}
