using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GymManagement.Classes;
using GymManagement.Database;

namespace GymManagement.Forms
{
    public class ClassesForm : Form
    {
        private DataGridView dgvClasses;
        private TextBox txtClassName;
        private TextBox txtDescription;
        private TextBox txtInstructor;
        private TextBox txtSchedule;
        private TextBox txtCapacity;
        private TextBox txtDuration;
        private TextBox txtSearch;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnSearch;
        private Button btnLoadAll;

        private Label lblTitle;
        private Label lblClassName, lblDesc, lblInstructor, lblSchedule, lblCapacity, lblDuration, lblSearch;

        private DatabaseHelper db;
        private int selectedClassID = -1;

        public ClassesForm()
        {
            db = new DatabaseHelper();
            InitializeComponents();
            LoadClasses();
        }

        private void InitializeComponents()
        {
            this.Text = "Class & Training Program Management";
            this.Size = new Size(920, 560);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Classes & Training Programs";
            lblTitle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
            lblTitle.Location = new Point(10, 8);
            lblTitle.Size = new Size(260, 25);

            int leftX = 10;
            int inputX = 110;
            int inputW = 160;

            lblClassName = new Label(); lblClassName.Text = "Class Name:"; lblClassName.Location = new Point(leftX, 45); lblClassName.Size = new Size(95, 20);
            txtClassName = new TextBox(); txtClassName.Location = new Point(inputX, 43); txtClassName.Size = new Size(inputW, 22);

            lblDesc = new Label(); lblDesc.Text = "Description:"; lblDesc.Location = new Point(leftX, 75); lblDesc.Size = new Size(95, 20);
            txtDescription = new TextBox(); txtDescription.Location = new Point(inputX, 73); txtDescription.Size = new Size(inputW, 22);

            lblInstructor = new Label(); lblInstructor.Text = "Instructor:"; lblInstructor.Location = new Point(leftX, 105); lblInstructor.Size = new Size(95, 20);
            txtInstructor = new TextBox(); txtInstructor.Location = new Point(inputX, 103); txtInstructor.Size = new Size(inputW, 22);

            lblSchedule = new Label(); lblSchedule.Text = "Schedule:"; lblSchedule.Location = new Point(leftX, 135); lblSchedule.Size = new Size(95, 20);
            txtSchedule = new TextBox(); txtSchedule.Location = new Point(inputX, 133); txtSchedule.Size = new Size(inputW, 22);

            lblCapacity = new Label(); lblCapacity.Text = "Capacity:"; lblCapacity.Location = new Point(leftX, 165); lblCapacity.Size = new Size(95, 20);
            txtCapacity = new TextBox(); txtCapacity.Location = new Point(inputX, 163); txtCapacity.Size = new Size(inputW, 22);

            lblDuration = new Label(); lblDuration.Text = "Duration:"; lblDuration.Location = new Point(leftX, 195); lblDuration.Size = new Size(95, 20);
            txtDuration = new TextBox(); txtDuration.Location = new Point(inputX, 193); txtDuration.Size = new Size(inputW, 22);

            btnAdd = new Button(); btnAdd.Text = "Add Class"; btnAdd.Location = new Point(10, 240); btnAdd.Size = new Size(100, 28);
            btnAdd.Click += new EventHandler(btnAdd_Click);

            btnUpdate = new Button(); btnUpdate.Text = "Update"; btnUpdate.Location = new Point(120, 240); btnUpdate.Size = new Size(80, 28);
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            btnDelete = new Button(); btnDelete.Text = "Delete"; btnDelete.Location = new Point(10, 278); btnDelete.Size = new Size(80, 28);
            btnDelete.Click += new EventHandler(btnDelete_Click);

            btnClear = new Button(); btnClear.Text = "Clear"; btnClear.Location = new Point(100, 278); btnClear.Size = new Size(80, 28);
            btnClear.Click += new EventHandler(btnClear_Click);

            lblSearch = new Label(); lblSearch.Text = "Search:"; lblSearch.Location = new Point(10, 330); lblSearch.Size = new Size(60, 20);
            txtSearch = new TextBox(); txtSearch.Location = new Point(10, 350); txtSearch.Size = new Size(175, 22);
            btnSearch = new Button(); btnSearch.Text = "Search"; btnSearch.Location = new Point(190, 348); btnSearch.Size = new Size(70, 25);
            btnSearch.Click += new EventHandler(btnSearch_Click);

            btnLoadAll = new Button(); btnLoadAll.Text = "Show All"; btnLoadAll.Location = new Point(10, 390); btnLoadAll.Size = new Size(100, 28);
            btnLoadAll.Click += new EventHandler(btnLoadAll_Click);

            dgvClasses = new DataGridView();
            dgvClasses.Location = new Point(285, 40);
            dgvClasses.Size = new Size(615, 470);
            dgvClasses.ReadOnly = true;
            dgvClasses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClasses.MultiSelect = false;
            dgvClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClasses.AllowUserToAddRows = false;
            dgvClasses.CellClick += new DataGridViewCellEventHandler(dgvClasses_CellClick);

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblClassName); this.Controls.Add(txtClassName);
            this.Controls.Add(lblDesc); this.Controls.Add(txtDescription);
            this.Controls.Add(lblInstructor); this.Controls.Add(txtInstructor);
            this.Controls.Add(lblSchedule); this.Controls.Add(txtSchedule);
            this.Controls.Add(lblCapacity); this.Controls.Add(txtCapacity);
            this.Controls.Add(lblDuration); this.Controls.Add(txtDuration);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
            this.Controls.Add(lblSearch); this.Controls.Add(txtSearch); this.Controls.Add(btnSearch);
            this.Controls.Add(btnLoadAll);
            this.Controls.Add(dgvClasses);
        }

