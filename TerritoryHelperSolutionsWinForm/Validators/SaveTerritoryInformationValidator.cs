using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperSolutionsWinForm.Validators;

public class SaveTerritoryInformationValidator:AbstractValidator<TerritoryHelperConfiguration>
{
	public SaveTerritoryInformationValidator()
	{
        RuleFor(config => config.UserName)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Territory Helper email cannot be null")
            .NotEmpty().WithMessage("Territory Helper email cannot be empty")
            .EmailAddress().WithMessage("Territory Helper email must be a valid email address");

        RuleFor(config => config.Password)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Territory Helper password cannot be null")
            .NotEmpty().WithMessage("Territory Helper password cannot be empty");

        RuleFor(config => config.TerritoryNotesPath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper territory notes file.")
            .NotEmpty().WithMessage("Please select a Territory Helper territory notes file.");


        RuleFor(config => config.EditedTerritoryHelperMasterAddressForImportFilePath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper address file.")
            .NotEmpty().WithMessage("Please select a Territory Helper address file.");

        RuleFor(config => config.TerritoriesFilePath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper territory json file.")
            .NotEmpty().WithMessage("Please select a Territory Helper territory json file.");

        RuleFor(config => config.FileSavedOutputLocation)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Territory Helper output location cannot be null.")
            .NotEmpty().WithMessage("Territory Helper output location cannot be empty");

        RuleFor(config => config.IsConfigurationSettingsLocked)
            .Cascade(CascadeMode.Stop).Equal(true).WithMessage("It appears that the configuration settings have not been saved.  Please check the configuration settings and save them.");
    }
}
