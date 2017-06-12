using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CmsUtils
{
    public class ExcelUtil
    {
        public static DataTable ReadExcel(string filePath)
        {
            try
            {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new HSSFWorkbook(fs);
                //获取excel的第一个sheet
                ISheet sheet = workbook.GetSheetAt(0);
                DataTable table = new DataTable();
                //获取sheet的首行
                IRow headerRow = sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                //最后一列的标号  即总的行数
                int rowCount = sheet.LastRowNum;

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    table.Rows.Add(dataRow);
                }

                workbook = null;
                sheet = null;
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
