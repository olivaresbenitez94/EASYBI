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
using static EasyBI.DAL.FORMS.frmWindowFolder;

namespace EasyBI.DAL.FORMS
{
    public partial class frmExtraction : Form
    {
		private string path = "";

        public frmExtraction(List<String> fileNames, int selectedFolder)
        {
            InitializeComponent();
			loadParentFolder(selectedFolder);

			if (fileNames.Count > 0)
            {
                List<string> listFilePath = fileNames.First().Split('\\').ToList();
                listFilePath.Remove(listFilePath.Last().ToString());
				this.path = string.Join("\\", listFilePath);
			}
			else
			{
				this.Close();
			}
            
            foreach (string item in fileNames)
            {
                string fileName = item.Split('\\').ToList().LastOrDefault();

                if(!string.IsNullOrEmpty(fileName))
                     chlbFileName.Items.Add(fileName, true);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void loadParentFolder(int folder)
		{
			List<ComboBoxItem> items = new List<ComboBoxItem>();
			Folders.getFolders().Where(item=> item.PARENT_ID != 0).ToList().ForEach(obj => items.Add(new ComboBoxItem(obj.NAME, obj.ID)));
			cmbFolder.Items.Clear();

			cmbFolder.Items.AddRange(items.Cast<object>().ToArray());

			if (folder != -1)
			{
				ComboBoxItem aux = null;
				aux = items.Where(item => Parser.toInt(item.Value) == (int)folder).FirstOrDefault();
				if(aux != null)
				{
					cmbFolder.SelectedItem = aux;
				}
			}	
		}

		private void btnAcept_Click(object sender, EventArgs e)
        {

			if (cmbFolder.SelectedItem == null || cmbFileType.SelectedItem == null || String.IsNullOrEmpty(txtDelimiter.Text))
			{
				MessageBox.Show("Folder, fileType and delimiter must be filled.");
			}
			else
			{
				List<Extraction> extractions = new List<OBJECTS.Extraction>();
				Extraction.fileType fileType;
				Cursor.Current = Cursors.WaitCursor;
				foreach (string item in chlbFileName.CheckedItems)
				{
					switch (cmbFileType.SelectedItem?.ToString() ?? null)
					{
						case "CSV":
							fileType = Extraction.fileType.CSV;
							break;

						case "JSON":
							fileType = Extraction.fileType.JSON;
							break;

						case "XML":
							fileType = Extraction.fileType.XML;
							break;

						default:
							fileType = Extraction.fileType.CSV;
							break;
					}
					ComboBoxItem folder =(ComboBoxItem) cmbFolder.SelectedItem;
					
					extractions.Add(Extraction.extraction(item.ToString(), path, txtDelimiter.Text, chbFirstLineHeader.Checked, Parser.toInt(folder.Value), fileType));
					Cursor.Current = Cursors.Default;
					this.Close();
				}
			}
           


        }
    }
}
