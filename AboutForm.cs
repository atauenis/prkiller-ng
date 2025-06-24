﻿using System;
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
			lblVersion.Text += Application.ProductVersion + " Beta";
			lblLanguage.Text = Killer.Language.Read("Language", "Language");
			cmdOk.Text = Killer.Language.Read("cmdOK", "Language");
			this.Text = Killer.Language.Read("AboutTitle", "Language");

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
