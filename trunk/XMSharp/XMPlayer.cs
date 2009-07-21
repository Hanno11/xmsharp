using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Xml;
using XMSharp.Utility;

namespace XMSharp
{
	public class XMPlayer
	{
        internal static object ChannelListLock = new object();

		#region Constructor
		public XMPlayer()
		{
			Channels = new XMChannelList();
			allChannelsPollingTimer = new Timer(new TimerCallback(allChannelsPollingTimer_Callback), null, Timeout.Infinite, this.AllChannelsPollingInterval * 1000);
			currentChannelPollingTimer = new Timer(new TimerCallback(currentChannelPollingTimer_Callback), null, Timeout.Infinite, this.CurrentChannelPollingInterval * 1000);

		}
		#endregion

		#region Urls
		public static string urlPreLogin = "http://xmro.xmradio.com/xstream/index.jsp?ep=xmroca";
		public static string urlLogin = "http://xmro.xmradio.com/xstream/login_servlet.jsp";
		public static string urlConfig = "http://player.xmradio.com/player/_js/config.jsp";
        public static string loginSuccessValue = "XM Radio - Listen Online";

		public static string urlXmlChannelData = "http://player.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true";
		public static string urlXmlChannelSingle = "http://player.xmradio.com/padData/pad_data_servlet.jsp?channel={Number}";

        public static string urlChannelNeighborhoods = "http://player.xmradio.com/player/2ft/channel_data.jsp";

		public static string urlChannels = "http://player.xmradio.com/player/2ft/channel_data.jsp";
		public static string urlNowPlayingAll = "http://player.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true&remote=true&rpc=NAPSCA";
		public static string urlNowPlayingChannel = "http://player.xmradio.com/padData/pad_data_servlet.jsp?channel={Number}&remote=true&rpc=NAPSCA";

		public static string urlPlayChannel = "http://player.xmradio.com/player/2ft/playMedia.jsp?ch={Number}&speed=high";
		#endregion

		#region Private
                //private AXVLC.VLCPlugin2 vlc = new AXVLC.VLCPlugin2Class();
                //private AXVLC.VLCPlugin vlc1 = new AXVLC.VLCPluginClass();
        private AXVLC.VLCPlugin2 vlc = new AXVLC.VLCPlugin2Class();
                        //.VLCPlugin vlc = new AXVLC.VLCPluginClass();
		private CookieContainer cookies = new CookieContainer();

		private int loginTimeout = 3;
		private DateTime lastLogin = DateTime.Now.AddDays(-99);
		private DateTime lastAllChannelPolling = DateTime.Now.AddDays(-99);
		private DateTime lastCurrentChannelPolling = DateTime.Now.AddDays(-99);

		private XMChannel currentChannel = null;
		private bool loggedIn = false;
		private bool initialLogin = false;

		System.Threading.Timer allChannelsPollingTimer = null;
		System.Threading.Timer currentChannelPollingTimer = null;

		private string email = string.Empty;
        private string password = string.Empty;
        private XMChannelList channels = new XMChannelList();

        public delegate void ChannelInfoChangedDelegate(List<XMChannel> changedChannels);
        public event ChannelInfoChangedDelegate OnChannelInfoChanged;

		private bool useProxy = false;
		private string proxyAddress = string.Empty;
		private int proxyPort = 8080;
		private string proxyUsername = string.Empty;
		private string proxyPassword = string.Empty;

		private int currentChannelPollingInterval = 15;
		private int allChannelsPollingInterval = 60;
		#endregion

		#region Properties
		public string Email
		{
            get { return email; }
            set { email = value; }
		}

		public string Password
		{
            get { return password; }
            set { password = value; }
		}

		public int LoginTimeout
		{
			get { return loginTimeout; }
			set { loginTimeout = value; }
		}

		public XMChannelList Channels
		{
            get { return channels; }
            set { channels = value; } 
		}

		public XMChannel CurrentChannel
		{
			get { return currentChannel; }
		}

		public bool UseProxy
		{
			get { return useProxy; }
			set { useProxy = value; }
		}

		public string ProxyAddress
		{
			get { return proxyAddress; }
			set { proxyAddress = value; }
		}

