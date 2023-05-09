using AngleSharp.Dom;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AddressScanner;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

namespace TerritoryHelperClassLibrary.BaseServices.Excel;

public class ExcelBaseService
{
	public ExcelBaseService()
	{
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
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

        var range = ws.Cells["A1"].LoadFromCollection(formattedDataRecords, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        //Second Worksheet:  Address Import
        var ws2 = package.Workbook.Worksheets.Add("TableScrapedRecords");

        var range2 = ws2.Cells["A1"].LoadFromCollection(tableScrapedRecords, true);
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

        var range4 = ws4.Cells["A1"].LoadFromCollection(tableANDONLYDataRecords, true);
        range4.AutoFitColumns();

        // Formats the header
        ws4.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws4.Row(1).Style.Font.Size = 11;
        ws4.Row(1).Style.Font.Bold = true;

        //Fifth Worksheet:  Address Import
        var ws5 = package.Workbook.Worksheets.Add("FormattedNOTTabled");

        var range5 = ws5.Cells["A1"].LoadFromCollection(dataRecordsbutNOTTableRecords, true);
        range4.AutoFitColumns();

        // Formats the header
        ws5.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws5.Row(1).Style.Font.Size = 11;
        ws5.Row(1).Style.Font.Bold = true;


        await package.SaveAsync();
    }

    //AToZ Database File Information

    public async Task<List<AtoZDatabaseImportRecord>> LoadAtoZDatabaseImportExcelFile(FileInfo file)
    {
        List<AtoZDatabaseImportRecord> importedRecords = new();

        CultureInfo provider = new CultureInfo("en-US");

        using var package = new ExcelPackage(file);

        await package.LoadAsync(file);

        var ws = package.Workbook.Worksheets[0];

        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
        {
            AtoZDatabaseImportRecord p = new();
            p.Source = ws.Cells[row, col].Value.ToString();
            p.Date = DateTime.Parse(ws.Cells[row, col + 1].Value.ToString(), provider, DateTimeStyles.AdjustToUniversal);
            p.ObsolescenceDate = DateTime.Parse(ws.Cells[row, col + 2].Value.ToString(), provider, DateTimeStyles.AdjustToUniversal);
            p.FirstName = ws.Cells[row, col + 3].Value.ToString();
            p.MiddleInitial = (ws.Cells[row, col + 4].Value ?? "").ToString();
            p.LastName = ws.Cells[row, col + 5].Value.ToString();
            p.PhysicalAddress = ws.Cells[row, col + 6].Value.ToString();
            p.PhysicalCity = ws.Cells[row, col + 7].Value.ToString();
            p.PhysicalState = ws.Cells[row, col + 8].Value.ToString();
            p.PhysicalZIP = ws.Cells[row, col + 9].Value.ToString();
            p.PhysicalZIP4 = ws.Cells[row, col + 10].Value.ToString();
            p.Phone = (ws.Cells[row, col + 11].Value ?? "").ToString();
            p.MetroArea = ws.Cells[row, col + 12].Value.ToString();
            p.County = ws.Cells[row, col + 13].Value.ToString();
            p.LengthofResidency = (ws.Cells[row, col + 15].Value ?? "").ToString();
            p.Latitude = decimal.Parse(ws.Cells[row, col + 20].Value.ToString());
            p.Longitude = decimal.Parse(ws.Cells[row, col + 21].Value.ToString());
            importedRecords.Add(p);
            row += 1;
        }

        return importedRecords;
    }

    public async Task<List<AddressMasterRecord>> LoadExistingSpanishAddreessesExcelFile(FileInfo file)
    {
        List<AddressMasterRecord> existingSpanishAddressList = new();

        CultureInfo provider = new CultureInfo("en-US");

        using var package = new ExcelPackage(file);

        await package.LoadAsync(file);

        var ws = package.Workbook.Worksheets[0];

        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
        {
            AddressMasterRecord p = new();
            p.Id = Guid.NewGuid();
            p.Order = row - 1;
            p.TerritoryType = (ws.Cells[row, col].Value ?? "").ToString();
            p.TerritoryNumber = (ws.Cells[row, col + 1].Value ?? "").ToString();
            p.LocationType = (ws.Cells[row, col + 2].Value ?? "").ToString();
            p.Status = (ws.Cells[row, col + 3].Value ?? "").ToString();
            p.Latitude = decimal.Parse((ws.Cells[row, col + 4].Value ?? "").ToString());
            p.Longitude = decimal.Parse((ws.Cells[row, col + 5].Value ?? "").ToString());
            p.CompleteAddress = (ws.Cells[row, col + 6].Value ?? "").ToString();
            p.Number = (ws.Cells[row, col + 7].Value ?? "").ToString();
            p.Street = (ws.Cells[row, col + 8].Value ?? "").ToString();
            p.Apartment = (ws.Cells[row, col + 9].Value ?? "").ToString();
            p.Floor = (ws.Cells[row, col + 10].Value ?? "").ToString();
            p.City = (ws.Cells[row, col + 11].Value ?? "").ToString();
            p.County = (ws.Cells[row, col + 12].Value ?? "").ToString();
            p.PostalCode = (ws.Cells[row, col + 13].Value ?? "").ToString();
            p.State = (ws.Cells[row, col + 14].Value ?? "").ToString();
            p.CountryCode = (ws.Cells[row, col + 15].Value ?? "").ToString();
            p.DateUpdated = DateTime.Now;

            existingSpanishAddressList.Add(p);
            row += 1;
        }

        return existingSpanishAddressList;

    }

    public async Task SaveExcelMasterFile(List<AddressMasterRecord> importObjectList, List<TerritoryHelperAddress> territoryHelperAddressList, FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //Cover Page Spreadsheet
        var ws0 = package.Workbook.Worksheets.Add("Cover Page");

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AllRecords");

        var range = ws.Cells["A1"].LoadFromCollection(importObjectList, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        //Second Worksheet:  Address Import

        var ws2 = package.Workbook.Worksheets.Add("ImportRecords");

        var range2 = ws2.Cells["A1"].LoadFromCollection(territoryHelperAddressList, true);
        range2.AutoFitColumns();

        // Formats the header
        ws2.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws2.Row(1).Style.Font.Size = 11;
        ws2.Row(1).Style.Font.Bold = true;

        //All other Worksheets: Notes per Territory

        var territoryNoteRecordsList = CreateRecordNotesList(importObjectList);

        foreach (var territoryNumber in territoryNoteRecordsList)
        {

            var ws3 = package.Workbook.Worksheets.Add(territoryNumber.TerritoryFullTypeNumber);


            //Header Text
            ws3.Cells["A1"].Value = "Dirección";
            ws3.Cells["B1"].Value = "Nombres";
            ws3.Cells["C1"].Value = "Número Telefónico";
            ws3.Cells["D1"].Value = "Notas";
            ws3.Cells["E1"].Value = "ConfirmedAddress";

            // Formats the header
            ws3.Row(1).Style.Font.Size = 11;
            ws3.Row(1).Style.Font.Bold = true;

            //Insert Data in Rows
            int row = 2;
            int col = 1;

            foreach (var territoryRecord in territoryNumber.AllIndividualTerritoryRecords)
            {
                ws3.Cells[row, col].Value = territoryRecord.Address;
                ws3.Cells[row, col + 1].Value = territoryRecord.Names;
                ws3.Cells[row, col + 2].Value = territoryRecord.TelephoneNumbers;
                ws3.Cells[row, col + 3].Value = territoryRecord.Notes;
                ws3.Cells[row, col + 4].Value = territoryRecord.DPVConfirmation;

                row += 1;
            }

            //Find out length of list
            int territoryRecordLength = territoryNumber.AllIndividualTerritoryRecords.Count;
            int territoryRecordTable = territoryRecordLength + 1;
            string indexedTableValue = $"D{territoryRecordTable}";

            //select table
            var modelTable = ws3.Cells[$"A1:{indexedTableValue}"];

            // Assign borders
            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //modelTable.AutoFitColumns();

            //Set Column Width
            ws3.Column(1).Width = 20;
            ws3.Column(2).Width = 20;
            ws3.Column(3).Width = 20;
            ws3.Column(4).Width = 20;
            ws3.Column(5).Width = 20;

            //Set row height for table
            int numberOfRows = territoryNumber.AllIndividualTerritoryRecords.Count;
            for (int i = 2; i < numberOfRows + 2; i++)
            {
                ws3.Row(i).Height = 60;
            }


            //Wrap Text
            modelTable.Style.WrapText = true;

        }

        await package.SaveAsync();
    }

    private List<TerritoryNoteRecordsList> CreateRecordNotesList(List<AddressMasterRecord> masterRecordList)
    {
        var distinctTerritories = masterRecordList.Select(x => x.TerritoryNumber).Distinct().ToList();

        List<int> territoryOrder = new();
        foreach (var territoryNumber in distinctTerritories)
        {
            var territoryAsInt = int.Parse(territoryNumber);
            territoryOrder.Add(territoryAsInt);
        }

        territoryOrder.Sort();


        distinctTerritories = new();

        foreach (var territoryNumber in territoryOrder)
        {
            var territoryString = territoryNumber.ToString();
            distinctTerritories.Add(territoryString);
        }

        var result = new List<TerritoryNoteRecordsList>();

        foreach (var territory in distinctTerritories)
        {
            var territoryRecordList = new TerritoryNoteRecordsList();

            var territoryType = masterRecordList.Where(x => x.TerritoryNumber == territory).Select(x => x.TerritoryType).FirstOrDefault();

            territoryRecordList.TerritoryFullTypeNumber = $"{territoryType}{territory}";
            territoryRecordList.TerritorySortNumber = int.Parse(territory);
            territoryRecordList.TerritorySortType = territoryType;

            foreach (var address in masterRecordList)
            {
                if (address.TerritoryNumber == territory)
                {
                    var territoryNoteRecord = new TerritoryNoteRecord
                    {
                        Address = address.CompleteAddress,
                        Names = address.NamesList,
                        TelephoneNumbers = address.PhoneNumbers,
                        Notes = "",
                        DPVConfirmation = address.DPVConfirmation
                    };

                    territoryRecordList.AllIndividualTerritoryRecords.Add(territoryNoteRecord);
                }
            }

            result.Add(territoryRecordList);
        }

        result = result.OrderBy(o => o.TerritorySortType).ThenBy(o => o.TerritorySortNumber).ToList();

        return result;
    }

    public async Task<List<string>> LoadSpanishLastNames(FileInfo file)
    {
        List<string> spanishLastNamesList = new();

        using var package = new ExcelPackage(file);

        await package.LoadAsync(file);

        var ws = package.Workbook.Worksheets[0];

        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
        {
            string lastName = ws.Cells[row, col].Value.ToString();
            spanishLastNamesList.Add(lastName);
            row += 1;
        }

        return spanishLastNamesList;
    }

    public async Task<List<AddressMasterRecord>> LoadEditedAndUpdatedMasterFileForTerritoryHelperImport(FileInfo file)
    {
        List<AddressMasterRecord> existingSpanishAddressList = new();

        CultureInfo provider = new CultureInfo("en-US");

        using var package = new ExcelPackage(file);

        await package.LoadAsync(file);

        var ws = package.Workbook.Worksheets[0];

        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
        {
            AddressMasterRecord p = new();
            //Order Information
            p.Id = new Guid((ws.Cells[row, col].Value ?? "").ToString());
            p.Order = int.Parse((ws.Cells[row, col+1].Value ?? "0").ToString());

            //Address Notes Information
            p.PhoneNumbers = (ws.Cells[row, col + 2].Value ?? "").ToString();
            p.NamesList= (ws.Cells[row, col + 3].Value ?? "").ToString();
            p.Notes= (ws.Cells[row, col + 4].Value ?? "").ToString();

            //Address Verification Information
            p.DeliveryPoint= (ws.Cells[row, col + 5].Value ?? "").ToString();
            p.CarrierRoute= (ws.Cells[row, col + 6].Value ?? "").ToString();
            p.DPVConfirmation= (ws.Cells[row, col + 7].Value ?? "").ToString();
            p.DPVCMRA= (ws.Cells[row, col + 8].Value ?? "").ToString();
            p.DPVFootnotes= (ws.Cells[row, col + 9].Value ?? "").ToString();
            p.Business= (ws.Cells[row, col + 10].Value ?? "").ToString();
            p.CentralDeliveryPoint= (ws.Cells[row, col + 11].Value ?? "").ToString();
            p.Vacant= (ws.Cells[row, col + 12].Value ?? "").ToString();
            p.HttpError= (ws.Cells[row, col + 13].Value ?? "").ToString();

            //Territory Notes Information
            p.TerritoryName= (ws.Cells[row, col + 14].Value ?? "").ToString();
            p.TerritorySpecialNotes= (ws.Cells[row, col + 15].Value ?? "").ToString();
            p.DateUpdated= DateTime.FromOADate(double.Parse((ws.Cells[row, col + 16].Value ?? "0").ToString()));
            p.UniqueIdentifierCreation= (ws.Cells[row, col + 17].Value ?? "").ToString();

            //Territory Helper Specific Information
            p.TerritoryType = (ws.Cells[row, col+18].Value ?? "").ToString();
            p.TerritoryNumber = (ws.Cells[row, col + 19].Value ?? "").ToString();
            p.LocationType = (ws.Cells[row, col + 20].Value ?? "").ToString();
            p.Status = (ws.Cells[row, col + 21].Value ?? "").ToString();
            p.Latitude = decimal.Parse((ws.Cells[row, col + 22].Value ?? "").ToString());
            p.Longitude = decimal.Parse((ws.Cells[row, col + 23].Value ?? "").ToString());
            p.CompleteAddress = (ws.Cells[row, col + 24].Value ?? "").ToString();
            p.Number = (ws.Cells[row, col + 25].Value ?? "").ToString();
            p.Street = (ws.Cells[row, col + 26].Value ?? "").ToString();
            p.Apartment = (ws.Cells[row, col + 27].Value ?? "").ToString();
            p.Floor = (ws.Cells[row, col + 28].Value ?? "").ToString();
            p.City = (ws.Cells[row, col + 29].Value ?? "").ToString();
            p.County = (ws.Cells[row, col + 30].Value ?? "").ToString();
            p.PostalCode = (ws.Cells[row, col + 31].Value ?? "").ToString();
            p.State = (ws.Cells[row, col + 32].Value ?? "").ToString();
            p.CountryCode = (ws.Cells[row, col + 33].Value ?? "").ToString();

            //Master Record Order
            //p.MasterRecordRoutePlanOrder= int.Parse((ws.Cells[row, col + 23].Value ?? "").ToString());

            existingSpanishAddressList.Add(p);
            row += 1;
        }

        return existingSpanishAddressList;
    }

    public async Task ExportMasterListToExcel(List<AddressMasterRecord> importObjectList, FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AllRecords");

        var range = ws.Cells["A1"].LoadFromCollection(importObjectList, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        await package.SaveAsync();
    }

    public async Task ExportAddressMasterRecordToExcel(List<AddressMasterRecord> importObjectList, List<AddressMasterRecord> excludedObjectList,FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AddedRecords");

        var range = ws.Cells["A1"].LoadFromCollection(importObjectList, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        //First Worksheet: All Records
        var ws1 = package.Workbook.Worksheets.Add("ExcludedRecords");

        var range1 = ws1.Cells["A1"].LoadFromCollection(excludedObjectList, true);
        range1.AutoFitColumns();

        // Formats the header
        ws1.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws1.Row(1).Style.Font.Size = 11;
        ws1.Row(1).Style.Font.Bold = true;

        await package.SaveAsync();
    }

    public async Task ExportAnyListToExcel<T>(List<T> exportedList, FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AllRecords");

        var range = ws.Cells["A1"].LoadFromCollection(exportedList, true);
        range.AutoFitColumns();

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        await package.SaveAsync();

    }

    //Temporary Complete Address Error Export flattened file
    public async Task ExportCompleteAddressErrorListToExcel(List<CompleteAddressError> exportedAddressErrorList, FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }

        using var package = new ExcelPackage(file);

        //First Worksheet: All Records
        var ws = package.Workbook.Worksheets.Add("AllRecords");

        // Formats the header
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Row(1).Style.Font.Size = 11;
        ws.Row(1).Style.Font.Bold = true;

        //Write Table
        int row = 2;
        int col = 1;

        //Write Header Names
        ws.Cells[1, col].Value = nameof(CompleteAddressError.TerritoryType);
        ws.Cells[1, col + 2].Value = nameof(CompleteAddressError.TerritoryNumber);
        ws.Cells[1, col + 3].Value = nameof(CompleteAddressError.LocationType);
        ws.Cells[1, col + 4].Value = nameof(CompleteAddressError.Status);
        ws.Cells[1, col + 5].Value = nameof(CompleteAddressError.Latitude);
        ws.Cells[1, col + 6].Value = nameof(CompleteAddressError.Longitude);
        ws.Cells[1, col + 7].Value = nameof(CompleteAddressError.CompleteAddress);
        ws.Cells[1, col + 8].Value = nameof(CompleteAddressError.Number);
        ws.Cells[1, col + 9].Value = nameof(CompleteAddressError.Street);
        ws.Cells[1, col + 10].Value = nameof(CompleteAddressError.Apartment);
        ws.Cells[1, col + 11].Value = nameof(CompleteAddressError.Floor);
        ws.Cells[1, col + 12].Value = nameof(CompleteAddressError.City);
        ws.Cells[1, col + 13].Value = nameof(CompleteAddressError.County);
        ws.Cells[1, col + 14].Value = nameof(CompleteAddressError.PostalCode);
        ws.Cells[1, col + 15].Value = nameof(CompleteAddressError.State);
        ws.Cells[1, col + 16].Value = nameof(CompleteAddressError.CountryCode);
        ws.Cells[1, col + 17].Value = nameof(CompleteAddressError.HasError);
        ws.Cells[1, col + 18].Value = nameof(AddressErrorRecord.AddressErrorId);
        ws.Cells[1, col + 19].Value = nameof(AddressErrorRecord.AddressErrorSeverity);
        ws.Cells[1, col + 20].Value = nameof(AddressErrorRecord.AddressErrorTitle);
        ws.Cells[1, col + 21].Value = nameof(AddressErrorRecord.AddressErrorMessage);


        foreach (var address in exportedAddressErrorList)
        {
            foreach(var error in address.AddressErrorRecords)
            {
                ws.Cells[row,col].Value= address.TerritoryType;
                ws.Cells[row,col+2].Value= address.TerritoryNumber;
                ws.Cells[row,col+3].Value= address.LocationType;
                ws.Cells[row,col+4].Value= address.Status;
                ws.Cells[row,col+5].Value= address.Latitude;
                ws.Cells[row,col+6].Value= address.Longitude;
                ws.Cells[row,col+7].Value= address.CompleteAddress;
                ws.Cells[row,col+8].Value= address.Number;
                ws.Cells[row,col+9].Value= address.Street;
                ws.Cells[row,col+10].Value= address.Apartment;
                ws.Cells[row,col+11].Value= address.Floor;
                ws.Cells[row,col+12].Value= address.City;
                ws.Cells[row,col+13].Value= address.County;
                ws.Cells[row,col+14].Value= address.PostalCode;
                ws.Cells[row,col+15].Value= address.State;
                ws.Cells[row,col+16].Value= address.CountryCode;
                ws.Cells[row,col+17].Value = address.HasError;
                ws.Cells[row,col+18].Value = error.AddressErrorId;
                ws.Cells[row,col+19].Value = error.AddressErrorSeverity;
                ws.Cells[row,col+20].Value = error.AddressErrorTitle;
                ws.Cells[row,col+21].Value = error.AddressErrorMessage;
                row++;
            }
        }

        await package.SaveAsync();
    }
}
