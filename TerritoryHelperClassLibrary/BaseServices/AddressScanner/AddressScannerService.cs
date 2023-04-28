using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AddressScanner;

namespace TerritoryHelperClassLibrary.BaseServices.AddressScanner
{
    public class AddressScannerService
    {
        public List<AddressErrorRecord> GetListOfAddressesWithErrors(List<TerritoryHelperAddress> territoryHelperAddressList)
        {
            List<AddressErrorRecord> addressErrorRecordList = new();

            if(territoryHelperAddressList != null && addressErrorRecordList.Count > 0)
            {
                foreach(var  address in territoryHelperAddressList)
                {
                    //TODO: Finish analyzing the errrors 
                }
            }

            return addressErrorRecordList;
        }
    }
}
