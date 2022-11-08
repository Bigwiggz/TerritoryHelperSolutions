using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.Excel;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels.GeoJSONExport;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperClassLibrary.BaseServices.GeoMapping;

public class GeoFileProcessing
{
    public FileInfo[] GetFiles(string fileDirectoryPath)
    {

        var allFiles = new DirectoryInfo(fileDirectoryPath);

        var allFilesInfo = allFiles.GetFiles();

        return allFilesInfo;
    }

    public void AddIdEnumeration(List<AtoZDatabaseImportRecord> recordList)
    {
        int idNumber = 1;
        foreach (var item in recordList)
        {
            item.Id = idNumber;
            item.UniqueLocationIndex = $"{item.PhysicalAddress}, {item.PhysicalCity}, {item.PhysicalState} {item.PhysicalZIP}, United States";
            item.FullName = $"{item.FirstName} {item.LastName}";
            item.IsSpanish = false;
            item.Phone = item.Phone.Replace("N/A", "");
            idNumber++;
        }
    }

    public void AddIsSpanish(List<AtoZDatabaseImportRecord> recordList, List<string> spanishLastNames)
    {
        foreach (var record in recordList)
        {
            foreach (var lastName in spanishLastNames)
            {
                if (record.LastName == lastName)
                {
                    record.IsSpanish = true;
                }
            }
        }
    }

    public async Task<List<AtoZDatabaseImportRecord>> AggregateListOfAllRecordsPerFile(FileInfo[] allRecordFiles)
    {
        var completeRecordList = new List<AtoZDatabaseImportRecord>();

        var excelServices = new ExcelBaseService();

        foreach (var file in allRecordFiles)
        {
            if (file.Exists)
            {
                var importedRecords = await excelServices.LoadAtoZDatabaseImportExcelFile(file);
                completeRecordList.AddRange(importedRecords);
            }
        }

        return completeRecordList;
    }
    public List<string> FilterOnSpanishNamesOnly(List<AtoZDatabaseImportRecord> recordsList)
    {
        List<string> distinctAddressList = recordsList.Where(x => x.IsSpanish == true).Select(x => x.UniqueLocationIndex).Distinct().ToList();

        return distinctAddressList;
    }

    public List<MasterRecord> FilterTerritoriesbyBoundary(List<MasterRecord> masterRecordsList, string territoryBoundaryListPath)
    {
        var territoryBoundaryText = File.ReadAllText(territoryBoundaryListPath);
        var congregationTerritoryBoundary = JsonSerializer.Deserialize<CongregationTerritoryBoundary>(territoryBoundaryText);

        var GeoFileProcessing = new GeoServices();

        var geometryStringBoundary = GeoFileProcessing.ConvertObjecttoJSONString(congregationTerritoryBoundary.features[0].geometry);

        var geometryBoundary = GeoFileProcessing.ConvertGeoJSONstringToGeometry(geometryStringBoundary);

        List<MasterRecord> filteredTerritoryBoundaryList = new();

        foreach (var record in masterRecordsList)
        {
            bool isInTerritoryCheck = GeoFileProcessing.IsLocationInTerritory(geometryBoundary, record.Latitude, record.Longitude);
            if (isInTerritoryCheck == true)
            {
                filteredTerritoryBoundaryList.Add(record);
            }
        }

        return filteredTerritoryBoundaryList;
    }

    public void FindTerritoryLocationPerAddress(List<MasterRecord> masterRecordsList, string congregationTerritoriesListPath)
    {
        var congregationTerritoriesText = File.ReadAllText(congregationTerritoriesListPath);
        var congregationTerritoriesList = JsonSerializer.Deserialize<CongregationTerritories>(congregationTerritoriesText);

        var GeoFileProcessing = new GeoServices();

        foreach (var territory in congregationTerritoriesList.features)
        {
            var congGeometryObject = GeoFileProcessing.ConvertObjecttoJSONString(territory.geometry);
            territory.properties.TerritoryGeometry = GeoFileProcessing.ConvertGeoJSONstringToGeometry(congGeometryObject);
        }

        foreach (var record in masterRecordsList)
        {
            foreach (var territory in congregationTerritoriesList.features)
            {
                bool isInTerritory = GeoFileProcessing.IsLocationInTerritory(territory.properties.TerritoryGeometry, record.Latitude, record.Longitude);
                if (isInTerritory == true)
                {
                    record.TerritoryNumber = territory.properties.TerritoryNumber;
                    record.TerritoryType = territory.properties.TerritoryType;
                }
            }
        }
    }

