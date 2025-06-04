﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class MainForm : Form
	{
		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		enum KeyModifier
		{
			None = 0,
			Alt = 1,
			Control = 2,
			Shift = 4,
			WinKey = 8
		}

		Killer.DoubleClickFeel doubleClickFeel;
		Killer.RightClickFeel rightClickFeel;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
			string ErrorMessage = "Error loading configuration:\n";

			try
			{
				if (!File.Exists(Killer.Config.Read("Language"))) throw new FileNotFoundException("Language file not found.");
				Killer.Language = new(Killer.Config.Read("Language"));

				int width;
				if (int.TryParse(Killer.Config.Read("Width"), out width)) Width = width;
				int height;
				if (int.TryParse(Killer.Config.Read("Height"), out height)) Height = height;

				string hotkeyModifierStr = "Control, Shift";
				int hotkeyModifier = (int)Enum.Parse(typeof(KeyModifier), hotkeyModifierStr);
				hotkeyModifierStr = Killer.Config.Read("HotKeyModifier");
				if (!string.IsNullOrWhiteSpace(hotkeyModifierStr))
					hotkeyModifier = (int)Enum.Parse(typeof(KeyModifier), hotkeyModifierStr);

				string hotkeyButtonStr = "A";
				int hotkeyButton = (int)Enum.Parse(typeof(Keys), hotkeyButtonStr);
				hotkeyButtonStr = Killer.Config.Read("HotKeyButton");
				if (!string.IsNullOrWhiteSpace(hotkeyButtonStr))
					hotkeyButton = (int)Enum.Parse(typeof(Keys), hotkeyButtonStr);

				RegisterHotKey(Handle, 0, hotkeyModifier, hotkeyButton);

				int interval = 1000;
				int.TryParse(Killer.Config.Read("UpdateInterval"), out interval);

				Timer.Start();
				Timer.Interval = interval;

				Timer_Tick(this, EventArgs.Empty);
				ProcessList.Select();

				doubleClickFeel = (Killer.DoubleClickFeel)Enum.Parse(typeof(Killer.DoubleClickFeel), Killer.Config.Read("DoubleClick"));
				rightClickFeel = (Killer.RightClickFeel)Enum.Parse(typeof(Killer.RightClickFeel), Killer.Config.Read("RightClick"));

				if (rightClickFeel == Killer.RightClickFeel.Disable) ProcessList.ContextMenuStrip = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ErrorMessage + ex.Message, "Process Killer NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			ErrorMessage = "Error loading localization:\n";
			try
			{
				cmdKill.Text = Killer.Language.Read("cmdKillText", "Language");
				toolTips.SetToolTip(cmdKill, Killer.Language.Read("cmdKill", "Language"));
				toolTips.SetToolTip(cmdInfo, Killer.Language.Read("cmdInfo", "Language"));
				toolTips.SetToolTip(cmdRestartExplorer, Killer.Language.Read("cmdRestartExplorer", "Language"));
				toolTips.SetToolTip(cmdRun, Killer.Language.Read("cmdRun", "Language"));
				toolTips.SetToolTip(cmdConfigure, Killer.Language.Read("cmdConfigure", "Language"));
				toolTips.SetToolTip(cmdHelp, Killer.Language.Read("cmdHelp", "Language"));
				toolTips.SetToolTip(lblPID, Killer.Language.Read("lblPID", "Language"));
				toolTips.SetToolTip(lblThreads, Killer.Language.Read("lblThreads", "Language"));
				toolTips.SetToolTip(lblPriority, Killer.Language.Read("lblPriority", "Language"));

				if (!string.IsNullOrWhiteSpace(cmdKill.Text)) cmdKill.Image = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ErrorMessage + ex.Message, "Process Killer NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}


		private void Timer_Tick(object sender, EventArgs e)
		{
			//backup previous state
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			var top = ProcessList.TopIndex;

			//update list of processes
			ProcessList.BeginUpdate();
			ProcessList.Items.Clear();
			Process[] procs = Process.GetProcesses();
			for (int i = procs.Length - 1; i >= 0; i--)
			{
				ProcessList.Items.Add(new ProcessInfo(procs[i]));

				if (selected != null)
				{
					//restore selection
					if (procs[i].Id == selected.ProcessId)
						ProcessList.SelectedIndex = ProcessList.Items.Count - 1;
				}
			}

			//restore previous state
			ProcessList.TopIndex = top;
			ProcessList.EndUpdate();

			//if selection is not defined, select 1st line
			if (ProcessList.SelectedItem == null) ProcessList.SelectedIndex = 0;

			this.Text = string.Format("({0}) Process Killer NG {1}", ProcessList.Items.Count, Application.ProductVersion);
		}

		private void ProcessList_SelectedIndexChanged(object sender, EventArgs e)
		{
			//fill quick info bar, update context menu

			priRTToolStripMenuItem.Checked = false;
			priHighToolStripMenuItem.Checked = false;
			priNormToolStripMenuItem.Checked = false;
			priLowToolStripMenuItem.Checked = false;

			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected != null)
			{
				lblPID.Text = "PID: \t" + selected.ProcessId.ToString();

				try { lblThreads.Text = "thr: " + selected.Proc.Threads.Count; }
				catch { lblThreads.Text = "thr: ???"; }

				try
				{
					lblPriority.Text = "pri: " + selected.ProcessPriority.ToString();
					switch (selected.ProcessPriority)
					{
						case ProcessPriorityClass.RealTime:
							priRTToolStripMenuItem.Checked = true;
							break;
						case ProcessPriorityClass.High:
							priHighToolStripMenuItem.Checked = true;
							break;
						case ProcessPriorityClass.Normal:
							priNormToolStripMenuItem.Checked = true;
							break;
						case ProcessPriorityClass.Idle:
							priLowToolStripMenuItem.Checked = true;
							break;
					}
					priRTToolStripMenuItem.Enabled = true;
					priHighToolStripMenuItem.Enabled = true;
					priNormToolStripMenuItem.Enabled = true;
					priLowToolStripMenuItem.Enabled = true;
				}
				catch
				{
					lblPriority.Text = "pri: ???";

					priRTToolStripMenuItem.Enabled = false;
					priHighToolStripMenuItem.Enabled = false;
					priNormToolStripMenuItem.Enabled = false;
					priLowToolStripMenuItem.Enabled = false;
				}
			}
		}

		private void ProcessList_Leave(object sender, EventArgs e)
		{
			//the list box should not lose focus
			ProcessList.Select();
		}



		private void cmdKill_MouseClick(object sender, MouseEventArgs e)
		{
			// KILL button click
			switch (e.Button)
			{
				case MouseButtons.Left:
					// KILL
					bool kill = false;
					ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
					if (selected != null) kill = true;

					if (selected.ProcessName == Assembly.GetExecutingAssembly().GetName().Name)
					{
						DialogResult quest = MessageBox.Show(Killer.Language.Read("SelfKillQuestion", "Language"), Killer.Language.Read("SelfKillTitle", "Language"), MessageBoxButtons.YesNo);
						kill = (quest == DialogResult.Yes);
					}

					try
					{
						if (kill) selected.Proc.Kill();
					}
					catch (Exception ex)
					{
						string KillErrMsg = string.Format(Killer.Language.Read("CannotKill", "Language"), ex.Message, selected.ProcessName, selected.ProcessId);
						MessageBox.Show(KillErrMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}

					break;
				case MouseButtons.Right:
					// MIMIMIZE
					this.Hide();
					break;
				case MouseButtons.Middle:
					// EXIT
					DialogResult questExit = MessageBox.Show(Killer.Language.Read("ExitQuestion", "Language"), Application.ProductName, MessageBoxButtons.YesNo);
					if (questExit == DialogResult.Yes) { Application.Exit(); }
					break;
			}
		}

		private void cmdKill_Click(object sender, EventArgs e)
		{

		}

		private void cmdKill_MouseUp(object sender, MouseEventArgs e)
		{
			cmdKill_MouseClick(sender, e);
		}

		private void cmdInfo_Click(object sender, EventArgs e)
		{
			// INFO button click
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected != null) selected.ShowInfoDialog();
		}

		private void cmdHelp_Click(object sender, EventArgs e)
		{
			// ABOUT button click
			new AboutForm().ShowDialog();
		}

		private void Hotkey_Pressed(Keys key, KeyModifier modifier, int id)
		{
			// Global hotkey pressed
			this.WindowState = FormWindowState.Normal;
			this.Show();
			this.Activate();
		}

		protected override void WndProc(ref Message m)
		{
			// Receive WinAPI window messages
			base.WndProc(ref m);

			if (m.Msg == 0x0312) //WM_HOTKEY
			{
				Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
				KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
				int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

				Hotkey_Pressed(key, modifier, id);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// "Close" button clicked
			if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
			this.Hide();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			UnregisterHotKey(this.Handle, 0);
		}

		private void ProcessList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// PROCESS LIST - double click
			switch (doubleClickFeel)
			{
				case Killer.DoubleClickFeel.ProcessInfo:
					cmdInfo_Click(sender, EventArgs.Empty);
					break;
				case Killer.DoubleClickFeel.Kill:
					cmdKill_Click(sender, EventArgs.Empty);
					break;
				case Killer.DoubleClickFeel.Disable:
				default:
					break;
			}
		}
	}
}
