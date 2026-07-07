using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GymManagement.Classes;
using GymManagement.Database;

namespace GymManagement.Forms
{
    public class MembersForm : Form
    {
        private DataGridView dgvMembers;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private DateTimePicker dtpDOB;
        private ComboBox cmbGender;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtTrainingProgram;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private TextBox txtSearch;
        private TextBox txtInstructorSearch;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnSearch;
        private Button btnSearchByInstructor;
        private Button btnLoadAll;

        private Label lblFirstName, lblLastName, lblDOB, lblGender, lblPhone;
        private Label lblAddress, lblTraining, lblStart, lblEnd, lblSearch, lblInstructor;
        private Label lblTitle;

        private DatabaseHelper db;
        private int selectedMemberID = -1;

        public MembersForm()
        {
            db = new DatabaseHelper();
            InitializeComponents();
            LoadMembers();
        }

        private void InitializeComponents()
        {
            this.Text = "Member Management";
            this.Size = new Size(980, 620);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Member Management";
            lblTitle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
            lblTitle.Location = new Point(10, 8);
            lblTitle.Size = new Size(220, 25);

            // ---- Left panel labels and inputs ----
            int leftX = 10;
            int inputX = 110;
            int inputW = 160;

            lblFirstName = new Label(); lblFirstName.Text = "First Name:"; lblFirstName.Location = new Point(leftX, 45); lblFirstName.Size = new Size(95, 20);
            txtFirstName = new TextBox(); txtFirstName.Location = new Point(inputX, 43); txtFirstName.Size = new Size(inputW, 22);

            lblLastName = new Label(); lblLastName.Text = "Last Name:"; lblLastName.Location = new Point(leftX, 75); lblLastName.Size = new Size(95, 20);
            txtLastName = new TextBox(); txtLastName.Location = new Point(inputX, 73); txtLastName.Size = new Size(inputW, 22);

            lblDOB = new Label(); lblDOB.Text = "Date of Birth:"; lblDOB.Location = new Point(leftX, 105); lblDOB.Size = new Size(95, 20);
            dtpDOB = new DateTimePicker(); dtpDOB.Location = new Point(inputX, 103); dtpDOB.Size = new Size(inputW, 22); dtpDOB.Format = DateTimePickerFormat.Short;

            lblGender = new Label(); lblGender.Text = "Gender:"; lblGender.Location = new Point(leftX, 135); lblGender.Size = new Size(95, 20);
            cmbGender = new ComboBox(); cmbGender.Location = new Point(inputX, 133); cmbGender.Size = new Size(inputW, 22);
            cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

            lblPhone = new Label(); lblPhone.Text = "Phone:"; lblPhone.Location = new Point(leftX, 165); lblPhone.Size = new Size(95, 20);
            txtPhone = new TextBox(); txtPhone.Location = new Point(inputX, 163); txtPhone.Size = new Size(inputW, 22);

            lblAddress = new Label(); lblAddress.Text = "Address:"; lblAddress.Location = new Point(leftX, 195); lblAddress.Size = new Size(95, 20);
            txtAddress = new TextBox(); txtAddress.Location = new Point(inputX, 193); txtAddress.Size = new Size(inputW, 22);

            lblTraining = new Label(); lblTraining.Text = "Training Prog:"; lblTraining.Location = new Point(leftX, 225); lblTraining.Size = new Size(95, 20);
            txtTrainingProgram = new TextBox(); txtTrainingProgram.Location = new Point(inputX, 223); txtTrainingProgram.Size = new Size(inputW, 22);

            lblStart = new Label(); lblStart.Text = "Start Date:"; lblStart.Location = new Point(leftX, 255); lblStart.Size = new Size(95, 20);
            dtpStartDate = new DateTimePicker(); dtpStartDate.Location = new Point(inputX, 253); dtpStartDate.Size = new Size(inputW, 22); dtpStartDate.Format = DateTimePickerFormat.Short;

            lblEnd = new Label(); lblEnd.Text = "End Date:"; lblEnd.Location = new Point(leftX, 285); lblEnd.Size = new Size(95, 20);
            dtpEndDate = new DateTimePicker(); dtpEndDate.Location = new Point(inputX, 283); dtpEndDate.Size = new Size(inputW, 22); dtpEndDate.Format = DateTimePickerFormat.Short;

            // buttons
            btnAdd = new Button(); btnAdd.Text = "Add Member"; btnAdd.Location = new Point(10, 325); btnAdd.Size = new Size(120, 28);
            btnAdd.Click += new EventHandler(btnAdd_Click);

            btnUpdate = new Button(); btnUpdate.Text = "Update"; btnUpdate.Location = new Point(140, 325); btnUpdate.Size = new Size(80, 28);
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            btnDelete = new Button(); btnDelete.Text = "Delete"; btnDelete.Location = new Point(10, 363); btnDelete.Size = new Size(80, 28);
            btnDelete.Click += new EventHandler(btnDelete_Click);

            btnClear = new Button(); btnClear.Text = "Clear"; btnClear.Location = new Point(100, 363); btnClear.Size = new Size(80, 28);
            btnClear.Click += new EventHandler(btnClear_Click);

            // search section
            lblSearch = new Label(); lblSearch.Text = "Search (ID/Name):"; lblSearch.Location = new Point(10, 410); lblSearch.Size = new Size(120, 20);
            txtSearch = new TextBox(); txtSearch.Location = new Point(10, 430); txtSearch.Size = new Size(180, 22);
            btnSearch = new Button(); btnSearch.Text = "Search"; btnSearch.Location = new Point(195, 428); btnSearch.Size = new Size(70, 25);
            btnSearch.Click += new EventHandler(btnSearch_Click);

            lblInstructor = new Label(); lblInstructor.Text = "Search by Instructor:"; lblInstructor.Location = new Point(10, 465); lblInstructor.Size = new Size(130, 20);
            txtInstructorSearch = new TextBox(); txtInstructorSearch.Location = new Point(10, 485); txtInstructorSearch.Size = new Size(180, 22);
            btnSearchByInstructor = new Button(); btnSearchByInstructor.Text = "Search"; btnSearchByInstructor.Location = new Point(195, 483); btnSearchByInstructor.Size = new Size(70, 25);
            btnSearchByInstructor.Click += new EventHandler(btnSearchByInstructor_Click);

            btnLoadAll = new Button(); btnLoadAll.Text = "Show All Members"; btnLoadAll.Location = new Point(10, 525); btnLoadAll.Size = new Size(150, 28);
            btnLoadAll.Click += new EventHandler(btnLoadAll_Click);

            // DataGridView on the right
            dgvMembers = new DataGridView();
            dgvMembers.Location = new Point(295, 40);
            dgvMembers.Size = new Size(660, 520);
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.MultiSelect = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.CellClick += new DataGridViewCellEventHandler(dgvMembers_CellClick);

            // add all controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblFirstName); this.Controls.Add(txtFirstName);
            this.Controls.Add(lblLastName); this.Controls.Add(txtLastName);
            this.Controls.Add(lblDOB); this.Controls.Add(dtpDOB);
            this.Controls.Add(lblGender); this.Controls.Add(cmbGender);
            this.Controls.Add(lblPhone); this.Controls.Add(txtPhone);
            this.Controls.Add(lblAddress); this.Controls.Add(txtAddress);
            this.Controls.Add(lblTraining); this.Controls.Add(txtTrainingProgram);
            this.Controls.Add(lblStart); this.Controls.Add(dtpStartDate);
            this.Controls.Add(lblEnd); this.Controls.Add(dtpEndDate);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
            this.Controls.Add(lblSearch); this.Controls.Add(txtSearch); this.Controls.Add(btnSearch);
            this.Controls.Add(lblInstructor); this.Controls.Add(txtInstructorSearch); this.Controls.Add(btnSearchByInstructor);
            this.Controls.Add(btnLoadAll);
            this.Controls.Add(dgvMembers);
        }

