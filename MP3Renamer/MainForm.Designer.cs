namespace MP3Renamer
{
	partial class MainForm
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
			this.directoryLabel = new System.Windows.Forms.Label();
			this.directoryTextBox = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.folderBrowserButton = new System.Windows.Forms.Button();
			this.formatLabel = new System.Windows.Forms.Label();
			this.formatTextBox = new System.Windows.Forms.TextBox();
			this.goButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// directoryLabel
			// 
			this.directoryLabel.AutoSize = true;
			this.directoryLabel.Location = new System.Drawing.Point(13, 13);
			this.directoryLabel.Name = "directoryLabel";
			this.directoryLabel.Size = new System.Drawing.Size(89, 13);
			this.directoryLabel.TabIndex = 0;
			this.directoryLabel.Text = "Choose directory:";
			// 
			// directoryTextBox
			// 
			this.directoryTextBox.Location = new System.Drawing.Point(108, 10);
			this.directoryTextBox.Name = "directoryTextBox";
			this.directoryTextBox.Size = new System.Drawing.Size(330, 20);
			this.directoryTextBox.TabIndex = 1;
			// 
			// folderBrowserButton
			// 
			this.folderBrowserButton.Location = new System.Drawing.Point(444, 8);
			this.folderBrowserButton.Name = "folderBrowserButton";
			this.folderBrowserButton.Size = new System.Drawing.Size(32, 23);
			this.folderBrowserButton.TabIndex = 2;
			this.folderBrowserButton.Text = "...";
			this.folderBrowserButton.UseVisualStyleBackColor = true;
			this.folderBrowserButton.Click += new System.EventHandler(this.folderBrowserButton_Click);
			// 
			// formatLabel
			// 
			this.formatLabel.AutoSize = true;
			this.formatLabel.Location = new System.Drawing.Point(13, 40);
			this.formatLabel.Name = "formatLabel";
			this.formatLabel.Size = new System.Drawing.Size(67, 13);
			this.formatLabel.TabIndex = 3;
			this.formatLabel.Text = "Enter format:";
			// 
			// formatTextBox
			// 
			this.formatTextBox.Location = new System.Drawing.Point(108, 37);
			this.formatTextBox.Name = "formatTextBox";
			this.formatTextBox.Size = new System.Drawing.Size(330, 20);
			this.formatTextBox.TabIndex = 4;
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(444, 35);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(32, 23);
			this.goButton.TabIndex = 5;
			this.goButton.Text = "Go!";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 169);
			this.label1.TabIndex = 6;
			this.label1.Text = "Valid formatting symbols:\r\n\r\n<title>\r\n<artist>\r\n<album>\r\n<year>\r\n<comment>\r\n<trac" +
    "k>\r\n<genre>\r\n\r\nExample:\r\n<artist> - <album> - <track> - <title>\r\nKenji Yamamoto " +
    "- Metroid Prime - 01 - Menu Theme";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 251);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.formatTextBox);
			this.Controls.Add(this.formatLabel);
			this.Controls.Add(this.folderBrowserButton);
			this.Controls.Add(this.directoryTextBox);
			this.Controls.Add(this.directoryLabel);
			this.Name = "MainForm";
			this.Text = "MP3Renamer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label directoryLabel;
		private System.Windows.Forms.TextBox directoryTextBox;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button folderBrowserButton;
		private System.Windows.Forms.Label formatLabel;
		private System.Windows.Forms.TextBox formatTextBox;
		private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.Label label1;
	}
}

