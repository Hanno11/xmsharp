using System;
using System.Collections.Generic;
using System.Text;
using MediaPortal.GUI.Library;
using MediaPortal.Configuration;
using MediaPortal.Profile;
using MediaPortal.Dialogs;

namespace XMSharp.MP
{
	public class XMSharpMP : GUIWindow, ISetupForm
	{
		public const string DefaultMenuName = "XM Radio";
		private XMSharp.XMPlayer player = new XMPlayer();
			       
		#region ISetupForm Members

		// Returns the name of the plugin which is shown in the plugin menu
		public string PluginName()
		{
            return "XMSharp MP";
		}

		// Returns the description of the plugin is shown in the plugin menu
		public string Description()
		{
			return "XM Radio Plugin for MediaPortal using Internet Streaming";
		}

		// Returns the author of the plugin which is shown in the plugin menu
		public string Author()
		{
			return "Redth";
		}

		// show the setup dialog
		public void ShowPlugin()
		{
			frmSetup fs = new frmSetup();
			if (fs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
			
			}

		}

		// Indicates whether plugin can be enabled/disabled
		public bool CanEnable()
		{
			return true;
		}

		// Get Windows-ID
		public int GetWindowId()
		{
			// WindowID of windowplugin belonging to this setup
			// enter your own unique code
			return 89552;
		}

		// Indicates if plugin is enabled by default;
		public bool DefaultEnabled()
		{
			return true;
		}

		// indicates if a plugin has it's own setup screen
		public bool HasSetup()
		{
			return true;
		}

		/// <summary>
		/// If the plugin should have it's own button on the main menu of MediaPortal then it
		/// should return true to this method, otherwise if it should not be on home
		/// it should return false
		/// </summary>
		/// <param name="strButtonText">text the button should have</param>
		/// <param name="strButtonImage">image for the button, or empty for default</param>
		/// <param name="strButtonImageFocus">image for the button, or empty for default</param>
		/// <param name="strPictureImage">subpicture for the button or empty for none</param>
		/// <returns>true : plugin needs it's own button on home
		/// false : plugin does not need it's own button on home</returns>

		public bool GetHome(out string strButtonText, out string strButtonImage, out string strButtonImageFocus, out string strPictureImage)
		{
            using (Settings settings = new Settings(Config.GetFile(Config.Dir.Config, "xmsharpmp.xml")))
            {
                strButtonText = settings.GetValueAsString("XMSharpMP", "MenuName", DefaultMenuName);
            }

			strButtonImage = String.Empty;
			strButtonImageFocus = String.Empty;
			strPictureImage = String.Empty;
			return true;
		}

		// With GetID it will be an window-plugin / otherwise a process-plugin
		// Enter the id number here again
		public override int GetID
		{
			get
			{
				return 89552;
			}

			set
			{
			}
		}

		#endregion

		#region Skin Controls
		[SkinControlAttribute(45)]
        protected GUIImage imgNP = null;

		[SkinControlAttribute(7)]
		protected GUIButtonControl buttonRefresh = null;

        [SkinControlAttribute(8)]
        protected GUIButtonControl buttonPreset1 = null;

        [SkinControlAttribute(9)]
        protected GUIButtonControl buttonPreset2 = null;

        [SkinControlAttribute(10)]
        protected GUIButtonControl buttonPreset3 = null;

        [SkinControlAttribute(11)]
        protected GUIButtonControl buttonPreset4 = null;

        [SkinControlAttribute(12)]
        protected GUIButtonControl buttonPreset5 = null;

        [SkinControlAttribute(13)]
        protected GUIButtonControl buttonPreset6 = null;

        [SkinControlAttribute(44)]
        protected GUIListControl listChannels = null;

        [SkinControlAttribute(3)]
        protected GUIFadeLabel labelNPChannel = null;

        [SkinControlAttribute(4)]
        protected GUIFadeLabel labelNPArtist = null;

        [SkinControlAttribute(5)]
        protected GUIFadeLabel labelNPAlbum = null;

        [SkinControlAttribute(6)]
        protected GUIFadeLabel labelNPSong = null;
		#endregion

		void logException(string methodName, Exception ex)
		{

		}

		public override bool Init()
        {
			bool result = false;

			try { result = Load(Config.GetFile(Config.Dir.Skin, "XMSharpMP", "xmsharpmp.xml")); }
			catch (Exception ex) { result = false; logException("Init", ex); }

                        return false;
        }

