using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelMigration.Models.UtilityModels
{
    public class ExcelTopReportModel
    {
        public int ExcelTopLevelPercentComplete { get; set; } = 0;
        public string ExcelTopLevelProgressMessage { get; set; } = "";
    }
}
