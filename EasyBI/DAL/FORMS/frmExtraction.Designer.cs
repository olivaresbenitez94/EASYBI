namespace EasyBI.DAL.FORMS
{
    partial class frmExtraction
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
			this.chbFirstLineHeader = new System.Windows.Forms.CheckBox();
			this.txtDelimiter = new System.Windows.Forms.TextBox();
			this.lblDelimiter = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.lblEscapeChars = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.cmbFileType = new System.Windows.Forms.ComboBox();
			this.lblFileType = new System.Windows.Forms.Label();
			this.btnAcept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chlbFileName = new System.Windows.Forms.CheckedListBox();
			this.lblFolder = new System.Windows.Forms.Label();
			this.cmbFolder = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// chbFirstLineHeader
			// 
			this.chbFirstLineHeader.AutoSize = true;
			this.chbFirstLineHeader.Location = new System.Drawing.Point(32, 133);
			this.chbFirstLineHeader.Margin = new System.Windows.Forms.Padding(2);
			this.chbFirstLineHeader.Name = "chbFirstLineHeader";
			this.chbFirstLineHeader.Size = new System.Drawing.Size(115, 17);
			this.chbFirstLineHeader.TabIndex = 0;
			this.chbFirstLineHeader.Text = "First line is Header.";
			this.chbFirstLineHeader.UseVisualStyleBackColor = true;
			// 
			// txtDelimiter
			// 
			this.txtDelimiter.Location = new System.Drawing.Point(238, 68);
			this.txtDelimiter.Margin = new System.Windows.Forms.Padding(2);
			this.txtDelimiter.Name = "txtDelimiter";
			this.txtDelimiter.Size = new System.Drawing.Size(57, 20);
			this.txtDelimiter.TabIndex = 1;
			this.txtDelimiter.Text = ",";
			// 
			// lblDelimiter
			// 
			this.lblDelimiter.AutoSize = true;
			this.lblDelimiter.Location = new System.Drawing.Point(30, 71);
			this.lblDelimiter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblDelimiter.Name = "lblDelimiter";
			this.lblDelimiter.Size = new System.Drawing.Size(51, 13);
			this.lblDelimiter.TabIndex = 2;
			this.lblDelimiter.Text = "*Delimiter";
			// 
			// lblEscapeChars
			// 
			this.lblEscapeChars.AutoSize = true;
			this.lblEscapeChars.Location = new System.Drawing.Point(29, 103);
			this.lblEscapeChars.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblEscapeChars.Name = "lblEscapeChars";
			this.lblEscapeChars.Size = new System.Drawing.Size(171, 13);
			this.lblEscapeChars.TabIndex = 4;
			this.lblEscapeChars.Text = "Escape chars (separate by space):";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(237, 103);
			this.textBox1.Margin = new System.Windows.Forms.Padding(2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(57, 20);
			this.textBox1.TabIndex = 3;
			// 
			// cmbFileType
			// 
			this.cmbFileType.FormattingEnabled = true;
			this.cmbFileType.Items.AddRange(new object[] {
            "CSV",
            "JSON"});
			this.cmbFileType.Location = new System.Drawing.Point(239, 37);
			this.cmbFileType.Margin = new System.Windows.Forms.Padding(2);
			this.cmbFileType.Name = "cmbFileType";
			this.cmbFileType.Size = new System.Drawing.Size(82, 21);
			this.cmbFileType.TabIndex = 5;
			// 
			// lblFileType
			// 
			this.lblFileType.AutoSize = true;
			this.lblFileType.Location = new System.Drawing.Point(30, 40);
			this.lblFileType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblFileType.Name = "lblFileType";
			this.lblFileType.Size = new System.Drawing.Size(54, 13);
			this.lblFileType.TabIndex = 6;
			this.lblFileType.Text = "*File Type";
			// 
			// btnAcept
			// 
			this.btnAcept.Location = new System.Drawing.Point(65, 282);
			this.btnAcept.Margin = new System.Windows.Forms.Padding(2);
			this.btnAcept.Name = "btnAcept";
			this.btnAcept.Size = new System.Drawing.Size(82, 21);
			this.btnAcept.TabIndex = 7;
			this.btnAcept.Text = "Accept";
			this.btnAcept.UseVisualStyleBackColor = true;
			this.btnAcept.Click += new System.EventHandler(this.btnAcept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(239, 281);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(82, 21);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chlbFileName
			// 
			this.chlbFileName.FormattingEnabled = true;
			this.chlbFileName.Location = new System.Drawing.Point(32, 167);
			this.chlbFileName.Margin = new System.Windows.Forms.Padding(2);
			this.chlbFileName.Name = "chlbFileName";
			this.chlbFileName.Size = new System.Drawing.Size(313, 94);
			this.chlbFileName.TabIndex = 9;
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Location = new System.Drawing.Point(30, 11);
			this.lblFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(40, 13);
			this.lblFolder.TabIndex = 10;
			this.lblFolder.Text = "*Folder";
			// 
			// cmbFolder
			// 
			this.cmbFolder.FormattingEnabled = true;
			this.cmbFolder.Items.AddRange(new object[] {
            "CSV",
            "JSON",
            "XML"});
			this.cmbFolder.Location = new System.Drawing.Point(239, 8);
			this.cmbFolder.Margin = new System.Windows.Forms.Padding(2);
			this.cmbFolder.Name = "cmbFolder";
			this.cmbFolder.Size = new System.Drawing.Size(131, 21);
			this.cmbFolder.TabIndex = 11;
			// 
			// frmExtraction
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 335);
			this.Controls.Add(this.cmbFolder);
			this.Controls.Add(this.lblFolder);
			this.Controls.Add(this.chlbFileName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAcept);
			this.Controls.Add(this.lblFileType);
			this.Controls.Add(this.cmbFileType);
			this.Controls.Add(this.lblEscapeChars);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.lblDelimiter);
			this.Controls.Add(this.txtDelimiter);
			this.Controls.Add(this.chbFirstLineHeader);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmExtraction";
			this.Text = "Extraction";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbFirstLineHeader;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.Label lblDelimiter;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblEscapeChars;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Button btnAcept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox chlbFileName;
		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.ComboBox cmbFolder;
	}
}