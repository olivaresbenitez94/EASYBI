using EasyBI.DAL;
using EasyBI.DAL.FORMS;
using EasyBI.DAL.OBJECTS;
using EasyBI.EasyBI;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Table = EasyBI.DAL.OBJECTS.Table;

namespace EasyBI
{
	public partial class frmEasyBI : Form
	{
		#region globalVariables
		// localización carpeta aplicación
		public string folderAppPath = null;
		public List<Extraction> extractions = new List<Extraction>();
		public List<Table> tables = new List<Table>();
		public Extraction selectdExtraction = null;
		public int? folderExtractionTab = null;
		public int? folderTableTab = null;
		public string folderNameTableTab = "";
		public Extraction selectedExtractionTable = null;
		public int rootFolder = -1;
		public DataGridView tableGrid = null;
		public Table selectedTable = null;
		public Table loadedTable = null;
		#endregion

		public frmEasyBI()
		{
			Extraction.usePython();
			InitializeComponent();
			folderAppPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			InitializeViewExtraction();
			InitizalizeTableGrid();
			InitializeApp();

		}

		#region Eventos

		#region EventosExtraccion
		private void btnCreateFolder_Click(object sender, EventArgs e)
		{
			frmWindowFolder frmWindow = new frmWindowFolder(folderExtractionTab);
			frmWindow.ShowDialog();

			loadTree();
		}


		private void btnDeleteFolder_Click(object sender, EventArgs e)
		{
			TreeNode node = tvExtraccion.SelectedNode;

			if (node == null || node.Parent == null)
				return;

			List<int> IDs = tabExtraction.getAllChildrenNodes(node);

			if (MessageBox.Show("Do you want to continue?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (Folders.deleteFolders(IDs))
				{
					Extraction.DeleteFolders(IDs, extractions);

					MessageBox.Show("Folder(s) deleted.", "Deleted");
				}
			}
			InitializeViewExtraction();
			InitializeApp();
		}

		private void btnExtract_Click(object sender, EventArgs e)
		{
			opdExtraction.ShowDialog();
			string[] fileNames = opdExtraction.FileNames;
			int folderSelected = Parser.toInt(tvExtraccion.SelectedNode?.Tag ?? "-1");

			List<Extraction> extractions = new List<Extraction>();

			frmExtraction frmExtraction = new frmExtraction(fileNames.ToList(), Parser.toInt(folderSelected));
			frmExtraction.ShowDialog();

			InitializeViewExtraction();
			InitializeApp();

		}

		#endregion

		#endregion

		#region Métodos
		#region Extracción

		private void loadTree()
		{
			List<Folders> folders = Folders.getFolders();
			tvExtraccion.Nodes.Clear();
			tvExtractionTable.Nodes.Clear();

			foreach (Folders fold in folders.Where(obj => obj.PARENT_ID == 0))
			{
				List<TreeNode> nodesExtraction = tabExtraction.GetTreeNodes(fold, folders);
				List<TreeNode> nodesTables = tabExtraction.GetTreeNodes(fold, folders);
				tvExtraccion.Nodes.AddRange(nodesExtraction.ToArray());

				tvExtractionTable.Nodes.AddRange(nodesTables.ToArray());

			}
			rootFolder = folders.Where(folder => Parser.toInt(folder.PARENT_ID) == 0)?.Select(folder => folder.ID).FirstOrDefault() ?? -1;
			tvExtraccion.ExpandAll();
			tvExtractionTable.ExpandAll();

		}

		private void loadExtractions()
		{
			extractions = Extraction.getAllDocuments();
		}

		private void InitializeApp()
		{
			loadExtractions();
			loadTables();
			loadTree();
		}

		#endregion

		#endregion
		private void InitializeViewExtraction()
		{
			dgvExtraction.DataSource = null;
			btnDeleteExtraction.Visible = false;
			txtDate.Text = "";
			txtColumns.Text = "";
			txtName.Text = "";
			txtFolder.Text = "";
			txtRegisters.Text = "";

			txtDate.Enabled = false;
			txtColumns.Enabled = false;
			txtName.Enabled = false;
			txtFolder.Enabled = false;
			txtRegisters.Enabled = false;
		}

		private void TvExtraccion_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string text = e.Node.Text.ToString();
			string tag = e.Node.Tag.ToString();

			InitializeViewExtraction();
			folderExtractionTab = (int)Parser.toInt(tag);
			lbtExtractions.DataSource = extractions.Where(ext => ext.metadata.folder.ToString() == tag).Select(ext => ext.metadata.name).OrderBy(ext => ext).ToList();

		}

		private void LbtExtractions_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = lbtExtractions.SelectedIndex;

			loadExtractions(index);
		}

