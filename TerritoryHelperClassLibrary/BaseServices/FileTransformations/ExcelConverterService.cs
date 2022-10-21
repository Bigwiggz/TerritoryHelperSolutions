using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.BaseServices.FileTransformations;

public class ExcelConverterService
{
    public string FreeSpireConvertXLStoXLSX(FileInfo file, string newFilePath)
    {
        Workbook workbook=new Workbook();
        workbook.LoadFromFile(file.FullName);
        var xlsxFileName = file.Name + "x";
        var fullxlsxFile = Path.Combine(newFilePath, xlsxFileName);
        workbook.SaveToFile(fullxlsxFile, ExcelVersion.Version2016);
        return fullxlsxFile;
    }
}
