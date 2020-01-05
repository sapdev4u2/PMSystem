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
    public partial class frmStockHolder : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string _SR_No = "0";

        public frmStockHolder()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            generateSRNumber();
            panel1.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAddNew.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSHNo.Text))
            {
                MessageBox.Show("Required stock holder number ","Missing data");
                return;
            }
            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO StockHolder
                                                   ([SH_SN]
                                                   ,[SH_NameE]
                                                   ,[SH_NameA]
                                                   ,[SH_Phone]
                                                   ,[SH_Mob]
                                                   ,[SH_Email]
                                                   ,[SH_Note])

                                    VALUES
                                    (@SH_SN,
                                      @SH_NameE,
                                      @SH_NameA,
                                      @SH_Phone,
                                      @SH_Mob,
                                      @SH_Email,
                                      @SH_Note
                                      )";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@SH_SN", SqlDbType.Int).Value = Convert.ToInt32(txtSHNo.Text);
                cmd.Parameters.Add("@SH_NameE", SqlDbType.NVarChar).Value = txtSHNameEng.Text ;
                cmd.Parameters.Add("@SH_NameA", SqlDbType.NVarChar).Value = txtSHNameArb.Text;
                cmd.Parameters.Add("@SH_Phone", SqlDbType.NVarChar).Value = txtPhoneNo.Text; 
                cmd.Parameters.Add("@SH_Mob", SqlDbType.NVarChar).Value = txtMobNo.Text;
                cmd.Parameters.Add("@SH_Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@SH_Note", SqlDbType.NVarChar).Value = txtNote.Text;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Record saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmd.Dispose();
                ShowAll();
                btnCancel.PerformClick();

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

        void generateSRNumber()
        {


            da = new SqlDataAdapter(@"select max(SH_SN)+1 from StockHolder", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();


            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
            {
                txtSHNo.Text = "1";
            }
            else
            {
                txtSHNo.Text = dt.Rows[0][0].ToString();
            }

        }

        void ShowAll()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }


            }

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            da = new SqlDataAdapter(@"select * from StockHolder", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

           

            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "SN #";
            dataGridView1.Columns[1].HeaderText = "Stock Holder Name (Eng)";
            dataGridView1.Columns[2].HeaderText = "Stock Holder Name (Arb)";
            dataGridView1.Columns[3].HeaderText = "Phone";
            dataGridView1.Columns[4].HeaderText = "Mobile";
            dataGridView1.Columns[5].HeaderText = "Email";
            dataGridView1.Columns[6].HeaderText = "Note";

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;



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
            
           


            txtRecordNo.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();

        }
        private void frmStockHolder_Load(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ConCls.OpenConnection();
                string sql = @"update [StockHolder] set 
                                           
                                            [SH_NameE] = @SH_NameE
                                           ,[SH_NameA] = @SH_NameA
                                           ,[SH_Phone] = @SH_Phone
                                           ,[SH_Mob] = @SH_Mob
                                           ,[SH_Email] = @SH_Email
                                           ,[SH_Note] = @SH_Note
                                        Where SH_SN like '" + txtSHNo.Text + "'";
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@SH_NameE", SqlDbType.NVarChar).Value = txtSHNameEng.Text;
                cmd.Parameters.Add("@SH_NameA", SqlDbType.NVarChar).Value = txtSHNameArb.Text;
                cmd.Parameters.Add("@SH_Phone", SqlDbType.NVarChar).Value = txtPhoneNo.Text;
                cmd.Parameters.Add("@SH_Mob", SqlDbType.NVarChar).Value = txtMobNo.Text;
                cmd.Parameters.Add("@SH_Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@SH_Note", SqlDbType.NVarChar).Value = txtNote.Text;
               

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd updated successfully", "Update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = false;
                cmd.Dispose();
                ShowAll();
                btnCancel.PerformClick();

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
           
            btnSave.Visible = true;
            btnAddNew.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                panel1.Visible = true;
                btnSave.Visible = false;
                btnAddNew.Enabled = false;
                btnUpdate.Visible = true;

                txtSHNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtSHNameEng.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSHNameArb.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPhoneNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtMobNo.Text =  dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtNote.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            if (e.ColumnIndex == 8)
            {
                DeleteRecord(dataGridView1.CurrentRow.Cells[0].Value.ToString());
               
            }
        }

        void DeleteRecord(string RecordNo)
        {
            try
            {
                if (string.IsNullOrEmpty(RecordNo))
                {
                    MessageBox.Show("Choose the row that you need to delete it from the grid","Missing data");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Are you sure like to delete this Stock holder no: " + RecordNo , "Confirem delete record", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else if (dialogResult == DialogResult.Yes)
                


                    
                ConCls.OpenConnection();
                string sql = @"Delete from [StockHolder] Where SH_SN=" + RecordNo;
                ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Deleted successfully", "Delete record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = false;
                cmd.Dispose();
                ShowAll();

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
    }
}