    public async Task<List<string>> FilterOnlyNewSpanishAddresses(List<string> distinctAddressList, FileInfo spanishExistingAddressesFile)
    {
        var excelFileProcessing = new ExcelBaseService();

        var spanishExistingRecords = await excelFileProcessing.LoadExistingSpanishAddreessesExcelFile(spanishExistingAddressesFile);

        var spanishExistingAddressesList = spanishExistingRecords.Select(x => x.CompleteAddress).ToList();

        List<string> resultList = distinctAddressList.Except(spanishExistingAddressesList).ToList();

        return resultList;
    }

    public List<MasterRecord> CreateMasterRecordsList(List<AtoZDatabaseImportRecord> recordsList, List<string> distinctAddressList)
    {

        var combinedPhoneNumbersList = CreateCombinedPhoneNumbersList(recordsList, distinctAddressList);

        var combinedNamesList = CreateCombinedNamesList(recordsList, distinctAddressList);

        var combinedAddressList = CreateCombinedAddressLocationList(recordsList, distinctAddressList);

        var masterRecordList = new List<MasterRecord>();

        int idNumber = 1;

        foreach (var record in combinedAddressList)
        {
            var masterRecord = new MasterRecord
            {
                Id = idNumber,
                City = record.City,
                CountryCode = record.CountryCode,
                County = record.County,
                Floor = record.Floor,
                Latitude = record.Latitude,
                Longitude = record.Longitude,
                LocationType = record.LocationType,
                Number = record.Number,
                State = record.State,
                StatusCode = record.StatusCode,
                Street = record.Street,
                UniqueLocationIndex = record.UniqueLocationIndex,
                Unit = record.Unit,
                ZipCode = record.ZipCode,
                IsSpanish = combinedNamesList.Where(x => x.UniqueLocationIndex == record.UniqueLocationIndex).Select(x => x.IsSpanish).FirstOrDefault(),
                NamesList = combinedNamesList.Where(x => x.UniqueLocationIndex == record.UniqueLocationIndex).Select(x => x.NamesList).FirstOrDefault(),
                PhoneNumbers = combinedPhoneNumbersList.Where(x => x.UniqueLocationIndex == record.UniqueLocationIndex).Select(x => x.PhoneNumberList).FirstOrDefault()
            };

            masterRecordList.Add(masterRecord);

            idNumber++;
        }

        return masterRecordList;
    }

    private List<CombinedNames> CreateCombinedNamesList(List<AtoZDatabaseImportRecord> recordsList, List<string> distinctAddressesList)
    {
        List<CombinedNames> combinedNamesList = new();

        var sb = new StringBuilder();

        var isNameSpanish = false;

        foreach (var address in distinctAddressesList)
        {
            var combinedNameRecord = new CombinedNames();

            combinedNameRecord.UniqueLocationIndex = address;

            foreach (var record in recordsList)
            {
                if (record.IsSpanish == true)
                {
                    isNameSpanish = true;
                }
                if (address == record.UniqueLocationIndex)
                {
                    sb.Append($"{record.FullName}, ");
                }
            }

            combinedNameRecord.IsSpanish = isNameSpanish;
            if (sb.Length != 0)
            {
                sb = sb.Remove(sb.Length - 2, 2);
                combinedNameRecord.NamesList = sb.ToString();
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }


            combinedNamesList.Add(combinedNameRecord);
        }

        return combinedNamesList;
    }

