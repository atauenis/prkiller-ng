using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace prkiller_ng
{
	public partial class HotkeysEditorForm : Form
	{
		List<string> RegisteredHotkeys = new();
		List<string> RemovingHotkeys = new();

		public HotkeysEditorForm()
		{
			InitializeComponent();
		}

		private void HotkeysEditorForm_Load(object sender, EventArgs e)
		{
			// localize
			this.Text = Killer.Language.ReadString("HotkeysEditorForm", "Language");
			cmdOk.Text = Killer.Language.ReadString("cmdOK", "Language");
			cmdCancel.Text = Killer.Language.ReadString("cmdCancel", "Language");
			lstHotkeys.Columns[0].Text = Killer.Language.ReadString("HotkeysEditorColumn0", "Language");
			lstHotkeys.Columns[1].Text = Killer.Language.ReadString("HotkeysEditorColumn1", "Language");
			tsbAddKey.Text = Killer.Language.ReadString("HotkeysEditorToolbarAdd", "Language");
			tsbAction.ToolTipText = Killer.Language.ReadString("HotkeysEditorToolbarAction", "Language");
			tsbRemove.Text = Killer.Language.ReadString("HotkeysEditorToolbarRemove", "Language");

			// add all available keyboard commands
			foreach (string x in Enum.GetNames(typeof(Killer.KeyboardCommand)))
			{
				tsbAction.Items.Add(x);
			}

			// parse initialization file
			string[] IniLines = System.IO.File.ReadAllLines(Killer.Config.IniPath);
			bool IniInSection = false;
			lstHotkeys.BeginUpdate();
			foreach (string Line in IniLines)
			{
				string NiceLine = Line.Trim();
				if (IniInSection)
				{
					// in right section
					if (NiceLine.StartsWith("[")) break; // end of section

					string[] LineParts = NiceLine.Split("=");
					if (LineParts.Length != 2) continue; // not a option

					if (NiceLine.Contains('+')) // will test only first key of combination
					{ NiceLine = LineParts[0].Substring(0, LineParts[0].IndexOf("+")); }
					else
					{ NiceLine = LineParts[0]; }

					if (!Enum.TryParse(typeof(Keys), NiceLine, out _))
					{ continue; }

					if (LineParts[0] == "Left")
					{ continue; }

					// add line
					ListViewItem lvi = new(LineParts[0]);
					lvi.SubItems.Add(LineParts[1]);
					lstHotkeys.Items.Add(lvi);
					RegisteredHotkeys.Add(LineParts[0]);
				}
				else
				{
					// not in right section or at its start
					if (NiceLine.StartsWith("[prkiller-ng]")) IniInSection = true; // start of right section
				}
			}
			lstHotkeys.EndUpdate();
			tsbRemove.Enabled = lstHotkeys.SelectedItems.Count > 0;
		}

		private void tsbAddKey_Click(object sender, EventArgs e)
		{
			// ADD HOTKEY button click

			HotkeySelectDialog hsd = new();
			if (hsd.ShowDialog() == DialogResult.OK)
			{
				// validate selected hotkey
				Keys mod = hsd.DetectedModifiers;
				Keys key = hsd.DetectedKey;
				bool valid = true;

				if (mod == Keys.None && key == Keys.None) return;

				switch (key)
				{
					// no key or only modifier key - not suitable
					case Keys.None:
					case Keys.ControlKey:
					case Keys.ShiftKey:
					case Keys.Menu:
						valid = false;
						break;
				}
				if (mod == Keys.None && key == Keys.Left) valid = false; // conflict with main window horizontal position ("Left")

				if (!Enum.TryParse(typeof(MainForm.KeyModifier), mod.ToString(), out _)) valid = false;

				if (valid)
				{
					// add hotkey to list
					string Hotkey = mod.ToString() + "+" + key.ToString();
					if (mod == Keys.None) Hotkey = key.ToString();

					if (RegisteredHotkeys.Contains(Hotkey))
					{
						// delete dublicate
						for (int i = 0; i < lstHotkeys.Items.Count; i++)
						{ if (lstHotkeys.Items[i].Text == Hotkey) lstHotkeys.Items.RemoveAt(i); }
					}
					else
					{
						RegisteredHotkeys.Add(Hotkey);
					}

					if (RemovingHotkeys.Contains(Hotkey)) RemovingHotkeys.Remove(Hotkey); // cancel remove if need

					// really add
					ListViewItem lvi = new(Hotkey);
					lvi.SubItems.Add(tsbAction.Text);
					lstHotkeys.Items.Add(lvi);

					// update selection
					lvi.Selected = true;
					lstHotkeys.Select();
				}
				else MessageBox.Show(Killer.Language.ReadString("HotkeysEditorBadHotKey", "Language"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void tsbRemove_Click(object sender, EventArgs e)
		{
			// REMOVE HOTKEY button click

			if (lstHotkeys.SelectedItems.Count > 0)
			{
				RemovingHotkeys.Add(lstHotkeys.SelectedItems[0].Text);
				RegisteredHotkeys.Remove(lstHotkeys.SelectedItems[0].Text);
				lstHotkeys.Items.Remove(lstHotkeys.SelectedItems[0]);
			}
		}

		private void tsbAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			// EDIT HOTKEY listbox selection change

			if (lstHotkeys.SelectedItems.Count > 0)
			{
				lstHotkeys.SelectedItems[0].SubItems[1].Text = tsbAction.Text;
				lstHotkeys.Select();
			}
		}

		private void lstHotkeys_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			// HOTKEYS LIST selection change

			tsbRemove.Enabled = lstHotkeys.SelectedItems.Count > 0;

			if (lstHotkeys.SelectedItems.Count > 0)
			{ tsbAction.Text = lstHotkeys.SelectedItems[0].SubItems[1].Text; }
			else
			{ tsbAction.Text = "None"; }
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			// OK button click

			foreach (string hk in RemovingHotkeys)
			{
				Killer.Config.DeleteKey(hk);
			}

			for (int i = 0; i < lstHotkeys.Items.Count; i++)
			{
				Killer.Config.Write(lstHotkeys.Items[i].Text, lstHotkeys.Items[i].SubItems[1].Text);
			}

			this.Close();
		}
	}
}
