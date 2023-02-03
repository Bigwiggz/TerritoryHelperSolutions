using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperSolutionsWinForm.Validators;

public class SearchAtoZDatabaseValidator:AbstractValidator<TerritoryHelperConfiguration>
{
	public SearchAtoZDatabaseValidator()
	{
        RuleFor(config => config.AtoZDatbaseFilesPath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select A to Z Database files.")
            .NotEmpty().WithMessage("Please select A to Z Database files.");

        RuleFor(config => config.SpanishLastNamesPath)
          .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a spanish last name file.")
          .NotEmpty().WithMessage("Please select a spanish last name file.");

        RuleFor(config => config.ExistingSpanishAddressesFilePath)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a PRIMARY Territory Helper existing addresses file.")
           .NotEmpty().WithMessage("Please select a PRIMARY Territory Helper existing addresses file.");

        RuleFor(config => config.TerritoriesFilePath)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper json territories file.")
           .NotEmpty().WithMessage("Please select a Territory Helper json territories file.");

        RuleFor(config => config.CongregationCurrentTerritoryBoundariesFilePath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Congregation territory boundary json file.")
            .NotEmpty().WithMessage("Please select a Congregation territory boundary json file.");

        RuleFor(config => config.TerritoryNotesPath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a territory notes json file.")
            .NotEmpty().WithMessage("Please select a territory notes json file.");

        RuleFor(config => config.CensoTerritoryAddressPath)
           .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper CENSO existing addresses file.")
           .NotEmpty().WithMessage("Please select a Territory Helper CENSO existing addresses file.");

        RuleFor(config => config.FileSavedOutputLocation)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Territory Helper output location cannot be null.")
            .NotEmpty().WithMessage("Territory Helper output location cannot be empty");

        RuleFor(config => config.IsConfigurationSettingsLocked)
            .Cascade(CascadeMode.Stop).Equal(true).WithMessage("It appears that the configuration settings have not been saved.  Please check the configuration settings and save them.");
    }
}
