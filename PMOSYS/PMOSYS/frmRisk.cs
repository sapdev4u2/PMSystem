using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMOSYS
{
    public partial class frmRisk : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        public frmRisk()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

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

        private void frmRisk_Load(object sender, EventArgs e)
        {
            panel1.Height = 141;
            Load_All_Projects();

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

        void Format_DGV1()
        {
            // MR_SN
            //,MR_ProjectNo
            //,MR_RiskNameEng
            //,MR_RiskNameArb
            //,MR_Score
            //,MR_Owner
            //,MR_MitigationStrategy

            dataGridView1.Columns[0].HeaderText = "Major Risk #";
            dataGridView1.Columns[1].HeaderText = "Project #";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Risk Name (Eng)";
            dataGridView1.Columns[3].HeaderText = "Risk Name (Arb)";
            dataGridView1.Columns[4].HeaderText = "Risk Score";
            dataGridView1.Columns[5].HeaderText = "Risk Owner";
            dataGridView1.Columns[6].HeaderText = "Mitigation Strategy";

            for (int x = 0; x < dataGridView1.Rows.Count; x++)
            {
                if (dataGridView1.Rows[x].Cells[4].Value.ToString() == "High")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.LightSalmon;

                }
                if (dataGridView1.Rows[x].Cells[4].Value.ToString() == "Medium")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.Gold;

                }
                if (dataGridView1.Rows[x].Cells[4].Value.ToString() == "Low")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.GreenYellow;

                }

            }

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Edit";
            btn.Text = "Edit";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn1);
            btn1.HeaderText = "Delete";
            btn1.Text = "Delete";
            btn1.Name = "btn1";
            btn1.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn2);
            btn2.HeaderText = "Convert to (ISSUE)";
            btn2.Text = "Convert";
            btn2.Name = "btn2";
            btn2.UseColumnTextForButtonValue = true;

            //dataGridView1.Columns[9].HeaderCell.Style.BackColor = Color.OrangeRed;

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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 7)
            //{
            //panel1.Visible = true;
            //btnSave.Visible = false;
            //btnAddNew.Enabled = false;
            //btnUpdate.Visible = true;

            txtProjectNo.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtProjectNameEng.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtProjectNameArb.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtStatus.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            panel1.Height = 141;
            btnCheck.PerformClick();
            Format_DGV2();


            //}
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
                ShowAllRisk();
                Format_DGV2();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtStatus.Text))
            //{
            //    MessageBox.Show("If you are not registered the status, go to project data and update the project data", "Missing (Project Status) value", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}
            //if (txtStatus.Text != "Strat" || txtStatus.Text != "Running")
            //{
            //    MessageBox.Show(@"You can not add risk for this project while the project status is :" + txtStatus.Text + ". \n go to project data and update the project data", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            ResetAllControls();
            cmbScore.SelectedIndex = 1;
            panel2.Visible = true;
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            GenerateRiskNo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetAllControls();
            cmbScore.SelectedIndex = 1;
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
            cmbScore.SelectedIndex = 0;
        }
        void ShowAllRisk()
        {
            dataGridView1.Columns.Clear();
            string sql_Str = @"select * from Risks where MR_ProjectNo = " + txtProjectNo.Text;
            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;
            //Format_DGV1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(@"No any risk was registered for this project: (" + txtProjectNo.Text + ")");
                Format_DGV1();
                return;
            }
            else
            {
                dataGridView1.DataSource = dt;
                Format_DGV1();
                txtTotalOfRisk.Text = dt.Rows.Count.ToString();

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {



            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO Risks
                                    (MR_SN
                                   ,MR_ProjectNo
                                   ,MR_RiskNameEng
                                   ,MR_RiskNameArb
                                   ,MR_Score
                                   ,MR_Owner
                                   ,MR_MitigationStrategy)
                                    VALUES
                                    (
                                    @MR_SN
                                   ,@MR_ProjectNo
                                   ,@MR_RiskNameEng
                                   ,@MR_RiskNameArb
                                   ,@MR_Score
                                   ,@MR_Owner
                                   ,@MR_MitigationStrategy)";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@MR_SN", SqlDbType.Int).Value = Convert.ToInt32(txtRiskNo.Text);
                cmd.Parameters.Add("@MR_ProjectNo", SqlDbType.Int).Value = Convert.ToInt32(txtProjectNo.Text);
                cmd.Parameters.Add("@MR_RiskNameEng", SqlDbType.NVarChar).Value = txtRiskNameEng.Text;
                cmd.Parameters.Add("@MR_RiskNameArb", SqlDbType.NVarChar).Value = txtRiskNameArb.Text;
                cmd.Parameters.Add("@MR_Score", SqlDbType.NVarChar).Value = cmbScore.SelectedItem.ToString();
                cmd.Parameters.Add("@MR_Owner", SqlDbType.NVarChar).Value = txtRiskOwner.Text;
                cmd.Parameters.Add("@MR_MitigationStrategy", SqlDbType.NVarChar).Value = txtMitigationStrategy.Text;
                MessageBox.Show(" Risk No: " + txtRiskNo.Text + "\nProject No: " + txtProjectNo.Text + "\nEnglish Name: " + txtRiskNameEng.Text + "\nArabic Name: " + txtRiskNameArb.Text + "\nScore: " + cmbScore.SelectedItem.ToString() + "\nOwner: " + txtRiskOwner.Text + "\nMitigation Strategy: " + txtMitigationStrategy.Text);
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Risk added successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
                cmd.Dispose();
                ShowAllRisk();
                Format_DGV1();



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
        void GenerateRiskNo()
        {
            try
            {

                SqlDataAdapter da1 = new SqlDataAdapter(@"select count(MR_SN)+1 from Risks", ConCls.con);
                DataTable dt1 = new DataTable();
                ConCls.OpenConnection();
                da1.Fill(dt1);
                ConCls.CloseConnection();
                //   MessageBox.Show("Count :" + dt1.Rows.Count.ToString());
                if (dt1.Rows.Count == 0)
                {
                    txtRiskNo.Text = "1";
                    // MessageBox.Show("1");
                    return;

                }
                else
                {
                    txtRiskNo.Text = dt1.Rows[0][0].ToString();
                    //MessageBox.Show(dt1.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ConCls.CloseConnection();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ConCls.OpenConnection();
                string sql = @"update Risks set
                                    MR_RiskNameEng = @MR_RiskNameEng
                                   ,MR_RiskNameArb =@MR_RiskNameArb
                                   ,MR_Score = @MR_Score
                                   ,MR_Owner = @MR_Owner
                                   ,MR_MitigationStrategy =@MR_MitigationStrategy 
                                    where MR_SN = " + txtRiskNo.Text;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@MR_RiskNameEng", SqlDbType.NVarChar).Value = txtRiskNameEng.Text;
                cmd.Parameters.Add("@MR_RiskNameArb", SqlDbType.NVarChar).Value = txtRiskNameArb.Text;
                cmd.Parameters.Add("@MR_Score", SqlDbType.NVarChar).Value = cmbScore.SelectedItem.ToString();
                cmd.Parameters.Add("@MR_Owner", SqlDbType.NVarChar).Value = txtRiskOwner.Text;
                cmd.Parameters.Add("@MR_MitigationStrategy", SqlDbType.NVarChar).Value = txtMitigationStrategy.Text;
                //  MessageBox.Show(" Risk No: " + txtRiskNo.Text + "\nProject No: " + txtProjectNo.Text + "\nEnglish Name: " + txtRiskNameEng.Text + "\nArabic Name: " + txtRiskNameArb.Text + "\nScore: " + cmbScore.SelectedItem.ToString() + "\nOwner: " + txtRiskOwner.Text + "\nMitigation Strategy: " + txtMitigationStrategy.Text);
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Risk number : " + txtRiskNo.Text + " updated successfully", "Updated record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
                cmd.Dispose();

                ShowAllRisk();
               // Format_DGV1();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ConCls.CloseConnection();
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                txtRiskNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
               // txtProjectNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtRiskNameEng.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtRiskNameArb.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cmbScore.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtRiskOwner.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtMitigationStrategy.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                panel2.Visible = true;
                btnSave.Visible = false;

            }

            if (e.ColumnIndex == 8)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you need remove this risk", "Delete confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteRecourd(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (e.ColumnIndex == 9)
            { 
            // insert into ISSUE table
            }
        }

        void DeleteRecourd(string RecordNo)
        {
            if (string.IsNullOrEmpty(RecordNo) || string.IsNullOrWhiteSpace(RecordNo))
            {
                MessageBox.Show("You shuld be choose the record that you need to delete it","Missing Data");
                return;
            }
           // MessageBox.Show(RecordNo);
            //try
            //{
                
                string sql = @"delete from Risks where MR_SN = " + RecordNo;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Risk number : " + RecordNo + " deleted successfully", "Deleted record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
                cmd.Dispose();

                ShowAllRisk();
                // Format_DGV1();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    ConCls.CloseConnection();
            //    return;
            //}
        }
    }   
}
