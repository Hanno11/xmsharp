using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XMSharp.Utility
{
	class XMRPCParser
	{

        public static List<XMChannel> ParseChannelNeighborhoods(string data, XMChannelList channels)
        {
			List<XMChannel> changedChannels = new List<XMChannel>();
            string nhoodRegex = @"{\s?nhood:(?<Neighborhood>.*?)[,]{1}\s?channels:\s?\[(?<Channels>.*?)\]";
            string chanNumRegex = @"num:\s?(?<Number>[0-9]{1,})";

            bool matched = Regex.IsMatch(data, nhoodRegex);

            MatchCollection nhoods = Regex.Matches(data, nhoodRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match nhood in nhoods)
            {
                string nhoodName = nhood.Groups["Neighborhood"].Value;

                if (nhoodName.StartsWith("\"") && nhoodName.EndsWith("\"") && nhoodName.Length > 2)
                    nhoodName = nhoodName.Substring(1, nhoodName.Length - 2);

                MatchCollection chans = Regex.Matches(nhood.Groups["Channels"].Value, chanNumRegex, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

                foreach (Match chan in chans)
                {
                    int chanNum = -1;

                    if (Int32.TryParse(chan.Groups["Number"].Value, out chanNum))
                    {
                        XMChannel xmChan = channels[chanNum];

						if (xmChan != null)
						{
							string oldChan = xmChan.ToString();

							xmChan.Neighborhood = nhoodName;

							if (!xmChan.ToString().Equals(oldChan))
								changedChannels.Add(xmChan);
						}
                    }
                }
            }

            return changedChannels;
        }

		public static List<XMChannel> ParseChannelData(string xml, XMChannelList channels)
		{
			List<XMChannel> changedChannels = new List<XMChannel>();

			System.Xml.XmlTextReader xr = new System.Xml.XmlTextReader(new System.IO.StringReader(xml));

			int channelNumOn = -1;

			string artist = string.Empty;
			string album = string.Empty;
			string song = string.Empty;
			string channelName = string.Empty;
			int trackID = -1;
			int albumID = -1;
			int artistID = -1;
			string linkUrl = string.Empty;

			while (xr.Read())
			{
				switch (xr.Name.ToLower())
				{
					case "artist":
						artist = xr.ReadElementString();
						break;
					case "album":
						album = xr.ReadElementString();
						break;
					case "songtitle":
						song = xr.ReadElementString();
						break;
					case "channelnumber":
						Int32.TryParse(xr.ReadElementString(), out channelNumOn);
						break;
					case "channelname":
						channelName = xr.ReadElementString();
						break;
					case "trackid":
						Int32.TryParse(xr.ReadElementString(), out trackID);
						break;
					case "artistid":
						Int32.TryParse(xr.ReadElementString(), out artistID);
						break;
					case "linkurl":
						linkUrl = xr.ReadElementString();
						break;
				}

				if (xr.Name.ToLower().Equals("event") && xr.IsStartElement())
				{
					XMChannel chanOn = channels[channelNumOn];

					//Channel may not exist
					if (chanOn == null && channelNumOn > 0)
					{
						chanOn = new XMChannel();
						chanOn.Number = channelNumOn;
						channels.Add(chanOn);
						chanOn = channels[channelNumOn];
					}

					//Make sure we have a valid channel
					if (chanOn != null)
					{
						string oldChan = chanOn.ToString();

						chanOn.Name = channelName;
						chanOn.CurrentAlbum = album;
						chanOn.CurrentArtist = artist;
						chanOn.CurrentSong = song;
						chanOn.CurrentArtistID = artistID;
						chanOn.CurrentAlbumID = albumID;
						chanOn.CurrentLinkUrl = linkUrl;

						//See if the channel Changed or not
						if (!chanOn.ToString().Equals(oldChan))
							changedChannels.Add(chanOn);
					}

					channelNumOn = -1;
					channelName = string.Empty;
					artist = string.Empty;
					album = string.Empty;
					song = string.Empty;
					trackID = -1;
					artistID = -1;
					linkUrl = string.Empty;
				}
			}

			return changedChannels;
		}

		//public static bool ParseRpcValues(string rpc, XMChannelList channels)
		//{
		//    MatchCollection chans = Regex.Matches(rpc, "[{]{1}(?<Attributes>.*?)[}]{1}", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

		//    //Looping through all our result regex matches
		//    foreach (Match chan in chans)
		//    {
		//        Dictionary<string, string> parsedAttributes = new Dictionary<string, string>();
		//        MatchCollection attributes = Regex.Matches(chan.Groups["Attributes"].Value, @"(?<Key>[a-zA-Z]+)[:]{1}\s?(?<Value>.*?)(,{1}\s?|$)", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

		//        //Go through all the attributes in each result
		//        foreach (Match attr in attributes)
		//        {
		//            //Get our key/value pairs from the regex groups
		//            string key = attr.Groups["Key"].Value.Trim().ToLower();
		//            string value = attr.Groups["Value"].Value.Trim();

		//            //Get rid of surrounding quotes
		//            if (value.StartsWith("\"") && value.EndsWith("\"") && value.Length > 2)
		//                value = value.Substring(1, value.Length - 2);

		//            //store the keys/values to process
		//            parsedAttributes.Add(key, value);
		//        }

		//        //We don't process these on the fly since we need all the key/value pairs first to ensure we 
		//        // got a 'num' key which will help us find the channel we are tryign to update!
		//        if (parsedAttributes.ContainsKey("num"))
		//        {
		//            int channelNumOn = -1;
					
		//            Int32.TryParse(parsedAttributes["num"], out channelNumOn);

		//            if (channelNumOn > 0) //make sure we have a valid channel #
		//            {
		//                //Try getting the channel by channel #
		//                XMChannel channelOn = channels[channelNumOn];

		//                if (channelOn == null)
		//                {
		//                    channelOn = new XMChannel();
		//                    channelOn.Number = channelNumOn;
		//                    channels.Add(channelOn);

		//                    channelOn = channels[channelNumOn];
		//                }

		//                foreach (string keyOn in parsedAttributes.Keys)
		//                {
		//                    switch (keyOn)
		//                    {
		//                        case "name":
		//                            channelOn.Name = parsedAttributes[keyOn];
		//                            break;
		//                        case "desc":
		//                            channelOn.Description = parsedAttributes[keyOn];
		//                            break;
		//                        case "artist":
		//                            channelOn.CurrentArtist = parsedAttributes[keyOn];
		//                            break;
		//                        case "song":
		//                            channelOn.CurrentSong = parsedAttributes[keyOn];
		//                            break;
		//                        case "album":
		//                            channelOn.CurrentAlbum = parsedAttributes[keyOn];
		//                            break;
		//                    }
		//                }
					
		//            }
		//        }

		//    }

		//    return true;
		//}


	}
}
