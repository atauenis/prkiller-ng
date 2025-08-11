using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class RunDialog : Form
	{
		int HistoryCount = 0;

		public RunDialog()
		{
			InitializeComponent();
		}

		private void RunDialog_Load(object sender, EventArgs e)
		{
			this.Text = Killer.Language.ReadString("RunTitle", "Language");
			lblIntro.Text = Killer.Language.ReadString("RunIntro", "Language");
			lblOpen.Text = Killer.Language.ReadString("RunLabel", "Language");
			cmdOk.Text = Killer.Language.Read("cmdOK", "Language");
			cmdCancel.Text = Killer.Language.Read("cmdCancel", "Language");
			cmdBrowse.Text = Killer.Language.Read("cmdBrowse", "Language");

			string PathExt = Environment.GetEnvironmentVariable("PATHEXT").Replace(".", "*.");
			openFileDialog1.Filter = Killer.Language.ReadString("RunFilter", "Language").Replace("%PATHEXT%", PathExt);
			openFileDialog1.FileName = "";

			while (true)
			{
				string HistoryEntry = Killer.Config.ReadString(HistoryCount.ToString(), "Run");
				if (string.IsNullOrWhiteSpace(HistoryEntry)) break;

				cbxOpen.Items.Add(HistoryEntry);
				HistoryCount++;
			}

			TopMost = Killer.Config.ReadBool(true, "AlwaysOnTop");
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			try
			{
				ProcessStartInfo psi = Killer.CreateProcessStartInfo(cbxOpen.Text);
				Process.Start(psi);

				try
				{
					if (ModifierKeys == Keys.None)
					{
						foreach (string HistoryEntry in cbxOpen.Items)
						{
							if (HistoryEntry == cbxOpen.Text)
							{
								this.Close();
								return;
							}
						}
						Killer.Config.Write(HistoryCount.ToString(), cbxOpen.Text, "Run");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "prkiller-ng.ini, [Run], №=Command", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}

				this.Close();
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				cbxOpen.Text = openFileDialog1.FileName;
			}
		}
	}
}
