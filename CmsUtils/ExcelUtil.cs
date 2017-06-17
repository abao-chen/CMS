using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// 写Excel文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="fileName">文件名称（带上文件名后缀.xlsx)</param>
        /// <param name="ignoreColumns">忽略的列名</param>
        /// <returns>文件字节流大小</returns>
        public static long WriteExcel(DataTable dt, string filePath, string fileName, string[] ignoreColumns = null)
        {
            long fileLength = 0;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = filePath + "/" + fileName;
            HSSFWorkbook workbook = new HSSFWorkbook();
            FileStream filestream = new FileStream(fullPath, FileMode.Create);
            ISheet sheet = workbook.CreateSheet(fileName);
            if (dt == null)
            {
                workbook.Close();
                filestream.Close();
                filestream.Dispose();
                return fileLength;
            }

            try
            {
                #region 创建表头

                IRow headRow = sheet.CreateRow(0);
                int columnIndex = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (ignoreColumns != null)
                    {
                        if (!ignoreColumns.Contains(dc.ColumnName))
                        {
                            ICell cell = headRow.CreateCell(columnIndex);
                            cell.SetCellValue(dc.ColumnName);

                            ICellStyle cellStyle = workbook.CreateCellStyle();
                            cellStyle.BorderTop = BorderStyle.Thin;
                            cellStyle.BorderRight = BorderStyle.Thin;
                            cellStyle.BorderBottom = BorderStyle.Thin;
                            cellStyle.BorderLeft = BorderStyle.Thin;
                            cell.CellStyle = cellStyle;

                            columnIndex++;
                        }
                    }
                    else
                    {
                        ICell cell = headRow.CreateCell(columnIndex);
                        cell.SetCellValue(dc.ColumnName);

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.BorderTop = BorderStyle.Thin;
                        cellStyle.BorderRight = BorderStyle.Thin;
                        cellStyle.BorderBottom = BorderStyle.Thin;
                        cellStyle.BorderLeft = BorderStyle.Thin;
                        cell.CellStyle = cellStyle;

                        columnIndex++;
                    }
                }

                #endregion

                #region 写数据内容

                int dtCount = dt.Rows.Count;
                columnIndex = 0;
                string cellValue = string.Empty;
                for (int rowIndex = 1; rowIndex <= dtCount; rowIndex++)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                    {
                        if (ignoreColumns != null)
                        {
                            if (!ignoreColumns.Contains(dt.Columns[colIndex].ColumnName))
                            {
                                SetCell(dt, rowIndex, colIndex, row, columnIndex, sheet, workbook);
                                columnIndex++;
                            }
                        }
                        else
                        {
                            SetCell(dt, rowIndex, colIndex, row, columnIndex, sheet, workbook);
                            columnIndex++;
                        }
                    }
                    rowIndex++;
                }

                #endregion

                workbook.Write(filestream);
                fileLength = new FileInfo(fullPath).Length;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                workbook.Close();
                filestream.Close();
                filestream.Dispose();
            }
            return fileLength;
        }

        /// <summary>
        /// 设置单元格的内容和格式
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="row">行对象</param>
        /// <param name="columnIndex"></param>
        /// <param name="sheet">sheet对象</param>
        private static void SetCell(DataTable dt, int rowIndex, int colIndex, IRow row, int columnIndex, ISheet sheet, IWorkbook workbook)
        {
            string cellValue = dt.Rows[rowIndex - 1][colIndex] == null
                ? string.Empty
                : dt.Rows[rowIndex - 1][colIndex].ToString();
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(cellValue);

            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cell.CellStyle = cellStyle;

            #region 设置列宽高度自适应

            int columnWidth = sheet.GetColumnWidth(columnIndex) / 256; //获取当前列宽度  
            int valuelength = Encoding.UTF8.GetBytes(cellValue).Length; //获取当前单元格的内容宽度  
            if (columnWidth < valuelength + 1)
            {
                columnWidth = valuelength + 1;
            } //若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
            sheet.SetColumnWidth(columnIndex, columnWidth * 256);

            int length = Encoding.UTF8.GetBytes(cellValue).Length;
            row.HeightInPoints = 20 * (length / 60 + 1);

            #endregion
        }
    }
}