		private void loadExtractions(int index)
		{
			InitializeViewExtraction();
			Extraction ext = null;
			try
			{
				ext = extractions.Where(e => e.metadata.folder == folderExtractionTab).ToList().OrderBy(e => e.metadata.name)?.ToArray()[index] ?? null;

			}
			catch
			{

			}


			dgvExtraction.DataSource = ExtractionToTable(ext);

			if (ext != null)
			{
				btnDeleteExtraction.Visible = true;
				txtDate.Text = ext.metadata.createDate.ToString("dddd, dd MMMM yyyy HH:mm:ss");
				txtColumns.Text = ext.Columns.Count.ToString();
				txtName.Text = ext.metadata.name;
				List<Folders> FOLDS = Folders.getFolders();
				Folders fold = Folders.getFolders()?.Where(f => f.ID == ext.metadata.folder).FirstOrDefault();
				txtFolder.Text = Folders.getFolders()?.Where(f => f.ID == ext.metadata.folder).FirstOrDefault()?.NAME ?? "-1";
				txtRegisters.Text = String.Format("{0:n0}", ext.metadata.registers);
			}

			selectdExtraction = ext;

		}

		public static DataTable ExtractionToTable(Extraction extraction)
		{
			DataTable table = new DataTable();

			table.Columns.Add(new DataColumn("Index", typeof(int)));
			table.Columns.Add(new DataColumn("Name", typeof(string)));
			table.Columns.Add(new DataColumn("MinLenght", typeof(int)));
			table.Columns.Add(new DataColumn("MaxLenght", typeof(int)));
			table.Columns.Add(new DataColumn("Type", typeof(string)));

			foreach (Extraction.Column col in extraction.Columns)
			{
				DataRow row = table.NewRow();
				row["Index"] = Parser.toInt(col.position);
				row["Name"] = col.name;
				row["MinLenght"] = Parser.toInt(col.minLength);
				row["MaxLenght"] = Parser.toInt(col.maxLength);
				row["Type"] = col.inferedType.ToString();

				table.Rows.Add(row);
			}

			return table;
		}

