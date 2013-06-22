using System;
using System.IO;
using System.Text;

namespace ID3Lib
{
	public class ID3v1Tag
	{
		public const int MaxFieldLength = 30;
		public const int TagLength = 128;

		public const int TagOffset = 0;
		public const int TitleOffset = 3;
		public const int ArtistOffset = 33;
		public const int AlbumOffset = 63;
		public const int YearOffset = 93;
		public const int CommentOffset = 97;
		public const int TrackOffset = 126;
		public const int GenreOffset = 127;

		private string _title;
		private string _artist;
		private string _album;
		private short? _year;
		private string _comment;
		private byte? _track;

		public ID3v1Tag()
		{
			_title = String.Empty;
			_artist = String.Empty;
			_album = String.Empty;
			_year = null;
			_comment = String.Empty;
			_track = null;
			Genre = Genre.None;
		}

		public ID3v1Tag(byte[] bytes)
		{
			if (bytes.Length < TagLength || Encoding.ASCII.GetString(bytes, bytes.Length - TagLength, 3) != "TAG")
				throw new InvalidDataException("No ID3 tag found");

			ArraySegment<byte> tag = new ArraySegment<byte>(bytes, bytes.Length - TagLength, TagLength);

			_title = Encoding.ASCII.GetString(tag.Array, tag.Offset + TitleOffset, MaxFieldLength).TrimEnd('\0');
			_artist = Encoding.ASCII.GetString(tag.Array, tag.Offset + ArtistOffset, MaxFieldLength).TrimEnd('\0');
			_album = Encoding.ASCII.GetString(tag.Array, tag.Offset + AlbumOffset, MaxFieldLength).TrimEnd('\0');

			short year;
			if (short.TryParse(Encoding.ASCII.GetString(tag.Array, tag.Offset + YearOffset, 4), out year))
				_year = year;

			if (tag.Array[tag.Offset + TrackOffset - 1] == 0)
			{
				_comment = Encoding.ASCII.GetString(tag.Array, tag.Offset + CommentOffset, MaxFieldLength - 2).TrimEnd('\0');
				_track = tag.Array[tag.Offset + TrackOffset];
			}
			else
			{
				_comment = Encoding.ASCII.GetString(tag.Array, tag.Offset + CommentOffset, MaxFieldLength).TrimEnd('\0');
			}

			Genre = (Genre)tag.Array[tag.Offset + GenreOffset];
		}

		public byte[] ToBytes()
		{
			byte[] bytes = new byte[TagLength];
			
			Encoding.ASCII.GetBytes("TAG").CopyTo(bytes, TagOffset);

			Encoding.ASCII.GetBytes(_title).CopyTo(bytes, TitleOffset);
			Encoding.ASCII.GetBytes(_artist).CopyTo(bytes, ArtistOffset);
			Encoding.ASCII.GetBytes(_album).CopyTo(bytes, AlbumOffset);

			Encoding.ASCII.GetBytes(_year.ToString()).CopyTo(bytes, YearOffset);

			Encoding.ASCII.GetBytes(_comment).CopyTo(bytes, CommentOffset);

			if (_track.HasValue)
			{
				bytes[TrackOffset - 1] = 0;
				bytes[TrackOffset] = _track.Value;
			}

			bytes[GenreOffset] = (byte)Genre;

			return bytes;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.Append("Title: ").AppendLine(_title);
			builder.Append("Artist: ").AppendLine(_artist);
			builder.Append("Album: ").AppendLine(_album);

			if (_year.HasValue)
				builder.Append("Year: ").AppendLine(_year.ToString());
			else
				builder.AppendLine("Year: None");

			builder.Append("Comment: ").AppendLine(_comment);

			if (_track.HasValue)
				builder.Append("Track: ").AppendLine(_track.ToString());
			else
				builder.AppendLine("Track: None");

			builder.Append("Genre: ").AppendLine(Genre.ToString());

			return builder.ToString();
		}

		public string Title
		{
			get { return _title; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Title");

				if (Encoding.ASCII.GetBytes(value).Length > MaxFieldLength)
					throw new ArgumentException(String.Format("Title must be {0} bytes or less", MaxFieldLength), "Title");

				_title = value;
			}
		}

		public string Artist
		{
			get { return _artist; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Artist");

				if (Encoding.ASCII.GetBytes(value).Length > MaxFieldLength)
					throw new ArgumentException(String.Format("Artist must be {0} bytes or less", MaxFieldLength), "Artist");

				_artist = value;
			}
		}

		public string Album
		{
			get { return _album; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Album");

				if (Encoding.ASCII.GetBytes(value).Length > MaxFieldLength)
					throw new ArgumentException(String.Format("Album must be {0} bytes or less", MaxFieldLength), "Album");

				_album = value;
			}
		}

		public short? Year
		{
			get { return _year; }
			set
			{
				if (value.HasValue && (value < -999 || value > 9999))
					throw new ArgumentOutOfRangeException("Year", value, "Year must be betweeen -999 and 9999");

				_year = value;
			}
		}

		public string Comment
		{
			get { return _comment; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Comment");

				if (Encoding.ASCII.GetBytes(value).Length > MaxFieldLength)
					throw new ArgumentException(String.Format("Comment must be {0} bytes or less", MaxFieldLength), "Comment");

				if (_track.HasValue && Encoding.ASCII.GetBytes(value).Length > MaxFieldLength - 2)
					throw new InvalidOperationException(String.Format("Comment must be {0} bytes or less when Track is set",
					                                                  MaxFieldLength - 2));

				_comment = value;
			}
		}

		public byte? Track
		{
			get { return _track; }
			set
			{
				if (value.HasValue && _comment != null && Encoding.ASCII.GetBytes(_comment).Length > MaxFieldLength - 2)
					throw new InvalidOperationException(String.Format("Track may not be set when Comment field is longer than {0} bytes", MaxFieldLength - 2));

				_track = value;
			}
		}

		public Genre Genre { get; set; }
	}
}
