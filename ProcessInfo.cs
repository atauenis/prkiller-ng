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
		public string ProcessName { get { return Proc.ProcessName; } }
		/// <summary>
		/// Gets process priority category.
		/// </summary>
		public ProcessPriorityClass ProcessPriority { get { return Proc.PriorityClass; } }

		/// <summary>
		/// Construct this class.
		/// </summary>
		/// <param name="proc">Target process.</param>
		public ProcessInfo(Process proc)
		{
			Proc = proc;
			ProcessId = Proc.Id;
		}

		public override string ToString()
		{
			string str = "";
			if (!Proc.Responding) str += "<!> ";
			str += ProcessName + ".exe"; //Proc.MainModule.ModuleName is too slow
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
			}
			catch { }

			try { wnd.txtProcImageName.Text = Proc.MainModule.FileName; }
			catch { wnd.txtProcImageName.Text = Proc.ProcessName + "\tPROCESS"; }

			try { wnd.txtProcWorkingDir.Text = Proc.StartInfo.WorkingDirectory; }
			catch { wnd.txtProcWorkingDir.Text = "?"; }

			using (ManagementObjectSearcher mos = new("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + Proc.Id))
			{
				foreach (ManagementObject mo in mos.Get())
				{
					wnd.txtProcCmdLine.Text = (string)mo["CommandLine"];
				}
			}

			Application.UseWaitCursor = false;
		}
	}
}
