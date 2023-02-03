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
            try
            {
                
                folderBrowserDialogAtoZFiles.Description = "Please select the folder with the A to Z database files";
                if (folderBrowserDialogAtoZFiles.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.AtoZDatbaseFilesPath = folderBrowserDialogAtoZFiles.SelectedPath;
                    int numberofFiles=Directory.GetFiles(folderBrowserDialogAtoZFiles.SelectedPath).Count();
                    MessageBox.Show($"{folderBrowserDialogAtoZFiles.SelectedPath} selected with {numberofFiles} and \r\n saved successfully", "Files");
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

        private void btnGetTerritoryNotes_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogTerritoryNotes.Filter = "Json Files|*.json";
                openFileDialogTerritoryNotes.FilterIndex = 1;
                openFileDialogTerritoryNotes.InitialDirectory = "c:\\";
                openFileDialogTerritoryNotes.Multiselect = false;
                if (openFileDialogTerritoryNotes.ShowDialog() == DialogResult.OK)
                {
                    panelSideMenu.territoryHelperConfiguration.TerritoryNotesPath = openFileDialogTerritoryNotes.FileName;
                    MessageBox.Show($"{openFileDialogTerritoryNotes.FileName} file \r\n saved successfully", "Files");
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
                    //Create Necessary Directories
                    CreateMapFileDirectoriesandPaths();

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

        //Map File Creation
        private void CreateMapFileDirectoriesandPaths()
        {
            //Clear xlsx folder
            var xlsxDirectory=new DirectoryInfo(panelSideMenu.territoryHelperConfiguration.AtoZXLSXFilesPath);
            if (xlsxDirectory.GetFiles().Length>0)
            {
                foreach (var file in xlsxDirectory.GetFiles())
                {
                    file.Delete();
                }
            }
            //Grab all files and subfolders and copy to another directory
            var sourcePath = Path.Combine(AppContext.BaseDirectory, "StaticContents");
            var targetPath = panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation;
            CopyFilesRecursively(sourcePath, targetPath);

            //Rename Map Directory
            var oldName = Path.Combine(targetPath, "Map");
            var mapPath = Path.Combine(targetPath, $"Map-{DateTime.Now.ToString("MM-dd-yyyy")}");
            if (Directory.Exists(mapPath))
            {
                //If files already exists delete them
                DirectoryInfo di = new DirectoryInfo(mapPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            else
            {
                Directory.Move(oldName, mapPath);
            }
            
            //Create Address Directory Paths for generated files
            panelSideMenu.territoryHelperConfiguration.AtoZExistingAddressesJSFilePath = Path.Combine(mapPath,"js","information","Addresses", "existingAddresses.js");
            panelSideMenu.territoryHelperConfiguration.AtoZExistingAddressesJSFilePath = Path.Combine(mapPath, "js", "information", "Addresses", "newAddresses.js"); 

            //Create Territory Directory Path for generated files
            panelSideMenu.territoryHelperConfiguration.AtoZTerritoriesJSFilePath = Path.Combine(mapPath,"js","information","Territories", "territories.js");
        }

        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        
    }
}
