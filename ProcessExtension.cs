using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

//Code source from 
//http://stackoverflow.com/questions/71257/suspend-process-in-c-sharp

public static class ProcessExtension
{
	[DllImport("kernel32.dll")]
	static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
	[DllImport("kernel32.dll")]
	static extern uint SuspendThread(IntPtr hThread);
	[DllImport("kernel32.dll")]
	static extern int ResumeThread(IntPtr hThread);
	[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
	static extern bool CloseHandle(IntPtr handle);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, bool bInheritHandle, Int32 dwProcessId);
	[DllImport("Psapi.dll")]
	public static extern Int32 GetProcessImageFileNameW(IntPtr hProcess, byte[] lpImageFileName, Int32 nSize);
	[DllImport("kernel32.dll")]
	public static extern bool QueryFullProcessImageNameW(IntPtr hProcess, uint dwFlags, byte[] lpExeName, ref uint lpdwSize);
	[DllImport("ntdll.dll")]
	private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);


	public static void Suspend(this Process process)
	{
		foreach (ProcessThread thread in process.Threads)
		{
			var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
			if (pOpenThread == IntPtr.Zero)
			{
				break;
			}
			SuspendThread(pOpenThread);
			//CloseHandle(pOpenThread);
		}
	}
	public static void Resume(this Process process)
	{
		foreach (ProcessThread thread in process.Threads)
		{
			var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
			if (pOpenThread == IntPtr.Zero)
			{
				break;
			}
			ResumeThread(pOpenThread);
			//CloseHandle(pOpenThread);
		}
	}
	public static void Print(this Process process)
	{
		Console.WriteLine("{0,8}    {1}", process.Id, process.ProcessName);
	}

	public static IntPtr ApiOpenProcess(this Process process, ProcessAccess access)
	{
		return OpenProcess((uint)access, false, process.Id);
	}

	/// <summary>
	/// Gets the parent process of a specified process.
	/// </summary>
	/// <param name="process">The specified process.</param>
	/// <returns>An instance of the Process class.</returns>
	public static Process GetParentProcess(this Process process)
	{
		ParentProcessUtilities pbi = new();
		int returnLength;
		int status = NtQueryInformationProcess(process.Handle, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
		if (status != 0)
			throw new Exception("NT Status " + status);
		try
		{
			return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
		}
		catch (ArgumentException)
		{
			// not found
			return null;
		}
	}

	public static string GetProcessImageFileName(this Process process)
	{
		// WinAPI call is faster than process.MainModule.ModuleName

		IntPtr handle = OpenProcess((uint)ProcessAccess.PROCESS_QUERY_LIMITED_INFORMATION, false, process.Id);
		byte[] buff = new byte[256];
		uint bufflen = 256;
		//var x = GetProcessImageFileNameW(handle, buff, buff.Length);
		bool success = QueryFullProcessImageNameW(handle, 0, buff, ref bufflen);
		CloseHandle(handle);
		if (success == false) return "";
		if ((int)bufflen * 2 < 256)
			return System.Text.Encoding.Unicode.GetString(buff, 0, (int)bufflen * 2);
		else return "";
	}
}

[Flags]
public enum ThreadAccess : int
{
	TERMINATE = (0x0001),
	SUSPEND_RESUME = (0x0002),
	GET_CONTEXT = (0x0008),
	SET_CONTEXT = (0x0010),
	SET_INFORMATION = (0x0020),
	QUERY_INFORMATION = (0x0040),
	SET_THREAD_TOKEN = (0x0080),
	IMPERSONATE = (0x0100),
	DIRECT_IMPERSONATION = (0x0200)
}

[Flags]
public enum ProcessAccess : uint
{
	//https://learn.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
	//https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess
	//https://www.cyberforum.ru/csharp-beginners/thread1544560.html

	/*PROCESS_ALL_ACCESS (STANDARD_RIGHTS_REQUIRED (0x000F0000L) | SYNCHRONIZE (0x00100000L) | 0xFFFF)	All possible access rights for a process object.Windows Server 2003 and Windows XP: The size of the PROCESS_ALL_ACCESS flag increased on Windows Server 2008 and Windows Vista. If an application compiled for Windows Server 2008 and Windows Vista is run on Windows Server 2003 or Windows XP, the PROCESS_ALL_ACCESS flag is too large and the function specifying this flag fails with ERROR_ACCESS_DENIED. To avoid this problem, specify the minimum set of access rights required for the operation. If PROCESS_ALL_ACCESS must be used, set _WIN32_WINNT to the minimum operating system targeted by your application (for example, #define _WIN32_WINNT _WIN32_WINNT_WINXP). For more information, see Using the Windows Headers.
	PROCESS_CREATE_PROCESS (0x0080)	Required to use this process as the parent process with PROC_THREAD_ATTRIBUTE_PARENT_PROCESS.
	PROCESS_CREATE_THREAD (0x0002)	Required to create a thread in the process.
	PROCESS_DUP_HANDLE (0x0040)	Required to duplicate a handle using DuplicateHandle.
	PROCESS_QUERY_INFORMATION (0x0400)	Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
	PROCESS_QUERY_LIMITED_INFORMATION (0x1000)	Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.Windows Server 2003 and Windows XP: This access right is not supported.
	PROCESS_SET_INFORMATION (0x0200)	Required to set certain information about a process, such as its priority class (see SetPriorityClass).
	PROCESS_SET_QUOTA (0x0100)	Required to set memory limits using SetProcessWorkingSetSize.
	PROCESS_SUSPEND_RESUME (0x0800)	Required to suspend or resume a process.
	PROCESS_TERMINATE (0x0001)	Required to terminate a process using TerminateProcess.
	PROCESS_VM_OPERATION (0x0008)	Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
	PROCESS_VM_READ (0x0010)	Required to read memory in a process using ReadProcessMemory.
	PROCESS_VM_WRITE (0x0020)	Required to write to memory in a process using WriteProcessMemory.
	SYNCHRONIZE (0x00100000L)	Required to wait for the process to terminate using the wait functions.*/

	PROCESS_CREATE_PROCESS = (0x0080),
	PROCESS_CREATE_THREAD = (0x0002),
	PROCESS_DUP_HANDLE = (0x0040),
	PROCESS_QUERY_INFORMATION = (0x0400),
	PROCESS_QUERY_LIMITED_INFORMATION = (0x1000),
	PROCESS_SET_INFORMATION = (0x0200),
	PROCESS_SET_QUOTA = (0x0100),
	PROCESS_SUSPEND_RESUME = (0x0800),
	PROCESS_TERMINATE = (0x0001),
	PROCESS_VM_OPERATION = (0x0008),
	PROCESS_VM_READ = (0x0010),
	PROCESS_VM_WRITE = (0x0020),
	SYNCHRONIZE = (0x00100000)
}



/// <summary>
/// A utility class to determine a process parent.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ParentProcessUtilities
{
	// These members must match PROCESS_BASIC_INFORMATION
	internal IntPtr Reserved1;
	internal IntPtr PebBaseAddress;
	internal IntPtr Reserved2_0;
	internal IntPtr Reserved2_1;
	internal IntPtr UniqueProcessId;
	internal IntPtr InheritedFromUniqueProcessId;

	//https://stackoverflow.com/a/3346055
}