    private List<CombinedPhoneNumbers> CreateCombinedPhoneNumbersList(List<AtoZDatabaseImportRecord> recordsList, List<string> distinctAddressesList)
    {
        List<CombinedPhoneNumbers> combinedPhoneNumbersList = new();

        var sb = new StringBuilder();

        foreach (var address in distinctAddressesList)
        {
            var combinedPhoneNumberRecord = new CombinedPhoneNumbers();

            combinedPhoneNumberRecord.UniqueLocationIndex = address;
            foreach (var record in recordsList)
            {
                if (address == record.UniqueLocationIndex && record.Phone != "")
                {
                    sb.Append($"{record.Phone}, ");
                }

            }

            if (sb.Length != 0)
            {
                sb = sb.Remove(sb.Length - 2, 2);
                combinedPhoneNumberRecord.PhoneNumberList = sb.ToString();
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }


            combinedPhoneNumbersList.Add(combinedPhoneNumberRecord);
        }

        return combinedPhoneNumbersList;
    }

    private List<CombinedAddresses> CreateCombinedAddressLocationList(List<AtoZDatabaseImportRecord> recordsList, List<string> distinctAddressList)
    {

        List<CombinedAddresses> combinedAddressList = new();

        foreach (var address in distinctAddressList)
        {
            var combinedAddressRecord = new CombinedAddresses
            {
                UniqueLocationIndex = address,
                StatusCode = "Unknown",
                CountryCode = "US",
            };

            foreach (var record in recordsList)
            {
                if (address == record.UniqueLocationIndex)
                {
                    combinedAddressRecord.Latitude = record.Latitude;
                    combinedAddressRecord.Longitude = record.Longitude;
                    combinedAddressRecord.State = record.PhysicalState;
                    combinedAddressRecord.City = record.PhysicalCity;
                    combinedAddressRecord.County = record.County;
                    combinedAddressRecord.Floor = "";
                    combinedAddressRecord.ZipCode = record.PhysicalZIP;
                    combinedAddressRecord.Unit = "";

                    if (record.PhysicalAddress.Contains("Apt"))
                    {
                        combinedAddressRecord.LocationType = "Apartment";
                        combinedAddressRecord.Street = record.PhysicalAddress.Split("Apt")[0];
                        combinedAddressRecord.Number = $"Apt {record.PhysicalAddress.Split("Apt")[1]}";

                    }
                    else if (record.PhysicalAddress.Contains("Lot"))
                    {
                        combinedAddressRecord.LocationType = "Mobile home";
                        combinedAddressRecord.Street = record.PhysicalAddress.Split("Lot")[0];
                        combinedAddressRecord.Number = $"Lot {record.PhysicalAddress.Split("Lot")[1]}";
                    }
                    else if (record.PhysicalAddress.Contains("Unit"))
                    {
                        combinedAddressRecord.LocationType = "Apartment";
                        combinedAddressRecord.Street = record.PhysicalAddress.Split("Unit")[0];
                        combinedAddressRecord.Number = $"Apt {record.PhysicalAddress.Split("Unit")[1]}";
                    }
                    else
                    {
                        combinedAddressRecord.LocationType = "House";
                        combinedAddressRecord.Number = record.PhysicalAddress.Split(" ")[0];
                        combinedAddressRecord.Street = record.PhysicalAddress.Split(" ")[1];
                    };
                };
            };
            combinedAddressList.Add(combinedAddressRecord);
        }
        return combinedAddressList;
    }

