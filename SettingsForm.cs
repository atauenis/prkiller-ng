﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class SettingsForm : Form
	{
		MainForm mainform;

		Keys hotkeyModifiers;
		Keys hotkeyButton;

		public SettingsForm(MainForm mainForm)
		{
			InitializeComponent();
			mainform = mainForm;
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			//display available languages and current language file
			string[] allFiles = Directory.GetFiles(new FileInfo(Process.GetCurrentProcess().MainModule.FileName).DirectoryName, "*.ini");
			foreach (string iniFile in allFiles)
			{
				try
				{
					IniFile langIniFile = new(iniFile);
					if (!string.IsNullOrWhiteSpace(langIniFile.Read("Language", "Language")))
					{ cmbLanguage.Items.Add(new SettingsOption(iniFile, langIniFile.Read("Language", "Language"))); }
					if (langIniFile.IniPath == Killer.Language.IniPath) cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
					langIniFile = null;
				}
				catch { /*just do not add incorrect file*/ }
			}

			//prepare interface and other settings
			try
			{
				ClearSettings();
				Localize();
				LoadSettings();
				TopMost = Killer.Config.ReadBool(true, "AlwaysOnTop");
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
		}

		private void Localize()
		{
			this.Text = Killer.Language.ReadString("SettingsWindow", "Language");
			cmdOk.Text = Killer.Language.ReadString("cmdOK", "Language");
			cmdCancel.Text = Killer.Language.ReadString("cmdCancel", "Language");

			grpMainSettings.Text = Killer.Language.ReadString("grpMainWindow", "Language");
			lblAutorun.Text = Killer.Language.ReadString("lblAutorun", "Language");
			lblSelfkill.Text = Killer.Language.ReadString("lblSelfkill", "Language");
			lblKillSystem.Text = Killer.Language.ReadString("lblKillSystem", "Language");
			lblKillTree.Text = Killer.Language.ReadString("lblKillTree", "Language");
			lblOwnPriority.Text = Killer.Language.ReadString("lblOwnPriority", "Language");
			chkKillOldShell.Text = Killer.Language.ReadString("chkKillOldShell", "Language");
			chkAutomaticFindShell.Text = Killer.Language.ReadString("chkAutomaticFindShell", "Language");

			grpMouse.Text = Killer.Language.ReadString("grpMouse", "Language");
			lblDoubleClick.Text = Killer.Language.ReadString("lblDoubleClick", "Language");
			lblRightClick.Text = Killer.Language.ReadString("lblRightClick", "Language");

			grpOther.Text = Killer.Language.ReadString("grpOther", "Language");
			lblErrorSound.Text = Killer.Language.ReadString("lblErrorSound", "Language");
			chkAlwaysActive.Text = Killer.Language.ReadString("chkAlwaysActive", "Language");
			chkMinimizeOnKill.Text = Killer.Language.ReadString("chkMinimizeOnKill", "Language");
			chkTooltips.Text = Killer.Language.ReadString("chkTooltips", "Language");
			chkTooltipsInOptions.Text = Killer.Language.ReadString("chkTooltipsInOptions", "Language");

			grpKeyboard.Text = Killer.Language.ReadString("grpKeyboard", "Language");
			lblHotkey.Text = Killer.Language.ReadString("lblHotkey", "Language");
			lblOtherKeys.Text = Killer.Language.ReadString("lblOtherKeys", "Language");
			cmdOtherKeys.Text = Killer.Language.ReadString("cmdOtherKeys", "Language");



			//fill all boxes with possible values
			cbxAutorun.Items.Add(new SettingsOption("Disabled", Killer.Language.ReadString("AutorunDisabled", "Language")));
			cbxAutorun.Items.Add(new SettingsOption("AllUsers", Killer.Language.ReadString("AutorunAllUsers", "Language")));
			cbxAutorun.Items.Add(new SettingsOption("CurrentUser", Killer.Language.ReadString("AutorunCurrentuser", "Language")));

			cbxSelfkill.Items.Add(new SettingsOption("Enable", Killer.Language.ReadString("KillEnable", "Language")));
			cbxSelfkill.Items.Add(new SettingsOption("Prompt", Killer.Language.ReadString("KillPrompt", "Language")));
			cbxSelfkill.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("KillDisable", "Language")));

			cbxKillSystem.Items.Add(new SettingsOption("Enable", Killer.Language.ReadString("KillEnable", "Language")));
			cbxKillSystem.Items.Add(new SettingsOption("Prompt", Killer.Language.ReadString("KillPrompt", "Language")));
			cbxKillSystem.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("KillDisable", "Language")));

			cbxKillTree.Items.Add(new SettingsOption("Enable", Killer.Language.ReadString("KillEnable", "Language")));
			cbxKillTree.Items.Add(new SettingsOption("Prompt", Killer.Language.ReadString("KillPrompt", "Language")));
			cbxKillTree.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("KillDisable", "Language")));

			cbxPriority.Items.Add(new SettingsOption("Idle", Killer.Language.ReadString("OwnPriorityIdle", "Language")));
			cbxPriority.Items.Add(new SettingsOption("Normal", Killer.Language.ReadString("OwnPriorityNormal", "Language")));
			cbxPriority.Items.Add(new SettingsOption("High", Killer.Language.ReadString("OwnPriorityHigh", "Language")));
			cbxPriority.Items.Add(new SettingsOption("Realtime", Killer.Language.ReadString("OwnPriorityRealtime", "Language")));

			cbxDoubleClick.Items.Add(new SettingsOption("Kill", Killer.Language.ReadString("DoubleClickKill", "Language")));
			cbxDoubleClick.Items.Add(new SettingsOption("ProcessInfo", Killer.Language.ReadString("DoubleClickPI", "Language")));
			cbxDoubleClick.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("DoubleClickDisable", "Language")));

			cbxRightClick.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("RightClickDisable", "Language")));
			cbxRightClick.Items.Add(new SettingsOption("ContextMenu", Killer.Language.ReadString("RightClickContext", "Language")));

			cbxErrorSound.Items.Add(new SettingsOption("Beep", Killer.Language.ReadString("ErrorSoundBeep", "Language")));
			cbxErrorSound.Items.Add(new SettingsOption("SpeakerBeep", Killer.Language.ReadString("ErrorSoundSpeakerBeep", "Language")));
			cbxErrorSound.Items.Add(new SettingsOption("Disable", Killer.Language.ReadString("ErrorSoundDisable", "Language")));
		}

		private void ClearSettings()
		{
			//clear all boxes
			cbxAutorun.Items.Clear();
			cbxSelfkill.Items.Clear();
			cbxKillSystem.Items.Clear();
			cbxKillTree.Items.Clear();
			cbxPriority.Items.Clear();
			chkKillOldShell.Checked = false;
			chkAutomaticFindShell.Checked = false;
			cbxDoubleClick.Items.Clear();
			cbxRightClick.Items.Clear();
			cbxErrorSound.Items.Clear();
			chkAlwaysActive.Checked = false;
			chkMinimizeOnKill.Checked = false;
			chkTooltips.Checked = false;
			chkTooltipsInOptions.Checked = false;
		}
		private void LoadSettings()
		{
			//display current config file (Program Files or Application Data)
			lblConfFile.Text = Killer.Language.ReadString("lblConfFile", "Language") + " " +
			Killer.Config.IniPath;

			//display autorun setting
			RegistryKey HKCU = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			RegistryKey HKLM = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			object UserARParameter = HKCU.GetValue("prkiller-ng");
			object MachineARParameter = HKLM.GetValue("prkiller-ng");
			if (UserARParameter == null && MachineARParameter == null)
			{ cbxAutorun.SelectedIndex = 0; }
			else
			{
				if (MachineARParameter != null && MachineARParameter.ToString() == Application.ExecutablePath) cbxAutorun.SelectedIndex = 1;
				if (UserARParameter != null && UserARParameter.ToString() == Application.ExecutablePath) cbxAutorun.SelectedIndex = 2;
			}

			//set all boxes to setted values
			hotkeyModifiers = Killer.Config.ReadEnum<Keys>("HotKeyModifier");
			hotkeyButton = Killer.Config.ReadEnum<Keys>("HotKeyButton");
			cmdHotkey.Text = hotkeyModifiers.ToString() + " + " + hotkeyButton.ToString();
			cmdHotkey.Text = cmdHotkey.Text.Replace(", ", " + ");
			FillGroupBox(grpMainSettings);
			FillGroupBox(grpMouse);
			FillGroupBox(grpOther);
		}

		/// <summary>
		/// Load settings values from INI onto GroupBox subcontrols
		/// </summary>
		private void FillGroupBox(GroupBox groupbox)
		{
			foreach (Control ctrl in groupbox.Controls)
			{
				//using Tag property to find corresponding INI key name
				if (ctrl.Tag == null) continue;
				string Key = ctrl.Tag.ToString();

				if (ctrl is ComboBox combobox)
				{
					foreach (SettingsOption opt in combobox.Items)
					{
						if (opt.Value == Killer.Config.Read(Key))
							combobox.SelectedItem = opt;
					}
				}

				if (ctrl is CheckBox checkbox)
				{
					if (Killer.Config.KeyExists(Key))
					{
						checkbox.Checked = Killer.Config.ReadBool(false, Key);
					}
					else checkbox.CheckState = CheckState.Indeterminate;
				}
			}
		}

		/// <summary>
		/// Save settings from GroupBox subcontrols to INI
		/// </summary>
		private void SaveGroupBox(GroupBox groupbox)
		{
			foreach (Control ctrl in groupbox.Controls)
			{
				//using Tag property to find corresponding INI key name
				if (ctrl.Tag == null) continue;
				string Key = ctrl.Tag.ToString();

				if (ctrl is ComboBox combobox)
				{
					if (combobox.SelectedItem is SettingsOption sel)
						Killer.Config.Write(Key, sel.Value);
				}

				if (ctrl is CheckBox checkbox)
				{
					if (checkbox.CheckState == CheckState.Indeterminate) continue;
					if (checkbox.Checked) Killer.Config.Write(Key, "true");
					else Killer.Config.Write(Key, "false");
				}
			}
		}

		private void SettingsForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			Application.UseWaitCursor = true;
			cmdOk.Enabled = false;

			//save settings
			try
			{
				//save options that are not binded to GroupBoxes
				if (mainform.WindowState == FormWindowState.Normal)
				{
					Killer.Config.Write("Width", mainform.Width.ToString());
					Killer.Config.Write("Height", mainform.Height.ToString());
					Killer.Config.Write("Top", mainform.Top.ToString());
					Killer.Config.Write("Left", mainform.Left.ToString());
				}
				Killer.Config.Write("RamVirtShowUsed", mainform.RamVirtShowUsed ? "true" : "false");
				Killer.Config.Write("RamPhysShowUsed", mainform.RamPhysShowUsed ? "true" : "false");
				Killer.Config.Write("UpdateInterval", mainform.TimerInterval.ToString());
				Killer.Config.Write("HotKeyModifier", hotkeyModifiers.ToString());
				Killer.Config.Write("HotKeyButton", hotkeyButton.ToString());

				if (cmbLanguage.SelectedItem is SettingsOption sel)
					Killer.Config.Write("Language", sel.Value);

				string ARKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
				string ARParameter = "PrKiller-NG";

				try
				{ Registry.LocalMachine.OpenSubKey(ARKey, true).DeleteValue(ARParameter, false); }
				catch { }
				try
				{ Registry.CurrentUser.OpenSubKey(ARKey, true).DeleteValue(ARParameter, false); }
				catch { }

				if (cbxAutorun.SelectedItem is SettingsOption)
					switch (((SettingsOption)cbxAutorun.SelectedItem).Value)
					{
						case "Disabled":
							break;
						case "AllUsers":
							try
							{ Registry.LocalMachine.OpenSubKey(ARKey, true).SetValue(ARParameter, Application.ExecutablePath, RegistryValueKind.String); }
							catch (Exception ex)
							{ MessageBox.Show(ex.Message, "HKEY_LOCAL_MACHINE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
							break;
						case "CurrentUser":
							try
							{ Registry.CurrentUser.OpenSubKey(ARKey, true).SetValue(ARParameter, Application.ExecutablePath, RegistryValueKind.String); }
							catch (Exception ex)
							{ MessageBox.Show(ex.Message, "HKEY_CURRENT_USER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
							break;
					}

				//save options from GroupBoxes
				SaveGroupBox(grpMainSettings);
				SaveGroupBox(grpMouse);
				SaveGroupBox(grpOther);
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			//reload settings on the application
			cmdOk.Text = "...";
			mainform.LoadConfiguration();
			Application.UseWaitCursor = false;
			Close();
		}

		private void cmdHotkey_Click(object sender, EventArgs e)
		{
			HotkeySelectDialog hsd = new(Killer.Language.ReadString("HotKeyMain", "Language"), hotkeyModifiers, hotkeyButton);
			if (hsd.ShowDialog() == DialogResult.OK)
			{
				// validate selected hotkey
				Keys mod = hsd.DetectedModifiers;
				Keys key = hsd.DetectedKey;
				bool valid = true;

				if (mod == Keys.None) valid = false;
				switch (key)
				{
					case Keys.None:
					case Keys.ControlKey:
					case Keys.ShiftKey:
					case Keys.Menu:
						valid = false;
						break;
				}
				if (!Enum.TryParse(typeof(MainForm.KeyModifier), mod.ToString(), out var test))
					valid = false;

				if (valid)
				{
					cmdHotkey.Text = mod.ToString() + " + " + key.ToString();
					cmdHotkey.Text = cmdHotkey.Text.Replace(", ", " + ");
					hotkeyModifiers = mod;
					hotkeyButton = key;
				}
				else MessageBox.Show(Killer.Language.ReadString("BadHotKey", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void cmbLanguage_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				if (cmbLanguage.SelectedItem is SettingsOption sel)
					Killer.Language = new(sel.Value);

				ClearSettings();
				Localize();
				LoadSettings();
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
		}
	}

	/// <summary>
	/// Nice display of possibile INI settings in ComboBoxes
	/// </summary>
	internal class SettingsOption
	{
		/// <summary>
		/// The option's displayed text
		/// </summary>
		public string Text = "";
		/// <summary>
		/// The option's value in initialization file
		/// </summary>
		public string Value = "";

		public SettingsOption(string value, string text)
		{ Text = text; Value = value; }

		public override string ToString()
		{
			return Text; //used by ComboBox
		}
	}
}
