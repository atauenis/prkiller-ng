using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			using (Font fontTester = new Font(
			"Playbill",
			16,
			FontStyle.Regular,
			GraphicsUnit.Pixel))
			{
				if (fontTester.Name == "Playbill")
				{
					lblVersion.Font = new("Playbill", 16);
					lblVersion.Text = "Process Killer NG ";
				}
				else
				{
					lblVersion.Text = "PrKiller-NG ";
				}
			}

			lblVersion.Text += Application.ProductVersion;
			lblOpenSource.Text = Killer.Language.ReadString("AboutOpenSourceLicense", "Language");
			cmdOk.Text = Killer.Language.ReadString("cmdOK", "Language");
			this.Text = Killer.Language.ReadString("AboutTitle", "Language");

			if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess) lblVersion.Text += " [WOW64]";
			if (!Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess) lblVersion.Text += " [32]";

			TopMost = Killer.Config.ReadBool(true, "AlwaysOnTop");
		}

		private void lnkUrl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("explorer", lnkUrl1.Text);
		}

		private void lnkUrl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("explorer", lnkUrl2.Text);
		}
	}
}