		public int ProxyPort
		{
			get { return proxyPort; }
			set { proxyPort = value; }
		}

		public string ProxyUsername
		{
			get { return proxyUsername; }
			set { proxyUsername = value; }
		}

		public string ProxyPassword
		{
			get { return proxyPassword; }
			set { proxyPassword = value; }
		}

		public int CurrentChannelPollingInterval
		{
			get { return currentChannelPollingInterval; }
			set { currentChannelPollingInterval = value; }
		}

		public int AllChannelsPollingInterval
		{
			get { return allChannelsPollingInterval; }
			set { allChannelsPollingInterval = value; }
		}

		public static Dictionary<string, System.Drawing.Image> ChannelImages = new Dictionary<string, System.Drawing.Image>();
		#endregion

		#region Timer Callbacks
		private void allChannelsPollingTimer_Callback(object state)
		{
			if (lastAllChannelPolling.AddSeconds(this.AllChannelsPollingInterval) <= DateTime.Now)
				UpdateChannels();
		}

		private void currentChannelPollingTimer_Callback(object state)
		{
			if (lastCurrentChannelPolling.AddSeconds(this.CurrentChannelPollingInterval) <= DateTime.Now)
				UpdateCurrentChannel();
		}
		#endregion

        public bool Login()
		{			
			cookies = new CookieContainer();
			string loginPageResponse = Http.Get(urlPreLogin, cookies);
				
			Dictionary<string, string> postVars = new Dictionary<string, string>();
			postVars.Add("user_id", Email);
			postVars.Add("pword", Password);
			postVars.Add("confirmationCode", "");
			postVars.Add("go.x", "12");
			postVars.Add("go.y", "3");

	
			Http.HttpSubmitInfo submit = new Http.HttpSubmitInfo();
			submit.AllowAutoRedirect = true;
			submit.Cookies = cookies;
			submit.Credentials = null;
			submit.KeepAlive = false;
			submit.MaximumRedirects = 3;
			submit.Method = Http.HttpSubmitMethod.Post;
			submit.Proxy = null;
			submit.RequestData = Http.GetRequestData(postVars);
			submit.Url = urlLogin;
			submit.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
			submit.Referer = "http://xmro.xmradio.com/xstream/index.jsp?ep=xmroca";
				
			string loginResponse = Http.Post(submit);
			string config = Http.Get(urlConfig, cookies);

            CookieCollection cookieCol = cookies.GetCookies(new Uri("http://xmro.xmradio.com"));

            
            foreach (Cookie cookieOn in cookieCol)
                Console.WriteLine(cookieOn.Name + ":" + cookieOn.Value + "  " + cookieOn.Expires.ToString());
			
            if (loginResponse.Contains(loginSuccessValue))
            {
                loggedIn = true;
				lastLogin = DateTime.Now;

                lock (ChannelListLock)
                {
                    UpdateChannels();
                    UpdateChannelNeighborhoods();
                }

				if (!initialLogin)
				{
					initialLogin = true;
					allChannelsPollingTimer.Change(0, this.AllChannelsPollingInterval * 1000);
					currentChannelPollingTimer.Change(0, this.CurrentChannelPollingInterval * 1000);
				}
            }

			return loggedIn;
		}

