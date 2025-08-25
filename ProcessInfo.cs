using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
				switch (Proc.ProcessName)
				{
					case "System":
					case "Idle":
					case "Registry":
					case "Memory Compression":
					case "Secure System":
						return Proc.ProcessName;
					default:
						return Proc.ProcessName + ".exe";
				}
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
				return Proc.GetCommandLine();
			}
		}

		/// <summary>
		/// Does the current Process Killer NG instance have enough access rights to this process.
		/// </summary>
		public bool Accessible { get; private set; }

		/// <summary>
		/// Total usage of CPU by the process in percents. 100% means full load of all cores and HyperThreading threads.
		/// </summary>
		public double ProcessorLoad { get; set; }

		/// <summary>
		/// Calculate CPU usage by process. This function should be called only in strict interval of <paramref name="TimerInterval"/> milliseconds.
		/// </summary>
		/// <param name="TimerInterval">Interval of this function callings.</param>
		public void CalculateCpuLoad(int TimerInterval)
		{
			TimeSpan OldProcessorTime = Proc.TotalProcessorTime;
			if (Killer.LastCpuTime.ContainsKey(ProcessId))
				OldProcessorTime = Killer.LastCpuTime[ProcessId];
			TimeSpan NewProcessorTime = Proc.TotalProcessorTime;
			Killer.LastCpuTime[ProcessId] = NewProcessorTime;

			double IterationProcessorTimeMilliseconds = NewProcessorTime.TotalMilliseconds - OldProcessorTime.TotalMilliseconds;

			ProcessorLoad = ((IterationProcessorTimeMilliseconds / TimerInterval) * 100) / Environment.ProcessorCount;
		}

		/// <summary>
		/// Construct this class.
		/// </summary>
		/// <param name="proc">Target process.</param>
		public ProcessInfo(Process proc)
		{
			Proc = proc;
			ProcessId = Proc.Id;

			try
			{
				Accessible = true;
				WinApiProcName = Proc.GetProcessImageFileName();
				if (!string.IsNullOrWhiteSpace(WinApiProcName))
					WinApiProcName = WinApiProcName.Substring(WinApiProcName.LastIndexOf("\\") + 1);
				else
					Accessible = false;
			}
			catch { }
		}

		/// <summary>
		/// Gets user-friendly process name wih its run status
		/// </summary>
		public override string ToString()
		{
			string str = "";
			if (!Suspended && !Proc.Responding) str += "<!> ";
			if (Suspended) str += "<s> ";
			if (Accessible && ProcessorLoad > 50) str += "<*> ";
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
			if (string.IsNullOrWhiteSpace(wnd.Text)) wnd.Text = Proc.ProcessName + " " + Killer.Language.ReadString("NoWindows", "Language");

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
				var test32on64 = Proc.MainModule.FileName;
			}
			catch (System.ComponentModel.Win32Exception)
			{
				if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
					wnd.txtProcessExtraInfo.Text += ", 64-bit";
			}
			catch { }

			try
			{
				wnd.txtProcessExtraInfo.Text += "\t PARENT ID=" + Proc.GetParentProcess().Id;
				if (Proc.GetParentProcess().IsUnderWow64()) wnd.txtProcessExtraInfo.Text += ", 32-bit";
				//var test32on64 = Proc.GetParentProcess().MainModule.FileName;
			}
			catch { }

			try
			{
				wnd.txtProcessExtraInfo.Text += "\t USER=" + Proc.GetProcessUser().Name;
			}
			catch { }

			try { wnd.txtProcImageName.Text = Proc.MainModule.FileName; }
			catch { wnd.txtProcImageName.Text = Proc.ProcessName + "\t(PROCESS)"; }

			try { wnd.txtProcWorkingDir.Text = Proc.GetCurrentDirectory(); }
			catch { wnd.txtProcWorkingDir.Text = "?"; }

			try
			{ wnd.txtProcCmdLine.Text = Proc.GetCommandLine(); }
			catch { wnd.txtProcCmdLine.Text = "?"; }

			try { wnd.pbxIcon.Image = ExtractIconFromFile(Proc.MainModule.FileName, true).ToBitmap(); }
			catch (NullReferenceException)
			{
				// EXE without icon
			}
			catch (System.ComponentModel.Win32Exception)
			{
				// not accessible icon
			}
			catch
			{ }

			try
			{
				X509Certificate cert = X509Certificate.CreateFromSignedFile(Proc.MainModule.FileName);
				X500DistinguishedName certname = new(cert.Subject);
				wnd.txtDescriptionSignature.Text = certname.Format(false);
			}
			catch (Exception e)
			{
				wnd.txtDescriptionSignature.Text = e.Message;
			}

			Application.UseWaitCursor = false;

			//todo: other way, useful under wow64 with 64bit procs, maybe other too - https://stackoverflow.com/a/26234529
		}


		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern uint ExtractIconEx
	(string szFileName, int nIconIndex,
	IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

		[DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
		private static extern int DestroyIcon(IntPtr hIcon);

		/// <summary>
		/// Extract the icon from file.
		/// </summary>
		/// <param name="fileAndParam">The params string, such as ex: 
		///    "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
		/// <param name="isLarge">Determines the returned icon is a large 
		///    (may be 32x32 px) or small icon (16x16 px).</param>
		public static Icon ExtractIconFromFile(string fileAndParam, bool isLarge)
		{
			uint readIconCount = 0;
			IntPtr[] hDummy = new IntPtr[1] { IntPtr.Zero };
			IntPtr[] hIconEx = new IntPtr[1] { IntPtr.Zero };

			try
			{
				EmbeddedIconInfo embeddedIcon =
					getEmbeddedIconInfo(fileAndParam);

				if (isLarge)
					readIconCount = ExtractIconEx
						(embeddedIcon.FileName, embeddedIcon.IconIndex, hIconEx, hDummy, 1);
				else
					readIconCount = ExtractIconEx
						(embeddedIcon.FileName, embeddedIcon.IconIndex, hDummy, hIconEx, 1);

				if (readIconCount > 0 && hIconEx[0] != IntPtr.Zero)
				{
					//Get first icon.
					Icon extractedIcon =
						(Icon)Icon.FromHandle(hIconEx[0]).Clone();

					return extractedIcon;
				}
				else //No icon read.
					return null;
			}
			catch (Exception exc)
			{
				//Extract icon error.
				throw new ApplicationException
					("Could not extract icon", exc);
			}
			finally
			{
				//Release resources.
				foreach (IntPtr ptr in hIconEx)
					if (ptr != IntPtr.Zero)
						DestroyIcon(ptr);

				foreach (IntPtr ptr in hDummy)
					if (ptr != IntPtr.Zero)
						DestroyIcon(ptr);
			}
		}

		/// <summary>
		/// Structure that encapsulates basic information of icon embedded in a file.
		/// </summary>
		public struct EmbeddedIconInfo
		{
			public string FileName;
			public int IconIndex;
		}

		/// <summary>
		/// Parses the parameters string to the structure of EmbeddedIconInfo.
		/// </summary>
		/// <param name="fileAndParam">The params string, such as ex: 
		///    "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
		public static EmbeddedIconInfo getEmbeddedIconInfo(string fileAndParam)
		{
			EmbeddedIconInfo embeddedIcon = new EmbeddedIconInfo();

			if (String.IsNullOrEmpty(fileAndParam))
				return embeddedIcon;

			//Use to store the file contains icon.
			string fileName = String.Empty;

			//The index of the icon in the file.
			int iconIndex = 0;
			string iconIndexString = String.Empty;

			int commaIndex = fileAndParam.IndexOf(",", StringComparison.Ordinal);
			//if fileAndParam is some thing likes this: 
			//"C:\\Program Files\\NetMeeting\\conf.exe,1".
			if (commaIndex > 0)
			{
				fileName = fileAndParam.Substring(0, commaIndex);
				iconIndexString = fileAndParam.Substring(commaIndex + 1);
			}
			else
				fileName = fileAndParam;

			if (!String.IsNullOrEmpty(iconIndexString))
			{
				//Get the index of icon.
				iconIndex = int.Parse(iconIndexString);
				/*if (iconIndex < 0)
					iconIndex = 0;  //To avoid the invalid index.
				 * may cause unexpeced benaviour
				 */
			}

			embeddedIcon.FileName = fileName;
			embeddedIcon.IconIndex = iconIndex;

			return embeddedIcon;
		}
	}

}
