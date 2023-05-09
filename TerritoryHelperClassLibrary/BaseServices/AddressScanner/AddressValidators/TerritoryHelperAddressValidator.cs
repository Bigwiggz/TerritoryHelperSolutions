using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AddressScanner;

namespace TerritoryHelperClassLibrary.BaseServices.AddressScanner.AddressValidators;

public class TerritoryHelperAddressValidator:AbstractValidator<TerritoryHelperAddress>
{
    public TerritoryHelperAddressValidator()
    {
        //Load Address Abbreviations
        AddressSuffixRecordService addressSuffixRecordService = new AddressSuffixRecordService();

        var addressSuffixRecordList = addressSuffixRecordService.AddressSuffixRecordList;

        //Not Have Invalid Characters
        RuleFor(x => x.CompleteAddress).NotNull().WithMessage("The Full Address Field must not be null.").WithErrorCode("Full Address Null").WithSeverity(Severity.Error)
            .Must(NotHaveInvalidCharacters).WithMessage("The Full Address Field has invalid characters !@$%^* or others.").WithErrorCode("Full Address Invalid Character").WithSeverity(Severity.Error);

        //Not Have Questionable Characters
        RuleFor(x => x.CompleteAddress)
            .Must(NotHaveQuestionableCharacters).WithMessage("The Full Address Field has questionable characters _-# or others.").WithErrorCode("Full Address Questionable Character").WithSeverity(Severity.Warning);

        //Have Country At End Of Address
        RuleFor(x => x.CompleteAddress)
            .Must(HaveCountryAtEndOfAddress).WithMessage("The Full Address Field does not contain ', United States'.").WithErrorCode("Country Code Warning").WithSeverity(Severity.Warning);
        /*
        //Not Have Invalid Formatted Postal Address
        RuleFor(x=>new {x.CompleteAddress,x.PostalCode}).NotNull().WithMessage("The Postal Code or Full Address Field must not be null.").WithErrorCode("Full Address or Postal Code Null").WithSeverity(Severity.Error)
            .Must(x=>NotHaveInvalidFormattedPostalAddress(x.CompleteAddress, x.PostalCode)).WithMessage("Either the Full Address or Postal Code field has an incorrectly formatted zip code.").WithErrorCode("Zip Code Error").WithSeverity(Severity.Error);
        */
        //Not Have Inconsistent Location Types
        RuleFor(x => new {x.CompleteAddress,x.LocationType }).NotNull().WithMessage("The Location Type or Full Address Field must not be null.").WithErrorCode("Full Address or Location Type Null").WithSeverity(Severity.Error)
            .Must(x=> NotHaveInconsistentLocationTypes(x.CompleteAddress,x.LocationType)).WithMessage("The location type of Mobile Home/Apartment does not have the correct Lot/Apt in the full address field.").WithErrorCode("Location Type Error").WithSeverity(Severity.Error);

        //Have Correct State Code
        RuleFor(x=> new {x.CompleteAddress, x.State }).NotNull().WithMessage("The State or Full Address Field must not be null.").WithErrorCode("Full Address or State Null").WithSeverity(Severity.Error)
            .Must(x => HaveCorrectStateCode(x.CompleteAddress, x.State)).WithMessage("The State Abbreviation is not correct.").WithErrorCode("State Abbreviation Error").WithSeverity(Severity.Error);
        /*
        //Not Have Misplaced Or Missing Commas
        RuleFor(x => new { x.CompleteAddress,x.State, x.City})
            .Must(x=> NotHaveMisplacedOrMissingCommas(x.CompleteAddress,x.State, x.City)).WithMessage("The commas are incorrectly placed.").WithErrorCode("Comma Placement Warning").WithSeverity(Severity.Warning);
        
        //Must Be Correctly Partitioned Address
        RuleFor(x=>x)
            .Must(BeCorrectlyPartitionedAddress).WithMessage("The address is not correctly partitioned.").WithErrorCode("Partitioned Address Error").WithSeverity(Severity.Error);
        
        //Must Have Correct Abbreviation Suffix for the Roads
        RuleFor(x=>new {x.CompleteAddress,x.State })
            .Must(x=> HaveCorrectAbbreviationSuffix(x.CompleteAddress,x.State, addressSuffixRecordList)).WithMessage("The address has the wrong abbreviation.").WithErrorCode("Street Abbreviation Error").WithSeverity(Severity.Error);
        */
    }

    protected bool NotHaveInvalidCharacters(string fullAddress)
    {
        char[] invalidChars = new char[] { '!', '@', '$', '%', '^', '&', '*', '(', ')', '{', '}', '[', ']', ':', '<', '>', '.' };
        bool containsInvalidChar = fullAddress.IndexOfAny(invalidChars) != -1;
        bool notHavInvalidChar = !containsInvalidChar;
        return notHavInvalidChar;
    }

    protected bool NotHaveQuestionableCharacters(string fullAddress)
    {
        char[] invalidChars = new char[] { '-', '_', '#' };
        bool containsInvalidChar = fullAddress.IndexOfAny(invalidChars) != -1;
        bool notHavInvalidChar = !containsInvalidChar;
        return notHavInvalidChar;
    }

