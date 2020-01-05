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
   
    public partial class frmNewProject : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;

        string sql_str;


        public frmNewProject()
        {
            
            InitializeComponent();
        }

     

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            //DateTime fd = dateTimePicker2.Value.Date;
            //DateTime ed = dateTimePicker3.Value.Date;
            //if (ed < fd) {
            //    MessageBox.Show("لايمكن ان يكون تاريخ النهاية قبل تاريخ البداية","Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //TimeSpan x = ed - fd;
            //textBox16.Text = x.Days.ToString();
        }

        private void frmNewProject_Load(object sender, EventArgs e)
        {
            GetZone();
            GetSites();

        }


        void GetZone()
        {
           
            da = new SqlDataAdapter(@"select * from Zone", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
         
            
            cmbZone.DisplayMember = "ZoneName";
            cmbZone.ValueMember = "ZoneNo";
            cmbZone.DataSource = dt;
            ConCls.CloseConnection();
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
            cmbSiteNo.DataSource = dt;
            cmbSiteNo.DisplayMember = "PlantName";
            cmbSiteNo.ValueMember = "PlantNo";
            //da.Dispose();
            //dt.Dispose();
        }

        void GetSites()
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
            cmbSiteNo.DataSource = dt;
            cmbSiteNo.DisplayMember = "DeptName";
            cmbSiteNo.ValueMember = "DeptNo";
            //da.Dispose();
            //dt.Dispose();
        }



        private void txtPlantManagerNo_KeyUp(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPlantManagerNo.Text))
                {
                    MessageBox.Show("You can not keep plant manager no empty", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPlantManagerNo.Focus();
                    return;
                }
                sql_str = @"select * from PlantManagers where PMNo like '%" + txtPlantManagerNo.Text + "%'";

                da = new SqlDataAdapter(sql_str, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any plant manager has this no " + txtPlantManagerNo.Text + ", Please try agine", "Data Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtPlantManagerName.Text = dt.Rows[0][1].ToString();
                    txtProjectManagerNo.Focus();
                }
                da.Dispose();
                dt.Dispose();

            }

        
    }

        private void button1_Click(object sender, EventArgs e)
        {
            frmConsultant f = new frmConsultant();
            f.Show();


        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            frmZone f = new frmZone();
            f.Show();

        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            frmSite f = new frmSite();
            f.Show();

        }


        

        void Save() {

            string sql = @"INSERT INTO [dbo].[Projects]
                                           ([ProjectNameE]
                                           ,[ProjectNameA]
                                           ,[ProjectNo]
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
                                           ,[CreatedBy]
                                           ,[CreatedDate])
                                     VALUES
                                           (
                                            @ProjectNameE
                                           ,@ProjectNameA
                                           ,@ProjectNo
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
                                           ,@CreatedBy
                                           ,@CreatedDate)";


            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ProjectNameE", SqlDbType.NVarChar).Value = txtProjectName_E.Text;
            cmd.Parameters.Add("@ProjectNameA", SqlDbType.NVarChar).Value = txtProjectName_A.Text;
            cmd.Parameters.Add("@ProjectNo", SqlDbType.NVarChar).Value = txtProjectNumber.Text;
            cmd.Parameters.Add("@ProjectNo_inBudget", SqlDbType.NVarChar).Value = txtProjectNumInBudget.Text;
            cmd.Parameters.Add("@ContractorNo", SqlDbType.NVarChar).Value = txtContractorNameE.Text ;
            cmd.Parameters.Add("@ConsultantNo", SqlDbType.NVarChar).Value = txtConsultantNumber.Text;
            cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(cmbZone.SelectedIndex.ToString());
            cmd.Parameters.Add("@SiteNo", SqlDbType.Int).Value = int.Parse(cmbSiteNo.SelectedIndex.ToString());
            cmd.Parameters.Add("@DeptNo", SqlDbType.Int).Value = int.Parse(cmbDepartment.SelectedIndex.ToString());
            cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = int.Parse(cmbPlant.SelectedIndex.ToString());
            cmd.Parameters.Add("@PlantManagerNo", SqlDbType.NVarChar).Value = txtPlantManagerNo.Text;
            cmd.Parameters.Add("@ProjectManagerNo", SqlDbType.NVarChar).Value = txtProjectManagerNo.Text;
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = dtpProjectStartDate.Value.Date;//
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = dtpProjectEndDate.Value.Date;//
            cmd.Parameters.Add("@ProjectDuration", SqlDbType.Int).Value = int.Parse(txtProjectDuration.Text);
            cmd.Parameters.Add("@SiteHO_Date", SqlDbType.Date).Value = dtpProjectHODate.Value.Date;//
            cmd.Parameters.Add("@ContractSignDate", SqlDbType.Date).Value = dtpContract_sign_date.Value.Date;//
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbProjectStatus.SelectedIndex.ToString();
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = "UserID";
            cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
           
            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();
            
        }

        void Clear() {

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                    txtProjectNumber.Focus();

                }
            }
            foreach (Control c in this.Controls)
            {

                cmbDepartment.SelectedIndex = -1;
                cmbPlant.SelectedIndex = -1;
                cmbProjectStatus.SelectedIndex = -1;
                cmbSiteNo.SelectedIndex = -1;
                cmbZone.SelectedIndex = -1;
                
            }

        }

        private void btnpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            Clear();
        }


        void Update()
        {

            string sql = @"update Projects set 
                                           (
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
                                            ) 
                                            where [ProjectNo] =" + txtProjectNumber.Text;


            cmd = new SqlCommand();
            cmd.Connection = ConCls.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ProjectNameE", SqlDbType.NVarChar).Value = txtProjectName_E.Text;
            cmd.Parameters.Add("@ProjectNameA", SqlDbType.NVarChar).Value = txtProjectName_A.Text;   
            cmd.Parameters.Add("@ProjectNo_inBudget", SqlDbType.NVarChar).Value = txtProjectNumInBudget.Text;
            cmd.Parameters.Add("@ContractorNo", SqlDbType.NVarChar).Value = txtContractorNameE.Text;
            cmd.Parameters.Add("@ConsultantNo", SqlDbType.NVarChar).Value = txtConsultantNumber.Text;
            cmd.Parameters.Add("@ZoneNo", SqlDbType.Int).Value = int.Parse(cmbZone.SelectedIndex.ToString());
            cmd.Parameters.Add("@SiteNo", SqlDbType.Int).Value = int.Parse(cmbSiteNo.SelectedIndex.ToString());
            cmd.Parameters.Add("@DeptNo", SqlDbType.Int).Value = int.Parse(cmbDepartment.SelectedIndex.ToString());
            cmd.Parameters.Add("@PlantNo", SqlDbType.Int).Value = int.Parse(cmbPlant.SelectedIndex.ToString());
            cmd.Parameters.Add("@PlantManagerNo", SqlDbType.NVarChar).Value = txtPlantManagerNo.Text;
            cmd.Parameters.Add("@ProjectManagerNo", SqlDbType.NVarChar).Value = txtProjectManagerNo.Text;
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = dtpProjectStartDate.Value.Date;//
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = dtpProjectEndDate.Value.Date;//
            cmd.Parameters.Add("@ProjectDuration", SqlDbType.Int).Value = int.Parse(txtProjectDuration.Text);
            cmd.Parameters.Add("@SiteHO_Date", SqlDbType.Date).Value = dtpProjectHODate.Value.Date;//
            cmd.Parameters.Add("@ContractSignDate", SqlDbType.Date).Value = dtpContract_sign_date.Value.Date;//
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbProjectStatus.SelectedIndex.ToString();
 
            ConCls.OpenConnection();
            cmd.ExecuteNonQuery();
            ConCls.CloseConnection();
            MessageBox.Show("Recourd Updated successfully", "Updated record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();

        }

        private void btnPlant_Manager_Click(object sender, EventArgs e)
        {
            /// to get value from plant manager windows
            /// you must decler parameter in this form (public) by using this name PARAMETTER_G
            frmPlantManager f = new frmPlantManager();
            f.PARAMETTER_G = "NewProject";
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtPlantManagerNo.Text = f.G_Value;
            }

        }

        private void txtPlantManagerNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProjectManagerNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtProjectManagerNo.Text))
                {
                    MessageBox.Show("You can not keep plant manager no empty", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtProjectManagerNo.Focus();
                    return;
                }
                sql_str = @"select * from txtProjectManagerNo where PMNo like '%" + txtProjectManagerNo.Text + "%'";

                da = new SqlDataAdapter(sql_str, ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No any project manager has this no " + txtProjectManagerNo.Text + ", Please try agine", "Data Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtPlantManagerName.Text = dt.Rows[0][1].ToString();
                    txtProjectManagerNo.Focus();
                }
                da.Dispose();
                dt.Dispose();

            }

        }

        private void txtProjectDuration_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
