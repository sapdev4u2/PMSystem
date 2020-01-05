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
    public partial class frmPlants : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        int key;

        public frmPlants()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmPlants_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from Plants", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            txtRecord.Text = dt.Rows.Count.ToString();
            dataGridView1.DataSource = dt;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            
            pnlinfo.Visible = true;
            txtPlantName.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtPlantName.Focus();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlantName.Text))
            {
                MessageBox.Show("You can not leave (Plant Name) empty", "Missing (Plant Name) value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPlantName.Focus();
                return;
            }
          

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Plants]
                                    (PlantName)
                                    VALUES
                                    (@PlantName)";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@PlantName", SqlDbType.NVarChar).Value = txtPlantName.Text;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlinfo.Visible = false;
                cmd.Dispose();
                LoadData();

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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                txtPlantName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                btnChangeValue.Text = " To Change/Edit (" + txtPlantName.Text + ") click here.";
                btnChangeValue.Visible = true;
            }
            else
            {
                btnChangeValue.Visible = false;
                return;
            }
        }

        void search()
        {

            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from plants where plantname like'%" + txtSearch.Text + "%'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            pnlinfo.Visible = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
