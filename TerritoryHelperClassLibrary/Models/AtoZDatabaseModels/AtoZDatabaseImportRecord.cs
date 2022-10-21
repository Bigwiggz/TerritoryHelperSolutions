using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class AtoZDatabaseImportRecord
{
    public int Id { get; set; }
    public string UniqueLocationIndex { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
    public DateTime ObsolescenceDate { get; set; }
    public string FirstName { get; set; }
    public string MiddleInitial { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string PhysicalAddress { get; set; }
    public string PhysicalCity { get; set; }
    public string PhysicalState { get; set; }
    public string PhysicalZIP { get; set; }
    public string PhysicalZIP4 { get; set; }
    public string Phone { get; set; }
    public string MetroArea { get; set; }
    public string County { get; set; }
    public string LengthofResidency { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsSpanish { get; set; }
}
