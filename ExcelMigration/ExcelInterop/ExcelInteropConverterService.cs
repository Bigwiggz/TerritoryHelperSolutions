using ExcelMigration.Models.UtilityModels;
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
        //Field
        public ExcelProgressReportModel lowerReport;
        public ExcelTopReportModel report;

        public ExcelInteropConverterService()
        {
            lowerReport = new ExcelProgressReportModel();
            report = new ExcelTopReportModel();
        }

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

        public void ConverterExcelService(string oldFileFormatPath, string newFileFormatPath, IProgress<ExcelTopReportModel> progress, IProgress<ExcelProgressReportModel> lowerProgress)
        {
            //Report
            report.ExcelTopLevelProgressMessage = "Converting xls files to xlsx (This may take some time)...";
            report.ExcelTopLevelPercentComplete = 21;
            progress.Report(report);
            var excelConverterService = new ExcelInteropConverterService();

            var allFiles = new DirectoryInfo(oldFileFormatPath);

            var allFilesInfo = allFiles.GetFiles();

            int i = 1;
            foreach (var file in allFilesInfo)
            {
                //Report
                lowerReport.ExcelProcessPercentComplete = i * 100 / allFilesInfo.Length;
                lowerReport.ExcelProcessMessage = $"Processing file {file.Name}....{i} of {allFilesInfo.Length}";
                lowerProgress.Report(lowerReport);

                if (file.Extension == ".xls" && allFilesInfo.Length > 0)
                {

                    excelConverterService.ConvertXLStoXLSX(file, newFileFormatPath);
                }
                i++;
            }
            
        }
    }
}
