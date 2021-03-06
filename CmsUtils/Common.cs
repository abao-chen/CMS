﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CmsUtils
{
    /// <summary>
    ///     常用公共类
    /// </summary>
    public class Common
    {
        #region 删除数组中的重复项

        /// <summary>
        ///     删除数组中的重复项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] RemoveDup(string[] values)
        {
            var list = new List<string>();
            for (var i = 0; i < values.Length; i++) //遍历数组成员
            {
                if (!list.Contains(values[i]))
                    list.Add(values[i]);
                ;
            }
            return list.ToArray();
        }

        #endregion

        #region Stopwatch计时器

        /// <summary>
        ///     计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            var watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }

        /// <summary>
        ///     计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }

        #endregion

        #region 自动生成编号

        /// <summary>
        ///     表示全局唯一标识符 (GUID)。
        /// </summary>
        /// <returns></returns>
        public static string GuId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            var random = new Random();
            var strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            var code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom; //形如
            return code;
        }

        #endregion

        #region 删除最后一个字符之后的字符

        /// <summary>
        ///     删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        ///     删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        /// <summary>
        ///     删除最后几位的长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string DelLastLength(string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Substring(0, str.Length - length);
            return str;
        }

        #endregion
    }
}