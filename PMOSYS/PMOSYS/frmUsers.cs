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
    public partial class frmUsers : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string UereS_No;
        public frmUsers()
        {
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            GetAllUserData();
            GetLogin_out_Data();
        }

        void GetAllUserData()
        {
            
            da = new SqlDataAdapter(@"select * from users", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;
            
            da.Dispose();
            dt.Dispose();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            pnlUsername.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "You can not leave (Username) empty");
                txtUserName.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "You can not leave (Password) empty");
                txtPassword.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                errorProvider1.SetError(txtFirstName, "You can not leave (User first name) empty");
                txtFirstName.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                errorProvider1.SetError(txtLastName, "You can not leave (User last name) empty");
                txtLastName.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "You can not leave (User email) empty");
                txtEmail.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbStatus, "You can not leave (Status) empty");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            try
            {
                ConCls.OpenConnection();
                string sql = @"INSERT INTO [Users]
                                    ( [Username],
                                      [pwd],
                                      [FirstName],
                                      [LastName],
                                      [UserEmail],
                                      [Status]
                                       )

                                    VALUES
                                    ( @Username,
                                      @pwd,
                                      @FirstName,
                                      @LastName,
                                      @UserEmail,
                                      @Status
                                      )";
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = txtUserName.Text;
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = txtPassword.Text;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
                MessageBox.Show(cmbStatus.SelectedItem.ToString());
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("User created successfully", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlUsername.Visible = false;
                cmd.Dispose();
                GetAllUserData();

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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter(@"select * from users where username like'%" + textBox7.Text +"%'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView1.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
             UereS_No = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(UereS_No))
            {
                MessageBox.Show("you must choose the user from grid.", "Missing user serial no", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                ConCls.OpenConnection();
                string sql = @"update [Users]
                                    set [Username] = @Username,
                                        [pwd] = @pwd,
                                        [FirstName] = @FirstName,
                                        [LastName] = @LastName,
                                        [UserEmail] = @UserEmail,
                                        [Status] = @Status
                                         where UserSrNo=" + UereS_No;

                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = txtUserName.Text;
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = txtPassword.Text;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = cmbStatus.SelectedItem.ToString();
              
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("User info updated successfully", "update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlUsername.Visible = false;
                cmd.Dispose();
                GetAllUserData();
               

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UereS_No = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtUserName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPassword.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFirstName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtLastName.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbStatus.SelectedItem =  dataGridView1.CurrentRow.Cells[6].Value.ToString();
            btnEdit.Text = "Edit (" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[4].Value.ToString() + " )";
            

            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UereS_No))
            {
                MessageBox.Show("you must choose the user from grid.", "Missing user serial no", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                pnlUsername.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlUsername.Visible = false;
        }

        void GetLogin_out_Data()
        {

            da = new SqlDataAdapter(@"select * from UserLogin", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView2.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }

        void Get_Online_user()
        {

            da = new SqlDataAdapter(@"select * from UserLogin where LogoutDateTime is null", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView2.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }

        void Get_Today_user()
        {

            da = new SqlDataAdapter(@"select * from UserLogin where LoginDateTime ='" + DateTime.Now.ToString() +"'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            dataGridView2.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Online_user();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetLogin_out_Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Get_Today_user();
        }
    }
}
