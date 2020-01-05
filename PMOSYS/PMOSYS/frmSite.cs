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
    public partial class frmSite : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        int key;
        public frmSite()
        {
            InitializeComponent();
        }

        private void frmSite_Load(object sender, EventArgs e)
        {
            FillComboBox();
            GatAllData();
            timer1.Enabled = true;
        }

        void FillComboBox()
        {
           
            da = new SqlDataAdapter(@"select * from Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            comboBox1.ValueMember = "ZoneNo";
            comboBox1.DisplayMember = "ZoneName";
            comboBox1.DataSource = dt;
            
            da.Dispose();
           // dt.Dispose();

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            txtSiteName.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtSiteName.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
           GatAllData();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSiteName.Text))
            {
                MessageBox.Show("You can not leave (Site Name) empty", "Missing (Site Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSiteName.Focus();
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("You can not leave (Zone List) empty", "Missing (Zone Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSiteName.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [Sites]
                                    set [SiteName] = @SiteName,
                                        [ZoneNo] = @ZoneNo
                                    where SiteNo=" + key;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@SiteName", SqlDbType.NVarChar).Value = txtSiteName.Text;
                cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(comboBox1.SelectedValue.ToString());
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd update successfully", "Record updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                GatAllData();

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GatSearch(txtSearch.Text);
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
        }



        void GatAllData()
        {
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from Sites", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].Visible = false;
            txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();




        }
        void GatSearch(string searchValue)
        {
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from sites where sitename like'%" + searchValue + "%'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].Visible = false;
            txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSiteName.Text))
            {
                MessageBox.Show("You can not leave (Site Name) empty", "Missing (Site Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSiteName.Focus();
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("You can not leave (Zone List) empty", "Missing (Zone Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSiteName.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Sites]
                                    ([SiteName],
                                      [ZoneNo]          )

                                    VALUES
                                    (@SiteName,
                                     @ZoneNo)";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@SiteName", SqlDbType.NVarChar).Value = txtSiteName.Text;
                cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(comboBox1.SelectedValue.ToString());
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                GatAllData();

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
            if (e.RowIndex >= 0)
            {
                key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                txtSiteName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox1.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                btnChangeValue.Text = " To Change/Edit (" + txtSiteName.Text + ") click here.";
                btnChangeValue.Visible = true;
            }
            else
            {
                btnChangeValue.Visible = false;
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString();
        }
    }
}