    public AllAddressesGeoJSON CreateNewAddressGeoJSON(List<MasterRecord> newRecordsList)
    {
        var allAddressGeoJSON = new AllAddressesGeoJSON();

        allAddressGeoJSON.type = "FeatureCollection";
        allAddressGeoJSON.features = Array.Empty<Models.AtoZDatabaseModels.GeoJSONExport.Feature>();

        List<Models.AtoZDatabaseModels.GeoJSONExport.Feature> featureList = new List<Models.AtoZDatabaseModels.GeoJSONExport.Feature>();

        //Add all new records
        foreach (var record in newRecordsList)
        {
            var newFeature = new Models.AtoZDatabaseModels.GeoJSONExport.Feature();

            newFeature.type = "Feature";


            var addedProperties = new Models.AtoZDatabaseModels.GeoJSONExport.Properties
            {
                Id = record.Id.ToString(),
                City = record.City,
                County = record.County,
                CountryCode = record.County,
                State = record.State,
                StatusCode = record.StatusCode,
                Floor = record.Floor,
                LocationType = record.LocationType,
                NamesList = record.NamesList,
                PhoneNumbers = record.PhoneNumbers,
                TerritoryNumber = record.TerritoryNumber,
                TerritoryType = record.TerritoryType,
                Unit = record.Unit,
                Number = record.Number,
                Street = record.Street,
                FullAddress = record.UniqueLocationIndex,
                ZipCode = record.ZipCode,
                LocationCoordinates = $"Latitude: {record.Latitude}, Longitude: {record.Longitude}",
                AddressType = "New Address"
            };

            var addedGeometry = new Models.AtoZDatabaseModels.GeoJSONExport.Geometry
            {
                type = "Point",
                coordinates = new decimal[] { record.Longitude, record.Latitude }
            };

            newFeature.properties = addedProperties;
            newFeature.geometry = addedGeometry;

            featureList.Add(newFeature);
        }
        allAddressGeoJSON.features = featureList.ToArray();
        return allAddressGeoJSON;
    }

    public async Task<AllAddressesGeoJSON> CreateExistingAddressGeoJSON(FileInfo spanishExistingAddressesFile)
    {
        var allAddressGeoJSON = new AllAddressesGeoJSON();

        allAddressGeoJSON.type = "FeatureCollection";

        allAddressGeoJSON.features = Array.Empty<Models.AtoZDatabaseModels.GeoJSONExport.Feature>();

        List<Models.AtoZDatabaseModels.GeoJSONExport.Feature> featureList = new List<Models.AtoZDatabaseModels.GeoJSONExport.Feature>();

        //Add in old Addresses
        var excelFileProcessing = new ExcelBaseService();

        var spanishExistingRecords = await excelFileProcessing.LoadExistingSpanishAddreessesExcelFile(spanishExistingAddressesFile);

        foreach (var record in spanishExistingRecords)
        {
            var newFeature = new Models.AtoZDatabaseModels.GeoJSONExport.Feature();

            newFeature.type = "Feature";

            var addedProperties = new Models.AtoZDatabaseModels.GeoJSONExport.Properties
            {
                Id = record.Id.ToString(),
                City = record.City,
                County = record.County,
                CountryCode = record.County,
                State = record.State,
                StatusCode = record.Status,
                Floor = record.Floor,
                LocationType = record.LocationType,
                NamesList = "",
                PhoneNumbers = "",
                TerritoryNumber = record.TerritoryNumber,
                TerritoryType = record.TerritoryType,
                Unit = record.Apartment,
                Number = record.Number,
                Street = record.Street,
                FullAddress = record.CompleteAddress,
                ZipCode = record.PostalCode,
                LocationCoordinates = $"Latitude: {record.Latitude}, Longitude: {record.Longitude}",
                AddressType = "Existing Address"
            };

            var addedGeometry = new Models.AtoZDatabaseModels.GeoJSONExport.Geometry
            {
                type = "Point",
                coordinates = new decimal[] { record.Longitude, record.Latitude }
            };

            newFeature.properties = addedProperties;
            newFeature.geometry = addedGeometry;

            featureList.Add(newFeature);
        }
        allAddressGeoJSON.features = featureList.ToArray();
        return allAddressGeoJSON;
    }

    public async Task CreateJavascriptTerritoriesAndLocationsFiles(TerritoryHelperConfiguration config, AllAddressesGeoJSON newAddressList, AllAddressesGeoJSON existingAddressList, FileInfo territoriesFile)
    {
        string territoriesFileStartText = "let territories=";
        string existingAddressesStartText = "let existingAddresses=";
        string newAddressesStartText = "let newAddresses=";

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };


        FileStream fs = territoriesFile.OpenRead();
        StreamReader sr = new StreamReader(fs);
        string territoriesTextJSON = sr.ReadToEnd();

