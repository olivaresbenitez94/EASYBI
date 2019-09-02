using EasyBI.DAL.OBJECTS;
using MongoDB.Bson;
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
using static EasyBI.DAL.OBJECTS.Table;

namespace EasyBI.DAL.FORMS
{
	public partial class frmTable : Form
	{
		private List<Extraction> extractions = new List<Extraction>();
		private Extraction selectedExtraction = null;
		public DataTable result = null;
		public TableType tableType = TableType.Landing; 

		public frmTable(string selectedFolder,List<Extraction> extractions, Extraction selectedExtraction)
		{
			this.extractions = extractions;
			this.selectedExtraction = selectedExtraction;
			InitializeComponent();
			txtFolder.Text = selectedFolder;
			txtFolder.Enabled = false;

			List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();

			extractions.ForEach(extraction => comboBoxItems.Add(new ComboBoxItem(extraction.metadata.name, extraction._id)));

			cmbExtraction.Items.AddRange(comboBoxItems.ToArray());

			cmbExtraction.SelectedItem = comboBoxItems.Where(combo => (ObjectId)combo.Value == selectedExtraction._id).FirstOrDefault();
		}



		private void BtnAcept_Click(object sender, EventArgs e)
		{
			switch (cmbType.SelectedItem.ToString())
			{
				case "Landing":
					tableType = TableType.Landing;
					result =  Table.TableToDatatable(Table.ExtractionToTable(selectedExtraction,true));
					break;

				case "Dimension/Fact":
					tableType = TableType.Dimension_fact;
					result = Table.TableToDatatable(Table.ExtractionToTable(selectedExtraction));
					break;
			}
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void CmbExtraction_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBoxItem aux = (ComboBoxItem)cmbExtraction.SelectedItem;
			selectedExtraction = extractions.Where(ext => ext._id == (ObjectId) aux.Value).FirstOrDefault();

		}

	}
}
