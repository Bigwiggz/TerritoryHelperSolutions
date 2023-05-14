using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerritoryHelperSolutionsWinForm.Validators;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class FormAddressErrorScanner : Form
    {
        public FormAddressErrorScanner()
        {
            InitializeComponent();
        }

        private void btnSelectAddressFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogInput.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                openFileDialogInput.FilterIndex = 1;
                openFileDialogInput.InitialDirectory = "c:\\";
                openFileDialogInput.Multiselect = false;
                if (openFileDialogInput.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath = openFileDialogInput.FileName;
                    MessageBox.Show($"{openFileDialogInput.FileName} path \n\r saved successfully", "ExcelFilePath");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }

        private void btnRunAddressErrorScannerScript_Click(object sender, EventArgs e)
        {
            try
            {
                var addressErrorScannerValidator = new AddressScannerValidator();
                var validatedResult = addressErrorScannerValidator.Validate(panelSideMenu.territoryHelperConfiguration);
                if(validatedResult.IsValid)
                {
                    var existingAddressesFile = new FileInfo(panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath);
                    panelSideMenu.territoryHelperConfiguration.ExistingSpanishAddressesFilePath = existingAddressesFile.FullName;

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
            catch (Exception)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
