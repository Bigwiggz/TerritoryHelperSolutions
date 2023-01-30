using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.UtilityModels;

public class LowerLeverProgressReportModel
{
    public int LowerLevelProcessPercentComplete { get; set; } = 0;
    public string LowerLevelProcessMessage { get; set; } = "";
}
