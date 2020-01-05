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
    public partial class frmKeyMilestone : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        public frmKeyMilestone()
        {
            InitializeComponent();
        }

        private void frmKeyMilestone_Load(object sender, EventArgs e)
        {

        }

        private void btnSearchPanal_Click(object sender, EventArgs e)
        {
            if (pnlSearch.Visible == false)
            {
                pnlSearch.Visible = true;

            }
            else
            {
                pnlSearch.Visible = false;
            }
        }




        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                
                da = new SqlDataAdapter(@"select * from Projects where Project_SNo =" + txtProjectNo.Text, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("No any project has this number : ({0})",txtProjectNo.Text), "Data not found ..",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtProjectNameEng.Text = dt.Rows[0]["ProjectNameE"].ToString();
                    txtProjectNameArb.Text = dt.Rows[0]["ProjectNameA"].ToString();
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();
                    txtStartDate.Text = dt.Rows[0]["StartDate"].ToString();
                    txtEndDate.Text = dt.Rows[0]["EndDate"].ToString();
                    txtDuration.Text = dt.Rows[0]["ProjectDuration"].ToString();

                    string PlantManager = dt.Rows[0]["PlantManagerNo"].ToString();
                    string ProjectManager = dt.Rows[0]["ProjectManagerNo"].ToString();


                    txtPlantManager.Text  = GetPlantManagerName(PlantManager);
                    txtProjectManager.Text = GetPlantManagerName(ProjectManager);
                    GetProjectMileStones();
                    lblCountTast.Text = dt.Rows.Count.ToString();
                    g1();
                }
            }
           

        }
        string GetPlantManagerName(string PlantManagerNumeber) 
        {
            if (string.IsNullOrEmpty(PlantManagerNumeber))
            {
                MessageBox.Show("No plant manage was added in this project", "No data ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "No Plant manager for this project";
            }
            else
            {
                da = new SqlDataAdapter(@"select * from PlantManagers where PMNo =" + PlantManagerNumeber, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("No any plant manager was found & has this number : ({0})", PlantManagerNumeber), "Data not found ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    return dt.Rows[0][1].ToString();
                }
            }
        }

        string GetProjectManagerName(string ProjectManagerNumeber)
        {

            if (string.IsNullOrEmpty(ProjectManagerNumeber))
            {
                MessageBox.Show("No project manager was added in this project", "No data ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "No Plant manager for this project";
            }
            else
            {
                da = new SqlDataAdapter(@"select * from ProjectManagers where ProNo =" + ProjectManagerNumeber, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("No any project manager was found & has this number : ({0})", ProjectManagerNumeber), "Data not found ..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    return dt.Rows[0][1].ToString();
                }
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            DateTime _Project_date = Convert.ToDateTime(txtStartDate.Text);
            MessageBox.Show(_Project_date.ToString());
            if (dtpStartDate.Value.Date < _Project_date)
            {
                errorProvider1.SetError(dtpStartDate, "Miestone date should be bigger than project start date");
                MessageBox.Show("Miestone date should be bigger than project start date");
                return;
            }
            else
            {
                errorProvider1.Clear();

            }
            if (string.IsNullOrEmpty(txtProjectNo.Text) || string.IsNullOrWhiteSpace(txtProjectNo.Text))
            {
                errorProvider1.SetError(txtProjectNo, "You should enter project no.");
                txtProjectNo.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
        
            if (string.IsNullOrEmpty(txtMilestonNameEng.Text) || string.IsNullOrWhiteSpace(txtMilestonNameEng.Text))
            {
                errorProvider1.SetError(txtMilestonNameEng, "Write milestone name in english.");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtMilestonNameArb.Text) || string.IsNullOrWhiteSpace(txtMilestonNameArb.Text))
            {
                errorProvider1.SetError(txtMilestonNameArb, " Write milestone name in arabic.");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(dtpStartDate.Value.ToString()) || string.IsNullOrWhiteSpace(dtpStartDate.Value.ToString()))
            {
                errorProvider1.SetError(dtpStartDate, " Enter start date.");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtMilestoneDuration.Text) || string.IsNullOrWhiteSpace(txtMilestoneDuration.Text))
            {
                errorProvider1.SetError(txtMilestoneDuration, "write milestone duration in number");
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbStatus, "Choose milestone from list");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            try
            {

               
                string sql = @"INSERT INTO [Milestones]
                                    ([ProjectNo]
                                        ,[MSNo]
                                       ,[MSName]
                                       ,[MSStratDate]
                                       ,[MSDuration]
                                       ,[MSAuctEndDate]
                                        ,[MSStatus]
                                       ,[MSNote])

                                    VALUES
                                    (@ProjectNo     
                                    ,@MSNo
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
                cmd.Parameters.Add("@MSNo", SqlDbType.Int).Value = int.Parse(txtMilestoneNo.Text);
                cmd.Parameters.Add("@MSName", SqlDbType.NVarChar).Value = txtMilestonNameEng.Text;
                cmd.Parameters.Add("@MSStratDate", SqlDbType.Date).Value = dtpStartDate.Value.Date;
                cmd.Parameters.Add("@MSDuration", SqlDbType.Int).Value = int.Parse(txtMilestoneDuration.Text); ;
                cmd.Parameters.Add("@MSAuctEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@MSStatus", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
                cmd.Parameters.Add("@MSNote", SqlDbType.NVarChar).Value = txtRemark.Text;
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
                GetProjectMileStones();
                g1();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Sorry there are an error happend... /n"+ ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }

        void SearchForKeystons()
        {
            try
            {
                dataGridView1.DataSource = null;
                da = new SqlDataAdapter(string.Format(@"select * from Milestones where MSName like '%{0}%' and ProjectNo = {1}", txtSearch.Text, txtProjectNo.Text), ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    lblSearchResult.Text = string.Format("Can not find any key milestone like : {0}", txtSearch.Text);
                    return;

                }
                else
                {
                    dataGridView1.DataSource = dt;
                    lblSearchResult.Text = string.Format("Search result : {0} record/s", dt.Rows.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectNo.Text))
            {
                MessageBox.Show("Enter Project number first");
                return;
            }
           
            if (pnlKeyMilestone.Enabled == false)
            {
                pnlKeyMilestone.Enabled = true;
            }
            else 
            {
                pnlKeyMilestone.Enabled = false;
            }
            generateMilestonesNumber();
        }

        void generateMilestonesNumber()
        {

            
            da = new SqlDataAdapter(@"select max(MSNo)+1 from Milestones", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
           
               
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                {
                txtMilestoneNo.Text = "1";
                }
            else
            {
                txtMilestoneNo.Text = dt.Rows[0][0].ToString(); 
            }
                
            }


        void GetProjectMileStones()
        {
            try
            {
                dataGridView1.DataSource = null;
                da = new SqlDataAdapter(@"select * from Milestones where ProjectNo ="+ txtProjectNo.Text, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any Milestones was found for this project.", "No Milestones ");
                    return;

                }
                else
                {
                    dataGridView1.DataSource = dt;
                    formatDGV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ConCls.CloseConnection();
                return;
            }
        }

        private void txtMilestoneDuration_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                {
                if (string.IsNullOrWhiteSpace(txtMilestoneDuration.Text) || string.IsNullOrEmpty(txtMilestoneDuration.Text))
                {
                   MessageBox.Show("Enter the duration in (days) for the milestone ");
                    txtMilestoneDuration.Focus();
                    return;
                }
                else
                {
                    
                    DateTime d = dtpStartDate.Value.Date;
                    dtpStartDate.Value = d.AddDays(int.Parse(txtMilestoneDuration.Text));

                }
            }
        }

        void CountTask()
        {
            try
            {
                dataGridView1.DataSource = null;
                SqlDataAdapter da1 = new SqlDataAdapter(@"select count(MSNo) from Milestones where ProjectNo =" + txtProjectNo.Text, ConCls.con);
                DataTable dt1 = new DataTable();
                ConCls.OpenConnection();
                da1.Fill(dt1);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    lblCountTast.Text = "0";
                    return;

                }
                else
                {
                    lblCountTast.Text = dt1.Rows[0][0].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ConCls.CloseConnection();
                return;
            }

        }

        void g1()
        {
            chart1.Series["s1"].Points.Clear();
            SqlCommand cmd = new SqlCommand("Select * from Milestones where ProjectNo =" + txtProjectNo.Text, ConCls.con);
            SqlDataReader dr;

            try
            {

                ConCls.OpenConnection();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DateTime _Satrt_Date = dr.GetDateTime(3);
                    int _Duarition = dr.GetInt32(4);
                    DateTime _DateAfterAddDuarition = _Satrt_Date.AddDays(_Duarition);

                    int _result = (_Satrt_Date - DateTime.Now.Date).Days;
                    this.chart1.Series["s1"].Points.AddXY(dr.GetString(2), _result);

                }
                dr.Close();
                cmd.Dispose();
                ConCls.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void formatDGV()
        {
           

            dataGridView1.Columns[1].Visible = false;
            for (int x = 0; x < dataGridView1.Rows.Count; x++)
            {
                if (dataGridView1.Rows[x].Cells["MSStatus"].Value.ToString() == "Finish")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.LightSalmon;
                  
                }
                if (dataGridView1.Rows[x].Cells["MSStatus"].Value.ToString() == "Completed")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.Gold;
                   
                }
                if (dataGridView1.Rows[x].Cells["MSStatus"].Value.ToString() == "Start")
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor =Color.GreenYellow;
                    
                }
               
            }
        
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql = @"update [Milestones] set
                                       [MSName] = @MSName
                                       ,[MSStratDate] =@MSStratDate
                                       ,[MSDuration] = @MSDuration
                                       ,[MSAuctEndDate] = @MSAuctEndDate 
                                       ,[MSStatus] = @MSStatus
                                       ,[MSNote] = @MSNote
                                       where MSNo = " + txtMilestoneNo.Text;
            MessageBox.Show(sql);

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
           
            cmd.Parameters.Add("@MSName", SqlDbType.NVarChar).Value = txtMilestonNameEng.Text;
            cmd.Parameters.Add("@MSStratDate", SqlDbType.Date).Value = dtpStartDate.Value.Date;
            cmd.Parameters.Add("@MSDuration", SqlDbType.Int).Value = int.Parse(txtMilestoneDuration.Text); ;
            cmd.Parameters.Add("@MSAuctEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@MSStatus", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
            cmd.Parameters.Add("@MSNote", SqlDbType.NVarChar).Value = txtRemark.Text;
            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd updated successfully", "Updated record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();
            GetProjectMileStones();
            g1();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure like to delete this recourd: " + txtMilestonNameEng.Text , "Confirem delete record", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else if (dialogResult == DialogResult.Yes)
            {
               
            
            string sql = @"delete from [Milestones] where MSNo = " + txtMilestoneNo.Text;
            MessageBox.Show(sql);

            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd deleted successfully", "Delete record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();
            GetProjectMileStones();
            g1();
            }
        }
    }


    
}
