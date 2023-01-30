using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerritoryHelperSolutionsWinForm.Models;
using TerritoryHelperSolutionsWinForm.UtilityForms;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class FormHelpConfiguration : Form
    {
        public FormHelpConfiguration()
        {
            InitializeComponent();
        }

        private void btnAccessProgressForm_Click(object sender, EventArgs e)
        {
            FormProgressBar formProgressBar = new FormProgressBar(ScriptName.GetTerritoryInformation);
            formProgressBar.Show();
        }
    }
}
