using System;
using System.Collections.Generic;
using System.Text;

namespace XMSharp.Utility
{
	class Encoding
	{
		public Encoding()
		{
		}

		public static string BytesToString(byte[] data)
		{
			return BytesToString(data, System.Text.Encoding.Default);
		}
		public static string BytesToString(byte[] data, System.Text.Encoding encoding)
		{
			return encoding.GetString(data);
		}

		public static byte[] StringToBytes(string data)
		{
			return StringToBytes(data, System.Text.Encoding.Default);
		}
		public static byte[] StringToBytes(string data, System.Text.Encoding encoding)
		{
			return encoding.GetBytes(data);
		}

		public static string EncodeUrl(string url)
		{
			return System.Web.HttpUtility.UrlEncode(url);
		}

		public static string DecodeUrl(string url)
		{
			return System.Web.HttpUtility.UrlDecode(url);
		}

		public static string EncodeHtml(string text)
		{
			return System.Web.HttpUtility.HtmlEncode(text);
		}

		public static string DecodeHtml(string text)
		{
			return System.Web.HttpUtility.HtmlDecode(text);
		}

		public static string EncodeBase64(string data)
		{
			string toReturn = "";

			//try	
			//{
			byte[] encData = new byte[data.Length];
			encData = System.Text.Encoding.UTF8.GetBytes(data);
			toReturn = Convert.ToBase64String(encData);
			//}
			//catch {}

			return toReturn;
		}

		public static string DecodeBase64(string data)
		{
			string toReturn = "";

			//try
			//{
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.Decoder utf8Decode = encoder.GetDecoder();

			byte[] decData = Convert.FromBase64String(data);
			int decChrCount = utf8Decode.GetCharCount(decData, 0, decData.Length);
			char[] decChr = new char[decChrCount];
			utf8Decode.GetChars(decData, 0, decData.Length, decChr, 0);
			toReturn = new String(decChr);
			//}
			//catch {}

			return toReturn;
		}
	}
}
