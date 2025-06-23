using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

		[DllImport("user32.dll")]
		private static extern bool LockSetForegroundWindow(UInt32 uLockCode);
		[DllImport("user32.dll")]
		private static extern bool AllowSetForegroundWindow(uint dwProcessId);

		const UInt32 LSFW_LOCK = 1;
		const UInt32 LSFW_UNLOCK = 2;

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		static extern IntPtr SetActiveWindow(IntPtr hWnd);

		Killer.DoubleClickAction doubleClickAction;
		Killer.RightClickAction rightClickAction;
		Killer.KillPolicy selfkillAction;
		Killer.KillPolicy killTreeAction;
		Killer.KillPolicy killSystemProcAction;

		bool AlwaysOnTop = true;
		bool AlwaysActive = false;
		bool AlwaysActivePause = false;

		PerformanceCounter cpuCounter;
		bool RamVirtShowUsed = false;
		bool RamPhysShowUsed = false;
		bool CtrlPressed = false;
		bool ShiftPressed = false;
		bool AltPressed = false;

		int CpuLoad = 0;
		List<int> CpuLoadHistory = new();
		Killer.CpuGraphStyle CpuGraphStyle = Killer.CpuGraphStyle.Disable;

		bool FirstTimeShow = true;

		public MainForm()
		{
			InitializeComponent();

			cpuCounter = new PerformanceCounter(
"Processor",
"% Processor Time",
"_Total",
true
);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			string ErrorMessage = "Error loading configuration:\n";

			if (CheckSecondInstance()) Application.Exit();

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

				if (Killer.Config.Read("RamVirtShowUsed").ToLowerInvariant() == "true") RamVirtShowUsed = true;
				if (Killer.Config.Read("RamPhysShowUsed").ToLowerInvariant() == "true") RamPhysShowUsed = true;

				int interval = 1000;
				int.TryParse(Killer.Config.Read("UpdateInterval"), out interval);

				freqHighToolStripMenuItem.Checked = interval == 400;
				freqNormalToolStripMenuItem.Checked = interval == 800;
				freqLowToolStripMenuItem.Checked = interval == 2000;
				freqVeryLowToolStripMenuItem.Checked = interval == 4000;

				Timer.Start();
				Timer.Interval = interval;

				Timer_Tick(this, EventArgs.Empty);
				ProcessList.Select();

				doubleClickAction = (Killer.DoubleClickAction)Enum.Parse(typeof(Killer.DoubleClickAction), Killer.Config.Read("DoubleClick"));
				rightClickAction = (Killer.RightClickAction)Enum.Parse(typeof(Killer.RightClickAction), Killer.Config.Read("RightClick"));

				if (rightClickAction == Killer.RightClickAction.Disable) ProcessList.ContextMenuStrip = null;

				CpuGraphStyle = (Killer.CpuGraphStyle)Enum.Parse(typeof(Killer.CpuGraphStyle), Killer.Config.Read("CpuGraphStyle"));

				if (Enum.TryParse(typeof(ProcessPriorityClass), Killer.Config.Read("StartupPriority"), out object startupPriority))
				{
					Process.GetCurrentProcess().PriorityClass = (ProcessPriorityClass)startupPriority;
				}
				Process.GetCurrentProcess().PriorityBoostEnabled = true;

				selfkillAction = (Killer.KillPolicy)Enum.Parse(typeof(Killer.KillPolicy), Killer.Config.Read("Selfkill"));
				killTreeAction = (Killer.KillPolicy)Enum.Parse(typeof(Killer.KillPolicy), Killer.Config.Read("KillTree"));
				killSystemProcAction = (Killer.KillPolicy)Enum.Parse(typeof(Killer.KillPolicy), Killer.Config.Read("KillSystem"));

				if (Killer.Config.Read("AlwaysOnTop").ToLowerInvariant() == "true") AlwaysOnTop = true;
				if (Killer.Config.Read("AlwaysActive").ToLowerInvariant() == "true") AlwaysActive = true;

				TopMost = AlwaysOnTop;

				if (AlwaysActive)
				{
					AllowSetForegroundWindow((uint)Process.GetCurrentProcess().Id);
					LockSetForegroundWindow(LSFW_LOCK);
				}
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

				priRTToolStripMenuItem.Text = Killer.Language.Read("priRTToolStripMenuItem", "Language");
				priHighToolStripMenuItem.Text = Killer.Language.Read("priHighToolStripMenuItem", "Language");
				priNormToolStripMenuItem.Text = Killer.Language.Read("priNormToolStripMenuItem", "Language");
				priLowToolStripMenuItem.Text = Killer.Language.Read("priLowToolStripMenuItem", "Language");
				procKillToolStripMenuItem.Text = Killer.Language.Read("procKillToolStripMenuItem", "Language");
				procKillTreeToolStripMenuItem.Text = Killer.Language.Read("procKillTreeToolStripMenuItem", "Language");
				procPauseToolStripMenuItem.Text = Killer.Language.Read("procPauseToolStripMenuItem", "Language");
				procInfoToolStripMenuItem.Text = Killer.Language.Read("procInfoToolStripMenuItem", "Language");

				frequTitleToolStripMenuItem.Text = Killer.Language.Read("freqTitleToolStripMenuItem", "Language");
				freqHighToolStripMenuItem.Text = Killer.Language.Read("freqHighToolStripMenuItem", "Language");
				freqHighToolStripMenuItem.Text = Killer.Language.Read("freqHighToolStripMenuItem", "Language");
				freqNormalToolStripMenuItem.Text = Killer.Language.Read("freqNormalToolStripMenuItem", "Language");
				freqLowToolStripMenuItem.Text = Killer.Language.Read("freqLowToolStripMenuItem", "Language");
				freqVeryLowToolStripMenuItem.Text = Killer.Language.Read("freqVeryLowToolStripMenuItem", "Language");
				freqPausedToolStripMenuItem.Text = Killer.Language.Read("freqPausedToolStripMenuItem", "Language");

				toolTips.SetToolTip(lblRamAll, Killer.Language.Read("lblRamAll", "Language"));
				toolTips.SetToolTip(lblRamPhys, Killer.Language.Read("lblRamPhys", "Language"));

				if (!string.IsNullOrWhiteSpace(cmdKill.Text)) cmdKill.Image = null;
			}
			catch (Exception ex)
			{
				AlwaysActivePause = true;
				MessageBox.Show(ErrorMessage + ex.Message, "Process Killer NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		private bool CheckSecondInstance()
		{
			foreach (Process proc in Process.GetProcesses())
			{
				try
				{
					if (proc.Id == Process.GetCurrentProcess().Id) continue;
					if (proc.MainModule.FileName == Process.GetCurrentProcess().MainModule.FileName)
					{
						MessageBox.Show(Killer.Language.Read("AnotherInstanceRunning", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return true;
					}
					if (proc.ProcessName == Process.GetCurrentProcess().ProcessName)
					{
						MessageBox.Show(Killer.Language.Read("AnotherSimilarInstanceRunning", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return true;
					}
				}
				catch { }
			}
			return false;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if (!Timer.Enabled) return;

			//correct window focus
			if (AlwaysActive && !AlwaysActivePause)
			{
				this.Activate();
				this.Focus();
			}

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

			//update memory statictics
			ulong RamAll = (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalVirtualMemory / 1024 / 1024 / 1024);
			ulong RamPhys = (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024);
			ulong RamAvail = (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1024 / 1024 / 1024);
			ulong RamPhysAvail = (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1024 / 1024);

			lblRamAll.Text = RamAll.ToString();
			lblRamPhys.Text = RamPhys.ToString();

			if (RamVirtShowUsed)
			{
				lblRam2.Text = (RamAll - RamAvail).ToString();
				toolTips.SetToolTip(lblRam2, Killer.Language.Read("lblRam2_Used", "Language"));
			}
			else
			{
				lblRam2.Text = RamAvail.ToString();
				toolTips.SetToolTip(lblRam2, Killer.Language.Read("lblRam2_Free", "Language"));
			}

			if (RamPhysShowUsed)
			{
				lblRamPhys2.Text = (RamPhys - RamPhysAvail).ToString();
				toolTips.SetToolTip(lblRamPhys2, Killer.Language.Read("lblRamPhys2_Used", "Language"));
			}
			else
			{
				lblRamPhys2.Text = RamPhysAvail.ToString();
				toolTips.SetToolTip(lblRamPhys2, Killer.Language.Read("lblRamPhys2_Free", "Language"));
			}

			//update CPU statistics
			CpuLoad = Convert.ToInt32(cpuCounter.NextValue());
			CpuLoadHistory.Add(CpuLoad);
			if (CpuLoadHistory.Count > Screen.PrimaryScreen.Bounds.Width) CpuLoadHistory.RemoveAt(0);
			lblCPU.Refresh();

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
					lblPriority.Text = "pri: " + selected.ProcessPriority.ToString() + " (" + selected.Proc.BasePriority + ")";
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
					KillProcess();
					break;
				case MouseButtons.Right:
					// MIMIMIZE
					this.Hide();
					break;
				case MouseButtons.Middle:
					// EXIT
					KillKiller();
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
			ProcessInfo();
		}

		private void cmdHelp_Click(object sender, EventArgs e)
		{
			// ABOUT button click
			AlwaysActivePause = true;
			new AboutForm().ShowDialog();
			AlwaysActivePause = false;
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
			switch (doubleClickAction)
			{
				case Killer.DoubleClickAction.ProcessInfo:
					ProcessInfo();
					break;
				case Killer.DoubleClickAction.Kill:
					KillProcess();
					break;
				case Killer.DoubleClickAction.Disable:
				default:
					break;
			}
		}

		private void ProcessList_KeyDown(object sender, KeyEventArgs e)
		{
			// PROCESS LIST - keyboard press
			if (e.Handled) return;

			CtrlPressed = e.Control;
			ShiftPressed = e.Shift;
			AltPressed = e.Alt;

			string key = (e.Modifiers + "+" + e.KeyCode).Replace(", ", "+").Replace("None+", "");
			string cmd = Killer.Config.Read(key);
			switch (cmd)
			{
				case "Hide":
					Hide();
					break;
				case "MoveUp":
					if (ProcessList.SelectedIndex > 0)
						ProcessList.SelectedIndex--;
					break;
				case "MoveDown":
					if (ProcessList.SelectedIndex < ProcessList.Items.Count - 1)
						ProcessList.SelectedIndex++;
					break;
				case "Kill":
				case "KillProcess":
					KillProcess();
					break;
				case "KillProcessTree":
					KillProcess(true);
					break;
				case "KillDontHide":
					KillProcess(false, false);
					break;
				case "KillProcessTreeDontHide":
					KillProcess(true, false);
					break;
				case "ProcessInfo":
					ProcessInfo();
					break;
				case "Exit":
					KillKiller();
					break;
				case "RunDialog":
					RunDialog();
					break;
				case "PriorityIncrease":
					SetProcessPriority(1);
					break;
				case "PriorityDecrease":
					SetProcessPriority(-1);
					break;
				case "PriorityIdle":
					SetProcessPriority(ProcessPriorityClass.Idle);
					break;
				case "PriorityNormal":
					SetProcessPriority(ProcessPriorityClass.Normal);
					break;
				case "PriorityHigh":
					SetProcessPriority(ProcessPriorityClass.High);
					break;
				case "PriorityRealTime":
					SetProcessPriority(ProcessPriorityClass.RealTime);
					break;
				case "ContextMenu":
					contextMenuStrip1.Show(ProcessList.Location);
					break;
				case "FindParent":
					int ParentProcId = ((ProcessInfo)ProcessList.SelectedItem).Proc.GetParentProcess().Id;
					foreach (ProcessInfo proc in ProcessList.Items)
					{
						if (proc.Proc.Id == ParentProcId)
						{
							ProcessList.SelectedItem = proc;
							break;
						}
					}
					this.Text = Killer.Language.Read("ParentProcessNotFound", "Language");
					break;
				case "Restart":
					ProcessInfo BaseProc = ((ProcessInfo)ProcessList.SelectedItem);
					ProcessStartInfo NewProcStartInfo = new();
					NewProcStartInfo.FileName = "cmd.exe";
					NewProcStartInfo.Arguments = @"/c start """" """ + ((ProcessInfo)ProcessList.SelectedItem).CommandLine + @"""";
					Process NewProc = new() { StartInfo = NewProcStartInfo };
					NewProc.Start();
					BaseProc.Proc.Kill();
					break;
				case "SuspendResumeProcess":
					ProcessInfo SelProcSusRes = ((ProcessInfo)ProcessList.SelectedItem);
					if (SelProcSusRes.Suspended) SelProcSusRes.Proc.Resume();
					else SelProcSusRes.Proc.Suspend();
					break;
				case "SuspendProcess":
					((ProcessInfo)ProcessList.SelectedItem).Proc.Suspend();
					break;
				case "ResumeProcess":
					((ProcessInfo)ProcessList.SelectedItem).Proc.Resume();
					break;
				case "":
					Debug.Print("Unknown key: " + key);
					break;
				case "RestartExplorer":
					AlwaysActivePause = true;
					MessageBox.Show("Not implemented", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					AlwaysActivePause = false;
					break;
				default:
					AlwaysActivePause = true;
					MessageBox.Show(Killer.Language.Read("BadKeyMapping", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					AlwaysActivePause = false;
					break;
			}
			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			RunDialog();
		}

		/// <summary>
		/// Kill selected process
		/// </summary>
		/// <param name="tree">Kill entire process tree</param>
		/// <param name="hide">Override `Hide window after kill` setting</param>
		private void KillProcess(bool tree = false, bool? hide = null)
		{
			// Check for need of hide the window
			if (hide is null && Killer.Config.Read("HideAfterKill").ToLowerInvariant() == "true") hide = true;
			if (hide is null) hide = false;
			if (CtrlPressed) hide = false;

			// Prepare variables
			bool kill = false;
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected != null) kill = true;

			// Check for system process kill
			if (selected.Proc.IsCritical())
			{
				switch (killSystemProcAction)
				{
					case Killer.KillPolicy.Disable:
						kill = false;
						this.Text = Killer.Language.Read("KillSystemDisabled", "Language");
						break;
					case Killer.KillPolicy.Prompt:
						AlwaysActivePause = true;
						DialogResult quest = MessageBox.Show(Killer.Language.Read("KillSystemQuestion", "Language"), Killer.Language.Read("KillSystemTitle", "Language"), MessageBoxButtons.YesNo);
						kill = (quest == DialogResult.Yes);
						AlwaysActivePause = false;
						break;
					case Killer.KillPolicy.Enable:
						kill = true;
						break;
				}
				if (kill == false) return;
			}

			// Check for process tree kill
			if (tree && killTreeAction == Killer.KillPolicy.Prompt)
			{
				AlwaysActivePause = true;
				DialogResult quest = MessageBox.Show(Killer.Language.Read("KillTreeQuestion", "Language"), Killer.Language.Read("KillTreeTitle", "Language"), MessageBoxButtons.YesNo);
				AlwaysActivePause = false;
				if (quest == DialogResult.No) return;
			}
			if (tree && killTreeAction == Killer.KillPolicy.Disable)
			{
				this.Text = Killer.Language.Read("KillTreeDisabled", "Language");
				return;
			}

			// Check for selfkill
			if (selected.ProcessId == Process.GetCurrentProcess().Id)
			{
				switch (selfkillAction)
				{
					case Killer.KillPolicy.Disable:
						kill = false;
						this.Text = Killer.Language.Read("SelfkillDisabled", "Language");
						break;
					case Killer.KillPolicy.Prompt:
						AlwaysActivePause = true;
						DialogResult quest = MessageBox.Show(Killer.Language.Read("SelfKillQuestion", "Language"), Killer.Language.Read("SelfKillTitle", "Language"), MessageBoxButtons.YesNo);
						kill = (quest == DialogResult.Yes);
						AlwaysActivePause = false;
						break;
					case Killer.KillPolicy.Enable:
						kill = true;
						break;
				}
				if (kill == false) return;
			}

			// Fire!
			try
			{
				if (kill) selected.Proc.Kill(tree);
				if (hide ?? false) this.Hide();
			}
			catch (Exception ex)
			{
				string KillErrMsg = string.Format(Killer.Language.Read("CannotKill", "Language"), ex.Message, selected.ProcessName, selected.ProcessId);
				this.Text = KillErrMsg;
			}
		}

		/// <summary>
		/// Exit Process Killer NG
		/// </summary>
		private void KillKiller()
		{
			AlwaysActivePause = true;
			DialogResult questExit = MessageBox.Show(Killer.Language.Read("ExitQuestion", "Language"), Application.ProductName, MessageBoxButtons.YesNo);
			if (questExit == DialogResult.Yes) { Application.Exit(); }
			AlwaysActivePause = false;
		}

		/// <summary>
		/// Show information about selected process
		/// </summary>
		private void ProcessInfo()
		{
			AlwaysActivePause = true;
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected != null) selected.ShowInfoDialog();
			AlwaysActivePause = false;
		}

		/// <summary>
		/// Open Run dialog
		/// </summary>
		private void RunDialog()
		{
			AlwaysActivePause = true;
			new RunDialog().ShowDialog();
			AlwaysActivePause = false;
		}

		private void procInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProcessInfo();
		}

		private void procKillToolStripMenuItem_Click(object sender, EventArgs e)
		{
			KillProcess();
		}

		private void procKillTreeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			KillProcess(true);
		}
		private void procPauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ProcessInfo SelProcSusRes = ((ProcessInfo)ProcessList.SelectedItem);
				if (SelProcSusRes.Suspended) SelProcSusRes.Proc.Resume();
				else SelProcSusRes.Proc.Suspend();
			}
			catch (Exception ex)
			{
				AlwaysActivePause = true;
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				AlwaysActivePause = false;
			}
		}

		private void SetProcessPriority(ProcessPriorityClass pri)
		{
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected == null) return;
			AlwaysActivePause = true;
			try
			{ selected.Proc.PriorityClass = pri; }
			catch (Exception ex) { MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
			AlwaysActivePause = false;
		}

		private void SetProcessPriority(int change)
		{
			ProcessInfo selected = ProcessList.SelectedItem as ProcessInfo;
			if (selected == null) return;

			try
			{
				switch (change)
				{
					case 1:
						//[+1] increase priority
						switch (selected.Proc.PriorityClass)
						{
							case ProcessPriorityClass.Idle:
								selected.Proc.PriorityClass = ProcessPriorityClass.BelowNormal;
								break;
							case ProcessPriorityClass.BelowNormal:
								selected.Proc.PriorityClass = ProcessPriorityClass.Normal;
								break;
							case ProcessPriorityClass.Normal:
								selected.Proc.PriorityClass = ProcessPriorityClass.AboveNormal;
								break;
							case ProcessPriorityClass.AboveNormal:
								selected.Proc.PriorityClass = ProcessPriorityClass.High;
								break;
							case ProcessPriorityClass.High:
								selected.Proc.PriorityClass = ProcessPriorityClass.RealTime;
								break;
							case ProcessPriorityClass.RealTime:
								this.Text = Killer.Language.Read("MaximumPriority", "Language");
								break;
							default:
								break;
						}
						return;
					case -1:
						//[-1] decrease priority
						switch (selected.Proc.PriorityClass)
						{
							case ProcessPriorityClass.RealTime:
								selected.Proc.PriorityClass = ProcessPriorityClass.High;
								break;
							case ProcessPriorityClass.High:
								selected.Proc.PriorityClass = ProcessPriorityClass.AboveNormal;
								break;
							case ProcessPriorityClass.AboveNormal:
								selected.Proc.PriorityClass = ProcessPriorityClass.Normal;
								break;
							case ProcessPriorityClass.Normal:
								selected.Proc.PriorityClass = ProcessPriorityClass.BelowNormal;
								break;
							case ProcessPriorityClass.BelowNormal:
								selected.Proc.PriorityClass = ProcessPriorityClass.Idle;
								break;
							case ProcessPriorityClass.Idle:
								selected.Proc.PriorityClass = ProcessPriorityClass.Idle;
								this.Text = Killer.Language.Read("MinimumPriority", "Language");
								break;
							default:
								break;
						}
						return;
					case 0:
						//[0] don't change priority
						return;
					default:
						throw new ArgumentException("Change level must be +1, 0 or -1.", nameof(change));
				}
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException) throw;
				string PriSetErrMsg = string.Format(Killer.Language.Read("CannotChangePriority", "Language"), ex.Message, selected.ProcessName, selected.ProcessId);
				this.Text = PriSetErrMsg;
			}
		}

		private void priLowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetProcessPriority(ProcessPriorityClass.Idle);
		}

		private void priNormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetProcessPriority(ProcessPriorityClass.Normal);
		}

		private void priHighToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetProcessPriority(ProcessPriorityClass.High);
		}

		private void priRTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetProcessPriority(ProcessPriorityClass.RealTime);
		}

		private void lblRam2_Click(object sender, EventArgs e)
		{
			RamVirtShowUsed = !RamVirtShowUsed;
			Timer_Tick(null, null);
		}

		private void lblRamPhys2_Click(object sender, EventArgs e)
		{
			RamPhysShowUsed = !RamPhysShowUsed;
			Timer_Tick(null, null);
		}

		private void lblCPU_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect = e.ClipRectangle;
			Graphics graph = e.Graphics;
			float percentH = (float)rect.Height / 100;
			float percentW = (float)rect.Width / 100;

			switch (CpuGraphStyle)
			{
				case Killer.CpuGraphStyle.Disable:
					//mode 0: none
					graph.FillRectangle(SystemBrushes.ButtonFace, rect);
					return;
				case Killer.CpuGraphStyle.Bar:
					//mode 1: just a bar
					graph.FillRectangle(Brushes.SlateGray, rect);
					Rectangle BarRect = new(0, 0, Convert.ToInt32(percentW * CpuLoad), rect.Height - 1);
					graph.FillRectangle(Brushes.LightBlue, BarRect);
					graph.DrawRectangle(SystemPens.MenuHighlight, BarRect);
					return;
				case Killer.CpuGraphStyle.Graph:
					//mode 2: CPU load graph
					graph.FillRectangle(Brushes.Black, rect);
					int histpos = CpuLoadHistory.Count - 1;
					for (int pos = rect.Width; pos > 0; pos--)
					{
						histpos--;
						if (histpos > CpuLoadHistory.Count) return;
						if (histpos < 0) return;
						int Ypos = Convert.ToInt32(percentH * CpuLoadHistory[histpos]);
						Rectangle GraphRect = new(pos, rect.Height - Ypos, 1, rect.Height);
						graph.FillRectangle(Brushes.LightGreen, GraphRect);
					}
					return;
				case Killer.CpuGraphStyle.Label:
					//mode 3: simply a text label
					lblCPU.Text = "CPU Usage: " + Convert.ToInt32(cpuCounter.NextValue()).ToString() + "%";
					return;
			}
		}

		private void freqHighToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer.Interval = 400;
			Timer.Enabled = true;

			freqHighToolStripMenuItem.Checked = true;
			freqNormalToolStripMenuItem.Checked = false;
			freqLowToolStripMenuItem.Checked = false;
			freqVeryLowToolStripMenuItem.Checked = false;
			freqPausedToolStripMenuItem.Checked = false;
		}

		private void freqNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer.Interval = 800;
			Timer.Enabled = true;

			freqHighToolStripMenuItem.Checked = false;
			freqNormalToolStripMenuItem.Checked = true;
			freqLowToolStripMenuItem.Checked = false;
			freqVeryLowToolStripMenuItem.Checked = false;
			freqPausedToolStripMenuItem.Checked = false;
		}

		private void freqLowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer.Interval = 2000;
			Timer.Enabled = true;

			freqHighToolStripMenuItem.Checked = false;
			freqNormalToolStripMenuItem.Checked = false;
			freqLowToolStripMenuItem.Checked = true;
			freqVeryLowToolStripMenuItem.Checked = false;
			freqPausedToolStripMenuItem.Checked = false;
		}

		private void freqVeryLowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer.Interval = 4000;
			Timer.Enabled = true;


			freqHighToolStripMenuItem.Checked = false;
			freqNormalToolStripMenuItem.Checked = false;
			freqLowToolStripMenuItem.Checked = false;
			freqVeryLowToolStripMenuItem.Checked = true;
			freqPausedToolStripMenuItem.Checked = false;
		}

		private void freqPausedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer.Enabled = false;

			freqHighToolStripMenuItem.Checked = false;
			freqNormalToolStripMenuItem.Checked = false;
			freqLowToolStripMenuItem.Checked = false;
			freqVeryLowToolStripMenuItem.Checked = false;
			freqPausedToolStripMenuItem.Checked = true;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (FirstTimeShow)
			{
				Hide();
				FirstTimeShow = false;
				return;
			}
		}
	}
}
