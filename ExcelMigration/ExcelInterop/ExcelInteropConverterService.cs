using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExcelMigration.ExcelInterop
{
    public class ExcelInteropConverterService
    {

        private string ConvertXLStoXLSX(FileInfo file, string newFilePath)
        {
            var app = new Microsoft.Office.Interop.Excel.Application();
            var xlsFile = file.FullName;
            var wb = app.Workbooks.Open(xlsFile);
            var xlsxFile = file.Name + "x";
            var fullxlsxFile = Path.Combine(newFilePath, xlsxFile);
            wb.SaveAs(Filename: fullxlsxFile, FileFormat: XlFileFormat.xlOpenXMLWorkbook);
            wb.Close();
            app.Quit();
            return fullxlsxFile;
        }

        public void ConverterExcelService(string oldFileFormatPath, string newFileFormatPath)
        {

            var excelConverterService = new ExcelInteropConverterService();

            var allFiles = new DirectoryInfo(oldFileFormatPath);

            var allFilesInfo = allFiles.GetFiles();

            foreach (var file in allFilesInfo)
            {
                if (file.Extension == ".xls" && allFilesInfo.Length > 0)
                {

                    excelConverterService.ConvertXLStoXLSX(file, newFileFormatPath);
                }
            }
            
        }
    }
}
