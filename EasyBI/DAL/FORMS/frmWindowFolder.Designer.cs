namespace EasyBI.DAL.FORMS
{
    partial class frmWindowFolder
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblParentFolder = new System.Windows.Forms.Label();
            this.cmbParentFolder = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(255, 65);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(295, 26);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(95, 68);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(98, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Folder name";
            // 
            // lblParentFolder
            // 
            this.lblParentFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParentFolder.AutoSize = true;
            this.lblParentFolder.Location = new System.Drawing.Point(95, 155);
            this.lblParentFolder.Name = "lblParentFolder";
            this.lblParentFolder.Size = new System.Drawing.Size(100, 20);
            this.lblParentFolder.TabIndex = 2;
            this.lblParentFolder.Text = "Parent folder";
            // 
            // cmbParentFolder
            // 
            this.cmbParentFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParentFolder.FormattingEnabled = true;
            this.cmbParentFolder.Location = new System.Drawing.Point(255, 147);
            this.cmbParentFolder.Name = "cmbParentFolder";
            this.cmbParentFolder.Size = new System.Drawing.Size(295, 28);
            this.cmbParentFolder.TabIndex = 3;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(271, 247);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(135, 32);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmWindow
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 334);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbParentFolder);
            this.Controls.Add(this.lblParentFolder);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.MaximizeBox = false;
            this.Name = "frmWindow";
            this.ShowIcon = false;
            this.Text = "Create Folder";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblParentFolder;
        private System.Windows.Forms.ComboBox cmbParentFolder;
        private System.Windows.Forms.Button btnCreate;
    }
}