using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
	public enum Media {
		PAPERBACK,
		HARDCOVER,
		DVD,
		BLURAY,
		UNKNOWN
	}

	public static class MediaFormat {

		public const string PAPERBACK = "Paperback";
		public const string HARDCOVER = "Hardcover";
		public const string DVD = "DVD";
		public const string BLURAY = "Blu-Ray";
		public const string UNKNOWN = "<Unknown>";

		public static string GetMediaFromKey(int key) {
			string media;
			switch (key) {
				case 0:
					media = "Paperback";
					break;
				case 1:
					media = MediaFormat.HARDCOVER;
					break;
				case 2:
					media = MediaFormat.DVD;
					break;
				case 3:
					media = MediaFormat.BLURAY;
					break;
				default:
					media = MediaFormat.UNKNOWN;
					break;
			}

			return media;
		}

		public static int GetMediaKey(string media) {
			int key;
			switch (media) {
				case MediaFormat.PAPERBACK:
					key = 0;
					break;
				case MediaFormat.HARDCOVER:
					key = 1;
					break;
				case MediaFormat.DVD:
					key = 2;
					break;
				case MediaFormat.BLURAY:
					key = 3;
					break;
				default:
					key = 4;
					break;
			}

			return key;

		}
	}

}
