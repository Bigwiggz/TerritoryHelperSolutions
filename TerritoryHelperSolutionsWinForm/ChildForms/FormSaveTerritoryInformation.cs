using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class FormSaveTerritoryInformation : Form
    {
        public FormSaveTerritoryInformation()
        {
            InitializeComponent();
        }

        private void btnSelectAddressFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileAddressesDialogInput.Filter = "Excel Files|*.xlsx";
                openFileAddressesDialogInput.FilterIndex = 1;
                openFileAddressesDialogInput.InitialDirectory = "c:\\";
                openFileAddressesDialogInput.Multiselect = false;
                if (openFileAddressesDialogInput.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.EditedTerritoryHelperMasterAddressForImportFilePath = openFileAddressesDialogInput.FileName;
                    MessageBox.Show($"{openFileAddressesDialogInput.SafeFileName} path \n\r saved successfully", "Excel File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnGetTerritoriesList_Click(object sender, EventArgs e)
        {
            try
            {
                openFileTerritoriesDialogInput.Filter = "Json Files|*.json";
                openFileTerritoriesDialogInput.FilterIndex = 1;
                openFileTerritoriesDialogInput.InitialDirectory = "c:\\";
                openFileTerritoriesDialogInput.Multiselect = false;
                if (openFileTerritoriesDialogInput.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoriesFilePath = openFileTerritoriesDialogInput.FileName;
                    MessageBox.Show($"{openFileTerritoriesDialogInput.SafeFileName} path \n\r saved successfully", "Territories File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
