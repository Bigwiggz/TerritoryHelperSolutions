﻿using System;
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
                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath = openFileAddressesDialogInput.FileName;
                    MessageBox.Show($"{openFileAddressesDialogInput.SafeFileName} path \n\r saved successfully", "Excel File Path");
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

        private void btnRunSaveTerritoryInformationScript_Click(object sender, EventArgs e)
        {
            try
            {
                //Information Validation


                SaveTerritoryInformationValidator saveTerritoryInformationValidator = new SaveTerritoryInformationValidator();
                var validatedResult = saveTerritoryInformationValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if (validatedResult.IsValid)
                {
                    FormProgressBar formProgresssBar = new FormProgressBar(ScriptName.SaveTerritoryInformation);
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileTerritorySpecialNotesInput.Filter = "Json Files|*.json";
                openFileTerritorySpecialNotesInput.FilterIndex = 1;
                openFileTerritorySpecialNotesInput.InitialDirectory = "c:\\";
                openFileTerritorySpecialNotesInput.Multiselect = false;
                if (openFileTerritorySpecialNotesInput.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoryNotesPath = openFileTerritorySpecialNotesInput.FileName;
                    MessageBox.Show($"{openFileTerritorySpecialNotesInput.SafeFileName} path \n\r saved successfully", "Territories File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectMasterAddressFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileMasterAddressDialogInput.Filter = "Excel Files|*.xlsx";
                openFileMasterAddressDialogInput.FilterIndex = 1;
                openFileMasterAddressDialogInput.InitialDirectory = "c:\\";
                openFileMasterAddressDialogInput.Multiselect = false;
                if (openFileMasterAddressDialogInput.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.EditedTerritoryHelperMasterAddressForImportFilePath = openFileMasterAddressDialogInput.FileName;
                    MessageBox.Show($"{openFileMasterAddressDialogInput.SafeFileName} path \n\r saved successfully", "Excel File Path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
