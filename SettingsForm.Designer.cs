
namespace prkiller_ng
{
	partial class SettingsForm
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
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.grpMainSettings = new System.Windows.Forms.GroupBox();
			this.txtSystemShell = new System.Windows.Forms.TextBox();
			this.chkAutomaticFindShell = new System.Windows.Forms.CheckBox();
			this.chkKillOldShell = new System.Windows.Forms.CheckBox();
			this.cbxPriority = new System.Windows.Forms.ComboBox();
			this.cbxKillTree = new System.Windows.Forms.ComboBox();
			this.cbxKillSystem = new System.Windows.Forms.ComboBox();
			this.cbxSelfkill = new System.Windows.Forms.ComboBox();
			this.cbxAutorun = new System.Windows.Forms.ComboBox();
			this.lblOwnPriority = new System.Windows.Forms.Label();
			this.lblKillTree = new System.Windows.Forms.Label();
			this.lblKillSystem = new System.Windows.Forms.Label();
			this.lblSelfkill = new System.Windows.Forms.Label();
			this.lblAutorun = new System.Windows.Forms.Label();
			this.grpMouse = new System.Windows.Forms.GroupBox();
			this.lblRightClick = new System.Windows.Forms.Label();
			this.lblDoubleClick = new System.Windows.Forms.Label();
			this.cbxRightClick = new System.Windows.Forms.ComboBox();
			this.cbxDoubleClick = new System.Windows.Forms.ComboBox();
			this.grpOther = new System.Windows.Forms.GroupBox();
			this.lblErrorSound = new System.Windows.Forms.Label();
			this.cbxErrorSound = new System.Windows.Forms.ComboBox();
			this.chkTooltipsInOptions = new System.Windows.Forms.CheckBox();
			this.chkTooltips = new System.Windows.Forms.CheckBox();
			this.chkMinimizeOnKill = new System.Windows.Forms.CheckBox();
			this.chkAlwaysActive = new System.Windows.Forms.CheckBox();
			this.lblConfFile = new System.Windows.Forms.Label();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.cmbLanguage = new System.Windows.Forms.ComboBox();
			this.grpKeyboard = new System.Windows.Forms.GroupBox();
			this.lblOtherKeys = new System.Windows.Forms.Label();
			this.cmdOtherKeys = new System.Windows.Forms.Button();
			this.cmdHotkey = new System.Windows.Forms.Button();
			this.lblHotkey = new System.Windows.Forms.Label();
			this.grpMainSettings.SuspendLayout();
			this.grpMouse.SuspendLayout();
			this.grpOther.SuspendLayout();
			this.grpKeyboard.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(453, 383);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 98;
			this.cmdOk.Text = "button1";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(534, 383);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 99;
			this.cmdCancel.Text = "button1";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// grpMainSettings
			// 
			this.grpMainSettings.Controls.Add(this.txtSystemShell);
			this.grpMainSettings.Controls.Add(this.chkAutomaticFindShell);
			this.grpMainSettings.Controls.Add(this.chkKillOldShell);
			this.grpMainSettings.Controls.Add(this.cbxPriority);
			this.grpMainSettings.Controls.Add(this.cbxKillTree);
			this.grpMainSettings.Controls.Add(this.cbxKillSystem);
			this.grpMainSettings.Controls.Add(this.cbxSelfkill);
			this.grpMainSettings.Controls.Add(this.cbxAutorun);
			this.grpMainSettings.Controls.Add(this.lblOwnPriority);
			this.grpMainSettings.Controls.Add(this.lblKillTree);
			this.grpMainSettings.Controls.Add(this.lblKillSystem);
			this.grpMainSettings.Controls.Add(this.lblSelfkill);
			this.grpMainSettings.Controls.Add(this.lblAutorun);
			this.grpMainSettings.Location = new System.Drawing.Point(12, 67);
			this.grpMainSettings.Name = "grpMainSettings";
			this.grpMainSettings.Size = new System.Drawing.Size(322, 250);
			this.grpMainSettings.TabIndex = 100;
			this.grpMainSettings.TabStop = false;
			this.grpMainSettings.Text = "groupBox1";
			// 
			// txtSystemShell
			// 
			this.txtSystemShell.Enabled = false;
			this.txtSystemShell.Location = new System.Drawing.Point(7, 216);
			this.txtSystemShell.Name = "txtSystemShell";
			this.txtSystemShell.Size = new System.Drawing.Size(309, 23);
			this.txtSystemShell.TabIndex = 3;
			this.txtSystemShell.Tag = "WindowsShell";
			this.txtSystemShell.Text = "explorer.exe";
			// 
			// chkAutomaticFindShell
			// 
			this.chkAutomaticFindShell.AutoSize = true;
			this.chkAutomaticFindShell.Enabled = false;
			this.chkAutomaticFindShell.Location = new System.Drawing.Point(7, 194);
			this.chkAutomaticFindShell.Name = "chkAutomaticFindShell";
			this.chkAutomaticFindShell.Size = new System.Drawing.Size(82, 19);
			this.chkAutomaticFindShell.TabIndex = 2;
			this.chkAutomaticFindShell.Tag = "AutomaticFindShell";
			this.chkAutomaticFindShell.Text = "checkBox1";
			this.chkAutomaticFindShell.UseVisualStyleBackColor = true;
			// 
			// chkKillOldShell
			// 
			this.chkKillOldShell.AutoSize = true;
			this.chkKillOldShell.Enabled = false;
			this.chkKillOldShell.Location = new System.Drawing.Point(7, 168);
			this.chkKillOldShell.Name = "chkKillOldShell";
			this.chkKillOldShell.Size = new System.Drawing.Size(82, 19);
			this.chkKillOldShell.TabIndex = 2;
			this.chkKillOldShell.Tag = "KillOldShell";
			this.chkKillOldShell.Text = "checkBox1";
			this.chkKillOldShell.UseVisualStyleBackColor = true;
			// 
			// cbxPriority
			// 
			this.cbxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxPriority.FormattingEnabled = true;
			this.cbxPriority.Location = new System.Drawing.Point(131, 139);
			this.cbxPriority.Name = "cbxPriority";
			this.cbxPriority.Size = new System.Drawing.Size(185, 23);
			this.cbxPriority.TabIndex = 1;
			this.cbxPriority.Tag = "StartupPriority";
			// 
			// cbxKillTree
			// 
			this.cbxKillTree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxKillTree.FormattingEnabled = true;
			this.cbxKillTree.Location = new System.Drawing.Point(131, 110);
			this.cbxKillTree.Name = "cbxKillTree";
			this.cbxKillTree.Size = new System.Drawing.Size(185, 23);
			this.cbxKillTree.TabIndex = 1;
			this.cbxKillTree.Tag = "KillTree";
			// 
			// cbxKillSystem
			// 
			this.cbxKillSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxKillSystem.FormattingEnabled = true;
			this.cbxKillSystem.Location = new System.Drawing.Point(131, 81);
			this.cbxKillSystem.Name = "cbxKillSystem";
			this.cbxKillSystem.Size = new System.Drawing.Size(185, 23);
			this.cbxKillSystem.TabIndex = 1;
			this.cbxKillSystem.Tag = "KillSystem";
			// 
			// cbxSelfkill
			// 
			this.cbxSelfkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxSelfkill.FormattingEnabled = true;
			this.cbxSelfkill.Location = new System.Drawing.Point(131, 52);
			this.cbxSelfkill.Name = "cbxSelfkill";
			this.cbxSelfkill.Size = new System.Drawing.Size(185, 23);
			this.cbxSelfkill.TabIndex = 1;
			this.cbxSelfkill.Tag = "Selfkill";
			// 
			// cbxAutorun
			// 
			this.cbxAutorun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxAutorun.FormattingEnabled = true;
			this.cbxAutorun.Location = new System.Drawing.Point(131, 23);
			this.cbxAutorun.Name = "cbxAutorun";
			this.cbxAutorun.Size = new System.Drawing.Size(185, 23);
			this.cbxAutorun.TabIndex = 1;
			// 
			// lblOwnPriority
			// 
			this.lblOwnPriority.AutoSize = true;
			this.lblOwnPriority.Location = new System.Drawing.Point(7, 142);
			this.lblOwnPriority.Name = "lblOwnPriority";
			this.lblOwnPriority.Size = new System.Drawing.Size(38, 15);
			this.lblOwnPriority.TabIndex = 0;
			this.lblOwnPriority.Text = "label1";
			// 
			// lblKillTree
			// 
			this.lblKillTree.AutoSize = true;
			this.lblKillTree.Location = new System.Drawing.Point(7, 113);
			this.lblKillTree.Name = "lblKillTree";
			this.lblKillTree.Size = new System.Drawing.Size(38, 15);
			this.lblKillTree.TabIndex = 0;
			this.lblKillTree.Text = "label1";
			// 
			// lblKillSystem
			// 
			this.lblKillSystem.AutoSize = true;
			this.lblKillSystem.Location = new System.Drawing.Point(7, 84);
			this.lblKillSystem.Name = "lblKillSystem";
			this.lblKillSystem.Size = new System.Drawing.Size(38, 15);
			this.lblKillSystem.TabIndex = 0;
			this.lblKillSystem.Text = "label1";
			// 
			// lblSelfkill
			// 
			this.lblSelfkill.AutoSize = true;
			this.lblSelfkill.Location = new System.Drawing.Point(7, 55);
			this.lblSelfkill.Name = "lblSelfkill";
			this.lblSelfkill.Size = new System.Drawing.Size(38, 15);
			this.lblSelfkill.TabIndex = 0;
			this.lblSelfkill.Text = "label1";
			// 
			// lblAutorun
			// 
			this.lblAutorun.AutoSize = true;
			this.lblAutorun.Location = new System.Drawing.Point(7, 26);
			this.lblAutorun.Name = "lblAutorun";
			this.lblAutorun.Size = new System.Drawing.Size(38, 15);
			this.lblAutorun.TabIndex = 0;
			this.lblAutorun.Text = "label1";
			// 
			// grpMouse
			// 
			this.grpMouse.Controls.Add(this.lblRightClick);
			this.grpMouse.Controls.Add(this.lblDoubleClick);
			this.grpMouse.Controls.Add(this.cbxRightClick);
			this.grpMouse.Controls.Add(this.cbxDoubleClick);
			this.grpMouse.Location = new System.Drawing.Point(340, 67);
			this.grpMouse.Name = "grpMouse";
			this.grpMouse.Size = new System.Drawing.Size(269, 85);
			this.grpMouse.TabIndex = 101;
			this.grpMouse.TabStop = false;
			this.grpMouse.Text = "groupBox1";
			// 
			// lblRightClick
			// 
			this.lblRightClick.AutoSize = true;
			this.lblRightClick.Location = new System.Drawing.Point(6, 58);
			this.lblRightClick.Name = "lblRightClick";
			this.lblRightClick.Size = new System.Drawing.Size(38, 15);
			this.lblRightClick.TabIndex = 0;
			this.lblRightClick.Text = "label1";
			// 
			// lblDoubleClick
			// 
			this.lblDoubleClick.AutoSize = true;
			this.lblDoubleClick.Location = new System.Drawing.Point(6, 29);
			this.lblDoubleClick.Name = "lblDoubleClick";
			this.lblDoubleClick.Size = new System.Drawing.Size(38, 15);
			this.lblDoubleClick.TabIndex = 0;
			this.lblDoubleClick.Text = "label1";
			// 
			// cbxRightClick
			// 
			this.cbxRightClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxRightClick.FormattingEnabled = true;
			this.cbxRightClick.Location = new System.Drawing.Point(111, 55);
			this.cbxRightClick.Name = "cbxRightClick";
			this.cbxRightClick.Size = new System.Drawing.Size(152, 23);
			this.cbxRightClick.TabIndex = 1;
			this.cbxRightClick.Tag = "RightClick";
			// 
			// cbxDoubleClick
			// 
			this.cbxDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxDoubleClick.FormattingEnabled = true;
			this.cbxDoubleClick.Location = new System.Drawing.Point(111, 26);
			this.cbxDoubleClick.Name = "cbxDoubleClick";
			this.cbxDoubleClick.Size = new System.Drawing.Size(152, 23);
			this.cbxDoubleClick.TabIndex = 1;
			this.cbxDoubleClick.Tag = "DoubleClick";
			// 
			// grpOther
			// 
			this.grpOther.Controls.Add(this.lblErrorSound);
			this.grpOther.Controls.Add(this.cbxErrorSound);
			this.grpOther.Controls.Add(this.chkTooltipsInOptions);
			this.grpOther.Controls.Add(this.chkTooltips);
			this.grpOther.Controls.Add(this.chkMinimizeOnKill);
			this.grpOther.Controls.Add(this.chkAlwaysActive);
			this.grpOther.Location = new System.Drawing.Point(340, 158);
			this.grpOther.Name = "grpOther";
			this.grpOther.Size = new System.Drawing.Size(269, 159);
			this.grpOther.TabIndex = 102;
			this.grpOther.TabStop = false;
			this.grpOther.Text = "groupBox1";
			// 
			// lblErrorSound
			// 
			this.lblErrorSound.AutoSize = true;
			this.lblErrorSound.Location = new System.Drawing.Point(6, 27);
			this.lblErrorSound.Name = "lblErrorSound";
			this.lblErrorSound.Size = new System.Drawing.Size(38, 15);
			this.lblErrorSound.TabIndex = 0;
			this.lblErrorSound.Text = "label1";
			// 
			// cbxErrorSound
			// 
			this.cbxErrorSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxErrorSound.FormattingEnabled = true;
			this.cbxErrorSound.Location = new System.Drawing.Point(111, 24);
			this.cbxErrorSound.Name = "cbxErrorSound";
			this.cbxErrorSound.Size = new System.Drawing.Size(152, 23);
			this.cbxErrorSound.TabIndex = 1;
			this.cbxErrorSound.Tag = "ErrorSound";
			// 
			// chkTooltipsInOptions
			// 
			this.chkTooltipsInOptions.AutoSize = true;
			this.chkTooltipsInOptions.Enabled = false;
			this.chkTooltipsInOptions.Location = new System.Drawing.Point(6, 127);
			this.chkTooltipsInOptions.Name = "chkTooltipsInOptions";
			this.chkTooltipsInOptions.Size = new System.Drawing.Size(82, 19);
			this.chkTooltipsInOptions.TabIndex = 2;
			this.chkTooltipsInOptions.Tag = "ShowToolTipsInSettings";
			this.chkTooltipsInOptions.Text = "checkBox1";
			this.chkTooltipsInOptions.UseVisualStyleBackColor = true;
			// 
			// chkTooltips
			// 
			this.chkTooltips.AutoSize = true;
			this.chkTooltips.Location = new System.Drawing.Point(6, 102);
			this.chkTooltips.Name = "chkTooltips";
			this.chkTooltips.Size = new System.Drawing.Size(82, 19);
			this.chkTooltips.TabIndex = 2;
			this.chkTooltips.Tag = "ShowToolTips";
			this.chkTooltips.Text = "checkBox1";
			this.chkTooltips.UseVisualStyleBackColor = true;
			// 
			// chkMinimizeOnKill
			// 
			this.chkMinimizeOnKill.AutoSize = true;
			this.chkMinimizeOnKill.Location = new System.Drawing.Point(6, 77);
			this.chkMinimizeOnKill.Name = "chkMinimizeOnKill";
			this.chkMinimizeOnKill.Size = new System.Drawing.Size(82, 19);
			this.chkMinimizeOnKill.TabIndex = 2;
			this.chkMinimizeOnKill.Tag = "HideAfterKill";
			this.chkMinimizeOnKill.Text = "checkBox1";
			this.chkMinimizeOnKill.UseVisualStyleBackColor = true;
			// 
			// chkAlwaysActive
			// 
			this.chkAlwaysActive.AutoSize = true;
			this.chkAlwaysActive.Location = new System.Drawing.Point(6, 52);
			this.chkAlwaysActive.Name = "chkAlwaysActive";
			this.chkAlwaysActive.Size = new System.Drawing.Size(82, 19);
			this.chkAlwaysActive.TabIndex = 2;
			this.chkAlwaysActive.Tag = "AlwaysActive";
			this.chkAlwaysActive.Text = "checkBox1";
			this.chkAlwaysActive.UseVisualStyleBackColor = true;
			// 
			// lblConfFile
			// 
			this.lblConfFile.AutoEllipsis = true;
			this.lblConfFile.Location = new System.Drawing.Point(12, 12);
			this.lblConfFile.Name = "lblConfFile";
			this.lblConfFile.Size = new System.Drawing.Size(597, 15);
			this.lblConfFile.TabIndex = 103;
			this.lblConfFile.Text = "label1";
			this.lblConfFile.UseMnemonic = false;
			// 
			// lblLanguage
			// 
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.Location = new System.Drawing.Point(12, 41);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(125, 15);
			this.lblLanguage.TabIndex = 103;
			this.lblLanguage.Text = "Language of interface:";
			// 
			// cmbLanguage
			// 
			this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLanguage.FormattingEnabled = true;
			this.cmbLanguage.Location = new System.Drawing.Point(143, 38);
			this.cmbLanguage.Name = "cmbLanguage";
			this.cmbLanguage.Size = new System.Drawing.Size(466, 23);
			this.cmbLanguage.TabIndex = 1;
			this.cmbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cmbLanguage_SelectionChangeCommitted);
			// 
			// grpKeyboard
			// 
			this.grpKeyboard.Controls.Add(this.lblOtherKeys);
			this.grpKeyboard.Controls.Add(this.cmdOtherKeys);
			this.grpKeyboard.Controls.Add(this.cmdHotkey);
			this.grpKeyboard.Controls.Add(this.lblHotkey);
			this.grpKeyboard.Location = new System.Drawing.Point(12, 324);
			this.grpKeyboard.Name = "grpKeyboard";
			this.grpKeyboard.Size = new System.Drawing.Size(597, 53);
			this.grpKeyboard.TabIndex = 104;
			this.grpKeyboard.TabStop = false;
			this.grpKeyboard.Text = "groupBox1";
			// 
			// lblOtherKeys
			// 
			this.lblOtherKeys.AutoSize = true;
			this.lblOtherKeys.Location = new System.Drawing.Point(328, 23);
			this.lblOtherKeys.Name = "lblOtherKeys";
			this.lblOtherKeys.Size = new System.Drawing.Size(38, 15);
			this.lblOtherKeys.TabIndex = 2;
			this.lblOtherKeys.Text = "label1";
			// 
			// cmdOtherKeys
			// 
			this.cmdOtherKeys.Enabled = false;
			this.cmdOtherKeys.Location = new System.Drawing.Point(439, 19);
			this.cmdOtherKeys.Name = "cmdOtherKeys";
			this.cmdOtherKeys.Size = new System.Drawing.Size(152, 23);
			this.cmdOtherKeys.TabIndex = 1;
			this.cmdOtherKeys.Text = "button1";
			this.cmdOtherKeys.UseVisualStyleBackColor = true;
			// 
			// cmdHotkey
			// 
			this.cmdHotkey.Location = new System.Drawing.Point(131, 19);
			this.cmdHotkey.Name = "cmdHotkey";
			this.cmdHotkey.Size = new System.Drawing.Size(185, 23);
			this.cmdHotkey.TabIndex = 1;
			this.cmdHotkey.Text = "button1";
			this.cmdHotkey.UseVisualStyleBackColor = true;
			this.cmdHotkey.Click += new System.EventHandler(this.cmdHotkey_Click);
			// 
			// lblHotkey
			// 
			this.lblHotkey.AutoSize = true;
			this.lblHotkey.Location = new System.Drawing.Point(7, 23);
			this.lblHotkey.Name = "lblHotkey";
			this.lblHotkey.Size = new System.Drawing.Size(38, 15);
			this.lblHotkey.TabIndex = 0;
			this.lblHotkey.Text = "label1";
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(623, 420);
			this.Controls.Add(this.grpKeyboard);
			this.Controls.Add(this.lblLanguage);
			this.Controls.Add(this.lblConfFile);
			this.Controls.Add(this.cmbLanguage);
			this.Controls.Add(this.grpOther);
			this.Controls.Add(this.grpMouse);
			this.Controls.Add(this.grpMainSettings);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SettingsForm";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SettingsForm_PreviewKeyDown);
			this.grpMainSettings.ResumeLayout(false);
			this.grpMainSettings.PerformLayout();
			this.grpMouse.ResumeLayout(false);
			this.grpMouse.PerformLayout();
			this.grpOther.ResumeLayout(false);
			this.grpOther.PerformLayout();
			this.grpKeyboard.ResumeLayout(false);
			this.grpKeyboard.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.GroupBox grpMainSettings;
		private System.Windows.Forms.TextBox txtSystemShell;
		private System.Windows.Forms.CheckBox chkAutomaticFindShell;
		private System.Windows.Forms.CheckBox chkKillOldShell;
		private System.Windows.Forms.ComboBox cbxPriority;
		private System.Windows.Forms.ComboBox cbxKillTree;
		private System.Windows.Forms.ComboBox cbxKillSystem;
		private System.Windows.Forms.ComboBox cbxSelfkill;
		private System.Windows.Forms.ComboBox cbxAutorun;
		private System.Windows.Forms.Label lblOwnPriority;
		private System.Windows.Forms.Label lblKillTree;
		private System.Windows.Forms.Label lblKillSystem;
		private System.Windows.Forms.Label lblSelfkill;
		private System.Windows.Forms.Label lblAutorun;
		private System.Windows.Forms.GroupBox grpMouse;
		private System.Windows.Forms.Label lblRightClick;
		private System.Windows.Forms.Label lblDoubleClick;
		private System.Windows.Forms.ComboBox cbxRightClick;
		private System.Windows.Forms.ComboBox cbxDoubleClick;
		private System.Windows.Forms.GroupBox grpOther;
		private System.Windows.Forms.Label lblErrorSound;
		private System.Windows.Forms.ComboBox cbxErrorSound;
		private System.Windows.Forms.CheckBox chkTooltipsInOptions;
		private System.Windows.Forms.CheckBox chkTooltips;
		private System.Windows.Forms.CheckBox chkMinimizeOnKill;
		private System.Windows.Forms.CheckBox chkAlwaysActive;
		private System.Windows.Forms.Label lblConfFile;
		private System.Windows.Forms.Label lblLanguage;
		private System.Windows.Forms.ComboBox cmbLanguage;
		private System.Windows.Forms.GroupBox grpKeyboard;
		private System.Windows.Forms.Label lblOtherKeys;
		private System.Windows.Forms.Button cmdOtherKeys;
		private System.Windows.Forms.Button cmdHotkey;
		private System.Windows.Forms.Label lblHotkey;
	}
}