		private void BtnDeleteExtraction_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you want to continue?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{

				Extraction.DeleteObject(selectdExtraction._id);

				extractions.Remove(extractions.Where(ext => ext._id == selectdExtraction._id).FirstOrDefault());
				InitializeViewExtraction();
				lbtExtractions.DataSource = extractions.Where(ext => ext.metadata.folder.ToString() == selectdExtraction.metadata.folder.ToString()).Select(ext => ext.metadata.name).ToList();

				MessageBox.Show("Extraction deleted.", "Deleted");

			}
		}

		private void TvExtractionTable_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string text = e.Node.Text.ToString();
			string tag = e.Node.Tag.ToString();

			InitializeViewExtraction();
			folderTableTab = (int)Parser.toInt(tag);
			folderNameTableTab = text;
			lbtExtractionsTable.DataSource = extractions.Where(ext => ext.metadata.folder.ToString() == tag).Select(ext => ext.metadata.name).OrderBy(ext => ext).ToList();

			InitizalizeTableGrid();
		}

		public Extraction getExtraction(int index, int folder, List<Extraction> extractions)
		{
			Extraction extraction = null;
			try
			{
				extraction = extractions.Where(ext => ext.metadata.folder == folder).
					ToList().OrderBy(ext => ext.metadata.name)?.ToArray()[index] ?? null;

			}
			catch
			{

			}
			return extraction;
		}

		public Table GetTable(int index, ObjectId extraction, List<Table> tables)
		{
			return tables.Where(table => table.metadata.extraction == extraction).
				OrderBy(table => table.metadata.name)?.ToArray()[index] ?? null;
		}

		private void BtnCreateFromExtraction_Click(object sender, EventArgs e)
		{
			frmTable table = new frmTable(folderNameTableTab, extractions, selectedExtractionTable);
			table.ShowDialog();

			txtTableName.Text = selectedExtractionTable.metadata.name;
			txtCreateDate.Text = "";
			if (table.DialogResult == DialogResult.OK)
			{
				loadedTable = null;
				pTableAll.Controls.Clear();

				DataGridView dgv;

				dgv = DAL.OBJECTS.Table.DataTableToGrid(table.tableType);
				dgv.Parent = pTableAll;
				pTableAll.Controls.Add(dgv);
				dgv.Dock = DockStyle.Fill;
				dgv.DataSource = table.result;
				FormatGrid(dgv);
				dgv.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValidated);
				InitizalizeTableGrid();
			}
		}

		private void BtnCreateFromTable_Click(object sender, EventArgs e)
		{
			txtTableName.Text = selectedTable?.metadata?.name ?? "table";
			txtCreateDate.Text = "";
			pTableAll.Controls.Clear();

			DataGridView dgv;

			dgv = DAL.OBJECTS.Table.DataTableToGrid();
			dgv.Parent = pTableAll;
			pTableAll.Controls.Add(dgv);
			dgv.Dock = DockStyle.Fill;
			dgv.DataSource = Table.TableToDatatable(selectedTable);

			loadedTable = null;
			FormatGrid(dgv);
			dgv.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValidated);

			InitizalizeTableGrid();
		}

		private void LbtExtractionsTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedExtractionTable = getExtraction(lbtExtractionsTable.SelectedIndex, Parser.toInt(folderTableTab), extractions);
			loadTablesCombo(selectedExtractionTable._id, Parser.toInt(folderTableTab));
			InitizalizeTableGrid();
		}

		private void loadTablesCombo(ObjectId extractionID, int folderTable)
		{
			lbtTables.DataSource = tables.Where(ext => ext.metadata.folder.ToString() == folderTable.ToString() && ext.metadata.extraction == extractionID).
				Select(ext => ext.metadata.name).OrderBy(ext => ext).ToList();
		}
		private void BtnSave_Click(object sender, EventArgs e)
		{
			DataGridView grid = null;
			if (pTableAll.Controls.Count > 0 && !string.IsNullOrEmpty(txtTableName.Text) && selectedExtractionTable != null)
			{
				try
				{
					grid = (DataGridView)pTableAll.Controls[0];
				}
				catch (Exception ex)
				{

				}

				Table table = Table.GridToTable(grid, txtTableName.Text, Parser.toInt(folderTableTab), selectedExtractionTable._id, Users.getUserID(Environment.UserName), loadedTable);

				string objectIdNull = "000000000000000000000000" ;

				if (table._id.ToString() != objectIdNull)
				{
					Table.UpdateTable(table);
				}
				else
				{
					Table.InsertTable(table);
				}
				

				tables.Where(t => t.metadata.extraction == table.metadata.extraction).Select(t => t).ToList().ForEach(t => tables.Remove(t));

				List<Table> updatedTables = Table.getDocuments(table.metadata.extraction);
				tables.AddRange(updatedTables);

				loadedTable = updatedTables.OrderByDescending(t => t.metadata.modifiedDate).LastOrDefault();
				
				txtCreateDate.Text = loadedTable.metadata.createDate.ToString("dd/MM/yyyy hh:mm:ss");

				loadTablesCombo(selectedExtractionTable._id, Parser.toInt(folderTableTab));

			}

		}

		private void LbtTables_DoubleClick(object sender, EventArgs e)
		{
			if(pTableAll.Controls.Count > 0)
			{
				if (MessageBox.Show("Do you want to continue? You will lose your progress if you don't save.", "Question", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					return;
				}
			}

			loadedTable = selectedTable;

			txtTableName.Text = selectedTable?.metadata?.name ?? "table";
			txtCreateDate.Text = selectedTable.metadata.createDate.ToString("dd/MM/yyyy hh:mm:ss");
			pTableAll.Controls.Clear();

			DataGridView dgv;

			dgv = DAL.OBJECTS.Table.DataTableToGrid();
			dgv.Parent = pTableAll;
			pTableAll.Controls.Add(dgv);
			dgv.Dock = DockStyle.Fill;
			
			dgv.DataSource = Table.TableToDatatable(selectedTable);
			InitizalizeTableGrid();
			dgv.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValidated);

			FormatGrid(dgv);


	}

		private void loadTables()
		{
			tables = Table.getAllDocuments(Users.getUserID(Environment.UserName));
		}

		private void BtnDeleteTable_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you want to continue?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (loadedTable == null)
				{
					pTableAll.Controls.Clear();
				}
				else
				{
					Table.DeleteTable(loadedTable);
					pTableAll.Controls.Clear();
					tables.Remove(loadedTable);
					loadTablesCombo(selectedExtractionTable._id, Parser.toInt(folderTableTab));
				}
				InitizalizeTableGrid();
			}
		}

		private void InitizalizeTableGrid()
		{
			bool existsGrid = (pTableAll.Controls.Count > 0);
			btnCreateFromExtraction.Visible = (selectedExtractionTable != null) && !existsGrid;
			btnCreateFromTable.Visible = (selectedTable != null) && !existsGrid;
			btnCloseTable.Visible = existsGrid;

			btnDeleteTable.Visible = existsGrid;
			lblTableName.Visible = existsGrid;
			btnSave.Visible = existsGrid;
			txtTableName.Visible = existsGrid;
			btnInsertRowDown.Visible = existsGrid;
			btnInsertRowUp.Visible = existsGrid;
			btnDeleteRows.Visible = existsGrid;
			btnExportToSQL.Visible = existsGrid;
			lblCreateDate.Visible = existsGrid;
			txtCreateDate.Visible = existsGrid;
		}

		private void LbtTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbtTables.SelectedIndex >= 0)
			{
				selectedTable = GetTable(lbtTables.SelectedIndex, selectedExtractionTable._id, tables);
			}
			InitizalizeTableGrid();
		}

		private void BtnCloseTable_Click(object sender, EventArgs e)
		{
			pTableAll.Controls.Clear();
			InitizalizeTableGrid();

		}

		private void BtnDeleteRows_Click(object sender, EventArgs e)
		{

			if (MessageBox.Show("Do you want to continue?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{			
				DataGridView grid = (DataGridView)pTableAll.Controls[0];

				foreach (DataGridViewRow row in grid.SelectedRows)
				{
					grid.Rows.Remove(row);
				}
				RecalculatePositions();
			}
		}

		private void RecalculatePositions()
		{
			if (pTableAll.Controls.Count > 0)
			{
				DataGridView grid = (DataGridView)pTableAll.Controls[0];
				grid.Columns["Position"].ReadOnly = false;
				grid.Refresh();
				int position = 0;
				grid.BeginEdit(false);
				foreach (DataGridViewRow row in grid.Rows)
				{
					row.Cells["Position"].Value = position;
					position++;
				}
				grid.EndEdit();
				grid.Columns["Position"].ReadOnly = true;
				grid.Refresh();
			}
		}

		private void BtnInsertRowUp_Click(object sender, EventArgs e)
		{
			if (pTableAll.Controls.Count > 0)
			{
				DataGridView grid = (DataGridView)pTableAll.Controls[0];

				if(grid.SelectedRows.Count == 1)
				{
					InsertRow(true);
				}
			}
		}

		private void InsertRow(bool up)
		{
			DataGridView grid = (DataGridView)pTableAll.Controls[0];


			DataGridViewRow selectedRow = null;

			foreach (DataGridViewRow row in grid.SelectedRows)
			{
				selectedRow = row;
			}

			DataTable table = (DataTable)grid.DataSource;

			DataRow datarow = table.NewRow();
			
			datarow["Type"] = Table.TableColumn.ColumnType.varchar.ToString();
			datarow["Precision"] = 0;
			datarow["Length"] = 20;
			datarow["Nullable"] = true;
			datarow["Position"] = Parser.toInt(selectedRow.Cells["Position"].Value);

			if (up)
			{
				datarow["Name"] = "00NEW ROW";
			}
			else
			{
				datarow["Name"] = "zzNEW ROW";
			}

			table.Rows.Add(datarow);

			table.DefaultView.Sort= "Position asc, Name asc";
			table = table.DefaultView.ToTable();

			grid.DataSource = table;
			FormatGrid(grid);
			grid.Refresh();
			RecalculatePositions();

		}

		private void BtnInsertRowDown_Click(object sender, EventArgs e)
		{
			if (pTableAll.Controls.Count > 0)
			{
				DataGridView grid = (DataGridView)pTableAll.Controls[0];

				if (grid.SelectedRows.Count == 1)
				{
					InsertRow(false);
				}
			}
		}

		private void FormatGrid(DataGridView grid)
		{
			foreach (DataGridViewRow row in grid.Rows)
			{
				FormatRow(row);
			}
		}

		private void FormatRow(DataGridViewRow row )
		{
			List<string> valuesWithLenght = new List<string>
			{
				"varchar", "numeric", "nvarchar"
			};

			bool precisionVisible = (row.Cells["Type"].Value.ToString() == "numeric");
			bool lengthVisible = (valuesWithLenght.Contains(row.Cells["Type"].Value.ToString()));

			row.Cells["Length"].ReadOnly = !lengthVisible;
			
			if (lengthVisible)
			{
				row.Cells["Length"].Style.ForeColor = Color.Black;
			}
			else
			{
				row.Cells["Length"].Value = 0;
				row.Cells["Length"].Style.ForeColor = Color.Transparent;
			}

			row.Cells["Precision"].ReadOnly = !precisionVisible;
			if (precisionVisible)
			{
				row.Cells["Precision"].Style.ForeColor = Color.Black;
			}
			else
			{
				row.Cells["Precision"].Value = 0;
				row.Cells["Precision"].Style.ForeColor = Color.Transparent;
			}
		}

		

		private void grid_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == 2)
			{
				DataGridViewRow row = ((DataGridView)pTableAll.Controls[0]).Rows[e.RowIndex];
				FormatRow(row);
			}
		}

		private void BtnExportToSQL_Click(object sender, EventArgs e)
		{
			frmSQL SQL = new frmSQL(selectedTable);
			SQL.ShowDialog();
		}
	}
}
