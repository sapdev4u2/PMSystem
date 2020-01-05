using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data;
using System.Data.SqlClient;

namespace PMOSYS
{
    public partial class MDIParent1 : Form
    {
        Con_Cls ConCls = new Con_Cls();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            panel1.Width = 17;
            button1.Text = ">";
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtTime.Text = DateTime.Now.ToString("HH:mm:ss");

          

            bool con = NetworkInterface.GetIsNetworkAvailable();
            if (con == true)
            {
                txtInternetConnection.Text = "Connected";
                txtInternetConnection.ForeColor = Color.Green;
            }
            else
            {
                txtInternetConnection.Text = "Not Connected";
                txtInternetConnection.ForeColor = Color.Red;
            }

            


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmNewProject f = new frmNewProject();
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmKeyMilestone f = new frmKeyMilestone();
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers f = new frmUsers();
            f.MdiParent = this;
            f.Show();
        }

        void RegUserLogout()
        {

            try
            {
                ConCls.OpenConnection();
                string sql = @"update [UserLogin]
                                    set
                          
                                    [LogoutDateTime] = @LogoutDateTime
                                    where SessionNo =" + txtSessionNo.Text ;
                cmd = new SqlCommand();
                cmd.Connection = ConCls.con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = txtUsername.Text;
                cmd.Parameters.Add("@LogoutDateTime", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                ConCls.OpenConnection();
                cmd.ExecuteNonQuery();
                ConCls.CloseConnection();
                MessageBox.Show("User loged-out successfully", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry there are some things going wrong, Plase try agine." + "/n" + ex.ToString(), "Wrong transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ConCls.CloseConnection();
            }
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegUserLogout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 202)
            {
                panel1.Width = 17;
                button1.Text = ">";
            }
            else
            {
                panel1.Width = 202;
                button1.Text = "<";
            }
        }
    }
}
