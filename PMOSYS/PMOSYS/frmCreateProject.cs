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
    public partial class frmCreateProject : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;

        public frmCreateProject()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                da = new SqlDataAdapter(@"select Max(Project_SNo)+1 from Projects", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                
                string x = dt.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x))
                {
                    txtProjectNumber.Text = "1";
                }
                else

                {
                    txtProjectNumber.Text = x;
                }

                ConCls.CloseConnection();
                da.Dispose();
                dt.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred : " + ex.ToString(), "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void frmCreateProject_Load(object sender, EventArgs e)
        {
            GetZone();
            GetPlant();
            GetCites();
            GetDepartment();
            LoadAllprojectInfo();



        }

        void LoadAllprojectInfo()
        {
            da = new SqlDataAdapter(@"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status  from projects", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
           
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;

        }

        void GetZone()
        {

            da = new SqlDataAdapter(@"select * from Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbZone.DataSource = dt;
            cmbZone.DisplayMember = "ZoneName";
            cmbZone.ValueMember = "ZoneNo";
            
            //da.Dispose();
            //dt.Dispose();
        }

        void GetPlant()
        {
            da = new SqlDataAdapter(@"select * from Plants", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbPlant.DataSource = dt;
            cmbPlant.DisplayMember = "PlantName";
            cmbPlant.ValueMember = "PlantNo";

            //da.Dispose();
            //dt.Dispose();
        }

        void GetCites()
        {
            da = new SqlDataAdapter(@"select * from Sites", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbSiteNo.DataSource = dt;
            cmbSiteNo.DisplayMember = "SiteName";
            cmbSiteNo.ValueMember = "SiteNo";
            //da.Dispose();
            //dt.Dispose();
        }

        void GetDepartment()
        {
            da = new SqlDataAdapter(@"select * from Dept", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "DeptName";
            cmbDepartment.ValueMember = "DeptNo";
            //da.Dispose();
            //dt.Dispose();
        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            frmZone f = new frmZone();
            f.ShowDialog();

        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            frmSite f = new frmSite();
            f.ShowDialog();

        }

        private void btnPlant_Click(object sender, EventArgs e)
        {
            frmPlants f = new frmPlants();
            f.Show();


        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            frmDepartments f = new frmDepartments();
            f.Show();
        }

        private void btnPlant_Manager_Click(object sender, EventArgs e)
        {
            frmPlantManager f = new frmPlantManager();
            f.ShowDialog();
        }

        private void btnProjectManger_Click(object sender, EventArgs e)
        {
            frmProjectManager f = new frmProjectManager();
            f.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmConsultant f = new frmConsultant();
            f.ShowDialog();
        }

        private void dtpProjectStartDate_ValueChanged(object sender, EventArgs e)
        {
            CollectDate();
        }

        private void dtpProjectEndDate_ValueChanged(object sender, EventArgs e)
        {
            CollectDate();
        }
        void CollectDate()
        {
            DateTime StartDate = dtpProjectStartDate.Value.Date;
            DateTime EndDate = dtpProjectEndDate.Value.Date;
            TimeSpan r = EndDate - StartDate;
            txtProjectDuration.Text = r.TotalDays.ToString();
        }

        private void txtProjectDuration_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtProjectDuration.Text))
                {
                    
                    return;
                
                }
                else
                {
                DateTime StartDate = dtpProjectStartDate.Value.Date;
                DateTime EndDate = dtpProjectEndDate.Value.Date;
                EndDate = StartDate.AddDays(int.Parse(txtProjectDuration.Text));
                dtpProjectEndDate.Value = EndDate; 
                }
                
            }
        }

        private void txtContractorNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                da = new SqlDataAdapter(@"select * from Controactors where ConstractorNo like'" + txtContractorNumber.Text + "'", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any Controactor Number has this number: " + txtContractorNumber.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    txtContractorNameE.Text = dt.Rows[0]["NameE"].ToString();
                    txtContractorNameA.Text = dt.Rows[0]["NameA"].ToString();
                }



            }
        }

        private void txtProjectManagerNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                da = new SqlDataAdapter(@"select * from ProjectManagers where ProNo like'" + txtProjectManagerNo.Text + "'", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any Project Manager has this number: " + txtProjectManagerNo.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    txtProjectManagerName.Text = dt.Rows[0]["ProNameE"].ToString();
                }



            }
        }

        private void txtPlantManagerNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                da = new SqlDataAdapter(@"select * from PlantManagers where PMNo like'" + txtPlantManagerNo.Text + "'", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any Plant Manager has this number: " + txtPlantManagerNo.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //dt.Rows[0]["ProUserID"].ToString();
                    txtPlantManagerName.Text = dt.Rows[0]["PMrNameE"].ToString();
                }
            }
        }

        private void txtConsultantNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                da = new SqlDataAdapter(@"select * from Consultant where ConsultantNo='" + txtConsultantNumber.Text +"'", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any Consultant Number has this number: " + txtConsultantNumber.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    txtConsultantNameE.Text = dt.Rows[0]["NameE"].ToString();
                    txtConsultantNameA.Text = dt.Rows[0]["NameA"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          

                string sql = @"INSERT INTO [dbo].[Projects]
                                           (
                                            [Project_SNo]
                                            ,[ProjectNameE]
                                           ,[ProjectNameA]
                                           ,[ProjectNo_inBudget]
                                           ,[ContractorNo]
                                           ,[ConsultantNo]
                                           ,[ZoneNo]
                                           ,[SiteNo]
                                           ,[DeptNo]
                                           ,[PlantNo]
                                           ,[PlantManagerNo]
                                           ,[ProjectManagerNo]
                                           ,[StartDate]
                                           ,[EndDate]
                                           ,[ProjectDuration]
                                           ,[SiteHO_Date]
                                           ,[ContractSignDate]
                                           ,[Status]
                                           ,[ProjectCost]
                                           ,[Remarks]
                                           ,[CreatedBy]
                                           ,[CreatedDate])
                                     VALUES
                                           (
                                            @Project_SNo
                                            ,@ProjectNameE
                                           ,@ProjectNameA
                                           ,@ProjectNo_inBudget
                                           ,@ContractorNo
                                           ,@ConsultantNo
                                           ,@ZoneNo
                                           ,@SiteNo
                                           ,@DeptNo
                                           ,@PlantNo
                                           ,@PlantManagerNo
                                           ,@ProjectManagerNo
                                           ,@StartDate
                                           ,@EndDate
                                           ,@ProjectDuration
                                           ,@SiteHO_Date
                                           ,@ContractSignDate
                                           ,@Status
                                           ,@ProjectCost
                                           ,@Remarks
                                           ,@CreatedBy
                                           ,@CreatedDate)";


                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@Project_SNo", SqlDbType.Int).Value = int.Parse(txtProjectNumber.Text);
                cmd.Parameters.Add("@ProjectNameE", SqlDbType.NVarChar).Value = txtProjectName_E.Text;
                cmd.Parameters.Add("@ProjectNameA", SqlDbType.NVarChar).Value = txtProjectName_A.Text;            
                cmd.Parameters.Add("@ProjectNo_inBudget", SqlDbType.NVarChar).Value = txtProjectNumInBudget.Text;
                cmd.Parameters.Add("@ContractorNo", SqlDbType.NVarChar).Value = txtContractorNumber.Text;
                cmd.Parameters.Add("@ConsultantNo", SqlDbType.NVarChar).Value = txtConsultantNumber.Text;
                cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(cmbZone.SelectedValue.ToString());
                cmd.Parameters.Add("@SiteNo", SqlDbType.Int).Value = int.Parse(cmbSiteNo.SelectedValue.ToString());
                cmd.Parameters.Add("@DeptNo", SqlDbType.Int).Value = int.Parse(cmbDepartment.SelectedValue.ToString());
                cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = int.Parse(cmbPlant.SelectedValue.ToString());
                cmd.Parameters.Add("@PlantManagerNo", SqlDbType.NVarChar).Value = txtPlantManagerNo.Text;
                cmd.Parameters.Add("@ProjectManagerNo", SqlDbType.NVarChar).Value = txtProjectManagerNo.Text;
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = dtpProjectStartDate.Value.Date;
                cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = dtpProjectEndDate.Value.Date;
                cmd.Parameters.Add("@ProjectDuration", SqlDbType.Int).Value = int.Parse(txtProjectDuration.Text);
                cmd.Parameters.Add("@SiteHO_Date", SqlDbType.Date).Value = dtpProjectHODate.Value.Date;
                cmd.Parameters.Add("@ContractSignDate", SqlDbType.Date).Value = dtpContract_sign_date.Value.Date;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbProjectStatus.SelectedItem.ToString();
                cmd.Parameters.Add("@ProjectCost", SqlDbType.Decimal).Value = Convert.ToDecimal(txtProjectCost.Text);
                cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = txtRemarks.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = "UserID";
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                

            ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();

            

        }

        void Load_Report_Info()
        {
            da = new SqlDataAdapter(@"select * from V_All_Project_Info where Project_SNo like'" + txtProjectNumber.Text + "'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No any Project has this number: " + txtProjectNumber.Text + " . May you was not Enter fill all project data", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

               
            }
        }

        private void btnOpenSearch_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.Search;
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateProject_Click(object sender, EventArgs e)
        {

        }

        private void txtContractorNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {


            string sql_Str;

            if (cmbSearch.SelectedIndex == -1)
            {
                MessageBox.Show("Please select search field first");
                return;
            }
            if (string.IsNullOrEmpty(txtSearch.Text) || string.IsNullOrWhiteSpace(txtSearch.Text)) 
            {
                MessageBox.Show("Write the search value ");
                txtSearch.Focus();
                return;
            }

            if (cmbSearch.SelectedItem == "Project Number")
            {
                sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where Project_SNo = " + txtSearch.Text;
            }
            else if (cmbSearch.SelectedItem == "Project English Name")
            {
                sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where ProjectNameE like '%" + txtSearch.Text + "%'";
            }
            else if (cmbSearch.SelectedItem == "Project Arabic Name")
            {
                sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where ProjectNameA like '%" + txtSearch.Text + "%'";
            }
            else if (cmbSearch.SelectedItem == "Project Number in Budget")
            {
                sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where ProjectNo_inBudget=" + txtSearch.Text;
            }
            else if (cmbSearch.SelectedItem == "Project Status")
            {
                sql_Str = @"select Project_SNo, ProjectNameE, ProjectNameA, ProjectNo_inBudget,Status from projects Where Status=" + txtSearch.Text;
            }
            else
            {
                MessageBox.Show("Please select search field first, then write search value...");
                return;
            }

            da = new SqlDataAdapter(sql_Str, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            cmbZone.DataSource = dt;
            ConCls.CloseConnection();
            
            dataGridView1.DataSource = dt;




        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {


            string sql = @"update [dbo].[Projects]
                                         set  
                                            [ProjectNameE] = @ProjectNameE
                                           ,[ProjectNameA] = @ProjectNameA
                                           ,[ProjectNo_inBudget] = @ProjectNo_inBudget
                                           ,[ContractorNo] = @ContractorNo
                                           ,[ConsultantNo] = @ConsultantNo
                                           ,[ZoneNo] = @ZoneNo
                                           ,[SiteNo] = @SiteNo
                                           ,[DeptNo] = @DeptNo
                                           ,[PlantNo] = @PlantNo
                                           ,[PlantManagerNo] = @PlantManagerNo
                                           ,[ProjectManagerNo] = @ProjectManagerNo
                                           ,[StartDate] = @StartDate
                                           ,[EndDate] = @EndDate
                                           ,[ProjectDuration] = @ProjectDuration
                                           ,[SiteHO_Date] = @SiteHO_Date
                                           ,[ContractSignDate] = @ContractSignDate
                                           ,[Status] = @Status
                                           ,[ProjectCost] = @ProjectCost
                                            ,[Remarks] = @Remarks
                                            where [Project_SNo] = " + txtProjectNumber.Text;


            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            //cmd.Parameters.Add("@Project_SNo", SqlDbType.Int).Value = int.Parse(txtProjectNumber.Text);
            cmd.Parameters.Add("@ProjectNameE", SqlDbType.NVarChar).Value = txtProjectName_E.Text;
            cmd.Parameters.Add("@ProjectNameA", SqlDbType.NVarChar).Value = txtProjectName_A.Text;
            cmd.Parameters.Add("@ProjectNo_inBudget", SqlDbType.NVarChar).Value = txtProjectNumInBudget.Text;
            cmd.Parameters.Add("@ContractorNo", SqlDbType.NVarChar).Value = txtContractorNumber.Text;
            cmd.Parameters.Add("@ConsultantNo", SqlDbType.NVarChar).Value = txtConsultantNumber.Text;
            cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(cmbZone.SelectedValue.ToString());
            cmd.Parameters.Add("@SiteNo", SqlDbType.Int).Value = int.Parse(cmbSiteNo.SelectedValue.ToString());
            cmd.Parameters.Add("@DeptNo", SqlDbType.Int).Value = int.Parse(cmbDepartment.SelectedValue.ToString());
            cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = int.Parse(cmbPlant.SelectedValue.ToString());
            cmd.Parameters.Add("@PlantManagerNo", SqlDbType.NVarChar).Value = txtPlantManagerNo.Text;
            cmd.Parameters.Add("@ProjectManagerNo", SqlDbType.NVarChar).Value = txtProjectManagerNo.Text;
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = dtpProjectStartDate.Value.Date;
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = dtpProjectEndDate.Value.Date;
            cmd.Parameters.Add("@ProjectDuration", SqlDbType.Int).Value = int.Parse(txtProjectDuration.Text);
            cmd.Parameters.Add("@SiteHO_Date", SqlDbType.Date).Value = dtpProjectHODate.Value.Date;
            cmd.Parameters.Add("@ContractSignDate", SqlDbType.Date).Value = dtpContract_sign_date.Value.Date;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbProjectStatus.SelectedItem.ToString();
            cmd.Parameters.Add("@ProjectCost", SqlDbType.Decimal).Value = Convert.ToDecimal(txtProjectCost.Text);
            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = txtRemarks.Text;
            //  cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = "UserID";
            //  cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd updated successfully", "update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {

                da = new SqlDataAdapter(@"select * from Projects where Project_SNo=" + txtProjectNumber.Text , ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);

                txtProjectName_E.Text = dt.Rows[0]["ProjectNameE"].ToString();
                txtProjectName_A.Text = dt.Rows[0]["ProjectNameA"].ToString();
                txtProjectNumInBudget.Text = dt.Rows[0]["ProjectNo_inBudget"].ToString();
                txtPlantManagerNo.Text = dt.Rows[0]["PlantManagerNo"].ToString();
                txtProjectManagerNo.Text = dt.Rows[0]["ProjectManagerNo"].ToString();
                txtProjectCost.Text = dt.Rows[0]["ProjectCost"].ToString();

                txtContractorNumber.Text = dt.Rows[0]["ContractorNo"].ToString();
               
                txtConsultantNumber.Text = dt.Rows[0]["ConsultantNo"].ToString();
                
                //load DateTimePickers 
                DateTime Strat_Date = DateTime.Parse(dt.Rows[0]["StartDate"].ToString());
                DateTime End_Date = DateTime.Parse(dt.Rows[0]["EndDate"].ToString());
                DateTime SiteHO_Date = DateTime.Parse(dt.Rows[0]["SiteHO_Date"].ToString());
                DateTime ContractSignDate = DateTime.Parse(dt.Rows[0]["ContractSignDate"].ToString());
                dtpProjectStartDate.Value = Strat_Date;
                dtpProjectEndDate.Value = End_Date;
                dtpProjectHODate.Value = SiteHO_Date;
                dtpContract_sign_date.Value = ContractSignDate;

                // Load comboxs data
                if (!string.IsNullOrEmpty(dt.Rows[0]["DeptNo"].ToString()))
                {
                    cmbDepartment.SelectedValue = dt.Rows[0]["DeptNo"].ToString();
                }
                //else
                //{
                //    cmbDepartment.SelectedIndex = -1;
                //}


                if (!string.IsNullOrEmpty(dt.Rows[0]["ZoneNo"].ToString()))
                {
                    cmbZone.SelectedValue = dt.Rows[0]["ZoneNo"].ToString();
                }
                //else
                //{
                //    cmbZone.SelectedIndex = -1;
                //}

                if (!string.IsNullOrEmpty(dt.Rows[0]["SiteNo"].ToString()))
                {
                    cmbSiteNo.SelectedValue = dt.Rows[0]["SiteNo"].ToString();
                }
                //else
                //{
                //    cmbZone.SelectedIndex = -1;
                //}
                if (!string.IsNullOrEmpty(dt.Rows[0]["PlantNo"].ToString()))
                {
                    cmbPlant.SelectedValue = dt.Rows[0]["PlantNo"].ToString();
                }
                //else
                //{
                //    cmbPlant.SelectedIndex = -1;
                //}

                if (dt.Rows[0]["Status"].ToString()== "-1")
                {
                    cmbProjectStatus.SelectedIndex = -1;
                }
                else
                {
                    
                    cmbProjectStatus.SelectedItem = dt.Rows[0]["Status"].ToString();
                    MessageBox.Show(dt.Rows[0]["Status"].ToString());
                }


                


                //MessageBox.Show(Strat_Date + " - " + End_Date + " - " + SiteHO_Date + " - " + ContractSignDate);


                ConCls.CloseConnection();

                da.Dispose();
                dt.Dispose();
                GetConsltantInfo();
                GetContractorInfo();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred : " + ex.ToString(), "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void txtConsultantNumber_TextChanged(object sender, EventArgs e)
        {

        }

        void GetContractorInfo()
        {
            da = new SqlDataAdapter(@"select * from Controactors where ConstractorNo like'" + txtContractorNumber.Text + "'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No any Controactor Number has this number: " + txtContractorNumber.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                txtContractorNameE.Text = dt.Rows[0]["NameE"].ToString();
                txtContractorNameA.Text = dt.Rows[0]["NameA"].ToString();
            }
        }
        void GetConsltantInfo()
        {
            da = new SqlDataAdapter(@"select * from Consultant where ConsultantNo like'" + txtConsultantNumber.Text + "'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No any Consultant Number has this number: " + txtConsultantNumber.Text + " .", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                txtConsultantNameE.Text = dt.Rows[0]["NameE"].ToString();
                txtConsultantNameA.Text = dt.Rows[0]["NameA"].ToString();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}


