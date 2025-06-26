
namespace prkiller_ng
{
	partial class HotkeySelectDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblHotKeyIntro = new System.Windows.Forms.Label();
			this.lblHotKeyDetected = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(123, 71);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "button1";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(217, 71);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "button1";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::prkiller_ng.Properties.Resources._48keyboard;
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblHotKeyIntro
			// 
			this.lblHotKeyIntro.AutoSize = true;
			this.lblHotKeyIntro.Location = new System.Drawing.Point(67, 13);
			this.lblHotKeyIntro.Name = "lblHotKeyIntro";
			this.lblHotKeyIntro.Size = new System.Drawing.Size(38, 15);
			this.lblHotKeyIntro.TabIndex = 2;
			this.lblHotKeyIntro.Text = "label1";
			// 
			// lblHotKeyDetected
			// 
			this.lblHotKeyDetected.AutoSize = true;
			this.lblHotKeyDetected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblHotKeyDetected.Location = new System.Drawing.Point(67, 45);
			this.lblHotKeyDetected.Name = "lblHotKeyDetected";
			this.lblHotKeyDetected.Size = new System.Drawing.Size(39, 15);
			this.lblHotKeyDetected.TabIndex = 3;
			this.lblHotKeyDetected.Text = "label1";
			// 
			// HotkeySelectDialog
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(304, 106);
			this.Controls.Add(this.lblHotKeyDetected);
			this.Controls.Add(this.lblHotKeyIntro);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotkeySelectDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "HotkeySelectDialog";
			this.Load += new System.EventHandler(this.HotkeySelectDialog_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeySelectDialog_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblHotKeyIntro;
		private System.Windows.Forms.Label lblHotKeyDetected;
	}
}