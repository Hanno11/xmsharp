using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;


namespace XMSharp.Utility
{
	/// <summary>
	/// Description of MyClass.
	/// Static Methods to assist in Performing Http Requests
	/// </summary>
	class Http
	{
		#region Post Overloads
		public static string Post(string url)
		{
			return post(new HttpSubmitInfo(url));
		}
		public static string Post(string url, HttpProxyInfo proxy)
		{
			return post(new HttpSubmitInfo(url, proxy));
		}
		public static string Post(string url, CookieContainer cookies)
		{
			return post(new HttpSubmitInfo(url, cookies));
		}
		public static string Post(string url, CookieContainer cookies, HttpProxyInfo proxy)
		{
			return post(new HttpSubmitInfo(url, cookies, proxy));
		}

		public static string Post(string url, byte[] requestData)
		{
			return post(new HttpSubmitInfo(url, requestData));
		}
		public static string Post(string url, byte[] requestData, HttpProxyInfo proxy)
		{
			return post(new HttpSubmitInfo(url, requestData, proxy));
		}
		public static string Post(string url, CookieContainer cookies, byte[] requestData)
		{
			return post(new HttpSubmitInfo(url, cookies, requestData));
		}
		public static string Post(string url, CookieContainer cookies, byte[] requestData, HttpProxyInfo proxy)
		{
			return post(new HttpSubmitInfo(url, cookies, requestData, proxy));
		}
		public static string Post(HttpSubmitInfo info)
		{
			return post(info);
		}

		private static string post(HttpSubmitInfo info)
		{
			info.Method = HttpSubmitMethod.Post;
			return stringRequest(info);
		}
		#endregion


		#region Get Overloads
		public static string Get(string url)
		{
			return get(new HttpSubmitInfo(url));
		}
		public static string Get(string url, HttpProxyInfo proxy)
		{
			return get(new HttpSubmitInfo(url, proxy));
		}
		public static string Get(string url, CookieContainer cookies)
		{
			return get(new HttpSubmitInfo(url, cookies));
		}
		public static string Get(string url, CookieContainer cookies, HttpProxyInfo proxy)
		{
			return get(new HttpSubmitInfo(url, cookies, proxy));
		}
		public static string Get(string url, byte[] requestData)
		{
			return get(new HttpSubmitInfo(url, requestData));
		}
		public static string Get(string url, byte[] requestData, HttpProxyInfo proxy)
		{
			return get(new HttpSubmitInfo(url, requestData, proxy));
		}
		public static string Get(string url, CookieContainer cookies, byte[] requestData)
		{
			return get(new HttpSubmitInfo(url, cookies, requestData));
		}
		public static string Get(string url, CookieContainer cookies, byte[] requestData, HttpProxyInfo proxy)
		{
			return get(new HttpSubmitInfo(url, cookies, requestData, proxy));
		}
		public static string Get(HttpSubmitInfo info)
		{
			return get(info);
		}
		private static string get(HttpSubmitInfo info)
		{
			info.Method = HttpSubmitMethod.Get;
			return stringRequest(info);
		}
		#endregion


		#region Get Image
		public static Image GetImage(string url)
		{
			HttpSubmitInfo info = new HttpSubmitInfo();
			info.Url = url;
			return GetImage(info);
		}
		public static Image GetImage(HttpSubmitInfo info)
		{
			Image toReturn = null;
			byte[] imgData = null;

			try
			{
				imgData = binaryRequest(info);

				MemoryStream ms = new MemoryStream(imgData);
				toReturn = Image.FromStream(ms);
			}
			catch { }

			return toReturn;
		}
		#endregion


		#region Get Bytes
		public static byte[] GetBytes(string url)
		{
			HttpSubmitInfo info = new HttpSubmitInfo();
			info.Url = url;
			return GetBytes(info);
		}
		public static byte[] GetBytes(HttpSubmitInfo info)
		{
			return binaryRequest(info);
		}
		#endregion


		#region Get Request Data
		public static byte[] GetRequestData(Dictionary<string, string> pairs)
		{
			//Assumed application/x-www-form-urlencoded since it's string/value pairs
			// application/x-www-form-urlencoded keys and values must be url encoded
			// separated by &'s
			// eg:   <UrlEncodedKey>=<UrlEncodedValue>&........
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			foreach (string keyOn in pairs.Keys)
			{
				sb.Append(keyOn + "=" + pairs[keyOn] + "&");
			}

			string data = sb.ToString();

			if (data.EndsWith("&"))
				data = data.Substring(0, data.Length - 1);

			
			return System.Text.Encoding.Default.GetBytes(data);
		}
		#endregion


