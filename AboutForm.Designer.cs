
namespace prkiller_ng
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			this.lnkUrl2 = new System.Windows.Forms.LinkLabel();
			this.lnkUrl1 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(50, 12);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(69, 15);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "PrKiller-NG ";
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(50, 29);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(147, 15);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "© 2025, Alexander Tauenis";
			// 
			// lblLanguage
			// 
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.Location = new System.Drawing.Point(6, 47);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(200, 15);
			this.lblLanguage.TabIndex = 3;
			this.lblLanguage.Text = "Здесь будет информация о языке...";
			// 
			// cmdOk
			// 
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.Location = new System.Drawing.Point(74, 133);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 0;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			// 
			// lnkUrl2
			// 
			this.lnkUrl2.Location = new System.Drawing.Point(-2, 97);
			this.lnkUrl2.Name = "lnkUrl2";
			this.lnkUrl2.Size = new System.Drawing.Size(232, 23);
			this.lnkUrl2.TabIndex = 4;
			this.lnkUrl2.TabStop = true;
			this.lnkUrl2.Text = "https://github.com/atauenis/prkiller-ng";
			this.lnkUrl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lnkUrl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUrl2_LinkClicked);
			// 
			// lnkUrl1
			// 
			this.lnkUrl1.Location = new System.Drawing.Point(12, 74);
			this.lnkUrl1.Name = "lnkUrl1";
			this.lnkUrl1.Size = new System.Drawing.Size(204, 23);
			this.lnkUrl1.TabIndex = 4;
			this.lnkUrl1.TabStop = true;
			this.lnkUrl1.Text = "https://atauenis.ru";
			this.lnkUrl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lnkUrl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUrl1_LinkClicked);
			// 
			// AboutForm
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(228, 168);
			this.Controls.Add(this.lnkUrl1);
			this.Controls.Add(this.lnkUrl2);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.lblLanguage);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.AboutForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblLanguage;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.LinkLabel lnkUrl2;
		private System.Windows.Forms.LinkLabel lnkUrl1;
	}
}