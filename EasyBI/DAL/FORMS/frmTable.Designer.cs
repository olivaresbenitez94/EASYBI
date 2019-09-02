namespace EasyBI.DAL.FORMS
{
	partial class frmTable
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
			this.lblFolder = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAcept = new System.Windows.Forms.Button();
			this.lblExtraction = new System.Windows.Forms.Label();
			this.cmbExtraction = new System.Windows.Forms.ComboBox();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.lblTypeTable = new System.Windows.Forms.Label();
			this.cmbType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Enabled = false;
			this.lblFolder.Location = new System.Drawing.Point(41, 17);
			this.lblFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(36, 13);
			this.lblFolder.TabIndex = 22;
			this.lblFolder.Text = "Folder";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(251, 186);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(86, 31);
			this.btnCancel.TabIndex = 20;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// btnAcept
			// 
			this.btnAcept.Location = new System.Drawing.Point(83, 187);
			this.btnAcept.Margin = new System.Windows.Forms.Padding(2);
			this.btnAcept.Name = "btnAcept";
			this.btnAcept.Size = new System.Drawing.Size(89, 30);
			this.btnAcept.TabIndex = 19;
			this.btnAcept.Text = "Accept";
			this.btnAcept.UseVisualStyleBackColor = true;
			this.btnAcept.Click += new System.EventHandler(this.BtnAcept_Click);
			// 
			// lblExtraction
			// 
			this.lblExtraction.AutoSize = true;
			this.lblExtraction.Location = new System.Drawing.Point(41, 68);
			this.lblExtraction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblExtraction.Name = "lblExtraction";
			this.lblExtraction.Size = new System.Drawing.Size(58, 13);
			this.lblExtraction.TabIndex = 18;
			this.lblExtraction.Text = "*Extraction";
			// 
			// cmbExtraction
			// 
			this.cmbExtraction.FormattingEnabled = true;
			this.cmbExtraction.Location = new System.Drawing.Point(169, 65);
			this.cmbExtraction.Margin = new System.Windows.Forms.Padding(2);
			this.cmbExtraction.Name = "cmbExtraction";
			this.cmbExtraction.Size = new System.Drawing.Size(212, 21);
			this.cmbExtraction.TabIndex = 17;
			this.cmbExtraction.SelectedIndexChanged += new System.EventHandler(this.CmbExtraction_SelectedIndexChanged);
			// 
			// txtFolder
			// 
			this.txtFolder.Location = new System.Drawing.Point(169, 14);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(212, 20);
			this.txtFolder.TabIndex = 23;
			// 
			// lblTypeTable
			// 
			this.lblTypeTable.AutoSize = true;
			this.lblTypeTable.Location = new System.Drawing.Point(41, 121);
			this.lblTypeTable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblTypeTable.Name = "lblTypeTable";
			this.lblTypeTable.Size = new System.Drawing.Size(31, 13);
			this.lblTypeTable.TabIndex = 25;
			this.lblTypeTable.Text = "Type";
			// 
			// cmbType
			// 
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Items.AddRange(new object[] {
            "Landing",
            "Dimension/Fact",
            "Both"});
			this.cmbType.Location = new System.Drawing.Point(169, 118);
			this.cmbType.Margin = new System.Windows.Forms.Padding(2);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(212, 21);
			this.cmbType.TabIndex = 24;
			// 
			// frmTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 259);
			this.Controls.Add(this.lblTypeTable);
			this.Controls.Add(this.cmbType);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.lblFolder);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAcept);
			this.Controls.Add(this.lblExtraction);
			this.Controls.Add(this.cmbExtraction);
			this.Name = "frmTable";
			this.Text = "frmTable";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAcept;
		private System.Windows.Forms.Label lblExtraction;
		private System.Windows.Forms.ComboBox cmbExtraction;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Label lblTypeTable;
		private System.Windows.Forms.ComboBox cmbType;
	}
}