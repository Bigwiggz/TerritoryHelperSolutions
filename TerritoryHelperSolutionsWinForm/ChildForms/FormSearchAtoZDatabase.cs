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
    public partial class FormSearchAtoZDatabase : Form
    {
        public FormSearchAtoZDatabase()
        {
            InitializeComponent();
        }

        private void btnSelectGetAllAtoZFiles_Click(object sender, EventArgs e)
        {
            //TODO: Find out how to load the files
            try
            {
                openFileDialogAllAToZFiles.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                openFileDialogAllAToZFiles.FilterIndex = 1;
                openFileDialogAllAToZFiles.InitialDirectory = "c:\\";
                openFileDialogAllAToZFiles.Multiselect = true;
                if (openFileDialogAllAToZFiles.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.AtoZDatbaseFilesPath = openFileDialogAllAToZFiles.FileName;
                    MessageBox.Show($"{openFileDialogAllAToZFiles.FileNames.Count()} files \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnSpanishLastNames_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogSpanishLastNames.Filter = "Excel Files|*.xlsx";
                openFileDialogSpanishLastNames.FilterIndex = 1;
                openFileDialogSpanishLastNames.InitialDirectory = "c:\\";
                openFileDialogSpanishLastNames.Multiselect = false;
                if (openFileDialogSpanishLastNames.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.SpanishLastNamesPath = openFileDialogSpanishLastNames.FileName;
                    MessageBox.Show($"{openFileDialogSpanishLastNames.FileName} files \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetExistingTerritoryAddresses_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogExistingAddresses.Filter = "Excel Files|*.xlsx";
                openFileDialogExistingAddresses.FilterIndex = 1;
                openFileDialogExistingAddresses.InitialDirectory = "c:\\";
                openFileDialogExistingAddresses.Multiselect = false;
                if (openFileDialogExistingAddresses.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath = openFileDialogExistingAddresses.FileName;
                    MessageBox.Show($"{openFileDialogExistingAddresses.FileName} file \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTerritories_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogAllTerritories.Filter = "Json Files|*.json";
                openFileDialogAllTerritories.FilterIndex = 1;
                openFileDialogAllTerritories.InitialDirectory = "c:\\";
                openFileDialogAllTerritories.Multiselect = false;
                if (openFileDialogAllTerritories.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoriesFilePath = openFileDialogAllTerritories.FileName;
                    MessageBox.Show($"{openFileDialogAllTerritories.FileName} file \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCongregationBoundaries_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogTerritoryBoundary.Filter = "Json Files|*.json";
                openFileDialogTerritoryBoundary.FilterIndex = 1;
                openFileDialogTerritoryBoundary.InitialDirectory = "c:\\";
                openFileDialogTerritoryBoundary.Multiselect = false;
                if (openFileDialogTerritoryBoundary.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.CongregationCurrentTerritoryBoundariesFilePath = openFileDialogTerritoryBoundary.FileName;
                    MessageBox.Show($"{openFileDialogTerritoryBoundary.FileName} file \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCensoAddresses_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogCensoAddresses.Filter = "Excel Files|*.xlsx";
                openFileDialogCensoAddresses.FilterIndex = 1;
                openFileDialogCensoAddresses.InitialDirectory = "c:\\";
                openFileDialogCensoAddresses.Multiselect = false;
                if (openFileDialogCensoAddresses.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.CensoTerritoryAddressPath = openFileDialogCensoAddresses.FileName;
                    MessageBox.Show($"{openFileDialogCensoAddresses.FileName} file \r\n saved successfully", "Files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRunGetAtoZDatabaseRecords_Click(object sender, EventArgs e)
        {
            try
            {
                //Information Validation

                SearchAtoZDatabaseValidator searchAtoZDatabaseValidator = new SearchAtoZDatabaseValidator();
                var validatedResult = searchAtoZDatabaseValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if (validatedResult.IsValid)
                {

                    FormProgressBar formProgresssBar = new FormProgressBar(ScriptName.SearchAToZDatabaseInformation);
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
