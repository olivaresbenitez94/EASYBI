namespace EasyBI.DAL.FORMS
{
	partial class frmSQL
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
			this.pSQL = new System.Windows.Forms.Panel();
			this.rtchSQL = new System.Windows.Forms.RichTextBox();
			this.btnCopToClipboard = new System.Windows.Forms.Button();
			this.btnAccpet = new System.Windows.Forms.Button();
			this.pSQL.SuspendLayout();
			this.SuspendLayout();
			// 
			// pSQL
			// 
			this.pSQL.Controls.Add(this.btnAccpet);
			this.pSQL.Controls.Add(this.btnCopToClipboard);
			this.pSQL.Controls.Add(this.rtchSQL);
			this.pSQL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pSQL.Location = new System.Drawing.Point(0, 0);
			this.pSQL.Name = "pSQL";
			this.pSQL.Size = new System.Drawing.Size(910, 732);
			this.pSQL.TabIndex = 0;
			// 
			// rtchSQL
			// 
			this.rtchSQL.Dock = System.Windows.Forms.DockStyle.Top;
			this.rtchSQL.Location = new System.Drawing.Point(0, 0);
			this.rtchSQL.Name = "rtchSQL";
			this.rtchSQL.Size = new System.Drawing.Size(910, 671);
			this.rtchSQL.TabIndex = 0;
			this.rtchSQL.Text = "";
			// 
			// btnCopToClipboard
			// 
			this.btnCopToClipboard.Location = new System.Drawing.Point(254, 682);
			this.btnCopToClipboard.Name = "btnCopToClipboard";
			this.btnCopToClipboard.Size = new System.Drawing.Size(133, 33);
			this.btnCopToClipboard.TabIndex = 1;
			this.btnCopToClipboard.Text = "Copy to Clipboard";
			this.btnCopToClipboard.UseVisualStyleBackColor = true;
			this.btnCopToClipboard.Click += new System.EventHandler(this.BtnCopToClipboard_Click);
			// 
			// btnAccpet
			// 
			this.btnAccpet.Location = new System.Drawing.Point(487, 682);
			this.btnAccpet.Name = "btnAccpet";
			this.btnAccpet.Size = new System.Drawing.Size(132, 33);
			this.btnAccpet.TabIndex = 2;
			this.btnAccpet.Text = "Accept";
			this.btnAccpet.UseVisualStyleBackColor = true;
			this.btnAccpet.Click += new System.EventHandler(this.BtnAccpet_Click);
			// 
			// frmSQL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(910, 732);
			this.Controls.Add(this.pSQL);
			this.Name = "frmSQL";
			this.Text = "SQL Code";
			this.pSQL.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pSQL;
		private System.Windows.Forms.RichTextBox rtchSQL;
		private System.Windows.Forms.Button btnAccpet;
		private System.Windows.Forms.Button btnCopToClipboard;
	}
}