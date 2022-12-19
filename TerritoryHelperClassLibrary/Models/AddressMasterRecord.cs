using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models;

public class AddressMasterRecord:TerritoryHelperAddress
{
    //Order
    public Guid Id { get; set; }
    public int? Order { get; set; }

    //Table Notes
    public string PhoneNumbers { get; set; }
    public string NamesList { get; set; }
    public string Notes { get; set; }

    //Address Verification
    public string? DeliveryPoint { get; set; }
    public string? CarrierRoute { get; set; }
    public string? DPVConfirmation { get; set; }
    public string? DPVCMRA { get; set; }
    public string? DPVFootnotes { get; set; }
    public string? Business { get; set; }
    public string? CentralDeliveryPoint { get; set; }
    public string? Vacant { get; set; }
    public string? HttpError { get; set; }

    //Territory Notes
    public string? TerritoryName { get; set; }
    public string? TerritorySpecialNotes { get; set; }

    //Address Update
    public DateTime DateUpdated { get; set; }

    //Internal Processing
    public string UniqueIdentifierCreation { get; set; }

    //Route Plan Order
    public int MasterRecordRoutePlanOrder { get; set; }
}
