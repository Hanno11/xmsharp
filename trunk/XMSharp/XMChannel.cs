using System;
using System.Collections.Generic;
using System.Text;

namespace XMSharp
{
	public class XMChannel
	{
        private int number = -1;
        private string name = string.Empty;
        private string description = string.Empty;
        private string neighborhood = string.Empty;
        private string currentArtist;
        private string currentAlbum = string.Empty;
        private string currentSong = string.Empty;
        private int currentArtistID = -1;
        private int currentAlbumID = -1;
        private string currentLinkUrl = string.Empty;

		public XMChannel()
		{			
		}

        public int Number
		{
            get { return number; }
            set { number = value; }
		}

		public string Name
		{
            get { return name; }
            set { name = value; }
		}

		public string Description
		{
            get { return description; }
            set { description = value; }
		}

        public string Neighborhood
        {
            get { return neighborhood; }
            set { neighborhood = value; }
        }

		public string CurrentArtist
		{
            get { return currentArtist; }
            set { currentArtist = value; }
		}

		public string CurrentAlbum
		{
            get { return currentAlbum; }
            set { currentAlbum = value; }
		}

		public string CurrentSong
		{
            get { return currentSong; }
            set { currentSong = value; }
		}

		public int CurrentArtistID
		{
            get { return currentArtistID; }
            set { currentArtistID = value; }
		}

		public int CurrentAlbumID
		{
            get { return currentAlbumID; }
            set { currentAlbumID = value; }
		}

		public string CurrentLinkUrl
		{
            get { return currentLinkUrl; }
            set { currentLinkUrl = value; }
		}

        public string LogoFilename
        {
            get
            {
                string imgFile = XMChannelList.LocalImageCacheLocation + "Logo" + this.Number.ToString() + this.Name + ".jpg";

                if (System.IO.File.Exists(imgFile))
                    return imgFile;
                else
                    return string.Empty;
            }
        }

        public string IconFilename
        {
            get
            {
                string imgFile = XMChannelList.LocalImageCacheLocation + "Icon" + this.Number.ToString() + this.Name + ".gif";

                if (System.IO.File.Exists(imgFile))
                    return imgFile;
                else
                    return string.Empty;
            }
        }

        public string MainFilename
        {
            get
            {
                string imgFile = XMChannelList.LocalImageCacheLocation + "Main" + this.Number.ToString() + this.Name + ".jpg";

                if (System.IO.File.Exists(imgFile))
                    return imgFile;
                else 
                    return string.Empty;
            }
        }

        public void PreloadImages()
        {
            System.Drawing.Image imgLogo = Logo;
            System.Drawing.Image imgIcon = Icon;
            System.Drawing.Image imgMain = Main;
        }

		public System.Drawing.Image Logo
		{
			get
			{
                string imgKey = "Logo" + this.Number.ToString() + this.Name + ".jpg";

                if (!XMPlayer.ChannelImages.ContainsKey(imgKey))
                {
                    System.Drawing.Image img = null;

                    if (XMChannelList.CacheImagesLocally)
                    {
                        if (System.IO.File.Exists(XMChannelList.LocalImageCacheLocation + imgKey))
                            img = System.Drawing.Image.FromFile(XMChannelList.LocalImageCacheLocation + imgKey);
                    }

                    if (img == null)
                        img = Utility.Http.GetImage("http://www.xmradio.ca/images/channels/logos/large/" + this.Number.ToString() + ".jpg");

                    if (img != null && XMChannelList.CacheImagesLocally)
                        saveImage(img, XMChannelList.LocalImageCacheLocation + imgKey);
               
                    XMPlayer.ChannelImages.Add(imgKey, img);
                }

				return XMPlayer.ChannelImages[imgKey];
			}
		}

		public System.Drawing.Image Icon
		{
            get
			{
                string imgKey = "Icon" + this.Number.ToString() + this.Name + ".gif";

                if (!XMPlayer.ChannelImages.ContainsKey(imgKey))
                {
                    System.Drawing.Image img = null;

                    if (XMChannelList.CacheImagesLocally)
                    {
                        if (System.IO.File.Exists(XMChannelList.LocalImageCacheLocation + imgKey))
                            img = System.Drawing.Image.FromFile(XMChannelList.LocalImageCacheLocation + imgKey);
                    }

                    if (img == null)
                        img = Utility.Http.GetImage("http://www.xmradio.ca/images/channels/logos/small/" + this.Number.ToString() + ".gif");

                    if (img != null && XMChannelList.CacheImagesLocally)
                        saveImage(img, XMChannelList.LocalImageCacheLocation + imgKey);
                   
                    XMPlayer.ChannelImages.Add(imgKey, img);
                }

				return XMPlayer.ChannelImages[imgKey];
			}
		}

		public System.Drawing.Image Main
		{
            get
			{
                string imgKey = "Main" + this.Number.ToString() + this.Name + ".jpg";

                if (!XMPlayer.ChannelImages.ContainsKey(imgKey))
                {
                    System.Drawing.Image img = null;

                    if (XMChannelList.CacheImagesLocally)
                    {
                        if (System.IO.File.Exists(XMChannelList.LocalImageCacheLocation + imgKey))
                            img = System.Drawing.Image.FromFile(XMChannelList.LocalImageCacheLocation + imgKey);
                    }

                    if (img == null)
                        img = Utility.Http.GetImage("http://www.xmradio.ca/images/channels/logos/main/" + this.Number.ToString() + ".jpg");

                    if (img != null && XMChannelList.CacheImagesLocally)
                        saveImage(img, XMChannelList.LocalImageCacheLocation + imgKey);
               
                    XMPlayer.ChannelImages.Add(imgKey, img);
                }

				return XMPlayer.ChannelImages[imgKey];
			}
		}

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Number: " + Number.ToString() + "; ");
            result.Append("Name: " + Name + "; ");
            result.Append("Neighborhood: " + Neighborhood + "; ");
            result.Append("CurrentArtist: " + CurrentArtist + "; ");
            result.Append("CurrentAlbum: " + CurrentAlbum + "; ");
            result.Append("CurrentSong: " + CurrentSong + ";");

            return result.ToString();
        }

        private void saveImage(System.Drawing.Image img, string path)
        {
            try
            {
                img.Save(path);
            }
            catch { }
        }
	}
}
