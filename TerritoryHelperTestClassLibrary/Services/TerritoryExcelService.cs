using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperTestClassLibrary.Models;

namespace TerritoryHelperTestClassLibrary.Services;

public class TerritoryExcelService
{
	public TerritoryExcelService()
	{
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
    }

    public async Task SaveTerritoryInformationExcelFile(FileInfo fileInfo, List<TerritoryInformation> territoryInformation)
    {
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        using var package = new ExcelPackage(fileInfo);

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AllRecords");

        var range = ws.Cells["A2"].LoadFromCollection(territoryInformation, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        await package.SaveAsync();
    }
}
