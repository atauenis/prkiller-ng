using System;
using System.Diagnostics;
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
			lblVersion.Text += Application.ProductVersion + " Beta 2";
			lblLanguage.Text = Killer.Language.ReadString("Language", "Language");
			cmdOk.Text = Killer.Language.ReadString("cmdOK", "Language");
			this.Text = Killer.Language.ReadString("AboutTitle", "Language");

			if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess) lblVersion.Text += " [WOW64]";
			if (!Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess) lblVersion.Text += " [32]";
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
