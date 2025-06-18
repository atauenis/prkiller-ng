﻿
namespace prkiller_ng
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.lblPID = new System.Windows.Forms.Label();
			this.lblThreads = new System.Windows.Forms.Label();
			this.lblPriority = new System.Windows.Forms.Label();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.cmdKill = new System.Windows.Forms.Button();
			this.cmdInfo = new System.Windows.Forms.Button();
			this.cmdRestartExplorer = new System.Windows.Forms.Button();
			this.cmdRun = new System.Windows.Forms.Button();
			this.cmdConfigure = new System.Windows.Forms.Button();
			this.cmdHelp = new System.Windows.Forms.Button();
			this.ProcessList = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.priRTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.priHighToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.priNormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.priLowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.procKillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.procKillTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.procPauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.procInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lblRamAll = new System.Windows.Forms.Label();
			this.lblRamPhys = new System.Windows.Forms.Label();
			this.lblRam2 = new System.Windows.Forms.Label();
			this.lblCPU = new System.Windows.Forms.Label();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.frequTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.freqHighToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freqNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freqLowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freqVeryLowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freqPausedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lblRamPhys2 = new System.Windows.Forms.Label();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.toolTips = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.ProcessList, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 380);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.lblPID);
			this.flowLayoutPanel1.Controls.Add(this.lblThreads);
			this.flowLayoutPanel1.Controls.Add(this.lblPriority);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 37);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(208, 16);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// lblPID
			// 
			this.lblPID.AutoSize = true;
			this.lblPID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPID.Location = new System.Drawing.Point(1, 0);
			this.lblPID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblPID.Name = "lblPID";
			this.lblPID.Size = new System.Drawing.Size(63, 17);
			this.lblPID.TabIndex = 0;
			this.lblPID.Text = "PID: 12345";
			// 
			// lblThreads
			// 
			this.lblThreads.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblThreads.Location = new System.Drawing.Point(66, 0);
			this.lblThreads.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblThreads.Name = "lblThreads";
			this.lblThreads.Size = new System.Drawing.Size(50, 17);
			this.lblThreads.TabIndex = 1;
			this.lblThreads.Text = "thr: 123";
			// 
			// lblPriority
			// 
			this.lblPriority.AutoSize = true;
			this.lblPriority.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPriority.Location = new System.Drawing.Point(118, 0);
			this.lblPriority.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(84, 17);
			this.lblPriority.TabIndex = 2;
			this.lblPriority.Text = "pri: normal (8)";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.cmdKill);
			this.flowLayoutPanel2.Controls.Add(this.cmdInfo);
			this.flowLayoutPanel2.Controls.Add(this.cmdRestartExplorer);
			this.flowLayoutPanel2.Controls.Add(this.cmdRun);
			this.flowLayoutPanel2.Controls.Add(this.cmdConfigure);
			this.flowLayoutPanel2.Controls.Add(this.cmdHelp);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 56);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.MaximumSize = new System.Drawing.Size(0, 23);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(208, 23);
			this.flowLayoutPanel2.TabIndex = 1;
			// 
			// cmdKill
			// 
			this.cmdKill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.cmdKill.Image = ((System.Drawing.Image)(resources.GetObject("cmdKill.Image")));
			this.cmdKill.Location = new System.Drawing.Point(3, 0);
			this.cmdKill.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdKill.Name = "cmdKill";
			this.cmdKill.Size = new System.Drawing.Size(48, 24);
			this.cmdKill.TabIndex = 0;
			this.cmdKill.Text = "Kill";
			this.toolTips.SetToolTip(this.cmdKill, "Finish him!");
			this.cmdKill.UseVisualStyleBackColor = true;
			this.cmdKill.Click += new System.EventHandler(this.cmdKill_Click);
			this.cmdKill.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmdKill_MouseClick);
			this.cmdKill.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdKill_MouseUp);
			// 
			// cmdInfo
			// 
			this.cmdInfo.Image = ((System.Drawing.Image)(resources.GetObject("cmdInfo.Image")));
			this.cmdInfo.Location = new System.Drawing.Point(57, 0);
			this.cmdInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdInfo.Name = "cmdInfo";
			this.cmdInfo.Size = new System.Drawing.Size(24, 24);
			this.cmdInfo.TabIndex = 1;
			this.cmdInfo.TabStop = false;
			this.cmdInfo.UseVisualStyleBackColor = true;
			this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
			// 
			// cmdRestartExplorer
			// 
			this.cmdRestartExplorer.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestartExplorer.Image")));
			this.cmdRestartExplorer.Location = new System.Drawing.Point(87, 0);
			this.cmdRestartExplorer.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdRestartExplorer.Name = "cmdRestartExplorer";
			this.cmdRestartExplorer.Size = new System.Drawing.Size(24, 24);
			this.cmdRestartExplorer.TabIndex = 2;
			this.cmdRestartExplorer.TabStop = false;
			this.cmdRestartExplorer.UseVisualStyleBackColor = true;
			// 
			// cmdRun
			// 
			this.cmdRun.Image = ((System.Drawing.Image)(resources.GetObject("cmdRun.Image")));
			this.cmdRun.Location = new System.Drawing.Point(117, 0);
			this.cmdRun.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(24, 24);
			this.cmdRun.TabIndex = 3;
			this.cmdRun.TabStop = false;
			this.cmdRun.UseVisualStyleBackColor = true;
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// cmdConfigure
			// 
			this.cmdConfigure.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfigure.Image")));
			this.cmdConfigure.Location = new System.Drawing.Point(147, 0);
			this.cmdConfigure.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdConfigure.Name = "cmdConfigure";
			this.cmdConfigure.Size = new System.Drawing.Size(24, 24);
			this.cmdConfigure.TabIndex = 4;
			this.cmdConfigure.TabStop = false;
			this.cmdConfigure.UseVisualStyleBackColor = true;
			// 
			// cmdHelp
			// 
			this.cmdHelp.Image = ((System.Drawing.Image)(resources.GetObject("cmdHelp.Image")));
			this.cmdHelp.Location = new System.Drawing.Point(177, 0);
			this.cmdHelp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cmdHelp.Name = "cmdHelp";
			this.cmdHelp.Size = new System.Drawing.Size(24, 24);
			this.cmdHelp.TabIndex = 5;
			this.cmdHelp.TabStop = false;
			this.cmdHelp.UseVisualStyleBackColor = true;
			this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
			// 
			// ProcessList
			// 
			this.ProcessList.ContextMenuStrip = this.contextMenuStrip1;
			this.ProcessList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProcessList.FormattingEnabled = true;
			this.ProcessList.ItemHeight = 15;
			this.ProcessList.Items.AddRange(new object[] {
            "ntoskrnl.exe",
            "svchost.exe",
            "explorer.exe",
            "internat.exe",
            "winword.exe * 32",
            "firefox.exe",
            "bash"});
			this.ProcessList.Location = new System.Drawing.Point(3, 83);
			this.ProcessList.Margin = new System.Windows.Forms.Padding(0);
			this.ProcessList.Name = "ProcessList";
			this.ProcessList.ScrollAlwaysVisible = true;
			this.ProcessList.Size = new System.Drawing.Size(208, 294);
			this.ProcessList.TabIndex = 2;
			this.ProcessList.SelectedIndexChanged += new System.EventHandler(this.ProcessList_SelectedIndexChanged);
			this.ProcessList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcessList_KeyDown);
			this.ProcessList.Leave += new System.EventHandler(this.ProcessList_Leave);
			this.ProcessList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProcessList_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priRTToolStripMenuItem,
            this.priHighToolStripMenuItem,
            this.priNormToolStripMenuItem,
            this.priLowToolStripMenuItem,
            this.toolStripSeparator1,
            this.procKillToolStripMenuItem,
            this.procKillTreeToolStripMenuItem,
            this.procPauseToolStripMenuItem,
            this.procInfoToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(141, 186);
			// 
			// priRTToolStripMenuItem
			// 
			this.priRTToolStripMenuItem.Name = "priRTToolStripMenuItem";
			this.priRTToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.priRTToolStripMenuItem.Text = "Pri: RT";
			this.priRTToolStripMenuItem.Click += new System.EventHandler(this.priRTToolStripMenuItem_Click);
			// 
			// priHighToolStripMenuItem
			// 
			this.priHighToolStripMenuItem.Name = "priHighToolStripMenuItem";
			this.priHighToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.priHighToolStripMenuItem.Text = "Pri: High";
			this.priHighToolStripMenuItem.Click += new System.EventHandler(this.priHighToolStripMenuItem_Click);
			// 
			// priNormToolStripMenuItem
			// 
			this.priNormToolStripMenuItem.Name = "priNormToolStripMenuItem";
			this.priNormToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.priNormToolStripMenuItem.Text = "Pri: Norm";
			this.priNormToolStripMenuItem.Click += new System.EventHandler(this.priNormToolStripMenuItem_Click);
			// 
			// priLowToolStripMenuItem
			// 
			this.priLowToolStripMenuItem.Name = "priLowToolStripMenuItem";
			this.priLowToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.priLowToolStripMenuItem.Text = "Pri: Low";
			this.priLowToolStripMenuItem.Click += new System.EventHandler(this.priLowToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
			// 
			// procKillToolStripMenuItem
			// 
			this.procKillToolStripMenuItem.Name = "procKillToolStripMenuItem";
			this.procKillToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.procKillToolStripMenuItem.Text = "Proc Kill";
			this.procKillToolStripMenuItem.Click += new System.EventHandler(this.procKillToolStripMenuItem_Click);
			// 
			// procKillTreeToolStripMenuItem
			// 
			this.procKillTreeToolStripMenuItem.Name = "procKillTreeToolStripMenuItem";
			this.procKillTreeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.procKillTreeToolStripMenuItem.Text = "Proc KillTree";
			this.procKillTreeToolStripMenuItem.Click += new System.EventHandler(this.procKillTreeToolStripMenuItem_Click);
			// 
			// procPauseToolStripMenuItem
			// 
			this.procPauseToolStripMenuItem.Name = "procPauseToolStripMenuItem";
			this.procPauseToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.procPauseToolStripMenuItem.Text = "Proc Pause";
			this.procPauseToolStripMenuItem.Click += new System.EventHandler(this.procPauseToolStripMenuItem_Click);
			// 
			// procInfoToolStripMenuItem
			// 
			this.procInfoToolStripMenuItem.Name = "procInfoToolStripMenuItem";
			this.procInfoToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.procInfoToolStripMenuItem.Text = "Proc Info";
			this.procInfoToolStripMenuItem.Click += new System.EventHandler(this.procInfoToolStripMenuItem_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.lblRamAll, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblRamPhys, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblRam2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.lblCPU, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblRamPhys2, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(208, 31);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// lblRamAll
			// 
			this.lblRamAll.AutoSize = true;
			this.lblRamAll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRamAll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRamAll.Location = new System.Drawing.Point(1, 0);
			this.lblRamAll.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblRamAll.Name = "lblRamAll";
			this.lblRamAll.Size = new System.Drawing.Size(33, 17);
			this.lblRamAll.TabIndex = 1;
			this.lblRamAll.Text = "4096";
			// 
			// lblRamPhys
			// 
			this.lblRamPhys.AutoSize = true;
			this.lblRamPhys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRamPhys.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRamPhys.Location = new System.Drawing.Point(36, 0);
			this.lblRamPhys.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblRamPhys.Name = "lblRamPhys";
			this.lblRamPhys.Size = new System.Drawing.Size(33, 17);
			this.lblRamPhys.TabIndex = 2;
			this.lblRamPhys.Text = "2048";
			// 
			// lblRam2
			// 
			this.lblRam2.AutoSize = true;
			this.lblRam2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRam2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRam2.Location = new System.Drawing.Point(1, 17);
			this.lblRam2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblRam2.Name = "lblRam2";
			this.lblRam2.Size = new System.Drawing.Size(33, 17);
			this.lblRam2.TabIndex = 4;
			this.lblRam2.Text = "4096";
			this.lblRam2.Click += new System.EventHandler(this.lblRam2_Click);
			// 
			// lblCPU
			// 
			this.lblCPU.AutoSize = true;
			this.lblCPU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCPU.ContextMenuStrip = this.contextMenuStrip2;
			this.lblCPU.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblCPU.Location = new System.Drawing.Point(71, 0);
			this.lblCPU.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblCPU.Name = "lblCPU";
			this.tableLayoutPanel2.SetRowSpan(this.lblCPU, 2);
			this.lblCPU.Size = new System.Drawing.Size(136, 34);
			this.lblCPU.TabIndex = 5;
			this.lblCPU.Text = "CPU Load";
			this.lblCPU.Paint += new System.Windows.Forms.PaintEventHandler(this.lblCPU_Paint);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequTitleToolStripMenuItem,
            this.toolStripSeparator2,
            this.freqHighToolStripMenuItem,
            this.freqNormalToolStripMenuItem,
            this.freqLowToolStripMenuItem,
            this.freqVeryLowToolStripMenuItem,
            this.freqPausedToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(152, 142);
			// 
			// frequTitleToolStripMenuItem
			// 
			this.frequTitleToolStripMenuItem.Enabled = false;
			this.frequTitleToolStripMenuItem.Name = "frequTitleToolStripMenuItem";
			this.frequTitleToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.frequTitleToolStripMenuItem.Text = "Frequ: title";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
			// 
			// freqHighToolStripMenuItem
			// 
			this.freqHighToolStripMenuItem.Name = "freqHighToolStripMenuItem";
			this.freqHighToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.freqHighToolStripMenuItem.Text = "Freq: High";
			this.freqHighToolStripMenuItem.Click += new System.EventHandler(this.freqHighToolStripMenuItem_Click);
			// 
			// freqNormalToolStripMenuItem
			// 
			this.freqNormalToolStripMenuItem.Name = "freqNormalToolStripMenuItem";
			this.freqNormalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.freqNormalToolStripMenuItem.Text = "Freq: Normal";
			this.freqNormalToolStripMenuItem.Click += new System.EventHandler(this.freqNormalToolStripMenuItem_Click);
			// 
			// freqLowToolStripMenuItem
			// 
			this.freqLowToolStripMenuItem.Name = "freqLowToolStripMenuItem";
			this.freqLowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.freqLowToolStripMenuItem.Text = "Freq: Low";
			this.freqLowToolStripMenuItem.Click += new System.EventHandler(this.freqLowToolStripMenuItem_Click);
			// 
			// freqVeryLowToolStripMenuItem
			// 
			this.freqVeryLowToolStripMenuItem.Name = "freqVeryLowToolStripMenuItem";
			this.freqVeryLowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.freqVeryLowToolStripMenuItem.Text = "Freq: Very Low";
			this.freqVeryLowToolStripMenuItem.Click += new System.EventHandler(this.freqVeryLowToolStripMenuItem_Click);
			// 
			// freqPausedToolStripMenuItem
			// 
			this.freqPausedToolStripMenuItem.Name = "freqPausedToolStripMenuItem";
			this.freqPausedToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.freqPausedToolStripMenuItem.Text = "Freq: Paused";
			this.freqPausedToolStripMenuItem.Click += new System.EventHandler(this.freqPausedToolStripMenuItem_Click);
			// 
			// lblRamPhys2
			// 
			this.lblRamPhys2.AutoSize = true;
			this.lblRamPhys2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRamPhys2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRamPhys2.Location = new System.Drawing.Point(36, 17);
			this.lblRamPhys2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.lblRamPhys2.Name = "lblRamPhys2";
			this.lblRamPhys2.Size = new System.Drawing.Size(33, 17);
			this.lblRamPhys2.TabIndex = 3;
			this.lblRamPhys2.Text = "2048";
			this.lblRamPhys2.Click += new System.EventHandler(this.lblRamPhys2_Click);
			// 
			// Timer
			// 
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(214, 380);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "(?? - ??) Process Killer new generation";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.contextMenuStrip2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label lblPID;
		private System.Windows.Forms.Label lblThreads;
		private System.Windows.Forms.Label lblPriority;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button cmdKill;
		private System.Windows.Forms.Button cmdInfo;
		private System.Windows.Forms.Button cmdRestartExplorer;
		private System.Windows.Forms.Button cmdRun;
		private System.Windows.Forms.Button cmdConfigure;
		private System.Windows.Forms.Button cmdHelp;
		private System.Windows.Forms.ListBox ProcessList;
		private System.Windows.Forms.Timer Timer;
		private System.Windows.Forms.ToolTip toolTips;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem priRTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem priHighToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem priNormToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem priLowToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem procKillToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem procKillTreeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem procPauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem procInfoToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblRamPhys2;
		private System.Windows.Forms.Label lblRamAll;
		private System.Windows.Forms.Label lblRamPhys;
		private System.Windows.Forms.Label lblRam2;
		private System.Windows.Forms.Label lblCPU;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem frequTitleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem freqHighToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freqNormalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freqLowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freqVeryLowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freqPausedToolStripMenuItem;
	}
}