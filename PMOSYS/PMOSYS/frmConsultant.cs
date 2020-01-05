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
    public partial class frmConsultant : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;


        //string sql_str;
        public frmConsultant()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlOption.Visible = true;
            btnAddNew.Enabled = false;
            btnUpdate.Enabled = false;
            int sr = GenerateConsultantNo();
            txtContNo.Text = sr.ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlOption.Visible = false;
            btnAddNew.Enabled = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VerifyEntery();

            try{
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Consultant]
                                         (   [ConsultantNo]
                                           ,[NameE]
                                           ,[NameA]
                                           ,[ZoneNo]
                                           ,[Street]
                                           ,[Address]
                                           ,[PhoneNo1]
                                           ,[PhoneNo2]
                                           ,[MobPhone1]
                                           ,[MobPhone2]
                                           ,[Email1]
                                           ,[Email2]
                                           ,[Website]
                                           ,[Remark])
                                        VALUES (
                                            @ConsultantNo
                                           ,@NameE
                                           ,@NameA
                                           ,@ZoneNo
                                           ,@Street
                                           ,@Address
                                           ,@PhoneNo1
                                           ,@PhoneNo2
                                           ,@MobPhone1
                                           ,@MobPhone2
                                           ,@Email1
                                           ,@Email2
                                           ,@Website
                                           ,@Remark)";
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@ConsultantNo", SqlDbType.NVarChar).Value = txtContNo.Text;
                cmd.Parameters.Add("@NameE", SqlDbType.NVarChar).Value = txtNameEng.Text;
                cmd.Parameters.Add("@NameA", SqlDbType.NVarChar).Value = txtNameArb.Text;
                cmd.Parameters.Add("@ZoneNo", SqlDbType.NVarChar).Value = cmbCity.SelectedValue.ToString(); 
                cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = txtStreet.Text;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;
                cmd.Parameters.Add("@PhoneNo1", SqlDbType.NVarChar).Value = txtPhone1.Text;
                cmd.Parameters.Add("@PhoneNo2", SqlDbType.NVarChar).Value = txtPhone2.Text;
                cmd.Parameters.Add("@MobPhone1", SqlDbType.NVarChar).Value = txtMobPhone1.Text;
                cmd.Parameters.Add("@MobPhone2", SqlDbType.NVarChar).Value = txtMobPhone2.Text;
                cmd.Parameters.Add("@Email1", SqlDbType.NVarChar).Value = txtEmail1.Text;
                cmd.Parameters.Add("@Email2", SqlDbType.NVarChar).Value = txtEmail2.Text;
                cmd.Parameters.Add("@Website", SqlDbType.NVarChar).Value = txtWebSite.Text;
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = txtRemark.Text;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlOption.Visible = false;
                btnAddNew.Enabled = true;
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

        private void frmConsultant_Load(object sender, EventArgs e)
        {
            pnlOption.Visible = false;
            GetAllData();
            LoadCities();
            
        }

        void GetAllData()
        {
            //dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from V_Consultant_Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
           // formatDataGrid();

        }
        //void formatDataGrid()
        //{
        //    dataGridView1.Columns[0].HeaderText = "Consultant No";
        //    dataGridView1.Columns[1].HeaderText = "Name (Eng)";
        //    dataGridView1.Columns[2].HeaderText = "Name (Arb)";
        //    dataGridView1.Columns[3].HeaderText = "City";
        //    dataGridView1.Columns[4].HeaderText = "Street";
        //    dataGridView1.Columns[5].HeaderText = "Address";
        //    dataGridView1.Columns[6].HeaderText = "Work Phone 1";
        //    dataGridView1.Columns[7].HeaderText = "Work Phone 2";
        //    dataGridView1.Columns[8].HeaderText = "Mobile Phone 1";
        //    dataGridView1.Columns[9].HeaderText = "Mobile Phone 2";
        //    dataGridView1.Columns[10].HeaderText = "Email 1";
        //    dataGridView1.Columns[11].HeaderText = "Email 2";
        //    dataGridView1.Columns[12].HeaderText = "Website";
        //    dataGridView1.Columns[13].HeaderText = "Remark";
        //}
        void LoadCities() 
        {
           
            da = new SqlDataAdapter(@"select * from Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbCity.DataSource = dt;
            cmbCity.DisplayMember = "ZoneName";
            cmbCity.ValueMember = "ZoneNo";

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlOption.Controls )
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }

            }
            cmbCity.SelectedIndex = 0;
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            VerifyEntery();

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [Consultant] set(
                                            [NameE] = @NameE
                                           ,[NameA] = @NameA
                                           ,[ZoneNo] = @ZoneNo
                                           ,[Street] = @Street
                                           ,[Address] = @Address
                                           ,[PhoneNo1] = @PhoneNo1
                                           ,[PhoneNo2] = @PhoneNo2
                                           ,[MobPhone1] =@MobPhone1
                                           ,[MobPhone2] = @MobPhone2
                                           ,[Email1] = @Email1
                                           ,[Email2] = @Email2
                                           ,[Website] = @Website
                                           ,[Remark] = @Remark)
                                            Where PMNo like '" + txtContNo.Text + "'";
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                
                cmd.Parameters.Add("@NameE", SqlDbType.NVarChar).Value = txtNameEng.Text;
                cmd.Parameters.Add("@NameA", SqlDbType.NVarChar).Value = txtNameArb.Text;
                cmd.Parameters.Add("@ZoneNo", SqlDbType.NVarChar).Value = cmbCity.SelectedItem.ToString();
                cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = txtStreet.Text;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;
                cmd.Parameters.Add("@PhoneNo1", SqlDbType.NVarChar).Value = txtPhone1.Text;
                cmd.Parameters.Add("@PhoneNo2", SqlDbType.NVarChar).Value = txtPhone2.Text;
                cmd.Parameters.Add("@MobPhone1", SqlDbType.NVarChar).Value = txtMobPhone1.Text;
                cmd.Parameters.Add("@MobPhone2", SqlDbType.NVarChar).Value = txtMobPhone2.Text;
                cmd.Parameters.Add("@Email1", SqlDbType.NVarChar).Value = txtEmail1.Text;
                cmd.Parameters.Add("@Email2", SqlDbType.NVarChar).Value = txtEmail2.Text;
                cmd.Parameters.Add("@Website", SqlDbType.NVarChar).Value = txtWebSite.Text;
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = txtRemark.Text;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd updated successfully", "Update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlOption.Visible = false;
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

        void VerifyEntery()
        {

            if (string.IsNullOrEmpty(txtContNo.Text))
            {
                MessageBox.Show("You can not leave (Plant Manager number) empty", "Missing (Constractor No) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNameEng.Text))
            {
                MessageBox.Show("You can not leave (Constractor Name (Eng)) empty", "Missing (Constractor Name (Eng)) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNameEng.Focus();
                return;
            }


            if (cmbCity.SelectedIndex == -1)
            {
                MessageBox.Show("You can not leave (City) empty", "Missing (City) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCity.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtStreet.Text))
            {
                MessageBox.Show("You can not leave (Street) empty", "Missing (Street) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStreet.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("You can not leave (Address) empty", "Missing (Address) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPhone1.Text))
            {
                MessageBox.Show("You can not leave (Work Phone 1) empty", "Missing (Work Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobPhone1.Text))
            {
                MessageBox.Show("You can not leave (Mobile Phone 1) empty", "Missing (Mobile Phone 1) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    // key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //    txtContNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            //    //btnChangeValue.Text = " To Change/Edit (" + txtContNo.Text + " - "+ txtNameEng.Text +") click here.";
            //    //btnChangeValue.Visible = true;
            

            //}
            //else
            //{
            //    //btnChangeValue.Visible = false;
            //    return;
            //}
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            pnlOption.Visible = true;
            btnUpdate.Visible = true;


        }
        public int GenerateConsultantNo()
        {
            int x;
            String s;
         
            da = new SqlDataAdapter(@"select max(ConsultantNo)+1 from Consultant", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            s = dt.Rows[0][0].ToString();
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
             //   MessageBox.Show("Table empty..\nThis it will be first recourd in the table." ,"Info");
                return 1;
            }
           // MessageBox.Show(s);
            x = int.Parse(s);
            
            da.Dispose();
            dt.Dispose();
            return x;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
