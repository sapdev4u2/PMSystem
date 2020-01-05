using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PMOSYS
{
    public partial class frmIssues : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        public frmIssues()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects where Project_SNo = " + txtProjectNo.Text;
            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(@"Not found any project has this number: (" + txtProjectNo.Text + ")");
                return;
            }
            else
            {
                txtProjectNo.Text = dt.Rows[0][0].ToString();
                txtProjectNameEng.Text = dt.Rows[0][1].ToString();
                txtProjectNameArb.Text = dt.Rows[0][2].ToString();
                txtStatus.Text = dt.Rows[0][4].ToString();
                ShowAllIssues();
                Format_DGV2();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 141)
            {
                panel1.Height = 363;
            }
            else
            {
                panel1.Height = 141;
            }
        }

        void Format_DGV2()
        {
            dataGridView2.Columns[0].HeaderText = "Project #";
            dataGridView2.Columns[1].HeaderText = "Project Name (Eng)";
            dataGridView2.Columns[2].HeaderText = "Project Name (Arb)";
            dataGridView2.Columns[3].HeaderText = "PO #";
            dataGridView2.Columns[3].HeaderText = "Status";

            for (int x = 0; x < dataGridView2.Rows.Count; x++)
            {
                if (dataGridView2.Rows[x].Cells[4].Value.ToString() == "Finish")
                {
                    dataGridView2.Rows[x].DefaultCellStyle.BackColor = Color.LightSalmon;

                }
                if (dataGridView2.Rows[x].Cells[4].Value.ToString() == "Completed")
                {
                    dataGridView2.Rows[x].DefaultCellStyle.BackColor = Color.Gold;

                }
                if (dataGridView2.Rows[x].Cells[4].Value.ToString() == "Start")
                {
                    dataGridView2.Rows[x].DefaultCellStyle.BackColor = Color.GreenYellow;

                }

            }


        }
        void ShowAllIssues()
        {
            dataGridView1.Columns.Clear();
            string sql_Str = @"select * from Issues where MI_ProjectNo = " + txtProjectNo.Text;
            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;
         
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(@"No any issues was registered for this project: (" + txtProjectNo.Text + ")");
               // Format_DGV1();
                return;
            }
            else
            {
                dataGridView1.DataSource = dt;
               // Format_DGV1();
                txtTotalOfRisk.Text = dt.Rows.Count.ToString();

            }

        }
        private void frmIssues_Load(object sender, EventArgs e)
        {
            panel1.Height = 141;
            Load_All_Projects();
        }
        void Load_All_Projects()
        {
            dataGridView2.DataSource = null;
            string sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects ";
            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView2.DataSource = dt;
            Format_DGV2();
        }
       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text) || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Write the search value ");
                txtSearch.Focus();
                return;
            }
            dataGridView2.DataSource = dt;
            string sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where ProjectNameE like '%" + txtSearch.Text + "%'";
            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView2.DataSource = dt;
            Format_DGV2();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProjectNo.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtProjectNameEng.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtProjectNameArb.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtStatus.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            panel1.Height = 141;
            btnCheck.PerformClick();
            Format_DGV2();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            ResetAllControls();
            cmbStatus.SelectedIndex = 1;
            panel2.Visible = true;
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            GenerateIssueNo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetAllControls();
            cmbStatus.SelectedIndex = 1;
            panel2.Visible = false;
        }

        void ResetAllControls()
        {



            foreach (Control c in panel2.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            cmbStatus.SelectedIndex = 1;
        }
        void GenerateIssueNo()
        {
            try
            {

                SqlDataAdapter da1 = new SqlDataAdapter(@"select MAX(MI_SN)+1 from Issues", ConCls.con);
                DataTable dt1 = new DataTable();
                ConCls.OpenConnection();
                da1.Fill(dt1);
                ConCls.CloseConnection();
                //   MessageBox.Show("Count :" + dt1.Rows.Count.ToString());
                if (dt1.Rows.Count == 0)
                {
                    txtIssueNo.Text = "1";
                    // MessageBox.Show("1");
                    return;

                }
                else
                {
                    txtIssueNo.Text = dt1.Rows[0][0].ToString();
                    //MessageBox.Show(dt1.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong while the record has been saved, Plase try agine.", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // GenerateIssueNo();

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO Issues
                                    (MI_SN
                                   ,MI_ProjectNo
                                   ,MI_IssueNameEng
                                   ,MI_IssueNameArb
                                   ,MI_Status
                                   ,MI_CorrectiveAction
                                   ,MI_ActionBy
                                   ,MI_DueDate
                                   ,MI_IssueCreateDate)
                                    VALUES
                                    (
                                    @MI_SN
                                   ,@MI_ProjectNo
                                   ,@MI_IssueNameEng
                                   ,@MI_IssueNameArb
                                   ,@MI_Status
                                   ,@MI_CorrectiveAction
                                   ,@MI_ActionBy
                                   ,@MI_DueDate
                                   ,@MI_IssueCreateDate
                                    )";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@MI_SN", SqlDbType.Int).Value = Convert.ToInt32(txtIssueNo.Text);
                cmd.Parameters.Add("@MI_ProjectNo", SqlDbType.Int).Value = Convert.ToInt32(txtProjectNo.Text);
                cmd.Parameters.Add("@MI_IssueNameEng", SqlDbType.NVarChar).Value = txtIssueNameEng.Text;
                cmd.Parameters.Add("@MI_IssueNameArb", SqlDbType.NVarChar).Value = txtIssueNameArb.Text;
                cmd.Parameters.Add("@MI_Status", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
                cmd.Parameters.Add("@MI_ActionBy", SqlDbType.NVarChar).Value = txtActionBy.Text;
                cmd.Parameters.Add("@MI_CorrectiveAction", SqlDbType.NVarChar).Value = txtCorrectiveAction.Text;
                cmd.Parameters.Add("@MI_DueDate", SqlDbType.Date).Value = dtpDueDate.Value.Date;
                cmd.Parameters.Add("@MI_IssueCreateDate", SqlDbType.Date).Value = DateTime.Now.Date;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Issue added successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
                cmd.Dispose();
                ShowAllIssues();
                //Format_DGV1();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong while the record has been saved, Plase try agine.", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                ConCls.OpenConnection();
                string sql = @"update Issues set
                                    MI_IssueNameEng = @MI_IssueNameEng
                                   ,MI_IssueNameArb =@MI_IssueNameArb
                                   ,MI_Status = @MI_Status
                                   ,MI_CorrectiveAction = @MI_CorrectiveAction
                                   ,MI_ActionBy =@MI_ActionBy 
                                   ,MI_DueDate = @MI_DueDate
                                   ,MI_IssueCreateDate =@MI_IssueCreateDate 
                                    where MI_SN = " + txtIssueNo.Text;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@MI_IssueNameEng", SqlDbType.NVarChar).Value = txtIssueNameEng.Text;
                cmd.Parameters.Add("@MI_IssueNameArb", SqlDbType.NVarChar).Value = txtIssueNameArb.Text;
                cmd.Parameters.Add("@MI_Status", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
                cmd.Parameters.Add("@MI_ActionBy", SqlDbType.NVarChar).Value = txtActionBy.Text;
                cmd.Parameters.Add("@MI_CorrectiveAction", SqlDbType.NVarChar).Value = txtCorrectiveAction.Text;
                cmd.Parameters.Add("@MI_DueDate", SqlDbType.NVarChar).Value = dtpDueDate.Value.Date;
                cmd.Parameters.Add("@MI_IssueCreateDate", SqlDbType.NVarChar).Value = DateTime.Now.Date;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Risk number : " + txtIssueNo.Text + " updated successfully", "Updated record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
                cmd.Dispose();
                ShowAllIssues();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong while the record has been saved, Plase try agine.", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