        private void LoadMembers()
        {
            try
            {
                List<Member> members = db.GetAllMembers();
                dgvMembers.DataSource = null;
                dgvMembers.DataSource = members;
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvMembers.Rows[e.RowIndex];
                selectedMemberID = Convert.ToInt32(row.Cells["MemberID"].Value);
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();

                if (row.Cells["DateOfBirth"].Value != null && row.Cells["DateOfBirth"].Value != DBNull.Value)
                    dtpDOB.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);

                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                txtPhone.Text = row.Cells["PhoneNumber"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtTrainingProgram.Text = row.Cells["TrainingProgram"].Value.ToString();

                if (row.Cells["MembershipStartDate"].Value != null && row.Cells["MembershipStartDate"].Value != DBNull.Value)
                    dtpStartDate.Value = Convert.ToDateTime(row.Cells["MembershipStartDate"].Value);

                if (row.Cells["MembershipEndDate"].Value != null && row.Cells["MembershipEndDate"].Value != DBNull.Value)
                    dtpEndDate.Value = Convert.ToDateTime(row.Cells["MembershipEndDate"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting row: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Member GetMemberFromForm()
        {
            Member m = new Member();
            m.FirstName = txtFirstName.Text.Trim();
            m.LastName = txtLastName.Text.Trim();
            m.DateOfBirth = dtpDOB.Value;
            m.Gender = cmbGender.Text;
            m.PhoneNumber = txtPhone.Text.Trim();
            m.Address = txtAddress.Text.Trim();
            m.TrainingProgram = txtTrainingProgram.Text.Trim();
            m.MembershipStartDate = dtpStartDate.Value;
            m.MembershipEndDate = dtpEndDate.Value;
            return m;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Member m = GetMemberFromForm();

                if (!m.IsValid())
                    throw new MemberValidationException("First name, last name and phone number are required.");

                db.AddMember(m);
                MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMembers();
            }
            catch (MemberValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMemberID == -1)
            {
                MessageBox.Show("Please select a member from the list first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Member m = GetMemberFromForm();
                m.MemberID = selectedMemberID;

                if (!m.IsValid())
                    throw new MemberValidationException("Required fields are missing.");

                db.UpdateMember(m);
                MessageBox.Show("Member updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMembers();
            }
            catch (MemberValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMemberID == -1)
            {
                MessageBox.Show("Select a member to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult res = MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                try
                {
                    db.DeleteMember(selectedMemberID);
                    MessageBox.Show("Member deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadMembers();
                }
                catch (DatabaseException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtTrainingProgram.Clear();
            cmbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            selectedMemberID = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term))
            {
                MessageBox.Show("Enter a search term.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<Member> results = db.SearchMembers(term);
                dgvMembers.DataSource = null;
                dgvMembers.DataSource = results;

                if (results.Count == 0)
                    MessageBox.Show("No members found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchByInstructor_Click(object sender, EventArgs e)
        {
            string instructor = txtInstructorSearch.Text.Trim();
            if (string.IsNullOrEmpty(instructor))
            {
                MessageBox.Show("Enter instructor name.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<Member> results = db.GetMembersByInstructor(instructor);
                dgvMembers.DataSource = null;
                dgvMembers.DataSource = results;

                if (results.Count == 0)
                    MessageBox.Show("No members found for that instructor.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtInstructorSearch.Clear();
            LoadMembers();
        }
    }
}
