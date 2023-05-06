using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AddressScanner;

public class CompleteAddressError:TerritoryHelperAddress
{
    public List<AddressErrorRecord> AddressErrorRecords { get; set; } = new List<AddressErrorRecord>();
}
