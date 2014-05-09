using TinyIoC;
using TinyMessenger;

namespace TinyIoC.PCL
{
	public class Assert
	{
		ITinyMessengerHub _messengerHub;
		public Assert()
		{
			_messengerHub = TinyIoCContainer.Current.Resolve<ITinyMessengerHub>();
		}

		public void SendMessage()
		{
			_messengerHub.Publish(new AssertMessage(this));
		}
	}
}

