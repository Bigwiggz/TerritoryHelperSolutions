using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class FormGetTerritoryInformation : Form
    {

        //Fields

        public FormGetTerritoryInformation()
        {
            InitializeComponent();
        }

        private void btnSelectAddressFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogInput.Filter= "Excel Files|*.xlsx;*.xls;*.xlsm";
                openFileDialogInput.FilterIndex = 1;
                openFileDialogInput.InitialDirectory = "c:\\";
                openFileDialogInput.Multiselect = false;
                if(openFileDialogInput.ShowDialog()==DialogResult.OK )
                {
                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath=openFileDialogInput.FileName;
                    MessageBox.Show($"{openFileDialogInput.FileName} path \n\r saved successfully", "ExcelFilePath");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }

        private async void btnRunGetTerritoryInformationScript_Click(object sender, EventArgs e)
        {
            try
            { 
                //Information Validation
                panelSideMenu.territoryHelperConfiguration.UserName=tbTerritoryHelperEmail.Text;
                panelSideMenu.territoryHelperConfiguration.Password=mTBTerritoryHelperPassword.Text;

                GetTerritoryInformationValidator getTerritoryInformationValidator= new GetTerritoryInformationValidator();
                var validatedResult=getTerritoryInformationValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if (validatedResult.IsValid)
                {
                    var existingAddressesFile = new FileInfo(panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath);

                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath = existingAddressesFile.FullName;

                    FormProgressBar formProgresssBar=new FormProgressBar(ScriptName.GetTerritoryInformation);
                    formProgresssBar.Show();


                }
                else
                {

                    string errorList = "THERE WERE SOME ERROR(S): \r\n \r\n";
                    foreach (var failure in validatedResult.Errors)
                    {
                        errorList = $"{errorList} •{failure} \r\n \r\n";
                    }

                    MessageBox.Show(errorList,"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void FormGetTerritoryInformation_Load(object sender, EventArgs e)
        {
            
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
