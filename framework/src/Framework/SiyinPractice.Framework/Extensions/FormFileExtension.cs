using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace SiyinPractice.Framework.Extensions
{
    public static class FormFileExtension
    {
        public static IWorkbook CreateWorkBook(byte[] content)
        {

            using (var stream = new MemoryStream(content))
            {
                return WorkbookFactory.Create(stream);
            }
        }
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            var fileLength = formFile.Length;
            using var stream = formFile.OpenReadStream();
            var bytes = new byte[fileLength];

            stream.Read(bytes, 0, (int)fileLength);

            return bytes;
        }
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static TableByImprot ExcelToDataTable(IFormFile file)
        {
            TableByImprot tableByImprot = new();
            tableByImprot.dataTable = new DataTable();
            //DataColumn column = null;
            tableByImprot.ListSn = new List<string>();
            tableByImprot.ListOrgPnSn = new List<string>();
            tableByImprot.ListNum = new List<string>();
            tableByImprot.ListIndex = new List<string>();
            tableByImprot.ListBin = new List<string>();
            tableByImprot.ListLocation = new List<string>();
            tableByImprot.ListDept = new List<string>();
            tableByImprot.ListProject = new List<string>();
            tableByImprot.ListPlantSn = new List<string>();
            tableByImprot.ListNumEmpty = new List<string>();
            ISheet sheet = null;
            //int startRow = headerRowIndex + 1;
            var headerRow = new List<string>();
            try
            {
                var stream = file.ToByteArray();
                IWorkbook workbook = CreateWorkBook(stream);
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                    var rowIterator = sheet.GetRowEnumerator();
                    while (rowIterator.MoveNext())
                    {
                        var row = (IRow)rowIterator.Current;
                        var isNotEmpty = row.Cells.Any(cell => cell != null && !string.IsNullOrEmpty(cell.ToString()));

                        if (isNotEmpty)
                        {
                            if (row.RowNum == 0)
                            {
                                // 处理表头
                                for (int i = 0; i < row.LastCellNum; i++)
                                {
                                    var cell = row.GetCell(i);
                                    if (cell != null)
                                    {
                                        headerRow.Add(cell.StringCellValue);
                                        tableByImprot.dataTable.Columns.Add(cell.StringCellValue);
                                    }
                                }
                            }
                            else
                            {
                                // 处理数据行
                                var dataRow = tableByImprot.dataTable.NewRow();
                                var isEmptyRow = false;
                                for (int i = 0; i < row.LastCellNum; i++)
                                {
                                    var cell = row.GetCell(i);
                                    if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                                    {
                                        if (i == 0)
                                        {
                                            if (tableByImprot.ListNum.Count <= 0)
                                            {
                                                tableByImprot.ListNum.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListNum.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListNum.Add(cell.ToString());
                                            }

                                        }
                                        if (i == 1)
                                        {
                                            if (tableByImprot.ListDept.Count <= 0)
                                            {
                                                tableByImprot.ListDept.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListDept.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListDept.Add(cell.ToString());
                                            }

                                        }
                                        if (i == 2)
                                        {
                                            if (tableByImprot.ListOrgPnSn.Count <= 0)
                                            {
                                                tableByImprot.ListOrgPnSn.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListOrgPnSn.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListOrgPnSn.Add(cell.ToString());
                                            }

                                        }
                                        if (i == 4)
                                        {
                                            if (tableByImprot.ListSn.Count <= 0)
                                            {
                                                tableByImprot.ListSn.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListSn.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListSn.Add(cell.ToString());
                                            }

                                        }
                                        if (i == 6)
                                        {
                                            if (tableByImprot.ListBin.Count <= 0)
                                            {
                                                tableByImprot.ListBin.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListBin.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListBin.Add(cell.ToString());
                                            }

                                        }
                                        if (i == 7)
                                        {
                                            if (tableByImprot.ListLocation.Count <= 0)
                                            {
                                                tableByImprot.ListLocation.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListLocation.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListLocation.Add(cell.ToString());
                                            }

                                        }
                                        //if (i == 8)
                                        //{
                                        //    if (tableByImprot.ListPlantSn.Count <= 0)
                                        //    {
                                        //        tableByImprot.ListPlantSn.Add(cell.ToString());
                                        //    }   
                                        //    if (!tableByImprot.ListPlantSn.Contains(cell.ToString()))
                                        //    {
                                        //        tableByImprot.ListPlantSn.Add(cell.ToString());
                                        //    }

                                        //}
                                        if (i == 10)
                                        {

                                            if (tableByImprot.ListProject.Count <= 0)
                                            {
                                                tableByImprot.ListProject.Add(cell.ToString());
                                            }
                                            if (!tableByImprot.ListProject.Contains(cell.ToString()))
                                            {
                                                tableByImprot.ListProject.Add(cell.ToString());
                                            }

                                        }
                                        dataRow[headerRow[i]] = cell.ToString();
                                        isEmptyRow = true;
                                    }
                                    else
                                    {
                                        if (i != 0 && i != 8 && i != 9 && i != 10 && i != 11)
                                        {
                                            if (!tableByImprot.ListIndex.Contains((row.RowNum + 1).ToString()))
                                                tableByImprot.ListIndex.Add((row.RowNum + 1).ToString());
                                        }
                                        if (i == 0)
                                        {
                                            if (!tableByImprot.ListNumEmpty.Contains(row.RowNum.ToString()))
                                                tableByImprot.ListNumEmpty.Add((row.RowNum + 1).ToString());
                                        }
                                    }
                                }
                                if (isEmptyRow)
                                    tableByImprot.dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                }

                return tableByImprot;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //    fs.Dispose();
                //}
            }
        }
        public static List<T> ExcelToT<T>(IFormFile file) where T : new()
        {
            List<T> list = new List<T>();
            DataTable dataTable = new DataTable();
            ISheet sheet = null;
            var headerRow = new List<string>();
            try
            {
                var stream = file.ToByteArray();
                IWorkbook workbook = CreateWorkBook(stream);
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                    var rowIterator = sheet.GetRowEnumerator();
                    while (rowIterator.MoveNext())
                    {
                        var row = (IRow)rowIterator.Current;
                        if (row.RowNum == 0)
                        {
                            // 处理表头
                            for (int i = 0; i < row.LastCellNum; i++)
                            {
                                var cell = row.GetCell(i);
                                if (cell != null)
                                {
                                    headerRow.Add(cell.StringCellValue);
                                    dataTable.Columns.Add(cell.StringCellValue);
                                }
                            }
                        }
                        else
                        {
                            // 处理数据行
                            var dataRow = dataTable.NewRow();
                            for (int i = 0; i < row.LastCellNum; i++)
                            {
                                var cell = row.GetCell(i);
                                if (cell != null)
                                {
                                    dataRow[headerRow[i]] = cell.ToString();
                                }
                                T entity = new T();
                                PropertyInfo[] properties = entity.GetType().GetProperties();
                                foreach (PropertyInfo property in properties)
                                {
                                    property.SetValue(entity, Convert.ChangeType(cell, property.PropertyType));
                                }
                                list.Add(entity);
                            }
                            //dataTable.Rows.Add(dataRow);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //    fs.Dispose();
                //}
            }
        }
        /// <summary>
        /// 根据datatable的顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToList<T>(DataTable dataTable, string deptName ,string Serialno) where T : new()
        {

            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T entity = new T();
                PropertyInfo[] properties = entity.GetType().GetProperties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    var property = properties[i];
                    if (i <= row.ItemArray.Length - 1)
                    {
                        //if (property.Name == "SysBin")
                        //{
                        //    property.SetValue(entity, DateTime.Now);
                        //} 
                        if (property.Name == "PiNum"&& row.ItemArray[i].ToString()=="")
                        {
                            property.SetValue(entity, Serialno);
                        }
                        else
                        {
                            property.SetValue(entity, Convert.ChangeType(row.ItemArray[i], property.PropertyType));
                        }
                    }
                    else
                    {
                        if (property.Name == "CreateTime")
                        {
                            property.SetValue(entity, DateTime.Now);
                        }
                        if (property.Name == "SnState")
                        {
                            property.SetValue(entity, "已导入");
                        }
                        if (property.Name == "Creator")
                        {
                            property.SetValue(entity, Framework.Security.UserTokenService.GetUserToken().UserName);
                        }
                        if (property.Name == "Id")
                        {
                            property.SetValue(entity, Guid.NewGuid());
                        }
                        if (property.Name == "CreateDept")
                        {
                            property.SetValue(entity, deptName);
                        }

                    }
                }
                list.Add(entity);
            }


            return list;
        }
        /// <summary>
        /// 根据datatable的列明和数据库字段匹配生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTable<T>(DataTable dataTable) where T : new()
        {

            List<T> list = new();
            foreach (DataRow row in dataTable.Rows)
            {
                T entity = new T();
                PropertyInfo[] properties = entity.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                    {
                        property.SetValue(entity, Convert.ChangeType(row[property.Name], property.PropertyType));
                    }
                }
                list.Add(entity);
            }


            return list;
        }
        /// <summary>
        /// 辅料导入数据转换table
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static TableByImprot ExcelToDataTableBySecondary(IFormFile file)
        {
            TableByImprot tableByImprot = new();
            tableByImprot.dataTable = new DataTable();
            //DataColumn column = null;
            tableByImprot.ListIndex = new List<string>();
             ISheet sheet = null;
            //int startRow = headerRowIndex + 1;
            var headerRow = new List<string>();
            try
            {
                var stream = file.ToByteArray();
                IWorkbook workbook = CreateWorkBook(stream);
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                    var rowIterator = sheet.GetRowEnumerator();
                    while (rowIterator.MoveNext())
                    {
                        var row = (IRow)rowIterator.Current;
                        var isNotEmpty = row.Cells.Any(cell => cell != null && !string.IsNullOrEmpty(cell.ToString()));
                        if (isNotEmpty)
                        {
                            if (row.RowNum == 0)
                            {
                                // 处理表头
                                for (int i = 0; i < row.LastCellNum; i++)
                                {
                                    var cell = row.GetCell(i);
                                    if (cell != null)
                                    {
                                        headerRow.Add(cell.StringCellValue);
                                        tableByImprot.dataTable.Columns.Add(cell.StringCellValue);
                                    }
                                }
                            }
                            else
                            {
                                // 处理数据行
                                var dataRow = tableByImprot.dataTable.NewRow();
                                var isEmptyRow = false;
                                for (int i = 0; i < row.LastCellNum; i++)
                                {
                                    var cell = row.GetCell(i);
                                    if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                                    {
                                        dataRow[headerRow[i]] = cell.ToString();
                                        isEmptyRow = true;
                                    }
                                    else//不为空
                                    {
                                        if (i == 1 || i == 0)
                                        {
                                            if (!tableByImprot.ListIndex.Contains((row.RowNum + 1).ToString()))
                                                tableByImprot.ListIndex.Add((row.RowNum + 1).ToString());
                                        }
                                    }
                                }
                                if (isEmptyRow)
                                    tableByImprot.dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                }

                return tableByImprot;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //    fs.Dispose();
                //}
            }
        }
        /// <summary>
        /// 辅料导入table转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="Serialno"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableBySecondary<T>(DataTable dataTable,string deptName, string Serialno) where T : new()
        {

            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T entity = new T();
                PropertyInfo[] properties = entity.GetType().GetProperties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    var property = properties[i];
                    if (property.Name == "Name")
                    {
                        property.SetValue(entity, Serialno);
                    }
                    if (property.Name == "CreateTime")
                    {
                        property.SetValue(entity, DateTime.Now);
                    }
                    if (property.Name == "PnState")
                    {
                        property.SetValue(entity, "已导入");
                    }
                    if (property.Name == "CreateDept")
                    {
                        property.SetValue(entity, deptName);
                    }
                    if (property.Name == "Creator")
                    {
                        property.SetValue(entity, Framework.Security.UserTokenService.GetUserToken().UserName);
                    }
                    if (property.Name == "Id")
                    {
                        property.SetValue(entity, Guid.NewGuid());
                    }
                    if (property.Name == "SysPn")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[0], property.PropertyType));
                    }if (property.Name == "PnQty")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[1], property.PropertyType));
                    }if (property.Name == "SysBin")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[2], property.PropertyType));
                    }if (property.Name == "SysLocation")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[3], property.PropertyType));
                    }
                   
                }
                list.Add(entity);
            }
            return list;
        }
        ///<summary>
        ///辅料盘点数据转table
        /// </summary>
        public static List<T> ConvertInventoryDataTableBySecondary<T>(DataTable dataTable, string deptName) where T : new()
        {

            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T entity = new T();
                PropertyInfo[] properties = entity.GetType().GetProperties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    var property = properties[i];
                    if (property.Name == "CreateTime")
                    {
                        property.SetValue(entity, DateTime.Now);
                    }
                    if (property.Name == "PnState")
                    {
                        property.SetValue(entity, "已导入");
                    }
                    if (property.Name == "CreateDept")
                    {
                        property.SetValue(entity, deptName);
                    }
                    if (property.Name == "Creator")
                    {
                        property.SetValue(entity, Framework.Security.UserTokenService.GetUserToken().UserName);
                    }
                    if (property.Name == "Id")
                    {
                        property.SetValue(entity, Guid.NewGuid());
                    }
                    if (property.Name == "Name")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[0], property.PropertyType));
                    }
                    if (property.Name == "SysPn")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[1], property.PropertyType));
                    }
                    if (property.Name == "PnQty")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[2], property.PropertyType));
                    }
                    if (property.Name == "SysBin")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[3], property.PropertyType));
                    }
                    if (property.Name == "SysLocation")
                    {
                        property.SetValue(entity, Convert.ChangeType(row.ItemArray[4], property.PropertyType));
                    }

                }
                list.Add(entity);
            }
            return list;
        }
    }
}
