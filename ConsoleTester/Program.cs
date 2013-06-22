using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID3Lib;

namespace ConsoleTester
{
	class Program
	{
		static void Main(string[] args)
		{
			foreach (string filename in Directory.EnumerateFiles(args[0]))
			{
				if (!filename.EndsWith(".mp3"))
					continue;

				var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				fs.Seek(-128, SeekOrigin.End);
				var reader = new BinaryReader(fs);

				var tagBytes = reader.ReadBytes(128);
				var tag = new ID3v1Tag(tagBytes);

				reader.Close();

				Console.WriteLine(tag.ToString());
			}
		}
	}
}
