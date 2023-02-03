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
using TerritoryHelperSolutionsWinForm.Validators;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class FormUpdateCensusTerritories : Form
    {
        public FormUpdateCensusTerritories()
        {
            InitializeComponent();
        }

        private void btnSelectAddressFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogMasterAddressList.Filter = "Excel Files|*.xlsx";
                openFileDialogMasterAddressList.FilterIndex = 1;
                openFileDialogMasterAddressList.InitialDirectory = "c:\\";
                openFileDialogMasterAddressList.Multiselect = false;
                if (openFileDialogMasterAddressList.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.EditedTerritoryHelperMasterAddressForImportFilePath = openFileDialogMasterAddressList.FileName;
                    MessageBox.Show($"{openFileDialogMasterAddressList.SafeFileName} path \n\r saved successfully", "Excel File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetTerritoriesList_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogGetTerritoriesList.Filter = "Json Files|*.json";
                openFileDialogGetTerritoriesList.FilterIndex = 1;
                openFileDialogGetTerritoriesList.InitialDirectory = "c:\\";
                openFileDialogGetTerritoriesList.Multiselect = false;
                if (openFileDialogGetTerritoriesList.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoriesFilePath = openFileDialogGetTerritoriesList.FileName;
                    MessageBox.Show($"{openFileDialogGetTerritoriesList.SafeFileName} path \n\r saved successfully", "Territories File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRunSaveTerritoryInformationScript_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogGetTerritorySpecialNotes.Filter = "Json Files|*.json";
                openFileDialogGetTerritorySpecialNotes.FilterIndex = 1;
                openFileDialogGetTerritorySpecialNotes.InitialDirectory = "c:\\";
                openFileDialogGetTerritorySpecialNotes.Multiselect = false;
                if (openFileDialogGetTerritorySpecialNotes.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoryNotesPath = openFileDialogGetTerritorySpecialNotes.FileName;
                    MessageBox.Show($"{openFileDialogGetTerritorySpecialNotes.SafeFileName} path \n\r saved successfully", "Territories File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTerritorySpecialNotes_Click(object sender, EventArgs e)
        {
            try
            {
                //Information Validation
                panelSideMenu.territoryHelperConfiguration.UserName = tbTerritoryHelperEmail.Text;
                panelSideMenu.territoryHelperConfiguration.Password = mTBTerritoryHelperPassword.Text;


                UpdateCensusTerritoriesValidator updateCensusTerritoriesValidator = new UpdateCensusTerritoriesValidator();
                var validatedResult = updateCensusTerritoriesValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if (validatedResult.IsValid)
                {
                    FormProgressBar formProgresssBar = new FormProgressBar(ScriptName.UpdateCensusTerritoryInformation);
                    formProgresssBar.Show();
                }
                else
                {

                    string errorList = "THERE WERE SOME ERROR(S): \r\n \r\n";
                    foreach (var failure in validatedResult.Errors)
                    {
                        errorList = $"{errorList} •{failure} \r\n \r\n";
                    }

                    MessageBox.Show(errorList, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
