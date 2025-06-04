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
			lblFileDesc.Text = Killer.Language.Read("lblFileDesc", "Language");
			lblFileCompany.Text = Killer.Language.Read("lblFileCompany", "Language");
			lblFileCopyright.Text = Killer.Language.Read("lblFileCopyright", "Language");
			lblFileVersion.Text = Killer.Language.Read("lblFileVersion", "Language");
			lblProcExtra.Text = Killer.Language.Read("lblProcExtra", "Language");
			lblProcImage.Text = Killer.Language.Read("lblProcImage", "Language");
			lblProcDir.Text = Killer.Language.Read("lblProcDir", "Language");
			lblProcCmd.Text = Killer.Language.Read("lblProcCmd", "Language");
		}
	}
}
