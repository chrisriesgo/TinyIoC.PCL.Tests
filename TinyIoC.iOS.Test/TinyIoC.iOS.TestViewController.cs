using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TinyIoC;
using TinyMessenger;
using TinyIoC.PCL;

namespace TinyIoC.iOS.Test
{
	public partial class TinyIoC_iOS_TestViewController : UIViewController
	{
		UIButton button;
		int count = 1;
		ITinyMessengerHub _messengerHub;
		TinyMessageSubscriptionToken _assertToken;

		public TinyIoC_iOS_TestViewController(IntPtr handle) : base(handle)
		{
			_messengerHub = TinyIoCContainer.Current.Resolve<ITinyMessengerHub>();
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			button = new UIButton () {
				Frame = new RectangleF (0, 0, View.Bounds.Width / 2, 44),
				BackgroundColor =  UIColor.Gray
			};

			button.SetTitle ("Tap to Start...", UIControlState.Normal);
			button.TouchUpInside += HandleButtonTap;

			button.Center = View.Center;
			View.AddSubviews (button);

			_assertToken = _messengerHub.Subscribe<AssertMessage>(AssertMessageReceived);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			_assertToken.Dispose();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
		}

		#endregion

		void HandleButtonTap (object sender, EventArgs e)
		{
			var assert = new Assert();
			assert.SendMessage();
		}

		void AssertMessageReceived(AssertMessage message)
		{
			button.SetTitle(string.Format("{0} clicks!", count++), UIControlState.Normal);
		}
	}
}

