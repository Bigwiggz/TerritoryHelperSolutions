using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;
using TerritoryHelperSolutionsWinForm;
using TerritoryHelperSolutionsWinForm.Validators;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class formConfiguration : Form
    {
        //Fields

        public formConfiguration()
        {
            InitializeComponent();
        }

        private void formConfiguration_Load(object sender, EventArgs e)
        {
            //File Directories
            lblFolderInputDirectoryPath.Text = Path.Combine(AppContext.BaseDirectory, "Input");
            lblOutputFolderDirectoryPath.Text= Path.Combine(AppContext.BaseDirectory, "Output");
            // Config settings
            tbTerritoryHelperUrlLogin.Text = "https://territoryhelper.com/";
            tbTerritoryHelperUrlTerritoryRecords.Text = "https://www.territoryhelper.com/en/View/Territory";
            tbAPIDelayMS.Text = "10";
            tbUSPSAPISite.Text = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=";
            tbAddressVerificationUserId.Text = "388PALME4313";
            tbAPIType.Text = "Verify";
            tbBatchId.Text = "1";
            tbKingdomHallAddress.Text = "3679 Leaphart Rd, West Columbia, SC 29169";
            tbKingdomHallLatitude.Text = "34.00261402946795";
            tbKingdomHallLongitude.Text = "-81.13513616286949";
            tbNumberofTableRows.Text = "4";
        }

        private void btnSaveConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                //Territory Helper
                panelSideMenu.territoryHelperConfiguration.LoginUrl = tbTerritoryHelperUrlLogin.Text;
                panelSideMenu.territoryHelperConfiguration.TerritoryRecordBaseUrl = tbTerritoryHelperUrlTerritoryRecords.Text;

                //USPS Address API
                panelSideMenu.territoryHelperConfiguration.APICallDelayinMiliseconds = int.Parse(tbAPIDelayMS.Text);
                panelSideMenu.territoryHelperConfiguration.USPSAPISite = tbUSPSAPISite.Text;
                panelSideMenu.territoryHelperConfiguration.AddressVerificationUserId = tbAddressVerificationUserId.Text;
                panelSideMenu.territoryHelperConfiguration.APIType = tbAPIType.Text;
                panelSideMenu.territoryHelperConfiguration.BatchID = tbBatchId.Text;

                //Congregation Information
                panelSideMenu.territoryHelperConfiguration.KingdomHallLocationLatitude = double.Parse(tbKingdomHallLatitude.Text);
                panelSideMenu.territoryHelperConfiguration.KingdomHallLocationLongitude = double.Parse(tbKingdomHallLongitude.Text);
                panelSideMenu.territoryHelperConfiguration.KindgomHallAddress = tbKingdomHallAddress.Text;

                //File Input Output
                panelSideMenu.territoryHelperConfiguration.FileInputLocation = Path.Combine(AppContext.BaseDirectory, "Input");
                panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation = Path.Combine(AppContext.BaseDirectory, "Output");

                //Selenium Web Browser
                panelSideMenu.territoryHelperConfiguration.NumberOfTableRows = int.Parse(tbNumberofTableRows.Text);

                //Create Directory Paths if they do not exist
                CreateSubFoldersForInputandOutputFiles();

                //Change status to configuration settings indicator
                panelSideMenu.territoryHelperConfiguration.IsConfigurationSettingsLocked = true;

                ConfigurationValidator configurationValidator = new ConfigurationValidator();
                var validatedResult = configurationValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if (validatedResult.IsValid==true)
                {
                    //Messagebox complete
                    string message = "Configuration Settings have been successfully locked!";
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons);
                }
                else
                {
                    string errorsList = "THERE WERE SOME ERROR(S): \r\n \r\n";
                    foreach(var error in validatedResult.Errors)
                    {
                        errorsList = $"{errorsList} •{error} \r\n \r\n";
                    }

                    MessageBox.Show(errorsList,"ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace,ex.Message);
            }
            
        }

        private void CreateSubFoldersForInputandOutputFiles()
        {
            string fileInputDirectory= Path.Combine(AppContext.BaseDirectory, "Input");
            string fileOutputDirectory= Path.Combine(AppContext.BaseDirectory, "Output");

            if(!Directory.Exists(fileInputDirectory))
            {
                Directory.CreateDirectory(fileInputDirectory);
            }

            if(!Directory.Exists(fileOutputDirectory))
            {
                Directory.CreateDirectory(fileOutputDirectory);
            }

            panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation = fileOutputDirectory;
        }


    }
}
