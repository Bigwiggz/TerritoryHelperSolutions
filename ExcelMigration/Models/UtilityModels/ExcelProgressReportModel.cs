using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelMigration.Models.UtilityModels
{
    public class ExcelProgressReportModel
    {
        public int ExcelProcessPercentComplete { get; set; } = 0;
        public string ExcelProcessMessage { get; set; } = "";
    }
}