		void player_OnChannelInfoChanged(List<XMChannel> changedChannels)
		{
			try { bindChannels(); }
			catch (Exception ex) { logException("player_OnChannelInfoChanged", ex); }
			////Loop through all of the 'changed' channels
			//foreach (XMChannel chanOn in changedChannels)
			//{
			//    GUIListItem editItem = null;
			//    bool newItem = true;

			//    foreach (GUIListItem item in this.listChannels.ListItems)
			//    {
			//        if ((int)item.TVTag == chanOn.Number)
			//        {
			//            editItem = item;
			//            newItem = false;
			//            break;						
			//        }
			//    }

			//    if (newItem)
			//        editItem = new GUIListItem();

			//    //Figure out our channel padding so the names appear at same place always
			//    string pad = string.Empty;

			//    for (int i = 0; i < 3 - chanOn.Number.ToString().Length; i++)
			//        pad += " ";

			//    string label = chanOn.Number + ". " + pad + chanOn.Name;

			//    editItem.OnItemSelected += new GUIListItem.ItemSelectedHandler(item_OnItemSelected);

			//    //Store the int channel number in this unused tag
			//    editItem.TVTag = chanOn.Number;
			//    editItem.Label = label;
			//    editItem.Label2 = chanOn.CurrentArtist; //Other info
			//    editItem.Label3 = chanOn.CurrentSong;
			//    editItem.IsFolder = false; //Not a folder

			//    //This will invoke finding the icon if it's not loaded
			//    if (chanOn.Icon != null)
			//        editItem.IconImage = chanOn.IconFilename;

			//    //New items need to be added!
			//    if (newItem)
			//    {
			//        int insertIndex = 0;

			//        for (int i = 0; i < listChannels.ListItems.Count; i++)
			//        {
			//            int chn = (int)listChannels.ListItems[i].TVTag;

			//            if (chanOn.Number >= chn)
			//                insertIndex = i;
			//        }

			//        if (insertIndex + 1 > listChannels.ListItems.Count)
			//            this.listChannels.ListItems.Add(editItem);
			//        else
			//            this.listChannels.ListItems.Insert(insertIndex, editItem);
			//    }


			//    if (chanOn.Number == player.CurrentChannel.Number)
			//    {
			//        labelNPChannel.Label = player.CurrentChannel.Name;
			//        labelNPArtist.Label = player.CurrentChannel.CurrentArtist;
			//        labelNPAlbum.Label = player.CurrentChannel.CurrentAlbum;
			//        labelNPSong.Label = player.CurrentChannel.CurrentSong;

			//        if (player.CurrentChannel.Logo != null)
			//            imgNP.SetFileName(player.CurrentChannel.LogoFilename);
			//    }


			//}
		}

        public override void DeInit()
        {
			try
			{
				player.Stop();
				base.DeInit();
			}
			catch (Exception ex)
			{
				logException("DeInit", ex);
			}
        }

        
		protected override void OnPageLoad()
		{
			player.OnChannelInfoChanged += new XMPlayer.ChannelInfoChangedDelegate(player_OnChannelInfoChanged);

			base.OnPageLoad();
			          
            loadSettings();

			if (player.Login())
			{
				//Load channels
                player.UpdateChannels(true);
                player.UpdateChannelNeighborhoods();
                
                //bindChannels();				
			}
			else
			{
				//No login, show error
								
				GUIDialogOK dlgOK = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
				if (dlgOK != null)
				{
					dlgOK.SetHeading("Login Error" /* or Message */);
					dlgOK.SetLine(1, "There was an Error logging in!");
					dlgOK.SetLine(2, "Please go to the plugin Configuration and ensure that");
					dlgOK.SetLine(3, "you have setup a valid XM Radio Online Listening Account!");
					dlgOK.DoModal(this.GetID);
				}

			}

            
	
		}

		protected override void OnPageDestroy(int new_windowId)
		{
            player.Stop();
			base.OnPageDestroy(new_windowId);

		}

