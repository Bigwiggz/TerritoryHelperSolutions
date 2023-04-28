using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;

namespace TerritoryHelperClassLibrary.BaseServices.AddressScanner.AddressValidators;

public class TerritoryHelperAddressValidator:AbstractValidator<TerritoryHelperAddress>
{
    public TerritoryHelperAddressValidator()
    {
        //TODO: Add test cases here
    }
}
