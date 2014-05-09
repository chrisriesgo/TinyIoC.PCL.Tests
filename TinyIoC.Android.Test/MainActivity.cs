using Android.App;
using Android.Widget;
using Android.OS;
using TinyIoC;
using TinyMessenger;
using TinyIoC.PCL;

namespace TinyIoC.Android.Test
{
	[Activity(Label = "TinyIoC.Android.Test", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		Button button;

		ITinyMessengerHub _messengerHub;

		TinyMessageSubscriptionToken _assertToken;

		public MainActivity()
		{
			_messengerHub = TinyIoCContainer.Current.Resolve<ITinyMessengerHub>();
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			button = FindViewById<Button>(Resource.Id.myButton);
			var assert = new Assert();

			button.Click += delegate
			{
				assert.SendMessage();
			};
		}

		protected override void OnResume()
		{
			base.OnResume();

			_assertToken = _messengerHub.Subscribe<AssertMessage>(AssertMessageReceived);
		}

		protected override void OnPause()
		{
			base.OnPause();

			_assertToken.Dispose();
		}

		void AssertMessageReceived(AssertMessage message)
		{
			button.Text = string.Format("{0} clicks!", count++);
		}
	}
}


