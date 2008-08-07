using System;
using System.Collections.Generic;
using System.Text;

namespace XMSharp
{
	public class XMChannelList : List<XMChannel>
	{
        private static bool cacheImagesLocally = true;
        private static string localImageCacheLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData).TrimEnd("\\".ToCharArray()) + "\\XMSharp\\";

		public XMChannelList()
		{
			
		}

        public static bool CacheImagesLocally
        {
            get { return cacheImagesLocally; }
            set { cacheImagesLocally = value; }
        }

        public static string LocalImageCacheLocation
        {
            get 
            {
                if (!System.IO.Directory.Exists(localImageCacheLocation))
                    System.IO.Directory.CreateDirectory(localImageCacheLocation);
                
                return localImageCacheLocation; 
            }
            set { localImageCacheLocation = value; }
        }

        public void SortByNumber(bool ascending)
        {
            this.Sort(new XMChannelNumberComparer(ascending));
        }

        public void SortByGenre(bool ascending)
        {

        }

		public new XMChannel this[int channelNumber]
		{
			get
			{
				XMChannel result = null;

				foreach (XMChannel channel in this)
				{
					if (channel.Number == channelNumber)
					{
						result = channel;
						break;
					}
				}

				return result;
			}

			set
			{
				XMChannel toChange = null;

				foreach (XMChannel channel in this)
				{
					if (channel.Number == channelNumber)
					{
						toChange = channel;
						break;
					}
				}

				if (toChange != null)
					toChange = value;
			}
		}


	}
}
