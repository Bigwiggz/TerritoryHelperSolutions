using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.AddressScanner.AddressValidators;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AddressScanner;

namespace TerritoryHelperClassLibrary.BaseServices.AddressScanner
{
    public class AddressScannerService
    {
        public List<CompleteAddressError> GetListOfAddressesWithErrors(List<TerritoryHelperAddress> territoryHelperAddressList)
        {
            List<CompleteAddressError> addressErrorRecordList = new();

            TerritoryHelperAddressValidator addressValidator = new TerritoryHelperAddressValidator();

            if(territoryHelperAddressList != null && territoryHelperAddressList.Count>0)
            {
                foreach(var address in territoryHelperAddressList)
                {
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

                        addressError.AddressErrorRecords=addressErrorList;
                    }
                }
            }

            return addressErrorRecordList;
        }
    }
}
