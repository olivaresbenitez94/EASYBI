namespace EasyBI
{
    partial class frmEasyBI
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.opdExtraction = new System.Windows.Forms.OpenFileDialog();
			this.tbcPages = new System.Windows.Forms.TabControl();
			this.tbpExtraccion = new System.Windows.Forms.TabPage();
			this.spcExtraction = new System.Windows.Forms.SplitContainer();
			this.spcFolderTree = new System.Windows.Forms.SplitContainer();
			this.pSpcExtraccion = new System.Windows.Forms.Panel();
			this.btnExtract = new System.Windows.Forms.Button();
			this.btnDeleteFolder = new System.Windows.Forms.Button();
			this.btnCreateFolder = new System.Windows.Forms.Button();
			this.pRspExtraccion = new System.Windows.Forms.Panel();
			this.spTreeFolderExtraction = new System.Windows.Forms.SplitContainer();
			this.tvExtraccion = new System.Windows.Forms.TreeView();
			this.lbtExtractions = new System.Windows.Forms.ListBox();
			this.spcExtracts = new System.Windows.Forms.SplitContainer();
			this.pMetadata = new System.Windows.Forms.Panel();
			this.btnDeleteExtraction = new System.Windows.Forms.Button();
			this.txtColumns = new System.Windows.Forms.TextBox();
			this.txtRegisters = new System.Windows.Forms.TextBox();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.lblColumns = new System.Windows.Forms.Label();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblFolder = new System.Windows.Forms.Label();
			this.lblCreatedDate = new System.Windows.Forms.Label();
			this.lblRegisters = new System.Windows.Forms.Label();
			this.lblNameExtraction = new System.Windows.Forms.Label();
			this.dgvExtraction = new System.Windows.Forms.DataGridView();
			this.tbpTablas = new System.Windows.Forms.TabPage();
			this.spcAllTables = new System.Windows.Forms.SplitContainer();
			this.spcLeftTables = new System.Windows.Forms.SplitContainer();
			this.tvExtractionTable = new System.Windows.Forms.TreeView();
			this.spcLleftButtomTable = new System.Windows.Forms.SplitContainer();
			this.lbtExtractionsTable = new System.Windows.Forms.ListBox();
			this.lbtTables = new System.Windows.Forms.ListBox();
			this.pTableAll = new System.Windows.Forms.Panel();
			this.pTablesUp = new System.Windows.Forms.Panel();
			this.txtCreateDate = new System.Windows.Forms.TextBox();
			this.lblCreateDate = new System.Windows.Forms.Label();
			this.btnExportToSQL = new System.Windows.Forms.Button();
			this.btnInsertRowDown = new System.Windows.Forms.Button();
			this.btnInsertRowUp = new System.Windows.Forms.Button();
			this.btnDeleteRows = new System.Windows.Forms.Button();
			this.btnCloseTable = new System.Windows.Forms.Button();
			this.btnDeleteTable = new System.Windows.Forms.Button();
			this.txtTableName = new System.Windows.Forms.TextBox();
			this.lblTableName = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCreateFromTable = new System.Windows.Forms.Button();
			this.btnCreateFromExtraction = new System.Windows.Forms.Button();
			this.lblshow = new System.Windows.Forms.Label();
			this.tbcPages.SuspendLayout();
			this.tbpExtraccion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcExtraction)).BeginInit();
			this.spcExtraction.Panel1.SuspendLayout();
			this.spcExtraction.Panel2.SuspendLayout();
			this.spcExtraction.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcFolderTree)).BeginInit();
			this.spcFolderTree.Panel1.SuspendLayout();
			this.spcFolderTree.Panel2.SuspendLayout();
			this.spcFolderTree.SuspendLayout();
			this.pSpcExtraccion.SuspendLayout();
			this.pRspExtraccion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spTreeFolderExtraction)).BeginInit();
			this.spTreeFolderExtraction.Panel1.SuspendLayout();
			this.spTreeFolderExtraction.Panel2.SuspendLayout();
			this.spTreeFolderExtraction.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcExtracts)).BeginInit();
			this.spcExtracts.Panel1.SuspendLayout();
			this.spcExtracts.Panel2.SuspendLayout();
			this.spcExtracts.SuspendLayout();
			this.pMetadata.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvExtraction)).BeginInit();
			this.tbpTablas.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcAllTables)).BeginInit();
			this.spcAllTables.Panel1.SuspendLayout();
			this.spcAllTables.Panel2.SuspendLayout();
			this.spcAllTables.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcLeftTables)).BeginInit();
			this.spcLeftTables.Panel1.SuspendLayout();
			this.spcLeftTables.Panel2.SuspendLayout();
			this.spcLeftTables.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcLleftButtomTable)).BeginInit();
			this.spcLleftButtomTable.Panel1.SuspendLayout();
			this.spcLleftButtomTable.Panel2.SuspendLayout();
			this.spcLleftButtomTable.SuspendLayout();
			this.pTablesUp.SuspendLayout();
			this.SuspendLayout();
			// 
			// opdExtraction
			// 
			this.opdExtraction.FileName = "openFileDialog1";
			this.opdExtraction.Multiselect = true;
			// 
			// tbcPages
			// 
			this.tbcPages.Controls.Add(this.tbpExtraccion);
			this.tbcPages.Controls.Add(this.tbpTablas);
			this.tbcPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbcPages.Location = new System.Drawing.Point(0, 0);
			this.tbcPages.Margin = new System.Windows.Forms.Padding(2);
			this.tbcPages.Name = "tbcPages";
			this.tbcPages.SelectedIndex = 0;
			this.tbcPages.Size = new System.Drawing.Size(1074, 752);
			this.tbcPages.TabIndex = 1;
			// 
			// tbpExtraccion
			// 
			this.tbpExtraccion.Controls.Add(this.spcExtraction);
			this.tbpExtraccion.Location = new System.Drawing.Point(4, 22);
			this.tbpExtraccion.Margin = new System.Windows.Forms.Padding(2);
			this.tbpExtraccion.Name = "tbpExtraccion";
			this.tbpExtraccion.Padding = new System.Windows.Forms.Padding(2);
			this.tbpExtraccion.Size = new System.Drawing.Size(1066, 726);
			this.tbpExtraccion.TabIndex = 0;
			this.tbpExtraccion.Text = "Extractions";
			this.tbpExtraccion.UseVisualStyleBackColor = true;
			// 
			// spcExtraction
			// 
			this.spcExtraction.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcExtraction.Location = new System.Drawing.Point(2, 2);
			this.spcExtraction.Margin = new System.Windows.Forms.Padding(2);
			this.spcExtraction.Name = "spcExtraction";
			// 
			// spcExtraction.Panel1
			// 
			this.spcExtraction.Panel1.Controls.Add(this.spcFolderTree);
			// 
			// spcExtraction.Panel2
			// 
			this.spcExtraction.Panel2.Controls.Add(this.spcExtracts);
			this.spcExtraction.Size = new System.Drawing.Size(1062, 722);
			this.spcExtraction.SplitterDistance = 479;
			this.spcExtraction.SplitterWidth = 3;
			this.spcExtraction.TabIndex = 1;
			// 
			// spcFolderTree
			// 
			this.spcFolderTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcFolderTree.Location = new System.Drawing.Point(0, 0);
			this.spcFolderTree.Margin = new System.Windows.Forms.Padding(2);
			this.spcFolderTree.Name = "spcFolderTree";
			this.spcFolderTree.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcFolderTree.Panel1
			// 
			this.spcFolderTree.Panel1.Controls.Add(this.pSpcExtraccion);
			// 
			// spcFolderTree.Panel2
			// 
			this.spcFolderTree.Panel2.Controls.Add(this.pRspExtraccion);
			this.spcFolderTree.Size = new System.Drawing.Size(479, 722);
			this.spcFolderTree.SplitterDistance = 37;
			this.spcFolderTree.SplitterWidth = 3;
			this.spcFolderTree.TabIndex = 0;
			// 
			// pSpcExtraccion
			// 
			this.pSpcExtraccion.Controls.Add(this.btnExtract);
			this.pSpcExtraccion.Controls.Add(this.btnDeleteFolder);
			this.pSpcExtraccion.Controls.Add(this.btnCreateFolder);
			this.pSpcExtraccion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pSpcExtraccion.Location = new System.Drawing.Point(0, 0);
			this.pSpcExtraccion.Margin = new System.Windows.Forms.Padding(2);
			this.pSpcExtraccion.Name = "pSpcExtraccion";
			this.pSpcExtraccion.Size = new System.Drawing.Size(479, 37);
			this.pSpcExtraccion.TabIndex = 0;
			// 
			// btnExtract
			// 
			this.btnExtract.Location = new System.Drawing.Point(369, 5);
			this.btnExtract.Margin = new System.Windows.Forms.Padding(2);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(96, 26);
			this.btnExtract.TabIndex = 1;
			this.btnExtract.Text = "Extract";
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// btnDeleteFolder
			// 
			this.btnDeleteFolder.Location = new System.Drawing.Point(104, 6);
			this.btnDeleteFolder.Margin = new System.Windows.Forms.Padding(2);
			this.btnDeleteFolder.Name = "btnDeleteFolder";
			this.btnDeleteFolder.Size = new System.Drawing.Size(94, 26);
			this.btnDeleteFolder.TabIndex = 1;
			this.btnDeleteFolder.Text = "Delete Folder";
			this.btnDeleteFolder.UseVisualStyleBackColor = true;
			this.btnDeleteFolder.Click += new System.EventHandler(this.btnDeleteFolder_Click);
			// 
			// btnCreateFolder
			// 
			this.btnCreateFolder.Location = new System.Drawing.Point(2, 5);
			this.btnCreateFolder.Margin = new System.Windows.Forms.Padding(2);
			this.btnCreateFolder.Name = "btnCreateFolder";
			this.btnCreateFolder.Size = new System.Drawing.Size(98, 27);
			this.btnCreateFolder.TabIndex = 0;
			this.btnCreateFolder.Text = "CreateFolder";
			this.btnCreateFolder.UseVisualStyleBackColor = true;
			this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
			// 
			// pRspExtraccion
			// 
			this.pRspExtraccion.Controls.Add(this.spTreeFolderExtraction);
			this.pRspExtraccion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pRspExtraccion.Location = new System.Drawing.Point(0, 0);
			this.pRspExtraccion.Margin = new System.Windows.Forms.Padding(2);
			this.pRspExtraccion.Name = "pRspExtraccion";
			this.pRspExtraccion.Size = new System.Drawing.Size(479, 682);
			this.pRspExtraccion.TabIndex = 1;
			// 
			// spTreeFolderExtraction
			// 
			this.spTreeFolderExtraction.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spTreeFolderExtraction.Location = new System.Drawing.Point(0, 0);
			this.spTreeFolderExtraction.Name = "spTreeFolderExtraction";
			// 
			// spTreeFolderExtraction.Panel1
			// 
			this.spTreeFolderExtraction.Panel1.Controls.Add(this.tvExtraccion);
			// 
			// spTreeFolderExtraction.Panel2
			// 
			this.spTreeFolderExtraction.Panel2.Controls.Add(this.lbtExtractions);
			this.spTreeFolderExtraction.Size = new System.Drawing.Size(479, 682);
			this.spTreeFolderExtraction.SplitterDistance = 226;
			this.spTreeFolderExtraction.SplitterWidth = 6;
			this.spTreeFolderExtraction.TabIndex = 0;
			// 
			// tvExtraccion
			// 
			this.tvExtraccion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExtraccion.HideSelection = false;
			this.tvExtraccion.Location = new System.Drawing.Point(0, 0);
			this.tvExtraccion.Margin = new System.Windows.Forms.Padding(2);
			this.tvExtraccion.Name = "tvExtraccion";
			this.tvExtraccion.Size = new System.Drawing.Size(226, 682);
			this.tvExtraccion.TabIndex = 0;
			this.tvExtraccion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvExtraccion_AfterSelect);
			// 
			// lbtExtractions
			// 
			this.lbtExtractions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbtExtractions.FormattingEnabled = true;
			this.lbtExtractions.Location = new System.Drawing.Point(0, 0);
			this.lbtExtractions.Name = "lbtExtractions";
			this.lbtExtractions.Size = new System.Drawing.Size(247, 682);
			this.lbtExtractions.TabIndex = 0;
			this.lbtExtractions.SelectedIndexChanged += new System.EventHandler(this.LbtExtractions_SelectedIndexChanged);
			// 
			// spcExtracts
			// 
			this.spcExtracts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcExtracts.Location = new System.Drawing.Point(0, 0);
			this.spcExtracts.Margin = new System.Windows.Forms.Padding(2);
			this.spcExtracts.Name = "spcExtracts";
			this.spcExtracts.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcExtracts.Panel1
			// 
			this.spcExtracts.Panel1.Controls.Add(this.pMetadata);
			// 
			// spcExtracts.Panel2
			// 
			this.spcExtracts.Panel2.Controls.Add(this.dgvExtraction);
			this.spcExtracts.Size = new System.Drawing.Size(580, 722);
			this.spcExtracts.SplitterDistance = 61;
			this.spcExtracts.SplitterWidth = 3;
			this.spcExtracts.TabIndex = 0;
			// 
			// pMetadata
			// 
			this.pMetadata.Controls.Add(this.btnDeleteExtraction);
			this.pMetadata.Controls.Add(this.txtColumns);
			this.pMetadata.Controls.Add(this.txtRegisters);
			this.pMetadata.Controls.Add(this.txtFolder);
			this.pMetadata.Controls.Add(this.lblColumns);
			this.pMetadata.Controls.Add(this.txtDate);
			this.pMetadata.Controls.Add(this.txtName);
			this.pMetadata.Controls.Add(this.lblFolder);
			this.pMetadata.Controls.Add(this.lblCreatedDate);
			this.pMetadata.Controls.Add(this.lblRegisters);
			this.pMetadata.Controls.Add(this.lblNameExtraction);
			this.pMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMetadata.Location = new System.Drawing.Point(0, 0);
			this.pMetadata.Name = "pMetadata";
			this.pMetadata.Size = new System.Drawing.Size(580, 61);
			this.pMetadata.TabIndex = 0;
			// 
			// btnDeleteExtraction
			// 
			this.btnDeleteExtraction.Location = new System.Drawing.Point(519, 3);
			this.btnDeleteExtraction.Margin = new System.Windows.Forms.Padding(2);
			this.btnDeleteExtraction.Name = "btnDeleteExtraction";
			this.btnDeleteExtraction.Size = new System.Drawing.Size(56, 26);
			this.btnDeleteExtraction.TabIndex = 10;
			this.btnDeleteExtraction.Text = "Delete";
			this.btnDeleteExtraction.UseVisualStyleBackColor = true;
			this.btnDeleteExtraction.Click += new System.EventHandler(this.BtnDeleteExtraction_Click);
			// 
			// txtColumns
			// 
			this.txtColumns.Location = new System.Drawing.Point(519, 33);
			this.txtColumns.Name = "txtColumns";
			this.txtColumns.Size = new System.Drawing.Size(47, 20);
			this.txtColumns.TabIndex = 9;
			// 
			// txtRegisters
			// 
			this.txtRegisters.Location = new System.Drawing.Point(331, 33);
			this.txtRegisters.Name = "txtRegisters";
			this.txtRegisters.Size = new System.Drawing.Size(91, 20);
			this.txtRegisters.TabIndex = 8;
			// 
			// txtFolder
			// 
			this.txtFolder.Location = new System.Drawing.Point(331, 7);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(140, 20);
			this.txtFolder.TabIndex = 7;
			// 
			// lblColumns
			// 
			this.lblColumns.AutoSize = true;
			this.lblColumns.Location = new System.Drawing.Point(436, 36);
			this.lblColumns.Name = "lblColumns";
			this.lblColumns.Size = new System.Drawing.Size(61, 13);
			this.lblColumns.TabIndex = 6;
			this.lblColumns.Text = "N. Columns";
			// 
			// txtDate
			// 
			this.txtDate.Location = new System.Drawing.Point(56, 33);
			this.txtDate.Name = "txtDate";
			this.txtDate.Size = new System.Drawing.Size(194, 20);
			this.txtDate.TabIndex = 5;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(47, 7);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(241, 20);
			this.txtName.TabIndex = 4;
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Location = new System.Drawing.Point(294, 10);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(36, 13);
			this.lblFolder.TabIndex = 3;
			this.lblFolder.Text = "Folder";
			// 
			// lblCreatedDate
			// 
			this.lblCreatedDate.AutoSize = true;
			this.lblCreatedDate.Location = new System.Drawing.Point(6, 36);
			this.lblCreatedDate.Name = "lblCreatedDate";
			this.lblCreatedDate.Size = new System.Drawing.Size(44, 13);
			this.lblCreatedDate.TabIndex = 2;
			this.lblCreatedDate.Text = "Created";
			// 
			// lblRegisters
			// 
			this.lblRegisters.AutoSize = true;
			this.lblRegisters.Location = new System.Drawing.Point(256, 36);
			this.lblRegisters.Name = "lblRegisters";
			this.lblRegisters.Size = new System.Drawing.Size(65, 13);
			this.lblRegisters.TabIndex = 1;
			this.lblRegisters.Text = "N. Registers";
			// 
			// lblNameExtraction
			// 
			this.lblNameExtraction.AutoSize = true;
			this.lblNameExtraction.Location = new System.Drawing.Point(6, 11);
			this.lblNameExtraction.Name = "lblNameExtraction";
			this.lblNameExtraction.Size = new System.Drawing.Size(35, 13);
			this.lblNameExtraction.TabIndex = 0;
			this.lblNameExtraction.Text = "Name";
			// 
			// dgvExtraction
			// 
			this.dgvExtraction.AllowUserToAddRows = false;
			this.dgvExtraction.AllowUserToDeleteRows = false;
			this.dgvExtraction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvExtraction.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvExtraction.Location = new System.Drawing.Point(0, 0);
			this.dgvExtraction.Name = "dgvExtraction";
			this.dgvExtraction.ReadOnly = true;
			this.dgvExtraction.Size = new System.Drawing.Size(580, 658);
			this.dgvExtraction.TabIndex = 0;
			// 
			// tbpTablas
			// 
			this.tbpTablas.Controls.Add(this.spcAllTables);
			this.tbpTablas.Controls.Add(this.lblshow);
			this.tbpTablas.Location = new System.Drawing.Point(4, 22);
			this.tbpTablas.Margin = new System.Windows.Forms.Padding(2);
			this.tbpTablas.Name = "tbpTablas";
			this.tbpTablas.Padding = new System.Windows.Forms.Padding(2);
			this.tbpTablas.Size = new System.Drawing.Size(1066, 726);
			this.tbpTablas.TabIndex = 1;
			this.tbpTablas.Text = "Tables";
			this.tbpTablas.UseVisualStyleBackColor = true;
			// 
			// spcAllTables
			// 
			this.spcAllTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcAllTables.Location = new System.Drawing.Point(2, 2);
			this.spcAllTables.Name = "spcAllTables";
			// 
			// spcAllTables.Panel1
			// 
			this.spcAllTables.Panel1.Controls.Add(this.spcLeftTables);
			// 
			// spcAllTables.Panel2
			// 
			this.spcAllTables.Panel2.Controls.Add(this.pTableAll);
			this.spcAllTables.Panel2.Controls.Add(this.pTablesUp);
			this.spcAllTables.Size = new System.Drawing.Size(1062, 722);
			this.spcAllTables.SplitterDistance = 274;
			this.spcAllTables.TabIndex = 1;
			// 
			// spcLeftTables
			// 
			this.spcLeftTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcLeftTables.Location = new System.Drawing.Point(0, 0);
			this.spcLeftTables.Name = "spcLeftTables";
			this.spcLeftTables.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcLeftTables.Panel1
			// 
			this.spcLeftTables.Panel1.Controls.Add(this.tvExtractionTable);
			// 
			// spcLeftTables.Panel2
			// 
			this.spcLeftTables.Panel2.Controls.Add(this.spcLleftButtomTable);
			this.spcLeftTables.Size = new System.Drawing.Size(274, 722);
			this.spcLeftTables.SplitterDistance = 314;
			this.spcLeftTables.TabIndex = 0;
			// 
			// tvExtractionTable
			// 
			this.tvExtractionTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExtractionTable.HideSelection = false;
			this.tvExtractionTable.Location = new System.Drawing.Point(0, 0);
			this.tvExtractionTable.Margin = new System.Windows.Forms.Padding(2);
			this.tvExtractionTable.Name = "tvExtractionTable";
			this.tvExtractionTable.Size = new System.Drawing.Size(274, 314);
			this.tvExtractionTable.TabIndex = 1;
			this.tvExtractionTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvExtractionTable_AfterSelect);
			// 
			// spcLleftButtomTable
			// 
			this.spcLleftButtomTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcLleftButtomTable.Location = new System.Drawing.Point(0, 0);
			this.spcLleftButtomTable.Name = "spcLleftButtomTable";
			this.spcLleftButtomTable.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcLleftButtomTable.Panel1
			// 
			this.spcLleftButtomTable.Panel1.Controls.Add(this.lbtExtractionsTable);
			// 
			// spcLleftButtomTable.Panel2
			// 
			this.spcLleftButtomTable.Panel2.Controls.Add(this.lbtTables);
			this.spcLleftButtomTable.Size = new System.Drawing.Size(274, 404);
			this.spcLleftButtomTable.SplitterDistance = 275;
			this.spcLleftButtomTable.TabIndex = 0;
			// 
			// lbtExtractionsTable
			// 
			this.lbtExtractionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbtExtractionsTable.FormattingEnabled = true;
			this.lbtExtractionsTable.Location = new System.Drawing.Point(0, 0);
			this.lbtExtractionsTable.Name = "lbtExtractionsTable";
			this.lbtExtractionsTable.Size = new System.Drawing.Size(274, 275);
			this.lbtExtractionsTable.TabIndex = 1;
			this.lbtExtractionsTable.SelectedIndexChanged += new System.EventHandler(this.LbtExtractionsTable_SelectedIndexChanged);
			// 
			// lbtTables
			// 
			this.lbtTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbtTables.FormattingEnabled = true;
			this.lbtTables.Location = new System.Drawing.Point(0, 0);
			this.lbtTables.Name = "lbtTables";
			this.lbtTables.Size = new System.Drawing.Size(274, 125);
			this.lbtTables.TabIndex = 2;
			this.lbtTables.SelectedIndexChanged += new System.EventHandler(this.LbtTables_SelectedIndexChanged);
			this.lbtTables.DoubleClick += new System.EventHandler(this.LbtTables_DoubleClick);
			// 
			// pTableAll
			// 
			this.pTableAll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pTableAll.Location = new System.Drawing.Point(0, 81);
			this.pTableAll.Name = "pTableAll";
			this.pTableAll.Size = new System.Drawing.Size(784, 641);
			this.pTableAll.TabIndex = 1;
			// 
			// pTablesUp
			// 
			this.pTablesUp.Controls.Add(this.txtCreateDate);
			this.pTablesUp.Controls.Add(this.lblCreateDate);
			this.pTablesUp.Controls.Add(this.btnExportToSQL);
			this.pTablesUp.Controls.Add(this.btnInsertRowDown);
			this.pTablesUp.Controls.Add(this.btnInsertRowUp);
			this.pTablesUp.Controls.Add(this.btnDeleteRows);
			this.pTablesUp.Controls.Add(this.btnCloseTable);
			this.pTablesUp.Controls.Add(this.btnDeleteTable);
			this.pTablesUp.Controls.Add(this.txtTableName);
			this.pTablesUp.Controls.Add(this.lblTableName);
			this.pTablesUp.Controls.Add(this.btnSave);
			this.pTablesUp.Controls.Add(this.btnCreateFromTable);
			this.pTablesUp.Controls.Add(this.btnCreateFromExtraction);
			this.pTablesUp.Dock = System.Windows.Forms.DockStyle.Top;
			this.pTablesUp.Location = new System.Drawing.Point(0, 0);
			this.pTablesUp.Name = "pTablesUp";
			this.pTablesUp.Size = new System.Drawing.Size(784, 81);
			this.pTablesUp.TabIndex = 0;
			// 
			// txtCreateDate
			// 
			this.txtCreateDate.Enabled = false;
			this.txtCreateDate.Location = new System.Drawing.Point(422, 48);
			this.txtCreateDate.Name = "txtCreateDate";
			this.txtCreateDate.Size = new System.Drawing.Size(127, 20);
			this.txtCreateDate.TabIndex = 12;
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.AutoSize = true;
			this.lblCreateDate.Location = new System.Drawing.Point(352, 51);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.Size = new System.Drawing.Size(62, 13);
			this.lblCreateDate.TabIndex = 11;
			this.lblCreateDate.Text = "Create date";
			// 
			// btnExportToSQL
			// 
			this.btnExportToSQL.Location = new System.Drawing.Point(592, 46);
			this.btnExportToSQL.Name = "btnExportToSQL";
			this.btnExportToSQL.Size = new System.Drawing.Size(82, 23);
			this.btnExportToSQL.TabIndex = 10;
			this.btnExportToSQL.Text = "Export to SQL";
			this.btnExportToSQL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnExportToSQL.UseVisualStyleBackColor = true;
			this.btnExportToSQL.Click += new System.EventHandler(this.BtnExportToSQL_Click);
			// 
			// btnInsertRowDown
			// 
			this.btnInsertRowDown.Location = new System.Drawing.Point(231, 45);
			this.btnInsertRowDown.Name = "btnInsertRowDown";
			this.btnInsertRowDown.Size = new System.Drawing.Size(96, 26);
			this.btnInsertRowDown.TabIndex = 9;
			this.btnInsertRowDown.Text = "Insert row Down";
			this.btnInsertRowDown.UseVisualStyleBackColor = true;
			this.btnInsertRowDown.Click += new System.EventHandler(this.BtnInsertRowDown_Click);
			// 
			// btnInsertRowUp
			// 
			this.btnInsertRowUp.Location = new System.Drawing.Point(115, 45);
			this.btnInsertRowUp.Name = "btnInsertRowUp";
			this.btnInsertRowUp.Size = new System.Drawing.Size(89, 25);
			this.btnInsertRowUp.TabIndex = 8;
			this.btnInsertRowUp.Text = "Insert row Up";
			this.btnInsertRowUp.UseVisualStyleBackColor = true;
			this.btnInsertRowUp.Click += new System.EventHandler(this.BtnInsertRowUp_Click);
			// 
			// btnDeleteRows
			// 
			this.btnDeleteRows.Location = new System.Drawing.Point(3, 45);
			this.btnDeleteRows.Name = "btnDeleteRows";
			this.btnDeleteRows.Size = new System.Drawing.Size(81, 25);
			this.btnDeleteRows.TabIndex = 7;
			this.btnDeleteRows.Text = "Delete row(s)";
			this.btnDeleteRows.UseVisualStyleBackColor = true;
			this.btnDeleteRows.Click += new System.EventHandler(this.BtnDeleteRows_Click);
			// 
			// btnCloseTable
			// 
			this.btnCloseTable.Location = new System.Drawing.Point(697, 46);
			this.btnCloseTable.Name = "btnCloseTable";
			this.btnCloseTable.Size = new System.Drawing.Size(81, 23);
			this.btnCloseTable.TabIndex = 6;
			this.btnCloseTable.Text = "Close table";
			this.btnCloseTable.UseVisualStyleBackColor = true;
			this.btnCloseTable.Click += new System.EventHandler(this.BtnCloseTable_Click);
			// 
			// btnDeleteTable
			// 
			this.btnDeleteTable.Location = new System.Drawing.Point(697, 9);
			this.btnDeleteTable.Name = "btnDeleteTable";
			this.btnDeleteTable.Size = new System.Drawing.Size(81, 24);
			this.btnDeleteTable.TabIndex = 5;
			this.btnDeleteTable.Text = "Delete table";
			this.btnDeleteTable.UseVisualStyleBackColor = true;
			this.btnDeleteTable.Click += new System.EventHandler(this.BtnDeleteTable_Click);
			// 
			// txtTableName
			// 
			this.txtTableName.Location = new System.Drawing.Point(422, 12);
			this.txtTableName.Name = "txtTableName";
			this.txtTableName.Size = new System.Drawing.Size(200, 20);
			this.txtTableName.TabIndex = 4;
			// 
			// lblTableName
			// 
			this.lblTableName.AutoSize = true;
			this.lblTableName.Location = new System.Drawing.Point(352, 15);
			this.lblTableName.Name = "lblTableName";
			this.lblTableName.Size = new System.Drawing.Size(63, 13);
			this.lblTableName.TabIndex = 3;
			this.lblTableName.Text = "Table name";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(231, 9);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(96, 27);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save Table";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// btnCreateFromTable
			// 
			this.btnCreateFromTable.Location = new System.Drawing.Point(137, 9);
			this.btnCreateFromTable.Name = "btnCreateFromTable";
			this.btnCreateFromTable.Size = new System.Drawing.Size(97, 27);
			this.btnCreateFromTable.TabIndex = 1;
			this.btnCreateFromTable.Text = "Create from table";
			this.btnCreateFromTable.UseVisualStyleBackColor = true;
			this.btnCreateFromTable.Click += new System.EventHandler(this.BtnCreateFromTable_Click);
			// 
			// btnCreateFromExtraction
			// 
			this.btnCreateFromExtraction.Location = new System.Drawing.Point(3, 9);
			this.btnCreateFromExtraction.Name = "btnCreateFromExtraction";
			this.btnCreateFromExtraction.Size = new System.Drawing.Size(123, 27);
			this.btnCreateFromExtraction.TabIndex = 0;
			this.btnCreateFromExtraction.Text = "Create from extraction";
			this.btnCreateFromExtraction.UseVisualStyleBackColor = true;
			this.btnCreateFromExtraction.Click += new System.EventHandler(this.BtnCreateFromExtraction_Click);
			// 
			// lblshow
			// 
			this.lblshow.AutoSize = true;
			this.lblshow.Location = new System.Drawing.Point(88, 53);
			this.lblshow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblshow.Name = "lblshow";
			this.lblshow.Size = new System.Drawing.Size(0, 13);
			this.lblshow.TabIndex = 0;
			// 
			// frmEasyBI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1074, 752);
			this.Controls.Add(this.tbcPages);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmEasyBI";
			this.Text = "EasyBI";
			this.tbcPages.ResumeLayout(false);
			this.tbpExtraccion.ResumeLayout(false);
			this.spcExtraction.Panel1.ResumeLayout(false);
			this.spcExtraction.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcExtraction)).EndInit();
			this.spcExtraction.ResumeLayout(false);
			this.spcFolderTree.Panel1.ResumeLayout(false);
			this.spcFolderTree.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcFolderTree)).EndInit();
			this.spcFolderTree.ResumeLayout(false);
			this.pSpcExtraccion.ResumeLayout(false);
			this.pRspExtraccion.ResumeLayout(false);
			this.spTreeFolderExtraction.Panel1.ResumeLayout(false);
			this.spTreeFolderExtraction.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spTreeFolderExtraction)).EndInit();
			this.spTreeFolderExtraction.ResumeLayout(false);
			this.spcExtracts.Panel1.ResumeLayout(false);
			this.spcExtracts.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcExtracts)).EndInit();
			this.spcExtracts.ResumeLayout(false);
			this.pMetadata.ResumeLayout(false);
			this.pMetadata.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvExtraction)).EndInit();
			this.tbpTablas.ResumeLayout(false);
			this.tbpTablas.PerformLayout();
			this.spcAllTables.Panel1.ResumeLayout(false);
			this.spcAllTables.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcAllTables)).EndInit();
			this.spcAllTables.ResumeLayout(false);
			this.spcLeftTables.Panel1.ResumeLayout(false);
			this.spcLeftTables.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcLeftTables)).EndInit();
			this.spcLeftTables.ResumeLayout(false);
			this.spcLleftButtomTable.Panel1.ResumeLayout(false);
			this.spcLleftButtomTable.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcLleftButtomTable)).EndInit();
			this.spcLleftButtomTable.ResumeLayout(false);
			this.pTablesUp.ResumeLayout(false);
			this.pTablesUp.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog opdExtraction;
		private System.Windows.Forms.TabControl tbcPages;
		private System.Windows.Forms.TabPage tbpExtraccion;
		private System.Windows.Forms.SplitContainer spcExtraction;
		private System.Windows.Forms.SplitContainer spcFolderTree;
		private System.Windows.Forms.Panel pSpcExtraccion;
		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.Button btnDeleteFolder;
		private System.Windows.Forms.Button btnCreateFolder;
		private System.Windows.Forms.Panel pRspExtraccion;
		private System.Windows.Forms.SplitContainer spcExtracts;
		private System.Windows.Forms.TreeView tvExtraccion;
		private System.Windows.Forms.TabPage tbpTablas;
		private System.Windows.Forms.Label lblshow;
		private System.Windows.Forms.SplitContainer spTreeFolderExtraction;
		private System.Windows.Forms.ListBox lbtExtractions;
		private System.Windows.Forms.DataGridView dgvExtraction;
		private System.Windows.Forms.Panel pMetadata;
		private System.Windows.Forms.Label lblCreatedDate;
		private System.Windows.Forms.Label lblRegisters;
		private System.Windows.Forms.Label lblNameExtraction;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.TextBox txtColumns;
		private System.Windows.Forms.TextBox txtRegisters;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Label lblColumns;
		private System.Windows.Forms.Button btnDeleteExtraction;
		private System.Windows.Forms.SplitContainer spcAllTables;
		private System.Windows.Forms.SplitContainer spcLeftTables;
		private System.Windows.Forms.SplitContainer spcLleftButtomTable;
		private System.Windows.Forms.TreeView tvExtractionTable;
		private System.Windows.Forms.ListBox lbtExtractionsTable;
		private System.Windows.Forms.Panel pTablesUp;
		private System.Windows.Forms.Panel pTableAll;
		private System.Windows.Forms.Button btnCreateFromTable;
		private System.Windows.Forms.Button btnCreateFromExtraction;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ListBox lbtTables;
		private System.Windows.Forms.TextBox txtTableName;
		private System.Windows.Forms.Label lblTableName;
		private System.Windows.Forms.Button btnDeleteTable;
		private System.Windows.Forms.Button btnCloseTable;
		private System.Windows.Forms.Button btnInsertRowDown;
		private System.Windows.Forms.Button btnInsertRowUp;
		private System.Windows.Forms.Button btnDeleteRows;
		private System.Windows.Forms.TextBox txtCreateDate;
		private System.Windows.Forms.Label lblCreateDate;
		private System.Windows.Forms.Button btnExportToSQL;
	}
}

