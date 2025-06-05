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
			lblVersion.Text += Application.ProductVersion;
			lblLanguage.Text = Killer.Language.Read("Language", "Language");
			cmdOk.Text = Killer.Language.Read("cmdOK", "Language");
			this.Text = Killer.Language.Read("AboutTitle", "Language");
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
