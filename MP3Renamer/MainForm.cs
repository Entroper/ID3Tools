using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ID3Lib;

namespace MP3Renamer
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void folderBrowserButton_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(directoryTextBox.Text))
				folderBrowserDialog.SelectedPath = directoryTextBox.Text;

			var result = folderBrowserDialog.ShowDialog(this);
			
			if (result == DialogResult.OK)
			{
				directoryTextBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void goButton_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(directoryTextBox.Text))
			{
				MessageBox.Show("Directory does not exist!");
				return;
			}

			if (!formatTextBox.Text.Contains("<title>") && !formatTextBox.Text.Contains("<track>"))
			{
				MessageBox.Show("Filenames will not be unique!  Use <title> or <track> somewhere in your format.");
				return;
			}

			var newFilenames = new Dictionary<string, string>();
			var newFilenameSet = new HashSet<string>();

			foreach (string filename in Directory.EnumerateFiles(directoryTextBox.Text))
			{
				if (!filename.EndsWith("mp3"))
					continue;

				var tag = GetID3v1Tag(filename);

				string destFileName = formatTextBox.Text;

				destFileName = destFileName.Replace("<title>", tag.Title);
				destFileName = destFileName.Replace("<artist>", tag.Artist);
				destFileName = destFileName.Replace("<album>", tag.Album);
				destFileName = destFileName.Replace("<year>", tag.Year.ToString());
				destFileName = destFileName.Replace("<comment>", tag.Comment);
				destFileName = destFileName.Replace("<track>", tag.Track.HasValue ? tag.Track.Value.ToString("D2") : String.Empty);
				destFileName = destFileName.Replace("<genre>", tag.Genre.ToString());

				// Invalid filename characters: \/:"*?<>|
				destFileName = destFileName.Replace("\\", "-");
				destFileName = destFileName.Replace("/", "-");
				destFileName = destFileName.Replace(":", "-");
				destFileName = destFileName.Replace("\"", "-");
				destFileName = destFileName.Replace("*", "-");
				destFileName = destFileName.Replace("?", "-");
				destFileName = destFileName.Replace("<", "-");
				destFileName = destFileName.Replace(">", "-");
				destFileName = destFileName.Replace("|", "-");

				destFileName = directoryTextBox.Text + "\\" + destFileName + ".mp3";

				newFilenames.Add(filename, destFileName);
				if (!newFilenameSet.Add(destFileName))
				{
					MessageBox.Show("Filename collision: " + destFileName);
					return;
				}
			}

			foreach (string newFilename in newFilenameSet)
			{
				if (newFilenames.Keys.Contains(newFilename))
				{
					MessageBox.Show("Filename collision: " + newFilename);
					return;
				}
			}

			foreach (string oldFilename in newFilenames.Keys)
			{
				string newFilename = newFilenames[oldFilename];
				File.Move(oldFilename, newFilename);
			}

			MessageBox.Show("Done!");
		}

		private ID3v1Tag GetID3v1Tag(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			fs.Seek(-128, SeekOrigin.End);
			var reader = new BinaryReader(fs);

			var tagBytes = reader.ReadBytes(128);

			reader.Close();

			return new ID3v1Tag(tagBytes);
		}
	}
}
