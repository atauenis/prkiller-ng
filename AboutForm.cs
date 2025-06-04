using System;
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
			this.Text = Killer.Language.Read("AboutTitle", "Language");
		}
	}
}
