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

    public partial class frmZone : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        int key;


        public frmZone()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            txtZoneName.Text = "";
            textBox1.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtZoneName.Focus();

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;

        }

        private void frmZone_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            GatAllData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtDateTime.Text = DateTime.Now.ToString();
        }




        void GatAllData() {
           
            da = new SqlDataAdapter(@"select * from Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
           txtCount.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();

            

            
        }
        void GatSearch(string searchValue)
        {
           
            da = new SqlDataAdapter(@"select * from Zone where zoneName like'%" + textBox1.Text + "%'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            txtCount.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GatSearch(txtSearch.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GatAllData();
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtZoneName.Text)) 
            {
                MessageBox.Show("You can not leave (Zone Name) empty","Missing (Zone Name) value" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZoneName.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Zone]
                                    ([ZoneName])
                                    VALUES
                                    (@ZoneName)";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@ZoneName", SqlDbType.NVarChar).Value = txtZoneName.Text;

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
            finally {
                ConCls.CloseConnection();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtZoneName.Text))
            {
                MessageBox.Show("You can not leave (Zone Name) empty", "Missing (Zone Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZoneName.Focus();
                return;
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [Zone]
                                    set [ZoneName] = @ZoneName
                                    where ZoneNo=" + textBox1.Text;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@ZoneName", SqlDbType.NVarChar).Value = txtZoneName.Text;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    txtZoneName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    
                    pnlinfo.Visible = true;
                }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //    txtZoneName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ////    btnChangeValue.Text = " To Change/Edit (" + txtZoneName.Text + ") click here.";
            ////    btnChangeValue.Visible = true;
            //}
            //else {
            //    //btnChangeValue.Visible = false;
            //    return;
            //}
        }

       
    }
}
