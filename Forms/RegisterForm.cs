using System;
using System.Drawing;
using System.Windows.Forms;
using GymManagement.Classes;

namespace GymManagement.Forms
{
    public class RegisterForm : Form
    {
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirm;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private Button btnRegister;
        private Button btnCancel;

        private FileHandler fileHandler;

        public RegisterForm(FileHandler fh)
        {
            fileHandler = fh;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Register New Staff Account";
            this.Size = new Size(370, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "Create Staff Account";
            lblTitle.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold);
            lblTitle.Location = new Point(70, 15);
            lblTitle.Size = new Size(220, 25);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(40, 60);
            lblUsername.Size = new Size(80, 23);

            txtUsername = new TextBox();
            txtUsername.Location = new Point(130, 58);
            txtUsername.Size = new Size(180, 23);

            lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(40, 95);
            lblPassword.Size = new Size(80, 23);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(130, 93);
            txtPassword.Size = new Size(180, 23);
            txtPassword.PasswordChar = '*';

            lblConfirm = new Label();
            lblConfirm.Text = "Confirm:";
            lblConfirm.Location = new Point(40, 130);
            lblConfirm.Size = new Size(80, 23);

            txtConfirm = new TextBox();
            txtConfirm.Location = new Point(130, 128);
            txtConfirm.Size = new Size(180, 23);
            txtConfirm.PasswordChar = '*';

            btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new Point(100, 172);
            btnRegister.Size = new Size(80, 28);
            btnRegister.Click += new EventHandler(btnRegister_Click);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(195, 172);
            btnCancel.Size = new Size(80, 28);
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblConfirm);
            this.Controls.Add(txtConfirm);
            this.Controls.Add(btnRegister);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnRegister;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new MemberValidationException("Username and password cannot be empty.");

                if (username.Length < 4)
                    throw new MemberValidationException("Username must be at least 4 characters.");

                if (password.Length < 5)
                    throw new MemberValidationException("Password must be at least 5 characters.");

                if (password != confirm)
                    throw new MemberValidationException("Passwords do not match.");

                if (fileHandler.UserExists(username))
                    throw new MemberValidationException("Username already exists. Please choose another.");

                fileHandler.WriteUser(username, password);
                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (MemberValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
