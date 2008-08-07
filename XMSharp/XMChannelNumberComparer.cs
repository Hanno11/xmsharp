using System;
using System.Collections.Generic;
using System.Text;

namespace XMSharp
{
	public class XMChannelNumberComparer : IComparer<XMChannel>
	{
        bool ascending = false;

        public XMChannelNumberComparer(bool sortAscending)
        {
            ascending = sortAscending;
        }

        public XMChannelNumberComparer()
        {
            ascending = false;
        }
		#region IComparer<XMChannel> Members

		public int Compare(XMChannel x, XMChannel y)
		{
            if (ascending)
                return x.Number.CompareTo(y.Number);
            else
                return y.Number.CompareTo(x.Number);
		}

		#endregion
	}
}