		public bool ChangeChannel(XMChannel newChannel)
		{
			string old = string.Empty;
			
			if (currentChannel != null)
				currentChannel.ToString();

			currentChannel = newChannel;

                        //vlc is thowing an exception when trying to access any sort of status about
                        //the current file if nothing is loaded.  A VLC issue?
			/*if (vlc.Playing)
				vlc.stop();*/
                        try
                        {
                                if (vlc.playlist.isPlaying)
                                {
                                        vlc.playlist.stop();
                                }
                        }
                        catch
                        {
                                //If nothing is loaded, COM object returns an error if 
                                //checking vlc.playlist.isPlaying
                                //This will ignore that case, but needs to be handled
                        }
                        
                       

            lock (ChannelListLock)
            {
                UpdateCurrentChannel();
            }

			string playerResponse = Http.Get(urlPlayChannel.Replace("{Number}", newChannel.Number.ToString()), cookies);

			Match streamMatch = Regex.Match(playerResponse, @"xmmediaplayer\.URL\s?=\s?[']{1}(?<Stream>.*?)[']{1}", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

			if (streamMatch != null)
			{
                                
				//string[] options = new string[] { ":mms-caching=3000", ":mms-timeout=10000" };
				string[] options = new string[] { };

				string streamUrl = streamMatch.Groups["Stream"].Value.Trim();
                string asxRedirect = Http.Get(streamUrl, cookies);
                string mmsStream = string.Empty;

                XmlTextReader asxText = new XmlTextReader(new System.IO.StringReader(asxRedirect));

                //find the mms stream address
                while (asxText.Read())
                {
                //TODO: END WHILE IF AT THE END OF THE ASX FILE
                //WOULD CRASH IF NO "Ref" FOUND
                        if (asxText.Name == "Ref" && asxText.HasAttributes)
                        {
                                mmsStream = asxText.GetAttribute("href");
                                break;
                        }
                }
                               
                /*Original vlc plugin function calls*/
				//vlc.playlistClear();
				//vlc.AutoPlay = true;
				//vlc.addTarget(streamUrl, options, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, 0);
				//vlc.play();

                //Using vlc plugin2
                vlc.playlist.clear();
                vlc.AutoPlay = true;
                vlc.playlist.add(mmsStream, null, options);

                //For some reason .play() always continues to play the first loaded 
                //station regardless of channel selection.  Calling .playItem(0) 
                //picks up on the changed channel
                vlc.playlist.playItem(0);
			}

			if (!old.Equals(currentChannel.ToString()))
			{
				if (OnChannelInfoChanged != null)
				{
					List<XMChannel> changedChannels = new List<XMChannel>();
					changedChannels.Add(currentChannel);
					OnChannelInfoChanged(changedChannels);
				}
			}

			return true;
		}

        public bool UpdateChannelNeighborhoods()
        {
            if (!loggedIn || lastLogin.AddMinutes(loginTimeout) <= DateTime.Now)
                Login();

            if (Channels.Count > 0)
            {
                string data = Http.Get(urlChannelNeighborhoods, cookies);

                List<XMChannel> changedChannels = new List<XMChannel>();

                lock (ChannelListLock)
                {
                   changedChannels = Utility.XMRPCParser.ParseChannelNeighborhoods(data, Channels);
                }

                if (changedChannels.Count > 0 && OnChannelInfoChanged != null)
                    OnChannelInfoChanged(changedChannels);
            }

            return true;
        }

        public bool UpdateChannels()
        {
            return UpdateChannels(false);
        }

		public bool UpdateChannels(bool preloadImages)
		{
			if (!loggedIn || lastLogin.AddMinutes(loginTimeout) <= DateTime.Now)
				Login();

			string channelListing = Http.Get(urlXmlChannelData, cookies);

            List<XMChannel> changedChannels = new List<XMChannel>();

            lock (ChannelListLock)
            {
               changedChannels = Utility.XMRPCParser.ParseChannelData(channelListing, this.Channels);
            }

			if (changedChannels.Count > 0 && OnChannelInfoChanged != null)
				OnChannelInfoChanged(changedChannels);
		
			return true;
		}

		public bool UpdateChannel(XMChannel channel)
		{
			if (channel == null)
				return false;

			if (!loggedIn || lastLogin.AddMinutes(loginTimeout) <= DateTime.Now)
				Login();

			string channelSingle = Http.Get(urlXmlChannelSingle.Replace("{Number}", channel.Number.ToString()), cookies);

            List<XMChannel> changedChannels = new List<XMChannel>();

            lock (ChannelListLock)
            {
                changedChannels = Utility.XMRPCParser.ParseChannelData(channelSingle, this.Channels);
            }

			if (changedChannels.Count > 0 && OnChannelInfoChanged != null)
				OnChannelInfoChanged(changedChannels);

			return true;
		}

        public bool Stop()
        {
            vlc.playlist.stop();
            return true;
        }

		public bool UpdateCurrentChannel()
		{
			return UpdateChannel(CurrentChannel);
		}

		public void NextChannel()
		{
			
		}

		public void PreviousChannel()
		{

		}

	}
}
