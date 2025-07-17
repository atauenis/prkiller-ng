using System;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class ProcessInfoDialog : Form
	{
		internal ProcessInfo Proc;
		internal ProcessInfoDialog(ProcessInfo PI)
		{
			InitializeComponent();
			Proc = PI;
		}

		private void ProcessInfoDialog_Load(object sender, EventArgs e)
		{
			lblFileDesc.Text = Killer.Language.ReadString("lblFileDesc", "Language");
			lblFileCompany.Text = Killer.Language.ReadString("lblFileCompany", "Language");
			lblFileCopyright.Text = Killer.Language.ReadString("lblFileCopyright", "Language");
			lblFileVersion.Text = Killer.Language.ReadString("lblFileVersion", "Language");
			lblProcExtra.Text = Killer.Language.ReadString("lblProcExtra", "Language");
			lblProcImage.Text = Killer.Language.ReadString("lblProcImage", "Language");
			lblProcDir.Text = Killer.Language.ReadString("lblProcDir", "Language");
			lblProcCmd.Text = Killer.Language.ReadString("lblProcCmd", "Language");

			TopMost = Killer.Config.ReadBool(true, "AlwaysOnTop");
		}

		private void ProcessInfoDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) Close();
		}
	}
}