		protected override void OnShowContextMenu()
		{
			base.OnShowContextMenu();

			if (this.GetFocusControlId() == listChannels.GetID)
			{
				GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEM_SELECTED, GetID, 0, listChannels.GetID, 0, 0, null);
				OnMessage(msg);
				int iItem = (int)msg.Param1;

				//Get teh channel # from the item's tvtag
				int chanNum = (int)this.listChannels.ListItems[iItem].TVTag;

				//Get the actual channel
                XMChannel chan = null;

                chan = player.Channels[chanNum];
                
                if (chan == null)
                    return;

				GUIDialogMenu dlgMenu = (GUIDialogMenu)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
				if (dlgMenu != null)
				{
					dlgMenu.Reset();
					dlgMenu.SetHeading("Choose a Preset: ");
					dlgMenu.Height = Convert.ToInt32(this.Height * 0.80);

					//dlgMenu.Add(chan.Number.ToString() + ". " + chan.Name);
					dlgMenu.Add("Preset 1");
					dlgMenu.Add("Preset 2");
					dlgMenu.Add("Preset 3");
					dlgMenu.Add("Preset 4");
					dlgMenu.Add("Preset 5");
					dlgMenu.Add("Preset 6");
					
					dlgMenu.DoModal(this.GetID);

					if (dlgMenu.SelectedLabel != -1) // Nothing was selected
					{
						using (Settings settings = new Settings(Config.GetFile(Config.Dir.Config, "xmsharpmp.xml")))
						{
							settings.SetValue("XMSharpMP", "Preset" + (dlgMenu.SelectedLabel + 1).ToString(), chan.Number);
						}

						loadSettings();
						bindChannels();
					}
				}

			}
			
		}

		protected override void OnClicked(int controlId, GUIControl control, MediaPortal.GUI.Library.Action.ActionType actionType)
		{
			if (control == buttonRefresh)
				OnButtonRefresh();

			if (actionType == Action.ActionType.ACTION_NEXT_CHANNEL || actionType == Action.ActionType.ACTION_NEXT_ITEM)
			{
				player.NextChannel();
			}
			else if (actionType == Action.ActionType.ACTION_PREV_CHANNEL || actionType == Action.ActionType.ACTION_PREV_ITEM)
			{
				player.PreviousChannel();
			}
			else if (actionType == Action.ActionType.ACTION_PLAY)
			{
				if (player.CurrentChannel != null)
					player.ChangeChannel(player.CurrentChannel);
			}
			else if (actionType == Action.ActionType.ACTION_STOP || actionType == Action.ActionType.ACTION_PAUSE)
			{
				player.Stop();
			}
			

            if (control == listChannels)
            {
                if (actionType == Action.ActionType.ACTION_SELECT_ITEM)
                {
					//Get which item is selected
                    GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEM_SELECTED, GetID, 0, controlId, 0, 0, null);
                    OnMessage(msg);
                    int iItem = (int)msg.Param1;

					//Get teh channel # from the item's tvtag
                    int chanNum = (int)this.listChannels.ListItems[iItem].TVTag;

					//Get the actual channel
                    XMChannel chan = player.Channels[chanNum];

                    if (chan != null)
                    {
						//Change channels
                        player.ChangeChannel(chan);
						//Update the channel
                        player.UpdateCurrentChannel();

						//Change now playing info
                        labelNPChannel.Label = player.CurrentChannel.Name;
                        labelNPArtist.Label = player.CurrentChannel.CurrentArtist;
                        labelNPAlbum.Label = player.CurrentChannel.CurrentAlbum;
                        labelNPSong.Label = player.CurrentChannel.CurrentSong;
    
                        if (player.CurrentChannel.Logo != null)
                            imgNP.SetFileName(player.CurrentChannel.LogoFilename);

                    }

                }
				   
				
			}
			else if (control == buttonPreset1)
			{
				processPreset((int)buttonPreset1.Data);
			}
			else if (control == buttonPreset2)
			{
				processPreset((int)buttonPreset2.Data);
			}
			else if (control == buttonPreset3)
			{
				processPreset((int)buttonPreset3.Data);
			}
			else if (control == buttonPreset4)
			{
				processPreset((int)buttonPreset4.Data);
			}
			else if (control == buttonPreset5)
			{
				processPreset((int)buttonPreset5.Data);
			}
			else if (control == buttonPreset6)
			{
				processPreset((int)buttonPreset6.Data);
			}
           

