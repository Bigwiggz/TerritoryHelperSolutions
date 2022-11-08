using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models;

public class TestAddress
{
    public string Id { get; set; }
    public string Order { get; set; }
    public string NamesList { get; set; }
    public string DateUpdated { get; set; }
    public string TerritoryType { get; set; }
    public string TerritoryNumber { get; set; }
    public string LocationType { get; set; }
    public string Status { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string CompleteAddress { get; set; }
    public string Number { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
    public string Floor { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string CountryCode { get; set; }
    public string PhoneNumbers { get; set; }

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
}

