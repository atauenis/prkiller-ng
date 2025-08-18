
namespace prkiller_ng
{
	partial class HotkeysEditorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeysEditorForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbAddKey = new System.Windows.Forms.ToolStripButton();
			this.tslAction = new System.Windows.Forms.ToolStripLabel();
			this.tsbAction = new System.Windows.Forms.ToolStripComboBox();
			this.tsbRemove = new System.Windows.Forms.ToolStripButton();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.lstHotkeys = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Location = new System.Drawing.Point(12, 347);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(400, 28);
			this.panel1.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddKey,
            this.tslAction,
            this.tsbAction,
            this.tsbRemove});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(400, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbAddKey
			// 
			this.tsbAddKey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAddKey.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddKey.Image")));
			this.tsbAddKey.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddKey.Name = "tsbAddKey";
			this.tsbAddKey.Size = new System.Drawing.Size(23, 22);
			this.tsbAddKey.Text = "toolStripButton1";
			this.tsbAddKey.Click += new System.EventHandler(this.tsbAddKey_Click);
			// 
			// tslAction
			// 
			this.tslAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tslAction.Image = ((System.Drawing.Image)(resources.GetObject("tslAction.Image")));
			this.tslAction.Name = "tslAction";
			this.tslAction.Size = new System.Drawing.Size(16, 22);
			this.tslAction.Text = "toolStripLabel2";
			// 
			// tsbAction
			// 
			this.tsbAction.Name = "tsbAction";
			this.tsbAction.Size = new System.Drawing.Size(130, 25);
			this.tsbAction.Text = "None";
			this.tsbAction.SelectedIndexChanged += new System.EventHandler(this.tsbAction_SelectedIndexChanged);
			// 
			// tsbRemove
			// 
			this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemove.Image")));
			this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRemove.Name = "tsbRemove";
			this.tsbRemove.Size = new System.Drawing.Size(23, 22);
			this.tsbRemove.Text = "toolStripButton2";
			this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(253, 382);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 3;
			this.cmdOk.Text = "button1";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(334, 382);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "button1";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// lstHotkeys
			// 
			this.lstHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lstHotkeys.Location = new System.Drawing.Point(12, 12);
			this.lstHotkeys.MultiSelect = false;
			this.lstHotkeys.Name = "lstHotkeys";
			this.lstHotkeys.Size = new System.Drawing.Size(400, 329);
			this.lstHotkeys.TabIndex = 4;
			this.lstHotkeys.UseCompatibleStateImageBehavior = false;
			this.lstHotkeys.View = System.Windows.Forms.View.Details;
			this.lstHotkeys.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstHotkeys_ItemSelectionChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 150;
			// 
			// HotkeysEditorForm
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(424, 418);
			this.Controls.Add(this.lstHotkeys);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotkeysEditorForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "HotkeysEditorForm";
			this.Load += new System.EventHandler(this.HotkeysEditorForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbAddKey;
		private System.Windows.Forms.ToolStripLabel tslAction;
		private System.Windows.Forms.ToolStripComboBox tsbAction;
		private System.Windows.Forms.ToolStripButton tsbRemove;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.ListView lstHotkeys;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}