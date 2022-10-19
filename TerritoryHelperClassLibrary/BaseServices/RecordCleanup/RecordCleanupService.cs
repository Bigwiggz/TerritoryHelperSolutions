using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;

namespace TerritoryHelperClassLibrary.BaseServices.RecordCleanup;

public class RecordCleanupService
{

    public void CreateUniqueIdentifierFromList(List<AddressMasterRecord> spanishAddressImports)
    {
        foreach (var address in spanishAddressImports)
        {
            var uniqueIdentifier = address.CompleteAddress
                .Replace(", United States", "")
                .Replace(",", "")
                .Replace(" ", " ")
                .Replace(" ", "")
                .Trim();
            address.UniqueIdentifierCreation = uniqueIdentifier;

        }
    }

    public void ReplaceNullsWithEmpty<T>(List<T> spanishAddressImports)
    {
        foreach (var record in spanishAddressImports)
        {
            foreach (var prop in record.GetType().GetProperties())
            {
                if (prop.GetValue(record) is null)
                {
                    if (prop.GetType() == typeof(string))
                    {
                        prop.SetValue(record, "");
                    }
                    if (prop.GetType() == typeof(int) || prop.GetType() == typeof(decimal))
                    {
                        prop.SetValue(record, 0);
                    }
                }
            }
        }
    }
}
