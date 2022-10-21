using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class TerritoryNoteRecord
{
    public string Address { get; set; }
    public string Names { get; set; }
    public string TelephoneNumbers { get; set; }
    public string Notes { get; set; }
    public string DPVConfirmation { get; set; }
}

public class TerritoryNoteRecordsList
{
    public TerritoryNoteRecordsList()
    {
        AllIndividualTerritoryRecords = new List<TerritoryNoteRecord>();
    }
    public string TerritoryFullTypeNumber { get; set; }
    public int TerritorySortNumber { get; set; }

    public string TerritorySortType { get; set; }
    public List<TerritoryNoteRecord> AllIndividualTerritoryRecords { get; set; }
}
