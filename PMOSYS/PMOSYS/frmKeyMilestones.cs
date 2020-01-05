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
    public partial class frmKeyMilestones : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        public string PARAMETTER_G;
        public string G_Value;

        string sql_str;
        public frmKeyMilestones()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmKeyMilestones_Load(object sender, EventArgs e)
        {




        }

        void GetProjectData()
        {
            txtProjectNameE.Text = "";
            txtProjectNameA.Text = "";
            txtStatus.Text = "";
            txtSituation.Text = "";

            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from Projects where Project_SNo =" + txtProjectNo.Text, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();


            txtProjectNameE.Text = dt.Rows[0][1].ToString();
            txtProjectNameA.Text = dt.Rows[0][2].ToString();
            txtStatus.Text = dt.Rows[0][18].ToString();



            DateTime sd = Convert.ToDateTime(dt.Rows[0][14].ToString());
            DateTime ed = Convert.ToDateTime(dt.Rows[0][13].ToString());
            DateTime td = DateTime.Now;

            TimeSpan diff = ed.Date.Subtract(sd);
            TimeSpan t = sd - td;
            txtSituation.Text = diff.Days.ToString()+"   "+ t.Days.ToString();


            //if (diff.Days == 0)
            //{
            //    txtSituation.Text = string.Format("The project "diff.Days.ToString();
            //}

            double pers;
            
            // pers = t.Days /diff.Days  * 100;
        
            chart1.To = 100;
           // chart1.Value = pers;
            txtStartDate.Text = sd.ToString();
            txtEndate.Text = ed.ToString();
            da.Dispose();
            dt.Dispose();
        }

        void getPlantManagerName()
        { 
        
        }

        private void txtProjectNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProjectNameE.Text = "";
                txtProjectNameA.Text = "";
                txtStatus.Text = "";
                txtSituation.Text = "";
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                GetProjectData();
                GetMilestones();
            }
        }

        private void txtProjectNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            pnlprojectMileston.Visible = true;

        }

        void GetMilestones()
        {
            dataGridView1.DataSource = null;
            da = new SqlDataAdapter(@"select * from Milestones where ProjectNo =" + txtProjectNo.Text, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No milestones was found for this project");
                return;
            }
            else
            {
                dataGridView1.DataSource = dt;
            }
        }

        void GetTasks()
        {
            dataGridView2.DataSource = null;
            da = new SqlDataAdapter(@"select * from Milestones where ProjectNo =" + txtProjectNo.Text, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            }

            private void btn_s_Save_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"INSERT INTO [Milestones]
                                    ([ProjectNo]
                                       ,[MSName]
                                       ,[MSStratDate]
                                       ,[MSDuration]
                                       ,[MSAuctEndDate]
                                        ,[MSStatus]
                                       ,[MSNote])

                                    VALUES
                                    (@ProjectNo
                                    ,@MSName
                                    ,@MSStratDate
                                    ,@MSDuration
                                    ,@MSAuctEndDate                             
                                    ,@MSStatus
                                    ,@MSNote)";

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ProjectNo", SqlDbType.Int).Value = int.Parse(txtProjectNo.Text);
            cmd.Parameters.Add("@MSName", SqlDbType.NVarChar).Value = txtMS_Name.Text;
            cmd.Parameters.Add("@MSStratDate", SqlDbType.Date).Value = dtpMS_SatrtDate.Value.Date;
            cmd.Parameters.Add("@MSDuration", SqlDbType.Int).Value = int.Parse(txtMS_Duration.Text); ;
           cmd.Parameters.Add("@MSAuctEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@MSStatus", SqlDbType.NVarChar).Value =cmbMS_Status.SelectedItem.ToString() ;
            cmd.Parameters.Add("@MSNote", SqlDbType.NVarChar).Value = txtMS_Note.Text;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();
        }

        void ClearMS_Form()
        {
            foreach (Control c in pnlprojectMileston.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }

                
            }
        
        }

        private void btn_s_Cancel_Click(object sender, EventArgs e)
        {
            pnlprojectMileston.Visible = false;

        }

        private void btn_s_Update_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"UPDATE [Milestones] SET  
                                       [MSName]= @MSName
                                       ,[MSStratDate]= @MSStratDate
                                       ,[MSDuration]= @MSDuration
                                       ,[MSStatus]= @MSStatus
                                       ,[MSNote]= @MSNote
                                         WHERE MSNo = " + txtMS_No.Text;
            
            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
        
            cmd.Parameters.Add("@MSName", SqlDbType.NVarChar).Value = txtMS_Name.Text;
            cmd.Parameters.Add("@MSStratDate", SqlDbType.Date).Value = dtpMS_SatrtDate.Value.Date;
            cmd.Parameters.Add("@MSDuration", SqlDbType.Int).Value = int.Parse(txtMS_Duration.Text); 
            cmd.Parameters.Add("@MSStatus", SqlDbType.NVarChar).Value = cmbMS_Status.SelectedItem.ToString();
            cmd.Parameters.Add("@MSNote", SqlDbType.NVarChar).Value = txtMS_Note.Text;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Updated successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();
        }

        private void btn_s_Delete_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"delete from [Milestones] 
                                        WHERE MSNo = " + txtMS_No.Text;
          
            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;




            if  ((MessageBox.Show("Recourd Deleted successfully", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
            {
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
            }
            else {
                return;
            } 
                
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();
        }

        private void btn_s_Edit_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.DataSource = null;
                string MS_No = dataGridView1.CurrentRow.Cells["MSNo"].Value.ToString();
                da = new SqlDataAdapter(@"select * from tasks where Task_MSNo =" + MS_No, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                txtMS_No.Text = dataGridView1.CurrentRow.Cells["MSNo"].Value.ToString();
                txtMS_Name.Text = dataGridView1.CurrentRow.Cells["MSName"].Value.ToString();
                dtpMS_SatrtDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["MSStratDate"].Value.ToString());
                txtMS_Duration.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                // put end date here 
                cmbMS_Status.SelectedItem = dataGridView1.CurrentRow.Cells["MSStatus"].Value.ToString();
                txtMS_Note.Text = dataGridView1.CurrentRow.Cells["MSNote"].Value.ToString();
                dataGridView2.DataSource = dt;

            }

         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"UPDATE [Milestones] SET  
                                        [MSAuctEndDate] = @MSAuctEndDate
                                       ,[MSStatus]= @MSStatus
                                         WHERE MSNo = " + txtMS_No.Text;

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            cmd.Parameters.Add("@MSAuctEndDate", SqlDbType.Date).Value = DateTime.Now.Date;
            cmd.Parameters.Add("@MSStatus", SqlDbType.NVarChar).Value = "Finish";
        

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Milestone Finish (Ended) successfully", "Milestone Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();
        }

        private void btn_task_Save_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"INSERT INTO [Tasks]
                                           (
                                            [Task_MSNo]
                                           ,[Task_ProjectNo]
                                           ,[TaskName]
                                           ,[TaskStartDate]
                                           ,[TaskDuration]
                                           ,[TaskAcutEndDate]
                                           ,[TaskStatus]
                                           ,[TaskNote])

                                    VALUES
                                    (@Task_MSNo
                                    ,@Task_ProjectNo
                                    ,@TaskName
                                    ,@TaskStartDate
                                    ,@TaskDuration
                                    ,@TaskAcutEndDate
                                    ,@TaskStatus
                                    ,@TaskNote)";

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@Task_MSNo", SqlDbType.Int).Value = int.Parse(txtMS_No.Text);
            cmd.Parameters.Add("@Task_ProjectNo", SqlDbType.Int).Value = int.Parse(txtProjectNo.Text);
            cmd.Parameters.Add("@TaskName", SqlDbType.NVarChar).Value = txt_Task_Name.Text;
            cmd.Parameters.Add("@TaskStartDate", SqlDbType.Date).Value = dtp_Task_Start_Date.Value.Date;
            cmd.Parameters.Add("@TaskDuration", SqlDbType.Int).Value = int.Parse(txt_Tsak_Duration.Text); ;
            cmd.Parameters.Add("@TaskAcutEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@TaskStatus", SqlDbType.NVarChar).Value = cmb_Task_Status.SelectedItem.ToString();
            cmd.Parameters.Add("@TaskNote", SqlDbType.NVarChar).Value = txt_Tsak_Note.Text;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();
            
        }

        private void btn_task_Update_Click(object sender, EventArgs e)
        {
            ConCls.OpenConnection();
            string sql = @"Update [Tasks] set
                                           (
                                            [Task_MSNo]= @Task_MSNo
                                           ,[Task_ProjectNo] = @Task_ProjectNo
                                           ,[TaskName] = @TaskName
                                           ,[TaskStartDate] = @TaskStartDate
                                           ,[TaskDuration] = @TaskDuration
                                           ,[TaskAcutEndDate] = @TaskAcutEndDate
                                           ,[TaskStatus] = @TaskStatus
                                           ,[TaskNote] = @TaskNote)
                                             where TaskNo = " +  txt_task_no.Text;

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@Task_MSNo", SqlDbType.Int).Value = int.Parse(txtMS_No.Text);
            cmd.Parameters.Add("@Task_ProjectNo", SqlDbType.Int).Value = int.Parse(txtProjectNo.Text);
            cmd.Parameters.Add("@TaskName", SqlDbType.NVarChar).Value = txt_Task_Name.Text;
            cmd.Parameters.Add("@TaskStartDate", SqlDbType.Date).Value = dtp_Task_Start_Date.Value.Date;
            cmd.Parameters.Add("@TaskDuration", SqlDbType.Int).Value = int.Parse(txt_Tsak_Duration.Text); ;
            cmd.Parameters.Add("@TaskAcutEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@TaskStatus", SqlDbType.NVarChar).Value = cmb_Task_Status.SelectedItem.ToString();
            cmd.Parameters.Add("@TaskNote", SqlDbType.NVarChar).Value = txt_Tsak_Note.Text;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlprojectMileston.Visible = false;
            cmd.Dispose();
            GetMilestones();

        }

        private void txtMS_No_TextChanged(object sender, EventArgs e)
        {
            txt_Task_MS_No.Text = txtMS_No.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pnlTask.Visible = true;

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                

                txt_task_no.Text = dataGridView2.CurrentRow.Cells["TaskNo"].Value.ToString();
                //txt_Task_Name.Text = dataGridView1.CurrentRow.Cells["MSName"].Value.ToString();
                //txt_Tsak_Duration.Text = dataGridView1.CurrentRow.Cells["MSName"].Value.ToString();
                //dtp_Task_Start_Date.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["MSName"].Value.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

}
