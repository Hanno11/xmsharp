using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMSharp;

namespace XMSharp.Gui
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		XMPlayer player = new XMPlayer();
		

		private void frmMain_Load(object sender, EventArgs e)
		{
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

			player.OnChannelInfoChanged += new XMPlayer.ChannelInfoChangedDelegate(player_OnChannelInfoChanged);
            //player.Email = string.Empty;
			//player.Password = string.Empty;

            player.Email = "scuba264@gmail.com";
            player.Password = "changeme";

			player.Login();

			player.UpdateChannels();
			//bindChannels();
		}

		void player_OnChannelInfoChanged(List<XMChannel> changedChannels)
		{
			
			bool exists = false;

            //Trying this to stop the illegal thread activity messages
            //when updating channel info
            CheckForIllegalCrossThreadCalls = false;

			foreach (XMChannel chan in changedChannels)
			{
				Console.WriteLine(chan.ToString());

				foreach (object item in this.listItems.Items)
				{
					int chn = (int)item;

					if (chn == chan.Number)
					{
						exists = true;
						break;
					}

				}

				if (!exists)
				{
					int insertIndex = 0;

					for (int i = 0; i < this.listItems.Items.Count; i++)
					{
						int chn = (int)this.listItems.Items[i];

						if (chan.Number >= chn)
						{
							insertIndex = i;
							break;
						}
					}

					this.listItems.Items.Insert(insertIndex, chan.Number);
				}

				if (player.CurrentChannel != null && chan.Number == player.CurrentChannel.Number)
				{
					this.label1.Text = "Channel " + chan.Number.ToString() + ": " + chan.Name + " - " + chan.Description;
					this.labelCurrentAlbum.Text = chan.CurrentAlbum;
					this.labelCurrentArtist.Text = chan.CurrentArtist;
					this.labelCurrentSong.Text = chan.CurrentSong;
					this.picCurrentIcon.Image = chan.Logo;
				}
			}


			this.listItems.Invalidate();
		}

		//private void bindChannels()
		//{
		//    player.Channels.SortByNumber(true);

		//    foreach (XMChannel channel in player.Channels)
		//    {
		//        bool exists = false;


		//        foreach (object objOn in this.listItems.Items)
		//        {
		//            int chc = (int)objOn;

		//            if (chc == channel.Number)
		//            {
		//                exists = true;
		//                break;
		//            }
		//        }


		//        if (!exists)
		//        {
		//            int insertIndex = 0;

		//            for (int i = 0; i < this.listItems.Items.Count; i++)
		//            {
		//                int chc = (int)this.listItems.Items[i];

		//                if (channel.Number >= chc)
		//                {
		//                    insertIndex = i;
		//                    break;
		//                }
		//            }

		//            this.listItems.Items.Insert(insertIndex, channel.Number);
		//        }				
		//    }

		//    this.listItems.Invalidate();
		//}

	

		private void buttonRefreshAll_Click(object sender, EventArgs e)
		{
			player.UpdateChannels();
//			bindChannels();
		}


		private void buttonRefreshCurrent_Click(object sender, EventArgs e)
		{
			player.UpdateCurrentChannel();
			
		}

		

		private void listItems_DrawItem(object sender, DrawItemEventArgs e)
		{
			int chanNum = (int)this.listItems.Items[e.Index];

			XMChannel channel = player.Channels[chanNum];

			if (channel != null)	
			{
				if (this.listItems.SelectedItem != null && chanNum == (int)this.listItems.SelectedItem)
					e.Graphics.FillRectangle(new LinearGradientBrush(new Point(e.Bounds.Left, e.Bounds.Top), new Point(e.Bounds.Left, e.Bounds.Bottom), Color.FromArgb(50, 0, 75, 130), Color.WhiteSmoke), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
				else if (player.CurrentChannel != null && player.CurrentChannel.Number == chanNum)
					e.Graphics.FillRectangle(new LinearGradientBrush(new Point(e.Bounds.Left, e.Bounds.Top), new Point(e.Bounds.Left, e.Bounds.Bottom), Color.FromArgb(90, 255, 106, 0), Color.WhiteSmoke), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
				else
					e.Graphics.FillRectangle(new LinearGradientBrush(new Point(e.Bounds.Left, e.Bounds.Top), new Point(e.Bounds.Left, e.Bounds.Bottom), Color.LightGray, Color.WhiteSmoke), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
				
				e.Graphics.DrawRectangle(new Pen(Brushes.Gray, 1), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);


				if (channel.Icon != null)
					e.Graphics.DrawImage(channel.Icon, e.Bounds.Left + 5, e.Bounds.Top + 5);

				
				e.Graphics.DrawString(channel.Number.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.SlateGray, e.Bounds.Left + 65, e.Bounds.Top + 15);
				e.Graphics.DrawString(channel.Name + " (" + channel.Neighborhood + ")", new Font("Arial", 13, FontStyle.Bold), Brushes.Black, e.Bounds.Left + 115, e.Bounds.Top + 8);

				e.Graphics.DrawString(channel.CurrentSong + " by " + channel.CurrentArtist, new Font("Arial", 10, FontStyle.Bold), Brushes.DarkGray, e.Bounds.Left + 120, e.Bounds.Top + 28);

			}	
		}

		private void listItems_MouseClick(object sender, MouseEventArgs e)
		{
			
		}

		private void listItems_KeyPress(object sender, KeyPressEventArgs e)
		{
			
		}

		private void listItems_KeyDown(object sender, KeyEventArgs e)
		{
			
		}

		private void listItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			listItems.Invalidate();
		}

		private void listItems_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.listItems.SelectedItem != null)
			{
				int number = (int)this.listItems.SelectedItem;

				if (number > 0)
				{
					XMChannel channel = player.Channels[number];

					if (channel != null)
					{
						player.ChangeChannel(channel);
						player.UpdateCurrentChannel();
					}
				}
			}
		}

		private void timerUpdateCurrent_Tick(object sender, EventArgs e)
		{
			player.UpdateCurrentChannel();
		}

		private void timerUpdateAll_Tick(object sender, EventArgs e)
		{
			player.UpdateChannels();
		}

        private void buttonStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }
	}
}
