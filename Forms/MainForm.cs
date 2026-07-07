using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymManagement.Forms
{
    public class MainForm : Form
    {
        private Label lblWelcome;
        private Button btnMembers;
        private Button btnClasses;
        private Button btnLogout;
        private Label lblInfo;

        private string currentUser;

        public MainForm(string username)
        {
            currentUser = username;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Gym Management System - Main Menu";
            this.Size = new Size(480, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblWelcome = new Label();
            lblWelcome.Text = "Welcome, " + currentUser + "!";
            lblWelcome.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Bold);
            lblWelcome.Location = new Point(80, 25);
            lblWelcome.Size = new Size(310, 30);
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;

            lblInfo = new Label();
            lblInfo.Text = "Select an option below to get started";
            lblInfo.Location = new Point(80, 60);
            lblInfo.Size = new Size(310, 20);
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.ForeColor = Color.Gray;

            // members button
            btnMembers = new Button();
            btnMembers.Text = "Manage Members";
            btnMembers.Location = new Point(150, 110);
            btnMembers.Size = new Size(170, 55);
            btnMembers.Font = new Font("Microsoft Sans Serif", 10f);
            btnMembers.Click += new EventHandler(btnMembers_Click);

            // classes button
            btnClasses = new Button();
            btnClasses.Text = "Manage Classes / Programs";
            btnClasses.Location = new Point(150, 185);
            btnClasses.Size = new Size(170, 55);
            btnClasses.Font = new Font("Microsoft Sans Serif", 10f);
            btnClasses.Click += new EventHandler(btnClasses_Click);

            // logout
            btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Location = new Point(185, 270);
            btnLogout.Size = new Size(100, 30);
            btnLogout.Click += new EventHandler(btnLogout_Click);

            this.Controls.Add(lblWelcome);
            this.Controls.Add(lblInfo);
            this.Controls.Add(btnMembers);
            this.Controls.Add(btnClasses);
            this.Controls.Add(btnLogout);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MembersForm membersForm = new MembersForm();
            membersForm.ShowDialog();
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            ClassesForm classesForm = new ClassesForm();
            classesForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        // when main form closes, close the whole app
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            // only close everything if logout was pressed - otherwise handled by buttons
        }
    }
}
