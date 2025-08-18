using Microsoft.Win32;
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

		[Flags]
		internal enum KeyModifier
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

		[DllImport("kernel32.dll")]
		public static extern bool Beep(uint frequency, uint duration);

		Killer.DoubleClickAction doubleClickAction = Killer.DoubleClickAction.ProcessInfo;
		Killer.RightClickAction rightClickAction = Killer.RightClickAction.ContextMenu;
		Killer.KillPolicy selfkillAction = Killer.KillPolicy.Prompt;
		Killer.KillPolicy killTreeAction = Killer.KillPolicy.Prompt;
		Killer.KillPolicy killSystemProcAction = Killer.KillPolicy.Enable;
		Killer.KillPolicy restartShellAction = Killer.KillPolicy.Enable;

		bool AlwaysOnTop = true;
		bool AlwaysActive = false;
		bool AlwaysActivePause = false;
		bool ShowToolTips = false;

		Killer.ErrorSound Sound = Killer.ErrorSound.Beep;

		PerformanceCounter cpuCounter;
		internal bool RamVirtShowUsed = false;
		internal bool RamPhysShowUsed = false;
		bool CtrlPressed = false;
		bool ShiftPressed = false;
		bool AltPressed = false;

		int CpuLoad = 0;
		List<int> CpuLoadHistory = new();
		Killer.CpuGraphStyle CpuGraphStyle = Killer.CpuGraphStyle.Disable;

		internal int TimerInterval { get { return Timer.Interval; } }

		bool FirstTimeShow = true;

		string CurrentUserName = @"localhost\root";

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
			LoadConfiguration();
		}

		/// <summary>
		/// Load or reload program configuration from initialization file
		/// </summary>
		public void LoadConfiguration()
		{
			string ErrorMessage = "Error loading configuration:\n";

			try
			{
				string exePath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).DirectoryName + @"\";
				string iniPath = exePath + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".ini";
				if (!File.Exists(iniPath)) throw new FileNotFoundException(iniPath + " not found.");
				Killer.Config = new(iniPath);

				if (!Killer.Config.KeyExists("Language")) throw new Exception("Language file is not specified.");
				string iniLangPath = Killer.Config.Read("Language");
				string altLangPath = exePath + Killer.Config.Read("Language");
				string langPath = string.Empty;
				if (File.Exists(iniLangPath)) langPath = iniLangPath;
				if (File.Exists(altLangPath)) langPath = altLangPath;
				if (!File.Exists(iniLangPath) && !File.Exists(altLangPath))
				{ throw new FileNotFoundException("Language file not found."); }
				Killer.Language = new(langPath);

				CurrentUserName = Process.GetCurrentProcess().GetProcessUser().Name;
				if (CheckSecondInstance()) Application.Exit();

				if (Killer.Config.KeyExists("Width"))
					Width = Killer.Config.ReadInt("Width");
				if (Killer.Config.KeyExists("Height"))
					Height = Killer.Config.ReadInt("Height");
				if (Killer.Config.KeyExists("Top"))
					Top = Killer.Config.ReadInt("Top");
				if (Killer.Config.KeyExists("Left"))
					Left = Killer.Config.ReadInt("Left");

				if (Top > Screen.FromControl(this).Bounds.Height) Top = Screen.FromControl(this).Bounds.Height - Height;
				if (Left > Screen.FromControl(this).Bounds.Width) Left = Screen.FromControl(this).Bounds.Width - Width;

				string hotkeyModifierStr = Killer.Config.Read("Control, Shift", "HotKeyModifier", null);
				int hotkeyModifier = (int)Enum.Parse(typeof(KeyModifier), hotkeyModifierStr);

				string hotkeyButtonStr = Killer.Config.Read("A", "HotKeyButton", null);
				int hotkeyButton = (int)Enum.Parse(typeof(Keys), hotkeyButtonStr);

				RegisterHotKey(Handle, 0, hotkeyModifier, hotkeyButton);

				RamVirtShowUsed = Killer.Config.ReadBool(false, "RamVirtShowUsed");
				RamPhysShowUsed = Killer.Config.ReadBool(false, "RamPhysShowUsed");

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

				if (Killer.Config.KeyExists("DoubleClick"))
					doubleClickAction = Killer.Config.ReadEnum<Killer.DoubleClickAction>("DoubleClick");

				if (Killer.Config.KeyExists("RightClick"))
					rightClickAction = Killer.Config.ReadEnum<Killer.RightClickAction>("RightClick");

				if (rightClickAction == Killer.RightClickAction.Disable) ProcessList.ContextMenuStrip = null;

				if (Killer.Config.KeyExists("CpuGraphStyle"))
					CpuGraphStyle = Killer.Config.ReadEnum<Killer.CpuGraphStyle>("CpuGraphStyle");

				if (Killer.Config.KeyExists("StartupPriority"))
					Process.GetCurrentProcess().PriorityClass = Killer.Config.ReadEnum<ProcessPriorityClass>("StartupPriority");

				Process.GetCurrentProcess().PriorityBoostEnabled = true;

				if (Killer.Config.KeyExists("Selfkill"))
					selfkillAction = Killer.Config.ReadEnum<Killer.KillPolicy>("Selfkill");
				if (Killer.Config.KeyExists("KillTree"))
					killTreeAction = Killer.Config.ReadEnum<Killer.KillPolicy>("KillTree");
				if (Killer.Config.KeyExists("KillSystem"))
					killSystemProcAction = Killer.Config.ReadEnum<Killer.KillPolicy>("KillSystem");
				if (Killer.Config.KeyExists("RestartShell"))
					restartShellAction = Killer.Config.ReadEnum<Killer.KillPolicy>("RestartShell");

				ShowToolTips = Killer.Config.ReadBool(true, "ShowToolTips");
				toolTips.Active = ShowToolTips;
				toolTips.UseAnimation = true;

				AlwaysOnTop = Killer.Config.ReadBool(true, "AlwaysOnTop");
				AlwaysActive = Killer.Config.ReadBool(true, "AlwaysActive");
				TopMost = AlwaysOnTop;

				if (AlwaysActive)
				{
					AllowSetForegroundWindow((uint)Process.GetCurrentProcess().Id);
					LockSetForegroundWindow(LSFW_LOCK);
				}

				if (Killer.Config.KeyExists("ErrorSound"))
					Sound = Killer.Config.ReadEnum<Killer.ErrorSound>("ErrorSound");
			}
			catch (Exception ex)
			{
				AlwaysActivePause = true;
				MessageBox.Show(ErrorMessage + ex.Message, "Process Killer NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			ErrorMessage = "Error loading localization:\n";
			try
			{
				cmdKill.Text = Killer.Language.Read("cmdKillText", "Language");
				toolTips.SetToolTip(cmdKill, Killer.Language.ReadString("cmdKill", "Language"));
				toolTips.SetToolTip(cmdInfo, Killer.Language.ReadString("cmdInfo", "Language"));
				toolTips.SetToolTip(cmdRestartExplorer, Killer.Language.ReadString("cmdRestartExplorer", "Language"));
				toolTips.SetToolTip(cmdRun, Killer.Language.ReadString("cmdRun", "Language"));
				toolTips.SetToolTip(cmdConfigure, Killer.Language.ReadString("cmdConfigure", "Language"));
				toolTips.SetToolTip(cmdHelp, Killer.Language.ReadString("cmdHelp", "Language"));
				toolTips.SetToolTip(lblPID, Killer.Language.ReadString("lblPID", "Language"));
				toolTips.SetToolTip(lblThreads, Killer.Language.ReadString("lblThreads", "Language"));
				toolTips.SetToolTip(lblPriority, Killer.Language.ReadString("lblPriority", "Language"));

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

				toolTips.SetToolTip(lblRamAll, Killer.Language.ReadString("lblRamAll", "Language"));
				toolTips.SetToolTip(lblRamPhys, Killer.Language.ReadString("lblRamPhys", "Language"));

				if (!string.IsNullOrWhiteSpace(cmdKill.Text)) cmdKill.Image = null;
			}
			catch (Exception ex)
			{
				AlwaysActivePause = true;
				MessageBox.Show(ErrorMessage + ex.Message, "Process Killer NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		/// <summary>
		/// Check for another running instance of PrKiller-NG
		/// </summary>
		/// <returns>`true` if another instance is running</returns>
		private bool CheckSecondInstance()
		{
			foreach (Process proc in Process.GetProcesses())
			{
				try
				{
					if (proc.Id == Process.GetCurrentProcess().Id) continue;
					if (proc.MainModule.FileName == Process.GetCurrentProcess().MainModule.FileName
					&& proc.GetProcessUser().Name == CurrentUserName)
					{
						MessageBox.Show(Killer.Language.ReadString("AnotherInstanceRunning", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return true;
					}
					if (proc.ProcessName == Process.GetCurrentProcess().ProcessName
					&& proc.GetProcessUser().Name == CurrentUserName)
					{
						MessageBox.Show(Killer.Language.ReadString("AnotherSimilarInstanceRunning", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

			if (Visible)
			{

				//correct window focus
				if (AlwaysActive && !AlwaysActivePause)
				{
					this.Activate();
					this.Focus();
				}
				try
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
						/*int PID = procs[i].Id;
						if (!Killer.ProcessCache.ContainsKey(PID)) Killer.ProcessCache.Add(PID, new ProcessInfo(procs[i]));
						ProcessList.Items.Add(Killer.ProcessCache[PID]);
						*/ //this breaks Suspend/Resume feature as processes have outdated status for unknown reason

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
				}
				catch (Exception ex) { this.Text = ex.Message; }

				//update memory statictics
				ulong RamAll = (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalVirtualMemory / 1024 / 1024 / 1024);
				ulong RamPhys = (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024);
				ulong RamAvail = (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1024 / 1024 / 1024);
				ulong RamPhysAvail = (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1024 / 1024);

				if (!Environment.Is64BitProcess)
				{
					RamAll = (ulong)Environment.SystemPageSize;
					RamAvail = (ulong)((GC.GetGCMemoryInfo().TotalAvailableMemoryBytes - (long)RamAll) / 1024 / 1024);
				}

				lblRamAll.Text = RamAll.ToString();
				lblRamPhys.Text = RamPhys.ToString();

				if (RamVirtShowUsed)
				{
					lblRam2.Text = (RamAll - RamAvail).ToString();
					toolTips.SetToolTip(lblRam2, Killer.Language.ReadString("lblRam2_Used", "Language"));
				}
				else
				{
					lblRam2.Text = RamAvail.ToString();
					toolTips.SetToolTip(lblRam2, Killer.Language.ReadString("lblRam2_Free", "Language"));
				}

				if (RamPhysShowUsed)
				{
					lblRamPhys2.Text = (RamPhys - RamPhysAvail).ToString();
					toolTips.SetToolTip(lblRamPhys2, Killer.Language.ReadString("lblRamPhys2_Used", "Language"));
				}
				else
				{
					lblRamPhys2.Text = RamPhysAvail.ToString();
					toolTips.SetToolTip(lblRamPhys2, Killer.Language.ReadString("lblRamPhys2_Free", "Language"));
				}
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

			e.Handled = true;
			e.SuppressKeyPress = true;

			CtrlPressed = e.Control;
			ShiftPressed = e.Shift;
			AltPressed = e.Alt;

			string key = (e.Modifiers + "+" + e.KeyCode).Replace(", ", "+").Replace("None+", "");
			string cmd = Killer.Config.Read(key);

			if (string.IsNullOrWhiteSpace(cmd))
			{
				Debug.Print("Unknown key: " + key);
				e.Handled = false;
				e.SuppressKeyPress = false;
				return;
			}

			if (key == "Left") return;

			if (Enum.TryParse(cmd, out Killer.KeyboardCommand Command))
			{
				switch (Command)
				{
					case Killer.KeyboardCommand.Hide:
						Hide();
						break;
					case Killer.KeyboardCommand.MoveUp:
						if (ProcessList.SelectedIndex > 0)
							ProcessList.SelectedIndex--;
						break;
					case Killer.KeyboardCommand.MoveDown:
						if (ProcessList.SelectedIndex < ProcessList.Items.Count - 1)
							ProcessList.SelectedIndex++;
						break;
					case Killer.KeyboardCommand.Kill:
						KillProcess();
						break;
					case Killer.KeyboardCommand.KillProcessTree:
						KillProcess(true);
						break;
					case Killer.KeyboardCommand.KillDontHide:
						KillProcess(false, false);
						break;
					case Killer.KeyboardCommand.KillProcessTreeDontHide:
						KillProcess(true, false);
						break;
					case Killer.KeyboardCommand.ProcessInfo:
						ProcessInfo();
						break;
					case Killer.KeyboardCommand.Exit:
						KillKiller();
						break;
					case Killer.KeyboardCommand.RunDialog:
						RunDialog();
						break;
					case Killer.KeyboardCommand.PriorityIncrease:
						SetProcessPriority(1);
						break;
					case Killer.KeyboardCommand.PriorityDecrease:
						SetProcessPriority(-1);
						break;
					case Killer.KeyboardCommand.PriorityIdle:
						SetProcessPriority(ProcessPriorityClass.Idle);
						break;
					case Killer.KeyboardCommand.PriorityNormal:
						SetProcessPriority(ProcessPriorityClass.Normal);
						break;
					case Killer.KeyboardCommand.PriorityHigh:
						SetProcessPriority(ProcessPriorityClass.High);
						break;
					case Killer.KeyboardCommand.PriorityRealTime:
						SetProcessPriority(ProcessPriorityClass.RealTime);
						break;
					case Killer.KeyboardCommand.ContextMenu:
						contextMenuStrip1.Show(ProcessList.Location);
						break;
					case Killer.KeyboardCommand.FindParent:
						FindParentProcess();
						break;
					case Killer.KeyboardCommand.Restart:
						RestartProcess();
						break;
					case Killer.KeyboardCommand.SuspendResumeProcess:
						SuspendResumeProcess();
						break;
					case Killer.KeyboardCommand.SuspendProcess:
						SuspendResumeProcess(true);
						break;
					case Killer.KeyboardCommand.ResumeProcess:
						SuspendResumeProcess(false);
						break;
					case Killer.KeyboardCommand.RestartExplorer:
						RestartWindowsShell();
						break;
					default:
						AlwaysActivePause = true;
						MessageBox.Show("Not implemented: " + Command, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						AlwaysActivePause = false;
						break;
				}
			}
			else
			{
				AlwaysActivePause = true;
				MessageBox.Show(Killer.Language.ReadString("BadKeyMapping", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				AlwaysActivePause = false;
			}
		}

		/// <summary>
		/// Suspend or resume the selected process
		/// </summary>
		private void SuspendResumeProcess(bool Suspend)
		{
			try
			{
				if (Suspend) ((ProcessInfo)ProcessList.SelectedItem).Proc.Suspend();
				else ((ProcessInfo)ProcessList.SelectedItem).Proc.Resume();
			}
			catch (Exception ex)
			{
				this.Text = ex.Message;
				PlayErrorSound();
			}
		}

		/// <summary>
		/// Suspend or resume the selected process, depending on its current state
		/// </summary>
		private void SuspendResumeProcess()
		{
			try
			{
				ProcessInfo SelProcSusRes = ((ProcessInfo)ProcessList.SelectedItem);
				if (SelProcSusRes.Suspended) SelProcSusRes.Proc.Resume();
				else SelProcSusRes.Proc.Suspend();
			}
			catch (Exception ex)
			{
				this.Text = ex.Message;
				PlayErrorSound();
			}
		}

		/// <summary>
		/// Restart the selected process
		/// </summary>
		private void RestartProcess()
		{
			try
			{
				ProcessInfo BaseProc = (ProcessInfo)ProcessList.SelectedItem;
				Process NewProc = new() { StartInfo = Killer.CreateProcessStartInfo(((ProcessInfo)ProcessList.SelectedItem).CommandLine) };
				NewProc.Start();
				BaseProc.Proc.Kill();
			}
			catch (Exception ex)
			{
				this.Text = ex.Message;
				PlayErrorSound();
			}
		}

		/// <summary>
		/// Find parent process for selected process
		/// </summary>
		private void FindParentProcess()
		{
			try
			{
				int ParentProcId = ((ProcessInfo)ProcessList.SelectedItem).Proc.GetParentProcess().Id;
				foreach (ProcessInfo proc in ProcessList.Items)
				{
					if (proc.Proc.Id == ParentProcId)
					{
						ProcessList.SelectedItem = proc;
						return;
					}
				}
				this.Text = Killer.Language.ReadString("ParentProcessNotFound", "Language");
				PlayErrorSound();
			}
			catch (Exception ex)
			{
				this.Text = ex.Message;
				PlayErrorSound();
			}
		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			RunDialog();
		}

		/// <summary>
		/// Kill the selected process
		/// </summary>
		/// <param name="tree">Kill entire process tree</param>
		/// <param name="hide">Override `Hide window after kill` setting</param>
		private void KillProcess(bool tree = false, bool? hide = null)
		{ KillProcess(ProcessList.SelectedItem as ProcessInfo, tree, hide); }

		/// <summary>
		/// Kill the specified process
		/// </summary>
		/// <param name="ProcToKill">The process to kill</param>
		/// <param name="tree">Kill entire process tree</param>
		/// <param name="hide">Override `Hide window after kill` setting</param>
		private void KillProcess(ProcessInfo ProcToKill, bool tree = false, bool? hide = null)
		{
			// Check for need of hide the window
			if (hide is null && Killer.Config.ReadBool(true, "HideAfterKill")) hide = true;
			if (hide is null) hide = false;
			if (CtrlPressed) hide = false;

			// Prepare variables
			bool kill = false;
			if (ProcToKill != null) kill = true;

			// Check for system process kill
			if (ProcToKill.Proc.IsCritical())
			{
				switch (killSystemProcAction)
				{
					case Killer.KillPolicy.Disable:
						kill = false;
						this.Text = Killer.Language.ReadString("KillSystemDisabled", "Language");
						break;
					case Killer.KillPolicy.Prompt:
						AlwaysActivePause = true;
						DialogResult quest = MessageBox.Show(Killer.Language.ReadString("KillSystemQuestion", "Language"), Killer.Language.ReadString("KillSystemTitle", "Language"), MessageBoxButtons.YesNo);
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
				DialogResult quest = MessageBox.Show(Killer.Language.ReadString("KillTreeQuestion", "Language"), Killer.Language.ReadString("KillTreeTitle", "Language"), MessageBoxButtons.YesNo);
				AlwaysActivePause = false;
				if (quest == DialogResult.No) return;
			}
			if (tree && killTreeAction == Killer.KillPolicy.Disable)
			{
				this.Text = Killer.Language.ReadString("KillTreeDisabled", "Language");
				return;
			}

			// Check for selfkill
			if (ProcToKill.ProcessId == Process.GetCurrentProcess().Id)
			{
				switch (selfkillAction)
				{
					case Killer.KillPolicy.Disable:
						kill = false;
						this.Text = Killer.Language.ReadString("SelfkillDisabled", "Language");
						break;
					case Killer.KillPolicy.Prompt:
						AlwaysActivePause = true;
						DialogResult quest = MessageBox.Show(Killer.Language.ReadString("SelfKillQuestion", "Language"), Killer.Language.ReadString("SelfKillTitle", "Language"), MessageBoxButtons.YesNo);
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
				if (kill) ProcToKill.Proc.Kill(tree);
				if (hide ?? false) this.Hide();
			}
			catch (Exception ex)
			{
				string KillErrMsg = string.Format(Killer.Language.ReadString("CannotKill", "Language"), ex.Message, ProcToKill.ProcessName, ProcToKill.ProcessId);
				this.Text = KillErrMsg;
				PlayErrorSound();
			}
		}

		/// <summary>
		/// Play sound that indicates error (and inform user to see to window title)
		/// </summary>
		private void PlayErrorSound()
		{
			switch (Sound)
			{
				case Killer.ErrorSound.Disable:
					break;
				case Killer.ErrorSound.Beep:
					System.Media.SystemSounds.Beep.Play();
					break;
				case Killer.ErrorSound.Asterisk:
					System.Media.SystemSounds.Asterisk.Play();
					break;
				case Killer.ErrorSound.Exclamination:
					System.Media.SystemSounds.Exclamation.Play();
					break;
				case Killer.ErrorSound.Hand:
					System.Media.SystemSounds.Hand.Play();
					break;
				case Killer.ErrorSound.Question:
					System.Media.SystemSounds.Question.Play();
					break;
				case Killer.ErrorSound.SpeakerBeep:
					Beep(500, 250);
					break;
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

		/// <summary>
		/// Set priority class of a running process
		/// </summary>
		/// <param name="pri">The priority class</param>
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

		/// <summary>
		/// Increase or decrease priority class of a running process
		/// </summary>
		/// <param name="change"><c>-1</c>, <c>0</c> or <c>+1</c></param>
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
								this.Text = Killer.Language.ReadString("MaximumPriority", "Language");
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
				string PriSetErrMsg = string.Format(Killer.Language.ReadString("CannotChangePriority", "Language"), ex.Message, selected.ProcessName, selected.ProcessId);
				this.Text = PriSetErrMsg;
				PlayErrorSound();
			}
		}

		/// <summary>
		/// Restart Windows shell by killing all its processes, then WinLogon should restart the shell
		/// </summary>
		private void RestartWindowsShell()
		{
			//Ask if need
			bool restart = false;
			switch (restartShellAction)
			{
				case Killer.KillPolicy.Disable:
					restart = false;
					this.Text = Killer.Language.ReadString("RestartShellDisabled", "Language");
					break;
				case Killer.KillPolicy.Prompt:
					AlwaysActivePause = true;
					DialogResult quest = MessageBox.Show(Killer.Language.ReadString("RestartShellQuestion", "Language"), Killer.Language.ReadString("RestartShellTitle", "Language"), MessageBoxButtons.YesNo);
					restart = (quest == DialogResult.Yes);
					AlwaysActivePause = false;
					break;
				case Killer.KillPolicy.Enable:
					restart = true;
					break;
			}
			if (restart == false) return;

			//Find Windows shell process image
			string WinShell = Killer.Config.Read(@"c:\windows\explorer.exe", "WindowsShell", null);
			if (Killer.Config.ReadBool(true, "AutomaticFindShell"))
			{
				//Automatically detect Windows shell
				RegistryKey HKLM = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon");
				RegistryKey HKCU = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon");
				object MachineShell = HKLM.GetValue("Shell");
				object UserShell = HKCU.GetValue("Shell");

				if (UserShell != null) WinShell = UserShell.ToString();
				else if (MachineShell != null) WinShell = MachineShell.ToString();
				else WinShell = "explorer.exe";
			}
			if (!WinShell.Contains("\\")) WinShell = @"c:\windows\" + WinShell;
			WinShell = WinShell.ToLowerInvariant();

			bool EnableTimer = Timer.Enabled;

			//Kill old shell before launching new copy
			Timer.Enabled = false;
			foreach (ProcessInfo procinf in ProcessList.Items)
			{
				try
				{
					if (procinf.Proc.MainModule.FileName.ToLowerInvariant().Replace("\"", "").StartsWith(WinShell)
					&& procinf.Proc.GetProcessUser().Name == CurrentUserName)
					{ KillProcess(procinf); }
				}
				catch { }
			}
			Timer.Enabled = true;

			//Wait 1 sec for WinLogon restarts the shell
			Timer_Tick(null, null);
			this.Text = WinShell;
			Application.UseWaitCursor = true;
			Application.DoEvents();
			System.Threading.Thread.Sleep(1000);
			Timer_Tick(null, null);
			Application.UseWaitCursor = false;

			//Check for does it got restarted
			bool Restarted = false;
			Timer.Enabled = false;
			foreach (ProcessInfo procinf in ProcessList.Items)
			{
				try
				{
					if (procinf.Proc.MainModule.FileName.ToLowerInvariant().Contains(WinShell)
					&& procinf.Proc.GetProcessUser().Name == CurrentUserName)
					{ Restarted = true; }
				}
				catch { }
			}
			Timer.Enabled = true;
			Timer_Tick(null, null);

			//Start shell process manually if WinLogon does not have restarted it
			if (!Restarted)
			{
				try
				{
					this.Text = WinShell;
					ProcessStartInfo psi = Killer.CreateProcessStartInfo(WinShell);
					Process.Start(psi);
				}
				catch (Exception ex)
				{ MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
			}

			Timer_Tick(null, null);
			Timer.Enabled = EnableTimer;
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

		private void cmdRestartExplorer_Click(object sender, EventArgs e)
		{
			RestartWindowsShell();
		}

		private void cmdConfigure_Click(object sender, EventArgs e)
		{
			AlwaysActivePause = true;
			new SettingsForm(this).ShowDialog();
			AlwaysActivePause = false;
		}
	}
}
