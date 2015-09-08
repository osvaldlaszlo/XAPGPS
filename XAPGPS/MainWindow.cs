
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace XAPGPS
{
	public partial class MainWindow : MonoMac.AppKit.NSWindow
	{
		// Called when created from unmanaged code
		public MainWindow (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindow (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			this.Title = "Xamarin Android Player - GPS Helper";
			NSTimer.CreateScheduledTimer (1, delegate() {
				browser.CustomUserAgent  = "Mozilla/5.0 () AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53";
				browser.MainFrame.LoadRequest (NSUrlRequest.FromUrl (NSUrl.FromString ("https://maps.google.com")));
				btnsend.Activated += delegate(object sender, EventArgs e) {
					string X = browser.MainFrameUrl;
					X = X.Substring(X.IndexOf("@")+1);
					string[] LL = X.Split(new []{","},StringSplitOptions.RemoveEmptyEntries);
					if(LL.Length>2) {
						System.Diagnostics.Process P = new System.Diagnostics.Process();
						P.StartInfo.FileName="/usr/bin/vboxmanage";
						P.StartInfo.WorkingDirectory = "/tmp/";
						P.StartInfo.Arguments = " \"list\" \"runningvms\"";
						P.StartInfo.UseShellExecute = false;
						P.StartInfo.CreateNoWindow=true;
						P.StartInfo.RedirectStandardOutput = true;
						P.Start();
						string output = P.StandardOutput.ReadToEnd ();
						P.WaitForExit ();
						string UUID = output.Substring(output.IndexOf("{"));
						P.StartInfo.FileName="/usr/bin/VBoxManage";
						P.StartInfo.Arguments = " guestproperty enumerate " + UUID;
						P.Start();
						output = P.StandardOutput.ReadToEnd ();
						P.WaitForExit ();
						string IP = output.Substring(output.IndexOf("androvm_ip_management"));
						IP = IP.Substring(IP.IndexOf(":")+1);
						IP = IP.Substring(0,IP.IndexOf(",")).Trim();
						P.StartInfo.FileName =  Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ "/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb";
						P.StartInfo.Arguments = " \"-s\" \""+IP+":5555\" \"shell\" \"setprop\" \"xapd.gps.status\" \"enabled\"";
						P.Start();
						P.StartInfo.Arguments = " \"-s\" \""+IP+":5555\" \"shell\" \"setprop\" \"xapd.gps.longitude\" \""+LL[1]+"\"";
						P.Start();
						P.WaitForExit ();
						P.StartInfo.Arguments = " \"-s\" \""+IP+":5555\" \"shell\" \"setprop\" \"xapd.gps.latitude\" \""+LL[0]+"\"";
						P.Start();
						P.WaitForExit ();
					}
				};
			});
		}
	}
}

