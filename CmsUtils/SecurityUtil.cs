using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CmsUtils
{
    public static class SecurityUtil
    {
        private static readonly Random random = new Random();

        /// <summary>
        ///     MD5加密
        /// </summary>
        /// <param name="strText">需要加密的字符串</param>
        /// <returns>返回加密后的结果</returns>
        public static string Md5Encrypt64(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(strText));
            return Convert.ToBase64String(result);
        }

        /// <summary>
        ///     生成随机码
        /// </summary>
        /// <param name="randomModel">生成模式，1：纯数字，2：纯字母；3：数字加字母</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomCode(int randomModel, int length)
        {
            var strList = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z"
            };
            var intList = new List<string>
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
            var ranList = new List<string>();
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

            var newRandom = new StringBuilder();
            for (var i = 0; i < length; i++)
                newRandom.Append(ranList[random.Next(ranList.Count)]);

            return newRandom.ToString();
        }
    }
}