			base.OnClicked(controlId, control, actionType);
		}

		private void processPreset(int chanNum)
		{
			XMChannel chan = player.Channels[chanNum];

			if (chan != null)
				player.ChangeChannel(chan);
		}

		private void OnButtonRefresh()
		{
            player.UpdateChannels();
           // bindChannels();
		}

		private void OnButtonTwo()
		{
		}


		private void loadSettings()
		{
			using (Settings settings = new Settings(Config.GetFile(Config.Dir.Config, "xmsharpmp.xml")))
			{

				player.Email = settings.GetValueAsString("XMSharpMP", "AccountEmail", string.Empty);
				player.Password = settings.GetValueAsString("XMSharpMP", "AccountPassword", string.Empty);
				player.LoginTimeout = settings.GetValueAsInt("XMSharpMP", "AccountTimeout", 5);
				
				//player.Quality = (StreamQualityType)Enum.Parse(typeof(StreamQualityType), settings.GetValueAsString("XMSharpMP", "StreamQuality", "High"));

                this.buttonPreset1.Data = settings.GetValueAsInt("XMSharpMP", "Preset1", -1);
                this.buttonPreset2.Data = settings.GetValueAsInt("XMSharpMP", "Preset2", -1);
                this.buttonPreset3.Data = settings.GetValueAsInt("XMSharpMP", "Preset3", -1);
                this.buttonPreset4.Data = settings.GetValueAsInt("XMSharpMP", "Preset4", -1);
                this.buttonPreset5.Data = settings.GetValueAsInt("XMSharpMP", "Preset5", -1);
                this.buttonPreset6.Data = settings.GetValueAsInt("XMSharpMP", "Preset6", -1);
                
			}
		}

		private void bindChannels()
		{
			//Sort the channels first, and clear existing list items
			player.Channels.SortByNumber(true);
			this.listChannels.ListItems.Clear();

			//Loop through the channels
			foreach (XMChannel chan in player.Channels)
			{
				//Figure out our channel padding so the names appear at same place always
				string pad = string.Empty;

				for (int i = 0; i < 3 - chan.Number.ToString().Length; i++)
					pad += " ";

				string label = chan.Number + ". " + pad + chan.Name;


				//New item
				GUIListItem item = new GUIListItem(label);
				//Handle its selected event
				item.OnItemSelected += new GUIListItem.ItemSelectedHandler(item_OnItemSelected);

				//Store the int channel number in this unused tag
				item.TVTag = chan.Number;
				item.Label2 = chan.CurrentArtist; //Other info
				item.Label3 = chan.CurrentSong;
				item.IsFolder = false; //Not a folder

				//This will invoke finding the icon if it's not loaded
				if (chan.Icon != null)
					item.IconImage = chan.IconFilename;

				//Add to our list finally!
				this.listChannels.ListItems.Add(item);
			}


			if (player.CurrentChannel != null)
			{
				labelNPChannel.Label = player.CurrentChannel.Name;
				labelNPArtist.Label = player.CurrentChannel.CurrentArtist;
				labelNPAlbum.Label = player.CurrentChannel.CurrentAlbum;
				labelNPSong.Label = player.CurrentChannel.CurrentSong;

				if (player.CurrentChannel.Logo != null)
					imgNP.SetFileName(player.CurrentChannel.LogoFilename);
			}


			//Update our presets in case channel names changed
			XMChannel preset1 = player.Channels[(int)this.buttonPreset1.Data];
			XMChannel preset2 = player.Channels[(int)this.buttonPreset2.Data];
			XMChannel preset3 = player.Channels[(int)this.buttonPreset3.Data];
			XMChannel preset4 = player.Channels[(int)this.buttonPreset4.Data];
			XMChannel preset5 = player.Channels[(int)this.buttonPreset5.Data];
			XMChannel preset6 = player.Channels[(int)this.buttonPreset6.Data];

			if (preset1 != null)
				this.buttonPreset1.Label = preset1.Name;
			else
				this.buttonPreset1.Label = "Preset 1";

			if (preset2 != null)
				this.buttonPreset2.Label = preset2.Name;
			else
				this.buttonPreset2.Label = "Preset 2";

			if (preset3 != null)
				this.buttonPreset3.Label = preset3.Name;
			else
				this.buttonPreset3.Label = "Preset 3";

			if (preset4 != null)
				this.buttonPreset4.Label = preset4.Name;
			else
				this.buttonPreset4.Label = "Preset 4";

			if (preset5 != null)
				this.buttonPreset5.Label = preset5.Name;
			else
				this.buttonPreset5.Label = "Preset 5";

			if (preset6 != null)
				this.buttonPreset6.Label = preset6.Name;
			else
				this.buttonPreset6.Label = "Preset 6";


		}

        
        void item_OnItemSelected(GUIListItem item, GUIControl parent)
        {
           
        }

        
        
	}

    
}