        private void LoadClasses()
        {
            try
            {
                List<GymClass> classes = db.GetAllClasses();
                dgvClasses.DataSource = null;
                dgvClasses.DataSource = classes;
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvClasses.Rows[e.RowIndex];
                selectedClassID = Convert.ToInt32(row.Cells["ClassID"].Value);
                txtClassName.Text = row.Cells["ClassName"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtInstructor.Text = row.Cells["Instructor"].Value.ToString();
                txtSchedule.Text = row.Cells["Schedule"].Value.ToString();
                txtCapacity.Text = row.Cells["Capacity"].Value.ToString();
                txtDuration.Text = row.Cells["Duration"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Row selection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private GymClass GetClassFromForm()
        {
            GymClass gc = new GymClass();
            gc.ClassName = txtClassName.Text.Trim();
            gc.Description = txtDescription.Text.Trim();
            gc.Instructor = txtInstructor.Text.Trim();
            gc.Schedule = txtSchedule.Text.Trim();
            gc.Duration = txtDuration.Text.Trim();

            int cap = 0;
            int.TryParse(txtCapacity.Text.Trim(), out cap);
            gc.Capacity = cap;

            return gc;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GymClass gc = GetClassFromForm();

                if (string.IsNullOrEmpty(gc.ClassName))
                    throw new MemberValidationException("Class name is required.");

                if (gc.Capacity <= 0)
                    throw new MemberValidationException("Capacity must be a number greater than 0.");

                db.AddClass(gc);
                MessageBox.Show("Class added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadClasses();
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
            if (selectedClassID == -1)
            {
                MessageBox.Show("Please select a class from the list.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                GymClass gc = GetClassFromForm();
                gc.ClassID = selectedClassID;

                if (string.IsNullOrEmpty(gc.ClassName))
                    throw new MemberValidationException("Class name cannot be empty.");

                db.UpdateClass(gc);
                MessageBox.Show("Class updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadClasses();
            }
            catch (MemberValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedClassID == -1)
            {
                MessageBox.Show("Select a class to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult res = MessageBox.Show("Delete this class?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                try
                {
                    db.DeleteClass(selectedClassID);
                    MessageBox.Show("Class deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadClasses();
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
            txtClassName.Clear();
            txtDescription.Clear();
            txtInstructor.Clear();
            txtSchedule.Clear();
            txtCapacity.Clear();
            txtDuration.Clear();
            selectedClassID = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term))
            {
                MessageBox.Show("Enter a class name or instructor to search.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<GymClass> results = db.SearchClasses(term);
                dgvClasses.DataSource = null;
                dgvClasses.DataSource = results;

                if (results.Count == 0)
                    MessageBox.Show("No classes found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadClasses();
        }
    }
}
