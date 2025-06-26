using System;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class HotkeySelectDialog : Form
	{
		/// <summary>
		/// Keys, pressed by user. Don't forget to check (<c>DialogResult == DialogResult.OK</c>) before use!
		/// </summary>
		public Keys DetectedKey = Keys.None;
		/// <summary>
		/// Keys-modifiers, pressed by user. Don't forget to check (<c>DialogResult == DialogResult.OK</c>) before use!
		/// </summary>
		public Keys DetectedModifiers = Keys.None;

		/// <summary>
		/// Construct the hot key selection dialog box
		/// </summary>
		/// <param name="IntroMessage">Message in intro label</param>
		/// <param name="defaultModifiers">Default hotkey modifiers</param>
		/// <param name="defaultKey">Default hotkey button</param>
		public HotkeySelectDialog(string IntroMessage = null, Keys defaultModifiers = Keys.None, Keys defaultKey = Keys.None)
		{
			InitializeComponent();

			DetectedModifiers = defaultModifiers;
			DetectedKey = defaultKey;

			if (IntroMessage == null)
				lblHotKeyIntro.Text = Killer.Language.ReadString("lblHotKeyIntro", "Language");
			else
				lblHotKeyIntro.Text = IntroMessage;
		}

		private void HotkeySelectDialog_Load(object sender, EventArgs e)
		{
			this.Text = Killer.Language.ReadString("HotkeySelectDialog", "Language");
			cmdOk.Text = Killer.Language.ReadString("cmdOK", "Language");
			cmdCancel.Text = Killer.Language.ReadString("cmdCancel", "Language");

			DisplayDetectedKeys();
		}

		private void HotkeySelectDialog_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
			DetectedModifiers = e.Modifiers;
			DetectedKey = e.KeyCode;

			DisplayDetectedKeys();
		}

		/// <summary>
		/// Display detected keyboard hotkey
		/// </summary>
		private void DisplayDetectedKeys()
		{
			string hotkeyText = DetectedModifiers + " + " + DetectedKey;

			if (DetectedModifiers == Keys.None) hotkeyText = DetectedKey.ToString();

			if (DetectedKey == Keys.ControlKey) hotkeyText = DetectedKey.ToString();
			if (DetectedKey == Keys.Menu) hotkeyText = DetectedKey.ToString();
			if (DetectedKey == Keys.ShiftKey) hotkeyText = DetectedKey.ToString();

			if (DetectedModifiers == Keys.None && DetectedKey == Keys.None)
				hotkeyText = Killer.Language.ReadString("lblHotKeyDetected", "Language"); ;

			lblHotKeyDetected.Text = hotkeyText.Replace(", ", " + ");
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
