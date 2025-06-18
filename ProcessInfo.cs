using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace prkiller_ng
{
	/// <summary>
	/// Information about running tasks (processes).
	/// </summary>
	class ProcessInfo
	{
		private string WinApiProcName = "";

		/// <summary>
		/// Deep process information.
		/// </summary>
		public Process Proc;

		/// <summary>
		/// Gets process identificator.
		/// </summary>
		public int ProcessId { get; private set; }
		/// <summary>
		/// Gets process visible name.
		/// </summary>
		public string ProcessName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(WinApiProcName)) return WinApiProcName;
				if (Proc.ProcessName == "System" || Proc.ProcessName == "Idle") return Proc.ProcessName;
				return Proc.ProcessName + ".exe";
			}
		}
		/// <summary>
		/// Gets process priority category.
		/// </summary>
		public ProcessPriorityClass ProcessPriority { get { return Proc.PriorityClass; } }

		/// <summary>
		/// Gets does the process is suspended
		/// </summary>
		public bool Suspended { get { return Proc.Threads[0].ThreadState == ThreadState.Wait && Proc.Threads[0].WaitReason == ThreadWaitReason.Suspended; } }

		/// <summary>
		/// Gets process command line
		/// </summary>
		public string CommandLine
		{
			get
			{
				using (ManagementObjectSearcher mos = new("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + Proc.Id))
				{
					foreach (ManagementObject mo in mos.Get())
					{
						return (string)mo["CommandLine"];
					}
				}
				return "";
			}
		}

		/// <summary>
		/// Construct this class.
		/// </summary>
		/// <param name="proc">Target process.</param>
		public ProcessInfo(Process proc)
		{
			Proc = proc;
			ProcessId = Proc.Id;
		}

		/// <summary>
		/// Gets user-friendly process name wih its run status
		/// </summary>
		public override string ToString()
		{
			string str = "";
			try
			{
				WinApiProcName = Proc.GetProcessImageFileName();
				if (!string.IsNullOrWhiteSpace(WinApiProcName))
					WinApiProcName = WinApiProcName.Substring(WinApiProcName.LastIndexOf("\\") + 1);
			}
			catch { }

			if (!Suspended && !Proc.Responding) str += "<!> ";
			if (Suspended) str += "<s> ";
			str += ProcessName;
			return str;
		}

		/// <summary>
		/// Show a dialog window with detailed information about this process.
		/// </summary>
		public void ShowInfoDialog()
		{
			Application.UseWaitCursor = true;
			ProcessInfoDialog wnd = new(this);
			wnd.Show();

			try { wnd.Text = Proc.MainWindowTitle; }
			catch { wnd.Text = Proc.ProcessName; }
			if (string.IsNullOrWhiteSpace(wnd.Text)) wnd.Text = Proc.ProcessName + Killer.Language.Read("NoWindows", "Language");

			try { wnd.txtDescription.Text = Proc.MainModule.FileVersionInfo.ProductName; }
			catch { wnd.txtDescription.Text = "?"; }

			try { wnd.txtDescriptionCompany.Text = Proc.MainModule.FileVersionInfo.CompanyName; }
			catch { wnd.txtDescriptionCompany.Text = "?"; }

			try { wnd.txtDescriptionCopyright.Text = Proc.MainModule.FileVersionInfo.LegalCopyright; }
			catch { wnd.txtDescriptionCopyright.Text = "?"; }

			try { wnd.txtDescriptionVersion.Text = Proc.MainModule.FileVersionInfo.FileVersion; }
			catch { wnd.txtDescriptionVersion.Text = "?"; }

			try
			{
				wnd.txtProcessExtraInfo.Text = "ID=" + Proc.Id;
				if (Proc.IsUnderWow64()) wnd.txtProcessExtraInfo.Text += ", 32-bit";
				//wnd.txtProcessExtraInfo.Text += " :)";
				wnd.txtProcessExtraInfo.Text += " PARENT ID=" + Proc.GetParentProcess().Id;
			}
			catch { }

			try { wnd.txtProcImageName.Text = Proc.MainModule.FileName; }
			catch { wnd.txtProcImageName.Text = Proc.ProcessName + "\tPROCESS"; }

			try { wnd.txtProcWorkingDir.Text = Proc.StartInfo.WorkingDirectory; }
			catch { wnd.txtProcWorkingDir.Text = "?"; }

			wnd.txtProcCmdLine.Text = CommandLine;

			Application.UseWaitCursor = false;
		}
	}
}
