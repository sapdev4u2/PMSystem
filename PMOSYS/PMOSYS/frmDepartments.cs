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
    public partial class frmDepartments : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
      //  int key;

        public frmDepartments()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;
            btnAddNew.Enabled = true;

        }

        private void frmDepartments_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            LoadPlants();
        }

        void LoadDepartment()
        {
            
            da = new SqlDataAdapter(@"select * from V_Dept_Plant", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
           txtRecord.Text = dt.Rows.Count.ToString();
            dataGridView1.DataSource = dt;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (cmbPalntName.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbPalntName, "You shuold choose plant name");
                cmbPalntName.Focus();
                return;
            }
            else
            {

                errorProvider1.Clear();
                txtDeptName.Focus();

            }

            if (string.IsNullOrEmpty(txtDeptName.Text))
            {
                errorProvider1.SetError(txtDeptName, "You shuold write department name");
                txtDeptName.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
                btnSave.Focus();
            }


            save();




        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            btnAddNew.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbPalntName.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbPalntName, "You shuold choose plant name");
                cmbPalntName.Focus();
                return;
            }
            else
            {

                errorProvider1.Clear();
                txtDeptName.Focus();

            }

            if (string.IsNullOrEmpty(txtDeptName.Text))
            {
                errorProvider1.SetError(txtDeptName, "You shuold write department name");
                txtDeptName.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
                btnSave.Focus();
            }


            update();
        }

        void save()
        {

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Dept]
                                    (PlantNo,
                                     DeptName)

                                    VALUES

                                    (@PlantNo,
                                     @DeptName)";

                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = cmbPalntName.SelectedValue;
                cmd.Parameters.Add("@DeptName", SqlDbType.NVarChar).Value = txtDeptName.Text;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                LoadDepartment();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong while the record has been saved, Plase try agine. \n"+ ex.ToString(), "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        void LoadPlants()
        {

            da = new SqlDataAdapter(@"select * from plants", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbPalntName.DataSource = dt;
            cmbPalntName.ValueMember = "PlantNo";
            cmbPalntName.DisplayMember = "PlantName";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    cmbPalntName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtDeptNo.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtDeptName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    pnlinfo.Visible = true;
                }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        void search() 
        {
            da = new SqlDataAdapter(@"select * from V_Dept_Plant  where DeptName like '%" + txtSearch.Text + "%'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            txtRecord.Text = dt.Rows.Count.ToString();
            dataGridView1.DataSource = dt;
        }

        void update()
        {

            

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [dept]
                                    set PlantNo = @PlantNo,
                                        DeptName = @DeptName
                                    where DeptNo=" + txtDeptNo.Text;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                
                cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = cmbPalntName.SelectedValue;
                cmd.Parameters.Add("@DeptName", SqlDbType.NVarChar).Value = txtDeptName.Text;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd update successfully", "Record updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                LoadDepartment();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong while the record has been update, Please try agine.", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmReportViewer f = new frmReportViewer();
            

        }
    }
}
