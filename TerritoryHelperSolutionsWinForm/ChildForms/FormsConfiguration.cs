using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerritoryHelperSolutionsWinForm;

namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    public partial class lblAddressVerificationUserId : Form
    {
        public static string folderInputDirectory = null;
        public static string folderOutputDirectory= null;

        public lblAddressVerificationUserId()
        {
            InitializeComponent();
        }

        private void btnSelectInputFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult=folderBrowserDialogInputFolder.ShowDialog();
            if(dialogResult== DialogResult.OK)
            {
                string fileDirectory=folderBrowserDialogInputFolder.SelectedPath;
                lblInputFolderPath.Text = fileDirectory;
                folderInputDirectory= fileDirectory;
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialogOutputFolder.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileDirectory = folderBrowserDialogOutputFolder.SelectedPath;
                lblOutputFolderPath.Text = fileDirectory;
                folderOutputDirectory = fileDirectory;
            }
        }

        private void lblAddressVerificationUserId_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectFolderInput_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialogInputFolder.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileDirectory = folderBrowserDialogInputFolder.SelectedPath;
                lblFolderInputDirectoryPath.Text = fileDirectory;
                folderInputDirectory = fileDirectory;
            }
        }

        private void btnSelectFolderOutput_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialogOutputFolder.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileDirectory = folderBrowserDialogOutputFolder.SelectedPath;
                lblOutputFolderDirectoryPath.Text = fileDirectory;
                folderOutputDirectory = fileDirectory;
            }
        }
    }
}
