using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperSolutionsWinForm.Validators;

public class ConfigurationValidator:AbstractValidator<TerritoryHelperConfiguration>
{
	public ConfigurationValidator()
	{

        RuleFor(config => config.LoginUrl)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Territory Helper Login Url cannot be null.  Please enter a value.")
            //.Empty().WithMessage("The Territory Helper Login Url cannot be empty.  Please enter a value")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(config => !string.IsNullOrEmpty(config.LoginUrl))
            .WithMessage("The Territory Helper Login Url you have entered does not appear to be a valid website.  Please check it again.");

        RuleFor(config => config.TerritoryRecordBaseUrl)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Territory Record Base Url cannot be null.  Please enter a value.")
            //.Empty().WithMessage("The Territory Record Base Url cannot be empty.  Please enter a value")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(config => !string.IsNullOrEmpty(config.TerritoryRecordBaseUrl))
            .WithMessage("The Territory Record Base Url you have entered does not appear to be a valid website.  Please check it again.");

        RuleFor(config => config.AddressVerificationUserId)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Address Verification User Id cannot be null");
			//.Empty().WithMessage("The Address Verification User Id cannot be empty");

		RuleFor(config => config.USPSAPISite)
			.Cascade(CascadeMode.Stop).NotNull().WithMessage("The USPS website cannot be null.  Please enter a value.")
			//.Empty().WithMessage("The USPS website cannot be empty.  Please enter a value")
            .Must(uri=>Uri.TryCreate(uri,UriKind.Absolute,out _)).When(config=>!string.IsNullOrEmpty(config.USPSAPISite))
            .WithMessage("The USPS website you have entered does not appear to be a valid website.  Please check it again.");

        RuleFor(config => config.APIType)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The API Type cannot be null");
            //.Empty().WithMessage("The API Type cannot be empty");

        RuleFor(config => config.BatchID)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Batch Id cannot be null");
            //.Empty().WithMessage("The BatchId cannot be empty");
           
        RuleFor(config=>config.AtoZXLSXFilesPath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Sorry something happened with the xlsx temp path setup.");

        RuleFor(config => config.APICallDelayinMiliseconds)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The API Delay cannot be null")
            //.Empty().WithMessage("The API Delay cannot be empty")
            .InclusiveBetween(1, 1000).WithMessage("The API Delay must be between 1 and 1000.  Your current value of is {PropertyValue}.");

        RuleFor(config => config.NumberOfTableRows)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Number of Table Rows cannot be null")
            //.Empty().WithMessage("The Number of Table Rows cannot be empty")
            .InclusiveBetween(1, 20).WithMessage("The Number of Table Rows must be between 1 and 20.  Your current value of is {PropertyValue}.");

        RuleFor(config => config.KindgomHallAddress)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Kingdom Hall Address cannot be null.  Please enter a value.");
            //.Empty().WithMessage("The Kingdom Hall Address cannot be empty.  Please enter a value.");

        RuleFor(config => config.KingdomHallLocationLatitude)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Kingdom Hall Address Latitude cannot be null.  Please enter a value.")
            //.Empty().WithMessage("The Kingdom Hall Address Latitude cannot be empty.  Please enter a value.")
			.InclusiveBetween(-90,90).WithMessage("The Kingdom Hall Address Latitude must be between -90 and 90.  Your current value of is {PropertyValue}.");

        RuleFor(config => config.KingdomHallLocationLongitude)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("The Kingdom Hall Address Longitude cannot be null.  Please enter a value.")
           //.Empty().WithMessage("The Kingdom Hall Address Longitude cannot be empty.  Please enter a value.")
           .InclusiveBetween(-180, 180).WithMessage("The Kingdom Hall Address Longitude must be between -180 and 180.  Your current value of is {PropertyValue}.");

        RuleFor(config => config.FileInputLocation)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("The input file location cannot be null.  Please enter a value.")
           //.Empty().WithMessage("The input file location cannot be empty.  Please enter a value.")
           .Must(BeValidFilePath).WithMessage("The following file path {PropertyValue} is not a valid file path or cannot be reached.");

        RuleFor(config => config.FileSavedOutputLocation)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("The output file location cannot be null.  Please enter a value.")
           //.Empty().WithMessage("The output file location cannot be empty.  Please enter a value.")
           .Must(BeValidFilePath).WithMessage("The following file path {PropertyValue} is not a valid file path or cannot be reached.");

    }


    protected bool BeValidFilePath(string filePath)
    {
        return Directory.Exists(filePath);
    }
}
