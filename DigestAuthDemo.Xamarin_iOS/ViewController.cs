using System;
using System.Threading.Tasks;
using CoreFoundation;
using DigestAuthDemo.Common;
using UIKit;

namespace DigestAuthDemo
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }

        partial void OnClearClecked(Foundation.NSObject sender)
            => Txt.Text = string.Empty;

        partial void OnHttpClientClicked(Foundation.NSObject sender)
            => Task.Factory.StartNew(() => MakeRequestImpl(new ClientBasedHttpBinService()));

        partial void OnNsUrlSessionClicked(Foundation.NSObject sender)
            => Task.Factory.StartNew(() => MakeRequestImpl(new NSUrlSessionBasedHttpBinService()));

        private async Task MakeRequestImpl(IHttpBinService service)
        {
            try
            {
                var response = await service.GetData();
                DispatchQueue.MainQueue.DispatchAsync(() => Txt.Text = response);
            }
            catch (Exception exc)
            {
                DispatchQueue.MainQueue.DispatchAsync(() => Txt.Text = "Error");
            }
        }
    }
}
