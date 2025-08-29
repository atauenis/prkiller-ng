using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace prkiller_ng
{
	/// <summary>
	/// Windows Service Host processes inspector
	/// </summary>
	class SvchostProcessInspector : ProcessInspector
	{
		public override bool Applicable(string ProcessName)
		{
			if (ProcessName.ToLowerInvariant() == "svchost") return true;
			else return false;
		}

		/// <summary>
		/// Inspect an svchost.exe process for details
		/// </summary>
		public override string Inspect(ProcessInfo PI)
		{
			string ServiceName;
			Match CmdLineRegex1 = Regex.Match(PI.CommandLine, @"-k (\w*)"); //Windows NT-style
			Match CmdLineRegex2 = Regex.Match(PI.CommandLine, @"-s (\w*)"); //Windows 10-style

			if (CmdLineRegex2.Success) ServiceName = CmdLineRegex2.Groups[1].Value;
			else if (CmdLineRegex1.Success) ServiceName = CmdLineRegex1.Groups[1].Value;
			else throw new Exception("Cannot detect service name.");

			RegistryKey ServicesKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services");
			RegistryKey ServiceKey = ServicesKey.OpenSubKey(ServiceName);
			if (ServiceKey == null)
			{
				//probably a service group or an unknown service
				RegistryKey SvchostGroupsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Svchost");
				if (SvchostGroupsKey.GetValue(ServiceName) != null)
					return GetServiceGroupInfo(ServiceName);

				throw new Exception("Unknown service: " + ServiceName);
			}

			//a regular service
			ServiceInfo svc = GetServiceInfo(ServiceName);
			if (svc.IsGroup)
			{
				if (svc.IsGroupWithDefaultService)
					return string.Format("Service: {0}, \"{1}\". Also: {2}", svc.DisplayName, svc.ServiceDll, GetServiceGroupInfo(ServiceName));
				else
					return string.Format("Service: {0}, \"{1}\". Also: {2}", svc.DisplayName, svc.ImagePath, GetServiceGroupInfo(ServiceName));
			}
			return string.Format("Service: {0}, \"{1}\", \"{2}\".", svc.DisplayName, svc.ServiceDll, svc.ImagePath);
		}

		private string GetServiceGroupInfo(string GroupName)
		{
			RegistryKey SvchostGroupsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Svchost");
			object GroupContent = SvchostGroupsKey.GetValue(GroupName);
			if (GroupContent is null) throw new Exception(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Svchost\" + GroupName + " is unregistered");
			if (GroupContent is not string[]) throw new Exception("Error in " + @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Svchost\" + GroupName);

			string ret = "Service group: ";
			bool firstSvc = true;
			foreach (string Service in GroupContent as string[])
			{
				if (Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\").OpenSubKey(Service) != null)
				{
					if (!firstSvc) ret += ", ";
					firstSvc = false;
					ServiceInfo svc = GetServiceInfo(Service);
					string filepath = Path.GetFullPath(svc.ImagePath);
					string filename = Path.GetFileName(filepath);
					ret += svc.DisplayName;
				}
				//else ret += "[Removed: " + Service + "], ";
			};
			return ret;
		}

		/// <summary>
		/// Windows service info
		/// </summary>
		private struct ServiceInfo
		{
			/// <summary>
			/// Name of the service in Windows registry
			/// </summary>
			public string ServiceName;
			/// <summary>
			/// Human-readable name of the service
			/// </summary>
			public string DisplayName;
			/// <summary>
			/// Service's EXE file
			/// </summary>
			public string ImagePath;
			/// <summary>
			/// Service's DLL file
			/// </summary>
			public string ServiceDll;
			/// <summary>
			/// Is the service a service group
			/// </summary>
			public bool IsGroup;
			/// <summary>
			/// Is the service a service group with an own EXE or DLL too
			/// </summary>
			public bool IsGroupWithDefaultService;
		}

		private ServiceInfo GetServiceInfo(string ServiceName)
		{
			ServiceInfo svc = new();
			RegistryKey ServiceKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\").OpenSubKey(ServiceName);

			svc.ServiceName = ServiceName;
			svc.DisplayName = ServiceKey.GetValue("DisplayName", "").ToString();
			svc.ImagePath = ServiceKey.GetValue("ImagePath", "").ToString();
			svc.ServiceDll = ServiceKey.GetValue("ServiceDll", "").ToString();

			RegistryKey ServiceParamsKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\" + ServiceName + @"\Parameters");
			if (ServiceParamsKey != null)
			{
				svc.ServiceDll = ServiceParamsKey.GetValue("ServiceDll", "").ToString();
			}

			Match DllResourceMask = Regex.Match(svc.DisplayName, @"@(.*),-([0-9]*)");
			if (DllResourceMask.Success)
			{
				string dllname = DllResourceMask.Groups[1].Value;
				int resourceid = int.Parse(DllResourceMask.Groups[2].Value);
				svc.DisplayName = Killer.ExtractStringFromDLL(dllname, resourceid);
			}

			if (svc.ImagePath.ToLowerInvariant().Contains(@"\system32\svchost.exe -k")) svc.IsGroup = true;
			if (svc.IsGroup)
			{
				//fix for Win10+ pseudo groups like "svchost.exe -k netsvcs"
				RegistryKey SvchostGroupsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Svchost");
				if (SvchostGroupsKey.GetValue(ServiceName) == null) svc.IsGroup = false;
			}
			if (svc.IsGroup && svc.ServiceDll != null) svc.IsGroupWithDefaultService = true;

			if (svc.DisplayName == "")
				svc.DisplayName = svc.ServiceName;
			return svc;
		}
	}
}
