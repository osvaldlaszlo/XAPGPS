// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace XAPGPS
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		MonoMac.WebKit.WebView browser { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton btnsend { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (browser != null) {
				browser.Dispose ();
				browser = null;
			}

			if (btnsend != null) {
				btnsend.Dispose ();
				btnsend = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