    protected bool HaveCountryAtEndOfAddress(string fullAddress)
    {
        bool containsCorrectCountryAddress = fullAddress.Contains(", United States");
        return containsCorrectCountryAddress;
    }

    protected bool NotHaveInvalidFormattedPostalAddress(string fullAddress, string postalCode)
    {
        bool isValid = false;

        string fullAddressPatternZip = @"\b\d{5}(?:[-\s]\d{4})?\b";
        Match match = Regex.Match(fullAddress, fullAddressPatternZip);
        if(match.Success && fullAddress.Contains(postalCode))
        {
            isValid = true;
        }

        return isValid;
    }

    protected bool NotHaveInconsistentLocationTypes(string completeAddress, string locationType)
    {
        bool notHaveInconsistentLocationTypes = true;

        if (locationType.Trim() == "Mobile home" || locationType.Trim() == "Casa móvil" && completeAddress.Contains("Lot"))
        {
            notHaveInconsistentLocationTypes = false;
        }
        if (locationType.Trim() == "Apartment" || locationType.Trim() == "Apartamentos" && completeAddress.Contains("Apt"))
        {
            notHaveInconsistentLocationTypes = false;
        }
        if (locationType.Trim() == "House" || locationType.Trim() == "Casa" && completeAddress.Contains("Apt") == false && !completeAddress.Contains("Lot") == false)
        {
            notHaveInconsistentLocationTypes = false;
        }

        return notHaveInconsistentLocationTypes;

    }

    protected bool HaveCorrectStateCode(string completeAddress, string state)
    {
        //Find State Code
        // Regular expression to match state codes
        bool hasCorrectStateCode = false;

        string pattern = @"\b(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)\b";
        Match stateMatch = Regex.Match(state, pattern);
        Match completeAddressMatch = Regex.Match(completeAddress, pattern);

        if (stateMatch.Success && completeAddressMatch.Success)
        {
            hasCorrectStateCode = true;
        }

        return hasCorrectStateCode;
    }

    protected bool NotHaveMisplacedOrMissingCommas(string completeAddress, string state, string city)
    {

        bool hasCorrectCommas = false;
        bool hasCityComma = false;
        bool hasStateComma = false;
        bool hasZipComma = false;
        if (state is not null)
        {
            //City Comma
            var indexOfCityComma = completeAddress.LastIndexOf(city) - 2;
            if (completeAddress[indexOfCityComma] == ',')
            {
                hasCityComma = true;
            }

            //State Comma
            var indexOfStateComma = completeAddress.LastIndexOf(state) - 2;
            if (completeAddress[indexOfStateComma] == ',')
            {
                hasStateComma = true;
            }

            //Zip Comma
            var indexOfZipComma = completeAddress.LastIndexOf("United States") - 2;
            if (completeAddress[indexOfZipComma] == ',')
            {
                hasZipComma = true;
            }

            if (completeAddress.IndexOf(", United States") != -1)
            {
                hasZipComma = true;
            }

            //All Commas
            if (hasCityComma == true && hasStateComma == true && hasZipComma == true)
            {
                hasCorrectCommas = true;
            }
        }

        return hasCorrectCommas;
    }

    protected bool BeCorrectlyPartitionedAddress(TerritoryHelperAddress address)
    {
        //TODO:Finish postal address
        bool beCorrectlyPartitionedAddress = false;
        var concatenatedAddress = "";
        if (address.LocationType == "Mobile home" || address.LocationType == "Casa móvil")
        {
            concatenatedAddress = $"{address.Street} Lot {address.Number}, {address.City}, {address.State} {address.PostalCode}, United States";
        }
        if (address.LocationType == "Apartment" || address.LocationType == "Apartamentos")
        {
            concatenatedAddress = $"{address.Street} Apt {address.Number}, {address.City}, {address.State} {address.PostalCode}, United States";
        }
        else
        {
            concatenatedAddress = $"{address.Number} {address.Street}, {address.City}, {address.State} {address.PostalCode}, United States";
        }

        concatenatedAddress = concatenatedAddress.Replace(", United States","").ToLower();
        var completeAddress = address.CompleteAddress.Replace(", United States", "").ToLower();

        if (concatenatedAddress == completeAddress)
        {
            beCorrectlyPartitionedAddress = true;
        }

        return beCorrectlyPartitionedAddress;
    }

    public bool HaveCorrectAbbreviationSuffix(string completeAddress, string street, List<AddressSuffixRecord> addressSuffixRecordsList)
    {
        //TODO: Finish adding HaveCorrectAbbrieviation
        if (completeAddress.Contains(street))
        {

        }
        bool hasCorrectAbbreviationSuffix = false;
        bool hasIncorrectSuffix = addressSuffixRecordsList.Any(x => x.CommonAbbreviation == "testSuffix");

        return hasCorrectAbbreviationSuffix;
    }

}
