using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class MasterRecord: CombinedAddresses
{
    public int Id { get; set; }
    public string PhoneNumbers { get; set; }
    public string NamesList { get; set; }
    public string TerritoryType { get; set; } = "G0";
    public string TerritoryNumber { get; set; } = "00";
}
