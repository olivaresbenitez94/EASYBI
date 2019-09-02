using EasyBI.DAL.OBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyBI.DAL.FORMS
{
    public partial class frmWindowFolder : Form
    {
		public int folderRoot = -1;
		public int? selectedFolder = null;
		public class ComboBoxItem
		{
			public string Text { get; set; }
			public object Value { get; set; }

			public override string ToString()
			{
				return Text;
			}

			public ComboBoxItem(string Text, object Value)
			{
				this.Text = Text;
				this.Value = Value;
			}
		}

		public frmWindowFolder(int? selectedFolder)
        {
            InitializeComponent();
            loadParentFolder(selectedFolder);
        }

        private void loadParentFolder(int? selectedFolder)
        {
            List<ComboBoxItem> items = new List<ComboBoxItem>();

			Folders.getFolders().ForEach(obj => items.Add(new ComboBoxItem(obj.NAME, obj.ID)));
            
            cmbParentFolder.Items.AddRange(items.Cast<object>().ToArray());

			if(selectedFolder != null)
			{
				cmbParentFolder.SelectedItem = items.Where(item => Parser.toInt(item.Value) == Parser.toInt(selectedFolder)).FirstOrDefault();
			}

            if (!items.Any())
                cmbParentFolder.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {      
            if (!string.IsNullOrEmpty(txtName.Text)){
                new Folders
                {
                    NAME = txtName.Text,
                    USER_ID = Users.getUserID(Environment.UserName),
                    PARENT_ID = (int?) ((ComboBoxItem)cmbParentFolder.SelectedItem)?.Value ?? null,
                    ACTIVE = true,
                    CREATED_DATE =  DateTime.UtcNow
                }.Insertar();
            }
            
            this.Close();
        }
    }
}