		#region WebRequest, String/Binary Requests
		private static HttpWebRequest formulateWebRequest(HttpSubmitInfo info)
		{
			System.Net.ServicePointManager.Expect100Continue = false;

			HttpWebRequest web = (HttpWebRequest)WebRequest.Create(info.Url);

			if (info.Method == HttpSubmitMethod.Post)
				web.Method = "POST";
			else
				web.Method = "GET";

			if (info.Accept != null)
				web.Accept = info.Accept;

			web.SendChunked = false;

			if (info.ContentType != null)
				web.ContentType = info.ContentType;

			if (info.Cookies != null)
				web.CookieContainer = info.Cookies;

			web.AllowAutoRedirect = info.AllowAutoRedirect;
			web.Pipelined = false;
			web.KeepAlive = info.KeepAlive;

			web.MaximumAutomaticRedirections = info.MaximumRedirects;

			web.Expect = string.Empty;
			web.TransferEncoding = string.Empty;
			

			if (info.Proxy != null)
			{
				web.Proxy = new WebProxy(info.Proxy.Url, info.Proxy.Port);

				if (info.Proxy.Username.Trim().Length > 0 && info.Proxy.Password.Trim().Length > 0)
					web.Proxy.Credentials = new NetworkCredential(info.Proxy.Username, info.Proxy.Password);
			}

			if (info.Referer != null)
				web.Referer = info.Referer;

			if (info.UserAgent != null)
				web.UserAgent = info.UserAgent;

			if (info.Credentials != null)
			{
				web.PreAuthenticate = true;
				web.Credentials = info.Credentials;
			}

			if (info.Timeout > 0)
				web.Timeout = info.Timeout;

			//If we have request data to send, do it.
			if (info.RequestData != null)
			{
				if (info.RequestData.Length > 0)
				{
					//Send out the data
					web.ContentLength = info.RequestData.Length;
					Stream sout = web.GetRequestStream();
					sout.Write(info.RequestData, 0, info.RequestData.Length);
					sout.Flush();
					sout.Close();
				}
			}


			return web;
		}

		private static string stringRequest(HttpSubmitInfo info)
		{
			HttpWebRequest web = formulateWebRequest(info);

			//Get the response
			HttpWebResponse res = (HttpWebResponse)web.GetResponse();
			StreamReader sr = new StreamReader(res.GetResponseStream());
			string response = sr.ReadToEnd();

			return response;
		}

		private static byte[] binaryRequest(HttpSubmitInfo info)
		{
			HttpWebRequest web = formulateWebRequest(info);

			//Get the response
			HttpWebResponse res = (HttpWebResponse)web.GetResponse();
			//StreamReader sr = new StreamReader(res.GetResponseStream());
			BinaryReader reader = new BinaryReader(res.GetResponseStream());

			// Since we don't know how many bytes there will be,
			// use a dynamic list to store them
			System.Collections.ArrayList byteList = new System.Collections.ArrayList();
			int totalLength = 0;
			byte[] bytes;

			// Keep reading until the reader returns 0 bytes
			while ((bytes = reader.ReadBytes(1024)).Length > 0)
			{
				byteList.Add(bytes);
				totalLength += bytes.Length;
			}

			// Create a byte array to store the final result
			bytes = new byte[totalLength];
			int position = 0;

			// Cycle through the list of byte arrays we read in
			// and copy each in turn to the final result
			foreach (byte[] b in byteList)
			{
				Array.Copy(b, 0, bytes, position, b.Length);
				position += b.Length;
			}

			return bytes;
		}
		#endregion


		#region HttpSubmitMethod
		public enum HttpSubmitMethod
		{
			Get,
			Post
		}
		#endregion


		#region HttpProxyInfo
		public class HttpProxyInfo
		{
			public HttpProxyInfo()
			{
			}

			public HttpProxyInfo(string url, int port)
			{
				this.url = url;
				this.port = port;
			}

