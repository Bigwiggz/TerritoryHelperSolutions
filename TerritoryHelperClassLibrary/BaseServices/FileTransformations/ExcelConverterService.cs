using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.BaseServices.FileTransformations;

public class ExcelConverterService
{
    public string ConvertXLStoXLSX(FileInfo file, string newFilePath)
    {
        var app = new Microsoft.Office.Interop.Excel.Application();
        var xlsFile = file.FullName;
        var wb = app.Workbooks.Open(xlsFile);
        var xlsxFileName = file.Name + "x";
        var fullxlsxFile = Path.Combine(newFilePath, xlsxFileName);
        wb.SaveAs(Filename: fullxlsxFile, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
        wb.Close();
        app.Quit();
        return fullxlsxFile;
    }
}
