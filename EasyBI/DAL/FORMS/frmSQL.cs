using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyBI.DAL.OBJECTS;

namespace EasyBI.DAL.FORMS
{
	public partial class frmSQL : Form
	{
		public frmSQL(Table table)
		{
			InitializeComponent();
			rtchSQL.Text = Table.TableToSQL(table);
		}

		private void BtnAccpet_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnCopToClipboard_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(rtchSQL.Text);
		}
	}
}
