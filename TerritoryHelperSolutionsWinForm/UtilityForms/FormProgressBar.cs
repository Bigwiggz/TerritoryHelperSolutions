using ExcelMigration.ExcelInterop;
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
using TerritoryHelperClassLibrary.BaseServices.GeoMapping;
using TerritoryHelperClassLibrary.Models.UtilityModels;
using TerritoryHelperSolutionsWinForm.Models;

//https://www.youtube.com/watch?v=ZTKGRJy5P2M&ab_channel=IAmTimCorey

namespace TerritoryHelperSolutionsWinForm.UtilityForms
{
    public partial class FormProgressBar : Form
    {
        //Fields
        private Stopwatch stopWatch;
        private readonly string _scriptName;

        public FormProgressBar(string scriptName)
        {
            InitializeComponent();
            _scriptName = scriptName;
        }

        private async void FormProgressBar_Load(object sender, EventArgs e)
        {
            stopWatch=new Stopwatch();

            stopWatch.Start();

            Progress<ProgressReportModel> progress=new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            Progress<LowerLeverProgressReportModel> lowerProgress = new Progress<LowerLeverProgressReportModel>();
            lowerProgress.ProgressChanged += LowerReportProgress;

            //Get Territory Information Script
            if (_scriptName==ScriptName.GetTerritoryInformation)
            {
                await panelSideMenu.territoryHelperService.ImportDataFromTerritoryHelper(panelSideMenu.territoryHelperConfiguration, progress,lowerProgress);

                openFileDialogOutput.Filter = "Excel Files|*.xlsx;";
                openFileDialogOutput.FilterIndex = 1;
                openFileDialogOutput.InitialDirectory = panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation;
                openFileDialogOutput.Multiselect = false;
                if (openFileDialogOutput.ShowDialog() == DialogResult.OK)
                {

                }

                stopWatch.Stop();
            }

            //Save Territory Information Script
            if (_scriptName == ScriptName.SaveTerritoryInformation)
            {
                await panelSideMenu.territoryHelperService.UpdateTerritoryHelperUsingMasterRecord(panelSideMenu.territoryHelperConfiguration, progress, lowerProgress);

                openFileDialogOutput.Filter = null;
                openFileDialogOutput.FilterIndex = 1;
                openFileDialogOutput.InitialDirectory = panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation;
                openFileDialogOutput.Multiselect = false;
                if (openFileDialogOutput.ShowDialog() == DialogResult.OK)
                {

                }

                stopWatch.Stop();
            }

            //Run A to Z Database Information Script
            if (_scriptName == ScriptName.SearchAToZDatabaseInformation)
            {
                var config = panelSideMenu.territoryHelperConfiguration;

                //Run xls to xlsx converter
                await Task.Run(()=>
                { 
                    
                    IProgress<ProgressReportModel> testProgress=new Progress<ProgressReportModel>();
                    ProgressReportModel report = new ProgressReportModel();
                    report.TopLevelProgressMessage = "Converting xls files to xlsx (This may take some time)...";
                    report.TopLevelPercentComplete = 5;
                    testProgress.Report(report);
                    
                    //TODO: Add in Progress Reporting
                    var excelInteropService = new ExcelInteropConverterService();
                    excelInteropService.ConverterExcelService(panelSideMenu.territoryHelperConfiguration.AtoZDatbaseFilesPath, panelSideMenu.territoryHelperConfiguration.AtoZXLSXFilesPath);
                });

                //Run main program
                await panelSideMenu.territoryHelperService.ImportAtoZDatabaseAddresses(panelSideMenu.territoryHelperConfiguration, progress, lowerProgress);

                openFileDialogOutput.Filter = null;
                openFileDialogOutput.FilterIndex = 1;
                openFileDialogOutput.InitialDirectory = panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation;
                openFileDialogOutput.Multiselect = false;
                if (openFileDialogOutput.ShowDialog() == DialogResult.OK)
                {

                }

                stopWatch.Stop();
            }

            //Update CENSO Records
            if (_scriptName == ScriptName.UpdateCensusTerritoryInformation)
            {
                await panelSideMenu.territoryHelperService.UpdateCENSOTerritoryHelperUsingMasterRecord(panelSideMenu.territoryHelperConfiguration, progress, lowerProgress);

                openFileDialogOutput.Filter = null;
                openFileDialogOutput.FilterIndex = 1;
                openFileDialogOutput.InitialDirectory = panelSideMenu.territoryHelperConfiguration.FileSavedOutputLocation;
                openFileDialogOutput.Multiselect = false;
                if (openFileDialogOutput.ShowDialog() == DialogResult.OK)
                {

                }

                stopWatch.Stop();
            }
        }

        private void LowerReportProgress(object? sender, LowerLeverProgressReportModel e)
        {
            progressBarSubTask.Value = e.LowerLevelProcessPercentComplete;
            lblSubTaskProcessing.Text = $"Sub Task Processing...{e.LowerLevelProcessPercentComplete}%";
            lblSubTaskMessage.Text = $"{e.LowerLevelProcessMessage}";
        }

        private void ReportProgress(object? sender, ProgressReportModel e)
        {
            progressBar.Value = e.TopLevelPercentComplete;
            lblStatus.Text = $"Main Task Processing...{e.TopLevelPercentComplete}%";
            lblWorkLabel.Text = $"{e.TopLevelProgressMessage}";

        }

        private void timerElapsedTime_Tick(object sender, EventArgs e)
        {
            this.lblTotalElapsedTime.Text= string.Format("Time Elapsed: {0:hh\\:mm\\:ss}",stopWatch.Elapsed);
        }
    }
}
