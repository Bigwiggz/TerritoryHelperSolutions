using FontAwesome.Sharp;
using System.Drawing;
using System.Runtime.InteropServices;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.TopLevelServices.Import;
using TerritoryHelperSolutionsWinForm.ChildForms;

namespace TerritoryHelperSolutionsWinForm
{
    public partial class panelSideMenu : Form
    {
        //Fields
        private IconButton currentBtn;
        private Form currentChildForm;

        //Data Fields
        public static TerritoryHelperConfiguration territoryHelperConfiguration=new TerritoryHelperConfiguration();
        public static TerritoryHelperServices territoryHelperService=new TerritoryHelperServices();

        public panelSideMenu()
        {
            InitializeComponent();

            //Form
            this.Text=string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered= true;
            this.MaximizedBounds=Screen.FromHandle(this.Handle).WorkingArea;
        }

        //https://www.youtube.com/watch?v=JP5rgXO_5Sk&ab_channel=RJCodeAdvanceEN
        //https://www.youtube.com/watch?v=5AsJJl7Bhvc&ab_channel=RJCodeAdvanceEN

        //Colors
        private struct RBGColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4= Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(11, 7, 17);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        //Method
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn!=null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(47, 79, 79);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //change icon
                iconCurrentChildForm.IconChar=currentBtn.IconChar;
                iconCurrentChildForm.IconColor = RBGColors.color1;

                //Change text
                lblTitleChildForm.Text=currentBtn.Text;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm!=null)
            {
                //open only form
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.TopMost = true;
            childForm.FormBorderStyle=FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag=childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormInstructions());
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new formConfiguration());
        }

        private void btnGetTerritoryInformation_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormGetTerritoryInformation());
        }

        private void btnSaveTerritoryInformation_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormSaveTerritoryInformation());
        }

        private void btnSearchAToZDB_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormSearchAtoZDatabase());    
        }

        private void btnUpdateCensusTerritories_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormUpdateCensusTerritories());   
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RBGColors.color1);
            OpenChildForm(new FormHelpConfiguration());
        }

        private void btnHome2_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void btnHome1_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.White;
            lblTitleChildForm.Text = "Home";
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void panelSideMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lblMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState=FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}