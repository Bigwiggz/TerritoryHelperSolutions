using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperSolutionsWinForm.Validators;

public class SearchAtoZDatabaseFolderValidator:AbstractValidator<TerritoryHelperConfiguration>
{
	public SearchAtoZDatabaseFolderValidator()
	{
		RuleFor(config=>config.AtoZTerritoriesJSFilePath)
			.Cascade(CascadeMode.Stop).NotEmpty().WithMessage("The js territories path is empty.")
			.NotNull().WithMessage("The js territories path is null.");

        RuleFor(config => config.AtoZNewAddressesJSFilePath)
			.Cascade(CascadeMode.Stop).NotEmpty().WithMessage("The js new addresses path is empty.")
			.NotNull().WithMessage("The js new addresses path is null.");

        RuleFor(config => config.AtoZExistingAddressesJSFilePath)
            .Cascade(CascadeMode.Stop).NotEmpty().WithMessage("The js existing addresses path is empty.")
            .NotNull().WithMessage("The js existing addresses path is null.");
    }
}
