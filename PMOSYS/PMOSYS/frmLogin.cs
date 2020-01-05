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
    public partial class frmLogin : Form
    {

        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string SessionNumber;


        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text)) 
            {
                errorProvider1.SetError(txtUsername, "Please enter your username and try agine.");
                txtUsername.Focus();
                return;
            }else
            {
                errorProvider1.Clear();

            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Please enter your password and try agine.");
                txtPassword.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();

            }


            GetData();







        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = System.Environment.UserName.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        void GetData()
        {
       
            da = new SqlDataAdapter(@"select * from users where username = '" + txtUsername.Text + "' and pwd = '" + txtPassword.Text + "'", ConCls.con);
            dt = new DataTable();
            ConCls.OpenConnection();
            da.Fill(dt);
            ConCls.CloseConnection();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Wrong username or password.", "Wrong entery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else 
            {

               
                RegUserLogin();
                this.Hide();
            }

             void GetData()
            {

                da = new SqlDataAdapter(@"select * from users where username = '" + txtUsername.Text + "' and pwd = '" + txtPassword.Text + "'", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Wrong username or password.", "Wrong entery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {

                    MDIParent1 f = new MDIParent1();
                    f.Show();
                    this.Hide();
                }

                da.Dispose();
                dt.Dispose();
            }


            void RegUserLogin()
            {
                GetSessionNo();
                try
                {
                    ConCls.OpenConnection();
                    string sql = @"INSERT INTO [UserLogin]
                                    ([SessionNo],
                                     [username],
                                     [LoginDateTime]
                                    )

                                    VALUES
                                    (@SessionNo,
                                     @username,
                                     @LoginDateTime)";
                    cmd = new SqlCommand();
                    cmd.Connection = ConCls.con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@SessionNo", SqlDbType.Int).Value = int.Parse(SessionNumber);
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = txtUsername.Text;
                    cmd.Parameters.Add("@LoginDateTime", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                    ConCls.OpenConnection();
                    cmd.ExecuteNonQuery();
                    ConCls.CloseConnection();
                   // MessageBox.Show("User loged-in successfully", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.Dispose();

                    MDIParent1 f = new MDIParent1();
                    f.txtUsername.Text = txtUsername.Text;
                    f.txtSessionNo.Text = SessionNumber;
                    f.txtLoginDateTime.Text = DateTime.Now.ToString();

                    f.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry there are some things going wrong, Plase try agine."+"/n"+ex.ToString(), "Wrong transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    ConCls.CloseConnection();
                }
            }

            void GetSessionNo() 
            {

                da = new SqlDataAdapter(@"select max(SessionNo)+1 from UserLogin", ConCls.con);
                dt = new DataTable();
                ConCls.OpenConnection();
                da.Fill(dt);
                ConCls.CloseConnection();
                SessionNumber = dt.Rows[0][0].ToString();
                MessageBox.Show("Session Number: " + SessionNumber);
                

                da.Dispose();
                dt.Dispose();
            }

        }
    }
}
