using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GymManagement.Classes;

namespace GymManagement.Forms
{
    public class LoginForm : Form
    {
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Button btnUnlockAccount;
        private Label lblAttempts;

        private FileHandler fileHandler;
        private int loginAttempts = 0;

        public LoginForm()
        {
            fileHandler = new FileHandler();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Gym Management System - Login";
            this.Size = new Size(400, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "Gym Management System";
            lblTitle.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Bold);
            lblTitle.Location = new Point(60, 20);
            lblTitle.Size = new Size(280, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(60, 75);
            lblUsername.Size = new Size(80, 23);

            txtUsername = new TextBox();
            txtUsername.Location = new Point(150, 73);
            txtUsername.Size = new Size(170, 23);

            lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(60, 110);
            lblPassword.Size = new Size(80, 23);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(150, 108);
            txtPassword.Size = new Size(170, 23);
            txtPassword.PasswordChar = '*';

            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new Point(150, 150);
            btnLogin.Size = new Size(80, 28);
            btnLogin.Click += new EventHandler(btnLogin_Click);

            btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new Point(240, 150);
            btnRegister.Size = new Size(80, 28);
            btnRegister.Click += new EventHandler(btnRegister_Click);

            btnUnlockAccount = new Button();
            btnUnlockAccount.Text = "Unlock Account (Admin)";
            btnUnlockAccount.Location = new Point(100, 195);
            btnUnlockAccount.Size = new Size(190, 28);
            btnUnlockAccount.Click += new EventHandler(btnUnlockAccount_Click);

            lblAttempts = new Label();
            lblAttempts.Text = "";
            lblAttempts.ForeColor = Color.Red;
            lblAttempts.Location = new Point(60, 235);
            lblAttempts.Size = new Size(280, 23);
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegister);
            this.Controls.Add(btnUnlockAccount);
            this.Controls.Add(lblAttempts);

            // press enter to login
            this.AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // check if account is locked
                if (fileHandler.IsAccountLocked(username))
                {
                    throw new AccountLockedException("Account '" + username + "' is locked. Please contact an admin.");
                }

                bool valid = fileHandler.ValidateLogin(username, password);

                if (valid)
                {
                    loginAttempts = 0;
                    MainForm mainForm = new MainForm(username);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    loginAttempts++;
                    throw new InvalidLoginException("Incorrect username or password. Attempt " + loginAttempts + " of 3.");
                }
            }
            catch (AccountLockedException ex)
            {
                MessageBox.Show(ex.Message, "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblAttempts.Text = "Account is locked.";
            }
            catch (InvalidLoginException ex)
            {
                lblAttempts.Text = ex.Message;

                if (loginAttempts >= 3)
                {
                    fileHandler.LockAccount(username);
                    MessageBox.Show("Too many failed attempts. Account has been locked.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    loginAttempts = 0;
                }
                else
                {
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm(fileHandler);
            regForm.ShowDialog();
        }

        private void btnUnlockAccount_Click(object sender, EventArgs e)
        {
            // simple admin unlock - only admin can unlock
            string adminPass = Microsoft.VisualBasic.Interaction.InputBox("Enter admin password to unlock accounts:", "Admin Unlock", "");

            if (adminPass != "admin123")
            {
                MessageBox.Show("Incorrect admin password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> locked = fileHandler.GetLockedAccounts();

            if (locked.Count == 0)
            {
                MessageBox.Show("No locked accounts found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string accountToUnlock = Microsoft.VisualBasic.Interaction.InputBox(
                "Locked accounts:\n" + string.Join("\n", locked) + "\n\nEnter username to unlock:",
                "Unlock Account", "");

            if (!string.IsNullOrEmpty(accountToUnlock))
            {
                fileHandler.UnlockAccount(accountToUnlock);
                MessageBox.Show("Account '" + accountToUnlock + "' has been unlocked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAttempts.Text = "";
            }
        }
    }
}
