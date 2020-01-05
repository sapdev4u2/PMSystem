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
   
    public partial class frmInterface : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string _SR_No = "0";

        string sql_string;

        public frmInterface()
        {
            InitializeComponent();
        }

        private void frmInterface_Load(object sender, EventArgs e)
        {
            Get_ProjectData();
        }



        void ShowAll() 
        {
            sql_string = @"select * from interface";
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
           // lblInterfaceNo.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
           
        }


        void Refresh_Data()
        {
            sql_string = @"select * from interface where InterfaceNo =" + lblInterfaceNo.Text ;
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();
           
        }


        void Complete_Interface()
        {
            sql_string = @"select * from interface where Interface_Status like 'Completed'" ;
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();

        }
        void InProgress_Interface()
        {
            sql_string = @"select * from interface where Interface_Status like 'In-progress'";
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();

        }

        void NotStarted_Interface()
        {
            sql_string = @"select * from interface where Interface_Status like 'Not-Started'";
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            this.dataGridView1.DataSource = dt;
            // txtRecord.Text = dt.Rows.Count.ToString();
            da.Dispose();
            dt.Dispose();

        }

        void Get_ProjectData() 
        {
            cmbProject.DataSource = null;

            sql_string = @"select * from Projects"; //  select * from Projects where Status like '"Active"'
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            cmbProject.DataSource = dt;
            cmbProject.DisplayMember ="ProjectNameE";
            cmbProject.ValueMember = "Project_SNo";
            


        }

        void Get_Milestone()
        {
            cmbKeyMilestoneName.DataSource = null;
          //  MessageBox.Show(cmbProject.SelectedValue.ToString());
            sql_string = @"select * from Milestones where ProjectNo = " + cmbProject.SelectedValue.ToString();
            da = new SqlDataAdapter(sql_string, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            cmbKeyMilestoneName.DataSource = dt;
            cmbKeyMilestoneName.DisplayMember = "MSName";
            cmbKeyMilestoneName.ValueMember = "MSNo";
            


        }



        private void btnCompleted_Click(object sender, EventArgs e)
        {

        }

        private void btnCanacl_Click(object sender, EventArgs e)
        {
            if (pnlData.Visible == true)
            {
              pnlData.Visible = false;
            }
        }

        private void cmbProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtProjectNo.Text = cmbProjectName.SelectedValue.ToString();
            //Get_Milestone();

        }

        private void txtProjectNo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
               
                

            //}
        }

        private void cmbProjectName_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void cmbKeyMilestoneName_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    if (string.IsNullOrEmpty(cmbKeyMilestoneName.SelectedValue.ToString()))
        //    {
        //        txtMilestoneNo.Text = null;
        //        return;
        //    }
        //    else
        //    {
        //        txtMilestoneNo.Text = cmbKeyMilestoneName.SelectedValue.ToString();

        //    }
        }

        private void cmbProject_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbProject_DisplayMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbProject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Get_Milestone();
        }

        void generateSRNumber()
        {


            da = new SqlDataAdapter(@"select max(InterfaceNo)+1 from Interface", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();


            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
            {
                _SR_No = "1";
            }
            else
            {
                _SR_No = dt.Rows[0][0].ToString();
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            generateSRNumber();
            //  lblInterfaceNo.Text = 
            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO Interface
                                                   ([InterfaceNo]
                                                   ,[Interface_ProjectNo]
                                                   ,[Interface_Milestonr]
                                                   ,[Interface_StakeHolder]
                                                   ,[Interface_Dec]
                                                   ,[Interface_Total]
                                                   ,[Interface_Status])

                                    VALUES
                                    (@InterfaceNo,
                                      @Interface_ProjectNo,
                                      @Interface_Milestonr,
                                      @Interface_StakeHolder,
                                      @Interface_Dec,
                                      @Interface_Total,
                                      @Interface_Status
                                      )";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@InterfaceNo", SqlDbType.Int).Value = Convert.ToInt32(_SR_No);
                cmd.Parameters.Add("@Interface_ProjectNo", SqlDbType.Int).Value = Convert.ToInt32(cmbProject.SelectedValue.ToString());
                cmd.Parameters.Add("@Interface_Milestonr", SqlDbType.Int).Value = Convert.ToInt32(cmbKeyMilestoneName.SelectedValue.ToString());
                cmd.Parameters.Add("@Interface_StakeHolder", SqlDbType.Int).Value = Convert.ToInt32(cmbStakeHolder.SelectedIndex.ToString()); // change to selected value when you add table for stackholder
                cmd.Parameters.Add("@Interface_Dec", SqlDbType.NVarChar).Value = txtInterfacedecs.Text;
                cmd.Parameters.Add("@Interface_Total", SqlDbType.NVarChar).Value = txtTotalInterface.Text;
                cmd.Parameters.Add("@Interface_Status", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
                MessageBox.Show(cmbStatus.SelectedItem.ToString());
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Interface saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