        string existingAddressesTextJSON = JsonSerializer.Serialize(existingAddressList, jsonOptions);
        string newAddressesTextJSON = JsonSerializer.Serialize(newAddressList, jsonOptions);

        await File.WriteAllTextAsync(config.AtoZTerritoriesJSFilePath, territoriesFileStartText);
        await File.WriteAllTextAsync(config.AtoZExistingAddressesJSFilePath, existingAddressesStartText);
        await File.WriteAllTextAsync(config.AtoZNewAddressesJSFilePath, newAddressesStartText);

        await File.AppendAllTextAsync(config.AtoZTerritoriesJSFilePath, territoriesTextJSON);
        await File.AppendAllTextAsync(config.AtoZExistingAddressesJSFilePath, existingAddressesTextJSON);
        await File.AppendAllTextAsync(config.AtoZNewAddressesJSFilePath, newAddressesTextJSON);

    }

    private void OpenFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = folderPath,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
        else
        {
            Console.WriteLine(string.Format("{0} Directory does not exist!", folderPath));
        }
    }

    public List<AddressMasterRecord> CreateFinalModelList(List<MasterRecord> masterRecordList)
    {
        List<AddressMasterRecord> spanishAddressImportList = new();
        int i = 1;
        foreach (var record in masterRecordList)
        {
            
            var spanishAddressImport = new AddressMasterRecord
            {
                Id = Guid.NewGuid(),
                Order = i,
                TerritoryType = record.TerritoryType,
                TerritoryNumber = record.TerritoryNumber,
                LocationType = record.LocationType,
                Status = record.StatusCode,
                Latitude = record.Latitude,
                Longitude = record.Longitude,
                CompleteAddress = record.UniqueLocationIndex,
                Number = record.Number,
                Street = record.Street,
                Apartment = record.Unit,
                Floor = record.Floor,
                City = record.City,
                County = record.County,
                PostalCode = record.ZipCode,
                State = record.State,
                CountryCode = record.CountryCode,
                PhoneNumbers = record.PhoneNumbers,
                NamesList = record.NamesList,
                DateUpdated=DateTime.Now
            };

            spanishAddressImportList.Add(spanishAddressImport);
            i++;
        }

        return spanishAddressImportList;
    }

    public void AddTerritoryNotes(List<AddressMasterRecord> addressMasterRecordList, TerritoryHelperConfiguration config)
    {
        var territoryNotestText = File.ReadAllText(config.TerritoryNotesPath);

        var territorySpecialNotesList = JsonSerializer.Deserialize<List<TerritorySpecificNote>>(territoryNotestText);

        foreach (var address in addressMasterRecordList)
        {
            foreach (var territory in territorySpecialNotesList)
            {
                if (address.TerritoryNumber == territory.TerritoryNumbers)
                {
                    address.TerritorySpecialNotes = territory.TerritorySpecialNotes;
                    address.TerritoryName = territory.TerritoryDescription;
                }
            }
        }

    }

    public List<TerritoryHelperAddress> CreateTerritoryHelperImportAddressRecordList(List<AddressMasterRecord> addressMasterRecordList)
    {
        List<TerritoryHelperAddress> territoryHelperAddressList = new();

        foreach (var record in addressMasterRecordList)
        {
            TerritoryHelperAddress territoryHelperAddress = new TerritoryHelperAddress
            {
                TerritoryType = record.TerritoryType,
                TerritoryNumber = record.TerritoryNumber,
                LocationType = record.LocationType,
                Status = record.Status,
                Latitude = record.Latitude,
                Longitude = record.Longitude,
                CompleteAddress = record.CompleteAddress,
                Number = record.Number,
                Street = record.Street,
                Apartment = record.Apartment,
                Floor = record.Floor,
                City = record.City,
                County = record.County,
                PostalCode = record.PostalCode,
                State = record.State,
                CountryCode = record.CountryCode
            };

            territoryHelperAddressList.Add(territoryHelperAddress);

        }

        return territoryHelperAddressList;
    }
}
