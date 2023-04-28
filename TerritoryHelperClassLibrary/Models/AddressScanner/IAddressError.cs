using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AddressScanner
{
    public interface IAddressError
    {
        public int AddressErrorId  {get;set;}
        public string AddressErrorTitle { get; set; }
        public string AddressErrorMessage { get; set; }
    }
}
