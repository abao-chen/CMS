using System;
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
            if (!FileUtil.IsExistFile(filePath))
            {
                return null;
            }

            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new HSSFWorkbook(fs);
            try
            {
                //获取excel的第一个sheet
                var sheet = workbook.GetSheetAt(0);
                var table = new DataTable();
                //获取sheet的首行
                var headerRow = sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                //最后一列的标号  即总的行数
                var rowCount = sheet.LastRowNum;

                for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();

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
            finally
            {
                workbook?.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }

        /// <summary>
        /// 写Excel文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="fileName">文件名称（带上文件名后缀.xlsx)</param>
        /// <param name="ignoreColumns">忽略的列名</param>
        public static void WriteExcel(DataTable dt, string filePath, string fileName, string[] ignoreColumns = null)
        {
            if (dt == null)
            {
                return;
            }

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var fullPath = filePath + "/" + fileName;
            var workbook = new HSSFWorkbook();
            var filestream = new FileStream(fullPath, FileMode.Create);
            var sheet = workbook.CreateSheet(fileName);

            //设置单元格样式
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;

            try
            {
                #region 创建表头

                var headRow = sheet.CreateRow(0);
                var columnIndex = 0;
                foreach (DataColumn dc in dt.Columns)
                    if (ignoreColumns != null)
                    {
                        if (!ignoreColumns.Contains(dc.ColumnName))
                        {
                            var cell = headRow.CreateCell(columnIndex);
                            cell.SetCellValue(dc.ColumnName);
                            cell.CellStyle = cellStyle;

                            columnIndex++;
                        }
                    }
                    else
                    {
                        var cell = headRow.CreateCell(columnIndex);
                        cell.SetCellValue(dc.ColumnName);
                        cell.CellStyle = cellStyle;

                        columnIndex++;
                    }

                #endregion

                #region 写数据内容

                var dtCount = dt.Rows.Count;
                var cellValue = string.Empty;
                for (var rowIndex = 1; rowIndex <= dtCount; rowIndex++)
                {
                    columnIndex = 0;
                    var row = sheet.CreateRow(rowIndex);
                    for (var colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                        if (ignoreColumns != null)
                        {
                            if (!ignoreColumns.Contains(dt.Columns[colIndex].ColumnName))
                            {
                                SetCell(dt, rowIndex, colIndex, row, columnIndex, sheet, cellStyle);
                                columnIndex++;
                            }
                        }
                        else
                        {
                            SetCell(dt, rowIndex, colIndex, row, columnIndex, sheet, cellStyle);
                            columnIndex++;
                        }
                }

                #endregion

                workbook.Write(filestream);
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
        }

        /// <summary>
        /// 根据已有模板写入Excel文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="fileName">文件名称（带上文件名后缀.xlsx)</param>
        /// <param name="templateFilePath">模板文件全路径</param>
        /// <param name="startIndex">写入数据开始行</param>
        /// <param name="ignoreColumns">忽略的列名</param>
        public static void WriteExcel(DataTable dt, string filePath, string fileName, string templateFilePath, int startIndex, string[] ignoreColumns = null)
        {
            if (dt == null || !FileUtil.IsExistFile(templateFilePath))
            {
                return;
            }

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var filestream = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(filestream);
            var fullPath = filePath + fileName;
            var newFilesStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var sheet = workbook.GetSheetAt(0);
            try
            {
                #region 写数据内容

                var dtCount = dt.Rows.Count;
                var rowIndex = startIndex;
                for (; rowIndex < dtCount + startIndex; rowIndex++)
                {
                    var columnIndex = 0;
                    var row = sheet.CreateRow(rowIndex);
                    for (var colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                        if (ignoreColumns != null)
                        {
                            if (!ignoreColumns.Contains(dt.Columns[colIndex].ColumnName))
                            {
                                var cellValue = dt.Rows[rowIndex - startIndex][colIndex] == null
                                    ? string.Empty
                                    : dt.Rows[rowIndex - startIndex][colIndex].ToString();
                                var cell = row.CreateCell(columnIndex);
                                cell.SetCellValue(cellValue);
                                columnIndex++;
                            }
                        }
                        else
                        {
                            var cellValue = dt.Rows[rowIndex - startIndex][colIndex] == null
                                ? string.Empty
                                : dt.Rows[rowIndex - startIndex][colIndex].ToString();
                            var cell = row.CreateCell(columnIndex);
                            cell.SetCellValue(cellValue);
                            columnIndex++;
                        }
                }
                IRow lastRow = sheet.GetRow(rowIndex);
                if (lastRow != null)
                {
                    sheet.RemoveRow(lastRow);
                }

                #endregion

                workbook.Write(newFilesStream);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                workbook.Close();
                newFilesStream.Close();
                filestream.Close();
                newFilesStream.Dispose();
                filestream.Dispose();
            }
        }

        /// <summary>
        ///     设置单元格的内容和格式
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="row">行对象</param>
        /// <param name="columnIndex"></param>
        /// <param name="sheet">sheet对象</param>
        /// <param name="cellStyle">单元格样式</param>
        private static void SetCell(DataTable dt, int rowIndex, int colIndex, IRow row, int columnIndex, ISheet sheet, ICellStyle cellStyle)
        {
            var cellValue = dt.Rows[rowIndex - 1][colIndex] == null
                ? string.Empty
                : dt.Rows[rowIndex - 1][colIndex].ToString();
            var cell = row.CreateCell(columnIndex);
            cell.SetCellValue(cellValue);
            cell.CellStyle = cellStyle;

            #region 设置列宽高度自适应

            var columnWidth = sheet.GetColumnWidth(columnIndex) / 256; //获取当前列宽度  
            var valuelength = Encoding.UTF8.GetBytes(cellValue).Length; //获取当前单元格的内容宽度  
            if (columnWidth < valuelength + 1)
                columnWidth = valuelength + 1;
            sheet.SetColumnWidth(columnIndex, columnWidth * 256);

            var length = Encoding.UTF8.GetBytes(cellValue).Length;
            row.HeightInPoints = 20 * (length / 60 + 1);

            #endregion
        }
    }
}