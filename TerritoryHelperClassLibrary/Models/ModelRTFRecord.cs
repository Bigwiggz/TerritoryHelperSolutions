using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models;

public class ModelRTFRecord
{
    public string TerritoryNumber { get; set; }
    public string Address { get; set; }
    public string NamesList { get; set; }
    public string PhoneNumbersList { get; set; }
    public string Notes { get; set; }

    //Internal Processing
    public string UniqueIdentifierCreation { get; set; }
}
