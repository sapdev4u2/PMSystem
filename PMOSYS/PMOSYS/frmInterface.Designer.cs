namespace PMOSYS
{
    partial class frmInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlData = new System.Windows.Forms.Panel();
            this.cmbProject = new System.Windows.Forms.ComboBox();
            this.lblInterfaceNo = new System.Windows.Forms.Label();
            this.btnCanacl = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtInterfacedecs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStakeHolder = new System.Windows.Forms.ComboBox();
            this.cmbKeyMilestoneName = new System.Windows.Forms.ComboBox();
            this.txtTotalInterface = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnnotStarted = new System.Windows.Forms.Button();
            this.btnInProgress = new System.Windows.Forms.Button();
            this.btnCompleted = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(570, 62);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(570, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Milestones Interfaces";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 737);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(570, 30);
            this.pnlFooter.TabIndex = 1;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.cmbProject);
            this.pnlData.Controls.Add(this.lblInterfaceNo);
            this.pnlData.Controls.Add(this.btnCanacl);
            this.pnlData.Controls.Add(this.btnUpdate);
            this.pnlData.Controls.Add(this.btnSave);
            this.pnlData.Controls.Add(this.cmbStatus);
            this.pnlData.Controls.Add(this.label7);
            this.pnlData.Controls.Add(this.button1);
            this.pnlData.Controls.Add(this.txtInterfacedecs);
            this.pnlData.Controls.Add(this.label4);
            this.pnlData.Controls.Add(this.cmbStakeHolder);
            this.pnlData.Controls.Add(this.cmbKeyMilestoneName);
            this.pnlData.Controls.Add(this.txtTotalInterface);
            this.pnlData.Controls.Add(this.label5);
            this.pnlData.Controls.Add(this.label6);
            this.pnlData.Controls.Add(this.label3);
            this.pnlData.Controls.Add(this.label2);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlData.Location = new System.Drawing.Point(0, 62);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(570, 271);
            this.pnlData.TabIndex = 3;
            // 
            // cmbProject
            // 
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(144, 23);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(398, 24);
            this.cmbProject.TabIndex = 10;
            this.cmbProject.SelectedIndexChanged += new System.EventHandler(this.cmbProject_SelectedIndexChanged);
            this.cmbProject.SelectionChangeCommitted += new System.EventHandler(this.cmbProject_SelectionChangeCommitted);
            this.cmbProject.DisplayMemberChanged += new System.EventHandler(this.cmbProject_DisplayMemberChanged);
            this.cmbProject.SelectedValueChanged += new System.EventHandler(this.cmbProject_SelectedValueChanged);
            // 
            // lblInterfaceNo
            // 
            this.lblInterfaceNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInterfaceNo.Location = new System.Drawing.Point(144, 227);
            this.lblInterfaceNo.Name = "lblInterfaceNo";
            this.lblInterfaceNo.Size = new System.Drawing.Size(398, 29);
            this.lblInterfaceNo.TabIndex = 9;
            this.lblInterfaceNo.Text = "--";
            this.lblInterfaceNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCanacl
            // 
            this.btnCanacl.Location = new System.Drawing.Point(467, 195);
            this.btnCanacl.Name = "btnCanacl";
            this.btnCanacl.Size = new System.Drawing.Size(75, 29);
            this.btnCanacl.TabIndex = 8;
            this.btnCanacl.Text = "Cancel";
            this.btnCanacl.UseVisualStyleBackColor = true;
            this.btnCanacl.Click += new System.EventHandler(this.btnCanacl_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(225, 195);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 29);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(144, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Completed",
            "In-progress",
            "Not started"});
            this.cmbStatus.Location = new System.Drawing.Point(362, 161);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(180, 24);
            this.cmbStatus.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Interface Status";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtInterfacedecs
            // 
            this.txtInterfacedecs.Location = new System.Drawing.Point(144, 80);
            this.txtInterfacedecs.Multiline = true;
            this.txtInterfacedecs.Name = "txtInterfacedecs";
            this.txtInterfacedecs.Size = new System.Drawing.Size(398, 51);
            this.txtInterfacedecs.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Interface Description";
            // 
            // cmbStakeHolder
            // 
            this.cmbStakeHolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStakeHolder.FormattingEnabled = true;
            this.cmbStakeHolder.Items.AddRange(new object[] {
            "STC"});
            this.cmbStakeHolder.Location = new System.Drawing.Point(144, 134);
            this.cmbStakeHolder.Name = "cmbStakeHolder";
            this.cmbStakeHolder.Size = new System.Drawing.Size(361, 24);
            this.cmbStakeHolder.TabIndex = 2;
            // 
            // cmbKeyMilestoneName
            // 
            this.cmbKeyMilestoneName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyMilestoneName.FormattingEnabled = true;
            this.cmbKeyMilestoneName.Location = new System.Drawing.Point(144, 52);
            this.cmbKeyMilestoneName.Name = "cmbKeyMilestoneName";
            this.cmbKeyMilestoneName.Size = new System.Drawing.Size(398, 24);
            this.cmbKeyMilestoneName.TabIndex = 2;
            this.cmbKeyMilestoneName.SelectedIndexChanged += new System.EventHandler(this.cmbKeyMilestoneName_SelectedIndexChanged);
            // 
            // txtTotalInterface
            // 
            this.txtTotalInterface.Location = new System.Drawing.Point(144, 162);
            this.txtTotalInterface.Name = "txtTotalInterface";
            this.txtTotalInterface.Size = new System.Drawing.Size(53, 23);
            this.txtTotalInterface.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Stake Holder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Interfaces";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Key Milestone No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Project No.";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 333);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(570, 51);
            this.pnlSearch.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnnotStarted);
            this.panel1.Controls.Add(this.btnInProgress);
            this.panel1.Controls.Add(this.btnCompleted);
            this.panel1.Controls.Add(this.btnShowAll);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 697);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 40);
            this.panel1.TabIndex = 5;
            // 
            // btnnotStarted
            // 
            this.btnnotStarted.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnnotStarted.Location = new System.Drawing.Point(456, 0);
            this.btnnotStarted.Name = "btnnotStarted";
            this.btnnotStarted.Size = new System.Drawing.Size(114, 40);
            this.btnnotStarted.TabIndex = 10;
            this.btnnotStarted.Text = "Not-Started";
            this.btnnotStarted.UseVisualStyleBackColor = true;
            // 
            // btnInProgress
            // 
            this.btnInProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnInProgress.Location = new System.Drawing.Point(342, 0);
            this.btnInProgress.Name = "btnInProgress";
            this.btnInProgress.Size = new System.Drawing.Size(114, 40);
            this.btnInProgress.TabIndex = 9;
            this.btnInProgress.Text = "In-progress";
            this.btnInProgress.UseVisualStyleBackColor = true;
            // 
            // btnCompleted
            // 
            this.btnCompleted.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCompleted.Location = new System.Drawing.Point(228, 0);
            this.btnCompleted.Name = "btnCompleted";
            this.btnCompleted.Size = new System.Drawing.Size(114, 40);
            this.btnCompleted.TabIndex = 8;
            this.btnCompleted.Text = "Completed";
            this.btnCompleted.UseVisualStyleBackColor = true;
            this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnShowAll.Location = new System.Drawing.Point(114, 0);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(114, 40);
            this.btnShowAll.TabIndex = 7;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(114, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 384);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(570, 313);
            this.dataGridView1.TabIndex = 6;
            // 
            // frmInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 767);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInterface";
            this.ShowIcon = false;
            this.Text = "Project Interfaces";
            this.Load += new System.EventHandler(this.frmInterface_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCanacl;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInterfacedecs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStakeHolder;
        private System.Windows.Forms.ComboBox cmbKeyMilestoneName;
        private System.Windows.Forms.TextBox txtTotalInterface;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnnotStarted;
        private System.Windows.Forms.Button btnInProgress;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Label lblInterfaceNo;
        private System.Windows.Forms.ComboBox cmbProject;
    }
}