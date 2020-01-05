using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;



namespace PMOSYS
{
    public partial class frmPlantManager : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        public string PARAMETTER_G;
        public string G_Value;

        string sql_str;
      

        public frmPlantManager()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
            if (comboBox2.SelectedIndex == -1) 
            {
                MessageBox.Show("You must select one item from list");
                return;
            }

            //---------------------------------------- 


            if (comboBox2.SelectedIndex == 0) // plant manager number
            {
                sql_str = @"select * from PlantManagers where PMNo like '%" + txtSearch.Text + "%'" ;
            }

            else if (comboBox2.SelectedIndex == 1) // full name english 
            {
                sql_str = @"select * from PlantManagers where PMrNameE like '%" + txtSearch.Text + "%'";
            }
            else if (comboBox2.SelectedIndex == 2) // full name arabic
            {
                sql_str = @"select * from PlantManagers where PMrNameA like '%" + txtSearch.Text + "%'";
            }
            else if (comboBox2.SelectedIndex == 3) // email account
            {
                sql_str = @"select * from PlantManagers where PMrEmail  like '%" + txtSearch.Text + "%'";
            }
           else if (comboBox2.SelectedIndex == 4) // work phone 1
            {
                sql_str = @"select * from PlantManagers where PMrWorkPhone1 like '%" + txtSearch.Text + "%'";
            }
            else if (comboBox2.SelectedIndex == 5) // mob phone 1
            {
                sql_str = @"select * from PlantManagers where PMrMobPhone1 like '%" + txtSearch.Text + "%'";
            }


            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
           // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
            formatDataGrid();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            btnUpdate.Enabled = false;

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPM_No.Text))
            {
                MessageBox.Show("You can not leave (Plant Manager number) empty", "Missing (Plant Manager number) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPM_No.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFullName_E.Text))
            {
                MessageBox.Show("You can not leave (Full Name - English) empty", "Missing (Full Name - English) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName_E.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("You can not leave (User ID) empty", "Missing (User ID) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("You can not leave (Email) empty", "Missing (Email) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtWorkPhone1.Text))
            {
                MessageBox.Show("You can not leave (Work Phone 1) empty", "Missing (Work Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWorkPhone1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobPhone1.Text))
            {
                MessageBox.Show("You can not leave (Mobile Phone 1) empty", "Missing (Mobile Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobPhone1.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO  [PlantManagers]
                                           ([PMNo]
                                           ,[PMrNameE]
                                           ,[PMrNameA]
                                           ,[PMrUserID]
                                           ,[PMr_JobTitle]
                                           ,[PMrEmail]
                                           ,[PMrWorkPhone1]
                                           ,[PMrWorkPhone2]
                                           ,[PMrMobPhone1]
                                           ,[PMrMobPhone2]
                                           ,[PMrStatus])
                                        VALUES (
                                            @PMNo
                                           ,@PMrNameE
                                           ,@PMrNameA
                                           ,@PMrUserID
                                           ,@PMr_JobTitle
                                           ,@PMrEmail
                                           ,@PMrWorkPhone1
                                           ,@PMrWorkPhone2
                                           ,@PMrMobPhone1
                                           ,@PMrMobPhone2
                                           ,@PMrStatus)";
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@PMNo", SqlDbType.NVarChar).Value = txtPM_No.Text;
                cmd.Parameters.Add("@PMrNameE", SqlDbType.NVarChar).Value = txtFullName_E.Text;
                cmd.Parameters.Add("@PMrNameA", SqlDbType.NVarChar).Value = txtFullName_A.Text;
                cmd.Parameters.Add("@PMrUserID", SqlDbType.NVarChar).Value = txtUserID.Text;
                cmd.Parameters.Add("@PMr_JobTitle", SqlDbType.NVarChar).Value = txtJobTitel.Text;
                cmd.Parameters.Add("@PMrEmail", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@PMrWorkPhone1", SqlDbType.NVarChar).Value = txtWorkPhone1.Text;
                cmd.Parameters.Add("@PMrWorkPhone2", SqlDbType.NVarChar).Value = txtWorkPhone2.Text;
                cmd.Parameters.Add("@PMrMobPhone1", SqlDbType.NVarChar).Value = txtMobPhone1.Text;
                cmd.Parameters.Add("@PMrMobPhone2", SqlDbType.NVarChar).Value = txtMobPhone2.Text;
                cmd.Parameters.Add("@PMrStatus", SqlDbType.NVarChar).Value = comboBox1.SelectedItem.ToString();

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                GetAllData();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //    txtSiteName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    comboBox1.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            //    btnChangeValue.Text = " To Change/Edit (" + txtSiteName.Text + ") click here.";
            //    btnChangeValue.Visible = true;
            //}
            //else
            //{
            //    btnChangeValue.Visible = false;
            //    return;
            //}
        }

        private void pnlinfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmPlantManager_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
         
            GetAllData();

            if (PARAMETTER_G == "NewProject") {
                btnSelect.Visible = true;
            }
        }

        void GetAllData() 
        {
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from PlantManagers", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
            formatDataGrid();
        }


        void formatDataGrid()
        {
            dataGridView1.Columns[0].HeaderText = "PM #";
            dataGridView1.Columns[1].HeaderText = "Full Name (Eng)";
            dataGridView1.Columns[2].HeaderText = "Full Name (Arb)";
            dataGridView1.Columns[3].HeaderText = "User ID";
            dataGridView1.Columns[4].HeaderText = "Job Title";
            dataGridView1.Columns[5].HeaderText = "Email";
            dataGridView1.Columns[6].HeaderText = "Work Phone 1";
            dataGridView1.Columns[7].HeaderText = "Work Phone 2";
            dataGridView1.Columns[8].HeaderText = "Mobile Phone 1";
            dataGridView1.Columns[9].HeaderText = "Mobile Phone 2";
            dataGridView1.Columns[10].HeaderText = "Status";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlinfo.Controls) 
            { 
                if(c is TextBox) 
                {
                    c.Text = "";
                }
               
            
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPM_No.Text))
            {
                MessageBox.Show("You can not leave (Plant Manager number) empty", "Missing (Plant Manager number) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPM_No.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFullName_E.Text))
            {
                MessageBox.Show("You can not leave (Full Name - English) empty", "Missing (Full Name - English) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName_E.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("You can not leave (User ID) empty", "Missing (User ID) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("You can not leave (Email) empty", "Missing (Email) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtWorkPhone1.Text))
            {
                MessageBox.Show("You can not leave (Work Phone 1) empty", "Missing (Work Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWorkPhone1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobPhone1.Text))
            {
                MessageBox.Show("You can not leave (Mobile Phone 1) empty", "Missing (Mobile Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobPhone1.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [PlantManagers] set 
                                           ([PMNo] = @PMNo
                                           ,[PMrNameE] = @PMrNameE
                                           ,[PMrNameA] = @PMrNameA
                                           ,[PMrUserID] = @PMrUserID
                                           ,[PMr_JobTitle] = @PMr_JobTitle
                                           ,[PMrEmail] = @PMrEmail
                                           ,[PMrWorkPhone1] = @PMrWorkPhone1
                                           ,[PMrWorkPhone2] = @PMrWorkPhone2
                                           ,[PMrMobPhone1] = @PMrMobPhone1
                                           ,[PMrMobPhone2] = @PMrMobPhone2
                                           ,[PMrStatus] = @PMrStatus)
                                        Where PMNo like '" + txtPM_No.Text +"'";
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@PMNo", SqlDbType.NVarChar).Value = txtPM_No.Text;
                cmd.Parameters.Add("@PMrNameE", SqlDbType.NVarChar).Value = txtFullName_E.Text;
                cmd.Parameters.Add("@PMrNameA", SqlDbType.NVarChar).Value = txtFullName_A.Text;
                cmd.Parameters.Add("@PMrUserID", SqlDbType.NVarChar).Value = txtUserID.Text;
                cmd.Parameters.Add("@PMr_JobTitle", SqlDbType.NVarChar).Value = txtJobTitel.Text;
                cmd.Parameters.Add("@PMrEmail", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@PMrWorkPhone1", SqlDbType.NVarChar).Value = txtWorkPhone1.Text;
                cmd.Parameters.Add("@PMrWorkPhone2", SqlDbType.NVarChar).Value = txtWorkPhone2.Text;
                cmd.Parameters.Add("@PMrMobPhone1", SqlDbType.NVarChar).Value = txtMobPhone1.Text;
                cmd.Parameters.Add("@PMrMobPhone2", SqlDbType.NVarChar).Value = txtMobPhone2.Text;
                cmd.Parameters.Add("@PMrStatus", SqlDbType.NVarChar).Value = comboBox1.SelectedItem.ToString();

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd updated successfully", "Update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                GetAllData();

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

        private void txtPM_No_TextChanged(object sender, EventArgs e)
        {
            txtUserID.Text = "U" + txtPM_No.Text;
            txtEmail.Text = "U" + txtPM_No.Text+"@swcc.gov.sa";
            if (string.IsNullOrEmpty(txtPM_No.Text))
            {
                txtUserID.Text = "";
                txtEmail.Text = "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GetAllData();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               txtPM_No.Text =  dataGridView1.CurrentRow.Cells[0].Value.ToString();
               txtFullName_E.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
               txtFullName_A.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
               txtUserID.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
               txtJobTitel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
               txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
               txtWorkPhone1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
               txtWorkPhone1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
               txtMobPhone1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
               txtMobPhone2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
               comboBox1.SelectedText = dataGridView1.CurrentRow.Cells[10].Value.ToString();
              
                
                btnSelect.Text = string.Format("Select ( {0} ) and return back", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                G_Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                button3.Visible = true;
                button3.Text = " To Change/Edit (" + txtFullName_E.Text + ") click here.";
                button3.Visible = true;
             }
             else
               {
                   button3.Visible = false;
                    return;
                }
            }



        private void button3_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

          
            

           
        }
    }
    
}
