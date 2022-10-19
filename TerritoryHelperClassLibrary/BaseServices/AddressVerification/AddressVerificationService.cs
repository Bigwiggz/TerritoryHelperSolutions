﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperClassLibrary.BaseServices.AddressVerification;

public class AddressVerificationService
{
    public async void VerifyAddress(List<AddressMasterRecord> addressMasterRecord, TerritoryHelperConfiguration config)
    {
        //Create HttpClient
        HttpClient client=new HttpClient();
        //Grab Excel file with information
        int i = 0;
        foreach (var item in addressMasterRecord)
        {
            string aptNumber;
            string streetNumberandName;
            if (item.LocationType == "Mobile home")
            {
                aptNumber = $"Lot {item.Number}";
                streetNumberandName = $"{item.Street}";
            }
            else if (item.LocationType == "Apartment")
            {
                aptNumber = $"Apt {item.Number}";
                streetNumberandName = $"{item.Street}";
            }
            else
            {
                aptNumber = null;
                streetNumberandName = $"{item.Number} {item.Street}";
            }

            //XML Input Generator
            XDocument requestXml = new XDocument(
                new XElement("AddressValidateRequest",
                    new XAttribute("USERID", config.AddressVerificationUserId),
                    new XElement("Revision", "1"),
                    new XElement("Address",
                        new XAttribute("ID", "0"),
                        new XElement("Address1", aptNumber),
                        new XElement("Address2", streetNumberandName),
                        new XElement("City", item.City),
                        new XElement("State", item.State),
                        new XElement("Zip5", item.PostalCode),
                        new XElement("Zip4", "")
                    )
                )
            );

            try
            {
                var url = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=" + requestXml;
                using HttpResponseMessage responseMessage=await client.GetAsync(url);  
                using HttpContent content=responseMessage.Content;
                var response = await content.ReadAsStringAsync();

                //Delay 
                Thread.Sleep(config.APICallDelayinMiliseconds);

                var xdoc = XDocument.Parse(response.ToString());

                foreach (XElement element in xdoc.Descendants("Address"))
                {

                    //Load all items

                    item.DeliveryPoint = GetXMLElement(element, "DeliveryPoint");
                    item.CarrierRoute = GetXMLElement(element, "CarrierRoute");
                    item.DPVConfirmation = GetXMLElement(element, "DPVConfirmation");
                    item.DPVCMRA = GetXMLElement(element, "DPVCMRA");
                    item.DPVFootnotes = GetXMLElement(element, "DPVFootnotes");
                    item.Business = GetXMLElement(element, "Business");
                    item.CentralDeliveryPoint = GetXMLElement(element, "CentralDeliveryPoint");
                    item.Vacant = GetXMLElement(element, "Vacant");
                    item.HttpError = null;

                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.ToString());
                item.HttpError = "Yes";
            }

            static string GetXMLElement(XElement element, string name)
            {
                var el = element.Element(name);
                if (el != null)
                {
                    return el.Value;
                }
                return "";
            }

            static string GetXMLAttribute(XElement element, string name)
            {
                var el = element.Attribute(name);
                if (el != null)
                {
                    return el.Value;
                }
                return "";
            }

            //increment i
            i++;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Processed record {i} of {addressMasterRecord.Count}");
            Console.WriteLine($"Address: {item.CompleteAddress}");
            Console.WriteLine($"Confirmed Address: {item.DPVConfirmation}");
            Console.WriteLine($"Http error: {item.HttpError}");
        }
    }
}
