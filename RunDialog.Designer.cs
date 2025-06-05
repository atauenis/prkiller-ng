
namespace prkiller_ng
{
	partial class RunDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunDialog));
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblIntro = new System.Windows.Forms.Label();
			this.lblOpen = new System.Windows.Forms.Label();
			this.cbxOpen = new System.Windows.Forms.ComboBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(81, 112);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "button1";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(162, 112);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "button1";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Location = new System.Drawing.Point(243, 112);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
			this.cmdBrowse.TabIndex = 3;
			this.cmdBrowse.Text = "button1";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblIntro
			// 
			this.lblIntro.AutoEllipsis = true;
			this.lblIntro.Location = new System.Drawing.Point(52, 13);
			this.lblIntro.Name = "lblIntro";
			this.lblIntro.Size = new System.Drawing.Size(266, 50);
			this.lblIntro.TabIndex = 2;
			this.lblIntro.Text = "Type the name of a program, folder, document, or Internet resource, and Windows w" +
    "ill open it for you.";
			// 
			// lblOpen
			// 
			this.lblOpen.AutoSize = true;
			this.lblOpen.Location = new System.Drawing.Point(12, 63);
			this.lblOpen.Name = "lblOpen";
			this.lblOpen.Size = new System.Drawing.Size(57, 15);
			this.lblOpen.TabIndex = 2;
			this.lblOpen.Text = "Открыть:";
			// 
			// cbxOpen
			// 
			this.cbxOpen.FormattingEnabled = true;
			this.cbxOpen.Location = new System.Drawing.Point(75, 60);
			this.cbxOpen.Name = "cbxOpen";
			this.cbxOpen.Size = new System.Drawing.Size(243, 23);
			this.cbxOpen.TabIndex = 0;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// RunDialog
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 147);
			this.Controls.Add(this.cbxOpen);
			this.Controls.Add(this.lblOpen);
			this.Controls.Add(this.lblIntro);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RunDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RunDialog";
			this.Load += new System.EventHandler(this.RunDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblIntro;
		private System.Windows.Forms.Label lblOpen;
		private System.Windows.Forms.ComboBox cbxOpen;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}