There are two 'projects' here.  One is the XMSharp C# library for developers to use in their own applications, the other is a MediaPortal plugin which uses the XMSharp C# library to make it possible for MediaPortal users to listen to XM Radio.



---



# 1. About XMSharp #
XMSharp is an XM Radio Internet Streaming library.  It handles logging in, fetching Channel information (including current song/artist/album for each channel and notifications of channel updates).  It uses VLC to playback the streams currently.

![http://content.screencast.com/users/Redth/folders/Jing/media/a56b6c48-21f9-4519-8ad8-84765e05781a/xmsharp-proj.png](http://content.screencast.com/users/Redth/folders/Jing/media/a56b6c48-21f9-4519-8ad8-84765e05781a/xmsharp-proj.png)

## XMSharp News ##
  * Source code moved to googlecode's svn server!



---



# 2. About XMSharp MediaPortal Plugin #
XMSharp was originally designed simply to be used in a MediaPortal plugin to allow XM Radio streaming.  This project was designed with using it in a MediaPortal plugin in mind, so the plugin project is also included.

![http://content.screencast.com/users/Redth/folders/Jing/media/93559fa4-e566-481d-86c4-fb10292cda15/xmsharp-mp-preview.png](http://content.screencast.com/users/Redth/folders/Jing/media/93559fa4-e566-481d-86c4-fb10292cda15/xmsharp-mp-preview.png)

## Installation ##
Copy all the files except the 'skins' folder into your 'Team MediaPortal\Plugins\windows\' folder.
From the appropriate skin folder inside the 'skins' folder, copy the xmsharpmp.xml file into the corresponding
folder in your 'Team MediaPortal\Plugins\skins\

&lt;skinName&gt;

\' folder.

NOTE: You must also install VideoLAN's VLC Player INCLUDING the ActiveX Control option during the installation.  You can get the installation here: http://www.videolan.org  If you do not install the ActiveX Control for VLC, you will get an Incompatible Version error when loading the plugin!

## News ##
### 09-Aug-2008 ###
  * Version: 1.0.4 Released
  * Added locking so multiple threads can access XMSharp's channels.  This should prevent MediaPortal from crashing hard as often as it was in the last release!
  * Fixed all the skin files where the navigation from the button for Preset 3 was not as expected.

### 08-Aug-2008 ###
  * Version: 1.0.2 Released
  * Some people are having issues with an incompatible plugin version error
  * I've put up v1.0.3 for download which I'm hoping will fix this
  * Please try the new download and if it does not work still, post in the forum or here in the issues

### 07-Aug-2008 ###
  * Uploaded first build/release of the MediaPortal Plugin
