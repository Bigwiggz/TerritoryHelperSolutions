using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.UtilityModels;

public class ProgressReportModel
{
    public int TopLevelPercentComplete { get; set; } = 0;
    public string TopLevelProgressMessage { get; set; } = "";
}
