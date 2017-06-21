using System.Data;

namespace CmsUtils
{
    public static class ExtTable
    {
        /// <summary>
        ///     获取表里某页的数据
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="allPage">返回总页数</param>
        /// <returns>返回当页表数据</returns>
        public static DataTable GetPage(this DataTable data, int pageIndex, int pageSize, out int allPage)
        {
            allPage = data.Rows.Count / pageSize;
            allPage += data.Rows.Count % pageSize == 0 ? 0 : 1;
            var Ntable = data.Clone();
            var startIndex = pageIndex * pageSize;
            var endIndex = startIndex + pageSize > data.Rows.Count ? data.Rows.Count : startIndex + pageSize;
            if (startIndex < endIndex)
                for (var i = startIndex; i < endIndex; i++)
                    Ntable.ImportRow(data.Rows[i]);
            return Ntable;
        }

        /// <summary>
        ///     根据字段过滤表的内容
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetDataFilter(DataTable data, string condition)
        {
            if (data != null && data.Rows.Count > 0)
                if (condition.Trim() == "")
                {
                    return data;
                }
                else
                {
                    var newdt = new DataTable();
                    newdt = data.Clone();
                    var dr = data.Select(condition);
                    for (var i = 0; i < dr.Length; i++)
                        newdt.ImportRow(dr[i]);
                    return newdt;
                }
            return null;
        }
    }
}