			public HttpProxyInfo(string url)
			{
				RegexOptions rxo = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase;
				string urlPattern = "(?<url>.*?)[:]{1}(?<port>[0-9]+)";

				if (Regex.IsMatch(url, urlPattern, rxo))
				{
					Match match = Regex.Match(url, urlPattern, rxo);
					this.url = match.Groups["url"].Value;
					this.port = Convert.ToInt32(match.Groups["port"].Value);
				}
				else
				{
					this.url = url;
				}
			}

			public HttpProxyInfo(string url, int port, string username, string password)
			{
				this.url = url;
				this.port = port;
				this.username = username;
				this.password = password;
			}

			private string url = "";
			private int port = 1080;
			private string username = "";
			private string password = "";

			public string Url
			{
				get { return url; }
				set { url = value; }
			}

			public int Port
			{
				get { return port; }
				set { port = value; }
			}

			public string Username
			{
				get { return username; }
				set { username = value; }
			}

			public string Password
			{
				get { return password; }
				set { password = value; }
			}
		}
		#endregion


		#region HttpSubmitInfo
		public class HttpSubmitInfo
		{
			#region Constructor Overloads
			public HttpSubmitInfo()
			{
			}

			public HttpSubmitInfo(string url)
			{
				this.url = url;
			}

			public HttpSubmitInfo(string url, HttpProxyInfo proxy)
			{
				this.url = url;
				this.proxy = proxy;
			}

			public HttpSubmitInfo(string url, CookieContainer cookies)
			{
				this.url = url;
				this.cookies = cookies;
			}

			public HttpSubmitInfo(string url, CookieContainer cookies, HttpProxyInfo proxy)
			{
				this.url = url;
				this.cookies = cookies;
				this.proxy = proxy;
			}

			public HttpSubmitInfo(string url, byte[] requestData)
			{
				this.url = url;
				this.requestData = requestData;
			}

			public HttpSubmitInfo(string url, byte[] requestData, HttpProxyInfo proxy)
			{
				this.url = url;
				this.requestData = requestData;
				this.proxy = proxy;
			}

			public HttpSubmitInfo(string url, CookieContainer cookies, byte[] requestData)
			{
				this.url = url;
				this.cookies = cookies;
				this.requestData = requestData;
			}

			public HttpSubmitInfo(string url, CookieContainer cookies, byte[] requestData, HttpProxyInfo proxy)
			{
				this.url = url;
				this.cookies = cookies;
				this.requestData = requestData;
				this.proxy = proxy;
			}
			#endregion


			#region Private Memebers
			private string url = "";
			private HttpSubmitMethod method = HttpSubmitMethod.Get;
			private string contentType = "application/x-www-form-urlencoded";
			private CookieContainer cookies = new CookieContainer();
			private NetworkCredential credentials = null;
			private string userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
			private string referer = "";
			private string accept = "";
			private bool allowAutoRedirect = true;
			private bool keepAlive = true;
			private int maximumRedirects = 1;
			private HttpProxyInfo proxy = null;
			private byte[] requestData = null;
			private int timeout = -1;
			#endregion


			#region Properties
			public string Url
			{
				get { return url; }
				set { url = value; }
			}

			public HttpSubmitMethod Method
			{
				get { return method; }
				set { method = value; }
			}

			public string ContentType
			{
				get { return contentType; }
				set { contentType = value; }
			}

			public CookieContainer Cookies
			{
				get { return cookies; }
				set { cookies = value; }
			}

			public NetworkCredential Credentials
			{
				get { return credentials; }
				set { credentials = value; }
			}

			public string UserAgent
			{
				get { return userAgent; }
				set { userAgent = value; }
			}

			public string Referer
			{
				get { return referer; }
				set { referer = value; }
			}

			public string Accept
			{
				get { return accept; }
				set { accept = value; }
			}

			public bool AllowAutoRedirect
			{
				get { return allowAutoRedirect; }
				set { allowAutoRedirect = value; }
			}

			public bool KeepAlive
			{
				get { return keepAlive; }
				set { keepAlive = value; }
			}

			public int MaximumRedirects
			{
				get { return maximumRedirects; }
				set { maximumRedirects = value; }
			}

			public HttpProxyInfo Proxy
			{
				get { return proxy; }
				set { proxy = value; }
			}

			public byte[] RequestData
			{
				get { return requestData; }
				set { requestData = value; }
			}

			public int Timeout
			{
				get { return timeout; }
				set { timeout = value; }
			}
			#endregion
		}
		#endregion
	}
}
