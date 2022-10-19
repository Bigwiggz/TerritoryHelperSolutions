using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;

namespace TerritoryHelperClassLibrary.BaseServices.Excel;

public class ExcelBaseService
{
	public ExcelBaseService()
	{
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
    }

    public async Task<List<AddressMasterRecord>> LoadTerritoryHelperAddresses(FileInfo fileInfo)
    {
        List<AddressMasterRecord> territoryHelperAddressList = new List<AddressMasterRecord>();

        CultureInfo provider= new CultureInfo("en-US");

        using var package= new ExcelPackage(fileInfo);

        await package.LoadAsync(fileInfo);

        var ws = package.Workbook.Worksheets[0];
        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
        {
            AddressMasterRecord p = new();
            p.TerritoryType = ws.Cells[row,col].Value.ToString();
            p.TerritoryNumber = ws.Cells[row, col + 1].Value.ToString();
            p.LocationType = ws.Cells[row, col + 2].Value.ToString();
            p.Status = ws.Cells[row, col + 3].Value.ToString();
            p.Latitude = decimal.Parse((ws.Cells[row, col + 4].Value??"0").ToString());
            p.Longitude = decimal.Parse((ws.Cells[row, col + 5].Value ?? "0").ToString());
            p.CompleteAddress = ws.Cells[row, col + 6].Value.ToString();
            p.Number = ws.Cells[row, col + 7].Value.ToString();
            p.Street = ws.Cells[row, col + 8].Value.ToString();
            p.Apartment = (ws.Cells[row, col + 9].Value ?? "").ToString();
            p.Floor = (ws.Cells[row, col + 10].Value ?? "").ToString();
            p.City = ws.Cells[row, col + 11].Value.ToString();
            p.County = ws.Cells[row, col + 12].Value.ToString();
            p.PostalCode = ws.Cells[row, col + 15].Value.ToString();
            p.State = ws.Cells[row, col + 13].Value.ToString();
            p.CountryCode = ws.Cells[row, col + 14].Value.ToString();

            territoryHelperAddressList.Add(p);
            row += 1;
        }

        return territoryHelperAddressList;
    }

    public async Task SaveExcelFileReconciliation(
            List<AddressMasterRecord> formattedDataRecords,
            List<AddressMasterRecord> tableScrapedRecords,
            List<AddressMasterRecord> tableButNOTDataRecords,
            List<AddressMasterRecord> tableANDONLYDataRecords,
            List<AddressMasterRecord> dataRecordsbutNOTTableRecords,
            FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //Cover Page Spreadsheet
        var ws0 = package.Workbook.Worksheets.Add("Cover Page");

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("FormattedDataRecords");

        var range = ws.Cells["A2"].LoadFromCollection(formattedDataRecords, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        //Second Worksheet:  Address Import
        var ws2 = package.Workbook.Worksheets.Add("TableScrapedRecords");

        var range2 = ws2.Cells["A2"].LoadFromCollection(tableScrapedRecords, true);
        range2.AutoFitColumns();

        // Formats the header
        ws2.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws2.Row(1).Style.Font.Size = 11;
        ws2.Row(1).Style.Font.Bold = true;

        //Third Worksheet:  Address Import
        var ws3 = package.Workbook.Worksheets.Add("TableNOTFormatted");

        var range3 = ws3.Cells["A2"].LoadFromCollection(tableButNOTDataRecords, true);
        range2.AutoFitColumns();

        // Formats the header
        ws3.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws3.Row(1).Style.Font.Size = 11;
        ws3.Row(1).Style.Font.Bold = true;

        //Fourth Worksheet:  Address Import
        var ws4 = package.Workbook.Worksheets.Add("TableANDFormatted");

        var range4 = ws4.Cells["A2"].LoadFromCollection(tableANDONLYDataRecords, true);
        range4.AutoFitColumns();

        // Formats the header
        ws4.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws4.Row(1).Style.Font.Size = 11;
        ws4.Row(1).Style.Font.Bold = true;

        //Fifth Worksheet:  Address Import
        var ws5 = package.Workbook.Worksheets.Add("FormattedNOTTabled");

        var range5 = ws5.Cells["A2"].LoadFromCollection(dataRecordsbutNOTTableRecords, true);
        range4.AutoFitColumns();

        // Formats the header
        ws5.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws5.Row(1).Style.Font.Size = 11;
        ws5.Row(1).Style.Font.Bold = true;


        await package.SaveAsync();
    }
}
