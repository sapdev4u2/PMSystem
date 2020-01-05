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
    public partial class frmPojectProcess : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string ProgressNo;

        public frmPojectProcess()
        {
            InitializeComponent();
        }

        private void frmPojectProcess_Load(object sender, EventArgs e)
        {

        }

        void FindProject(string projectNo)
        {
            da = new SqlDataAdapter(@"select * from projects where Project_SNo =" + projectNo, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No project no was found");
                return;
            }
            else
            {
                string a_name = dt.Rows[0]["ProjectNameE"].ToString();
                string e_name = dt.Rows[0]["ProjectNameA"].ToString();
                txtProjoctName.Text = a_name + System.Environment.NewLine + e_name;
                txtProjectCost.Text = dt.Rows[0]["ProjectCost"].ToString();
                txtProjectStatus.Text = dt.Rows[0]["Status"].ToString();
                GetProjectProgress(txtProjectNo.Text);
            }

            da.Dispose();
           
        }

        void GetProjectProgress(string ProjectNo)
        {
            da = new SqlDataAdapter(@"select * from ProjectProgress where ProjectNo =" + ProjectNo, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            if (dt.Rows.Count == 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {

                dataGridView1.DataSource = dt;

            }
            da.Dispose();
            g1();
            g2();
            g3();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindProject(txtProjectNo.Text);
        }

        private void btnPlannedProcess_Click(object sender, EventArgs e)
        {
            if (pnl_Planned.Visible == false)
            {
                pnl_Planned.Visible = true;
                //pnl_Actual.Visible = false;
            }
            else
            {
                pnl_Planned.Visible = false;
                //pnl_Actual.Visible = false;
            }
        }

        private void btnActualProcess_Click(object sender, EventArgs e)
        {
        //    if (pnl_Actual.Visible == false)
        //    {
        //        pnl_Planned.Visible = false;
        //        pnl_Actual.Visible = true;
        //    }
        //    else
        //    {
        //        pnl_Planned.Visible = false;
        //        pnl_Actual.Visible = false;
        //    }
        }

        private void btnSavePlanned_Click(object sender, EventArgs e)
        {
            GetProgressNo();
            string month_no = cmbMonth.SelectedItem.ToString() + txtYear.Text;
            MessageBox.Show(month_no);

            if (string.IsNullOrEmpty(txtMonthlyPlanned.Text) || string.IsNullOrWhiteSpace(txtMonthlyPlanned.Text))
                {
                MessageBox.Show("You must enter the monthly planned percentage..");
                txtMonthlyPlanned.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAcualMonthly.Text) || string.IsNullOrWhiteSpace(txtAcualMonthly.Text))
            {

                txtAcualMonthly.Text = "0";
                txtAcualMonthlyValue.Text = "0";


            }
           
            try
            {

                ConCls.OpenConnection();
                string sql = @"INSERT INTO [ProjectProgress]
                                       ( [ProgressNo]
                                        ,[ProjectNo]
                                        ,[MonthNo]
                                        ,[MonthlyPlanned]
                                        ,[MonthlyValue]
                                        ,[MonthlyActual]
                                        ,[ActualValue]
                                       )

                                    VALUES
                                    (@ProgressNo
                                    ,@ProjectNo     
                                    ,@MonthNo
                                    ,@MonthlyPlanned
                                    ,@MonthlyValue
                                    ,@MonthlyActual
                                    ,@ActualValue
                                    )";

                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                MessageBox.Show(ProgressNo + " --- " + txtProjectNo.Text  + " --- " + month_no + " --- "  + txtMonthlyPlanned.Text + " --- " + txtValuePlanned.Text);
                cmd.Parameters.Add("@ProgressNo", SqlDbType.Int).Value = int.Parse(ProgressNo) ; 
                cmd.Parameters.Add("@ProjectNo", SqlDbType.NVarChar).Value = txtProjectNo.Text;
                cmd.Parameters.Add("@MonthNo", SqlDbType.NVarChar).Value = month_no;
                cmd.Parameters.Add("@MonthlyPlanned", SqlDbType.Decimal).Value = decimal.Parse(txtMonthlyPlanned.Text);
                cmd.Parameters.Add("@MonthlyValue", SqlDbType.Decimal).Value = decimal.Parse(txtValuePlanned.Text);
                cmd.Parameters.Add("@MonthlyActual", SqlDbType.Decimal).Value = decimal.Parse(txtAcualMonthly.Text);
                cmd.Parameters.Add("@ActualValue", SqlDbType.Decimal).Value = decimal.Parse(txtAcualMonthlyValue. Text);

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend... /n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }


        void GetProgressNo()
        {
            try
            {

                da = new SqlDataAdapter(@"select Max(ProgressNo)+1 from ProjectProgress", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                string x = dt.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x))
                {
                    ProgressNo = "1";
                    MessageBox.Show(" ----- step 1----->>" + ProgressNo);
                
                }
                else {
                    ProgressNo = x;
                    MessageBox.Show( " ----- step 2----->>" + ProgressNo);

                }
               

                
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

        private void btnCancelPlanned_Click(object sender, EventArgs e)
        {
            GetProgressNo();
        }

        private void btnUpdatePlanned_Click(object sender, EventArgs e)
        {
            string month_no = cmbMonth.SelectedItem.ToString() + txtYear.Text;
            try
            {

                ConCls.OpenConnection();
                string sql = @"update [ProjectProgress] set
                                        [ProjectNo] = @ProjectNo  
                                        ,[MonthNo] = @MonthNo
                                        ,[MonthlyPlanned] =@MonthlyPlanned
                                        ,[MonthlyValue] = @MonthlyValue
                                       where ProgressNo = " + txtProjectNo.Text;

                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

               // MessageBox.Show(ProgressNo + " --- " + txtProjectNo.Text + " --- " + month_no + " --- " + txtMonthlyPlanned.Text + " --- " + txtValuePlanned.Text);
               // cmd.Parameters.Add("@ProgressNo", SqlDbType.Int).Value = int.Parse(ProgressNo);
                cmd.Parameters.Add("@ProjectNo", SqlDbType.NVarChar).Value = txtProjectNo.Text;
                cmd.Parameters.Add("@MonthNo", SqlDbType.NVarChar).Value = month_no;
                cmd.Parameters.Add("@MonthlyPlanned", SqlDbType.Decimal).Value = decimal.Parse(txtMonthlyPlanned.Text);
                cmd.Parameters.Add("@MonthlyValue", SqlDbType.Decimal).Value = decimal.Parse(txtValuePlanned.Text);

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend... /n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
 string mno;
            mno = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            MessageBox.Show(mno);
            }
           
        }

        void g1() 
        {
            chart1.Series["s1"].Points.Clear();
            SqlCommand cmd = new SqlCommand("Select * from ProjectProgress where ProjectNo =" + txtProjectNo.Text, ConCls.con);
            SqlDataReader dr;

            try
            {

                ConCls.OpenConnection();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.chart1.Series["s1"].Points.AddXY(dr.GetString(2), dr.GetDecimal(4));
                    
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

        void g2()
        {
            this.chart2.Series["Planned"].Points.Clear();
            this.chart2.Series["Actual"].Points.Clear();
            SqlCommand cmd = new SqlCommand("Select * from ProjectProgress where ProjectNo =" +txtProjectNo.Text , ConCls.con);
            SqlDataReader dr;

            try
            {
                ConCls.OpenConnection();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.chart2.Series["Planned"].Points.AddXY(dr.GetString(2), dr.GetDecimal(4));
                    this.chart2.Series["Actual"].Points.AddXY(dr.GetString(2), dr.GetDecimal(6));
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

        void g3()
        { 
            try
            {
            this.chart3.Series["Payment"].Points.Clear();
//            this.chart3.Series["Plance"].Points.Clear();
             da = new SqlDataAdapter("Select sum(ActualValue) from ProjectProgress where  ProjectNo =" + txtProjectNo.Text, ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            string v = dt.Rows[0][0].ToString();
             //   MessageBox.Show(v);
                
                decimal project_cost = Convert.ToDecimal(txtProjectCost.Text);
                decimal rema = project_cost - Convert.ToDecimal(v);

                txtTotal_of_payment.Text = Convert.ToString(Math.Round(Convert.ToDecimal(v), 2));
                txtRemaining_Amount.Text = Convert.ToString(Math.Round (rema,2));


                decimal per1 = (Convert.ToDecimal(v) / project_cost) * 100;
                decimal per2 = 100- per1;
                txtTotal_of_paymentPer.Text = Convert.ToString(Math.Round(per1,2));
                txtRemaining_Amount_Per.Text = Convert.ToString(Math.Round(per2, 2));


                MessageBox.Show(rema.ToString());
                    this.chart3.Series["Payment"].Points.AddXY("Plance",rema);
                    this.chart3.Series["Payment"].Points.AddXY("Payment",Convert.ToDecimal(v));
                

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnDeletePlanned_Click(object sender, EventArgs e)
        {
           
        }

        private void txtMonthlyPlanned_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal ProjectCost = Convert.ToDecimal(txtProjectCost.Text);
                decimal PlannedPercentage = Convert.ToDecimal(txtMonthlyPlanned.Text);
                decimal result = ProjectCost / 100 * (PlannedPercentage);
                txtValuePlanned.Text = result.ToString();


            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {

                ConCls.OpenConnection();
                string sql = @"update [ProjectProgress] set
                                        [MonthlyActual] = @MonthlyActual  
                                        ,[ActualValue] = @ActualValue
                                        ,[PayDate] = @PayDate
                                       where ProgressNo = " + txtProjectNo.Text;

                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

              
                cmd.Parameters.Add("@MonthlyActual", SqlDbType.Decimal).Value = decimal.Parse(txtAcualMonthly.Text);
                cmd.Parameters.Add("@ActualValue", SqlDbType.Decimal).Value = decimal.Parse(txtAcualMonthlyValue.Text);
                cmd.Parameters.Add("@PayDate", SqlDbType.DateTime).Value = dateTimePicker1.Value;

                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("Recourd Saved successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are an error happend... /n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void txtAcualMonthly_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal ProjectCost = Convert.ToDecimal(txtProjectCost.Text);
                decimal PlannedPercentage = Convert.ToDecimal(txtAcualMonthly.Text);
                decimal result = ProjectCost / 100 * (PlannedPercentage);
                txtAcualMonthlyValue.Text = result.ToString();


            }
        }
    }
}
