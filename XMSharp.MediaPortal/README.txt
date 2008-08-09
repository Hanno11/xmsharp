XMSharp MP
=================
XM Radio Plugin for MediaPortal using Internet Streaming

http://code.google.com/p/xmsharp/ is the official home for this project!


Installation:
=================
Copy all the files except the 'skins' folder into your 'Team MediaPortal\Plugins\windows\' folder.
From the appropriate skin folder inside the 'skins' folder, copy the xmsharpmp.xml file into the corresponding
folder in your 'Team MediaPortal\Plugins\skins\<skinName>\' folder.

NOTE: You MUST have VideoLAN VLC installed for this plugin to work, INCLUDING the ActiveX Control for VLC.
You are given a choice to install the ActiveX Control during the VideoLAN VLC installation.
If you do not have these installed, you will see a type of 'Incompatible Version' error in MediaPortal!

Releases:
=================

09-Aug-2008   v.1.0.4
 - Added thread locking on the channels list in XMSharp so MediaPortal shouldn't crash from
   List modification/enumeration conflicts
 - Fixed the navigation order for Preset 3 button in all skins

08-Aug-2008   v.1.0.2
 - Initial Release
 - Basic Channel List with images
 - Now Playing Information
 - Current Song/Artist/Album automatically updates for all channels
 - Support for BlueTwo, BlueTwoWide, Xface, Monochrome Wide, AeonWide skins