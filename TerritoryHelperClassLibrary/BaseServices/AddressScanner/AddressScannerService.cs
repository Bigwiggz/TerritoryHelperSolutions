using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.AddressScanner.AddressValidators;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AddressScanner;
using TerritoryHelperClassLibrary.Models.UtilityModels;

namespace TerritoryHelperClassLibrary.BaseServices.AddressScanner
{
    public class AddressScannerService
    {
        //Fields
        public LowerLeverProgressReportModel lowerReport;

        public AddressScannerService()
        {
            lowerReport = new LowerLeverProgressReportModel();
        }

        public List<CompleteAddressError> GetListOfAddressesWithErrors(List<TerritoryHelperAddress> territoryHelperAddressList, IProgress<LowerLeverProgressReportModel> lowerProgress)
        {
            List<CompleteAddressError> addressErrorRecordList = new();

            TerritoryHelperAddressValidator addressValidator = new TerritoryHelperAddressValidator();

            if(territoryHelperAddressList != null && territoryHelperAddressList.Count>0)
            {
                //Grab Excel file with information
                int i = 0;

                foreach (var address in territoryHelperAddressList)
                {
                    //Report
                    int reportCount = i + 1;
                    lowerReport.LowerLevelProcessPercentComplete = reportCount * 100 / territoryHelperAddressList.Count;
                    lowerReport.LowerLevelProcessMessage = $"Processing Address Verification: {address.CompleteAddress}; Record {reportCount} of {territoryHelperAddressList.Count}";
                    lowerProgress.Report(lowerReport);

                    CompleteAddressError addressError = new CompleteAddressError 
                    {
                        TerritoryType = address.TerritoryType,
                        TerritoryNumber = address.TerritoryNumber,
                        LocationType = address.LocationType,
                        Status = address.Status,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude,
                        CompleteAddress = address.CompleteAddress,
                        Number = address.Number,
                        Street = address.Street,
                        Apartment = address.Apartment,
                        Floor = address.Floor,
                        City = address.City,
                        County = address.County,
                        PostalCode = address.PostalCode,
                        State = address.State,
                        CountryCode = address.CountryCode
                    };

                    var validationResult=addressValidator.Validate(address);

                    if (validationResult.IsValid == false)
                    {
                        var addressErrorList=new List<AddressErrorRecord>();

                        foreach(var failure in  validationResult.Errors)
                        {
                            var addressErrorRecord = new AddressErrorRecord 
                            { 
                                AddressErrorId=failure.ErrorCode,
                                AddressErrorSeverity=failure.Severity.ToString(),
                                AddressErrorMessage=failure.ErrorMessage,
                                AddressErrorTitle=failure.ErrorCode
                            };

                            addressErrorList.Add(addressErrorRecord);
                        }
                        addressError.HasError = true;
                        addressError.AddressErrorRecords=addressErrorList;
                    }

                    addressErrorRecordList.Add(addressError);
                }
            }

            return addressErrorRecordList;
        }
    }
}
