﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperSolutionsWinForm.Validators;

public class GetTerritoryInformationValidator:AbstractValidator<TerritoryHelperConfiguration>
{
	public GetTerritoryInformationValidator()
	{
		RuleFor(config=>config.ExistingSpanishAddressesFilePath)
			.Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper address file.")
            .NotEmpty().WithMessage("Please select a Territory Helper address file.");

        RuleFor(config => config.TerritoriesFilePath)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Please select a Territory Helper territories file.")
            .NotEmpty().WithMessage("Please select a Territory Helper territories file.");

        RuleFor(config=>config.FileSavedOutputLocation)
            .Cascade(CascadeMode.Stop).NotNull().WithMessage("Territory Helper output location cannot be null.")
            .NotEmpty().WithMessage("Territory Helper output location cannot be empty");

        RuleFor(config => config.IsConfigurationSettingsLocked)
			.Cascade(CascadeMode.Stop).Equal(true).WithMessage("It appears that the configuration settings have not been saved.  Please check the configuration settings and save them.");
    }
}
