using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AddressScanner
{
    public class AddressErrorRecord : IAddressError
    {
        public string AddressErrorId {get; set;}
        public string AddressErrorSeverity { get; set;}
        public string AddressErrorTitle { get; set;}
        public string AddressErrorMessage { get; set;}
    }
}
