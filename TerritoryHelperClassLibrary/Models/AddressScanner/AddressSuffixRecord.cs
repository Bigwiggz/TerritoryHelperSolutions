using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AddressScanner;

public class AddressSuffixRecord
{
    public int Id { get; set; }
    public string CommonAbbreviation { get; set; }
    public string RecommendedAbbreviation { get; set; }
}
