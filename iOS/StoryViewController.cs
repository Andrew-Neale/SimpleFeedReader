using System;
using Foundation;
using UIKit;

namespace SimpleFeedReader
{
    public partial class StoryViewController : UIViewController
    {
        public string Uri {get;set;}

        public StoryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoadingIndicator.Center = View.Center;
            LoadingIndicator.StartAnimating();

            WebView.ScalesPageToFit = true;

            WebView.ContentMode = UIViewContentMode.ScaleAspectFit;
            WebView.LoadFinished += WebViewLoadFinished;
           
            if (NetworkStatus.IsDataConnectionAvailable())
            {
                WebView.LoadRequest(new NSUrlRequest(new NSUrl(Uri)));
                WebView.SizeToFit();
            }
            else
            {
                // note this this will thorw a native exception if the cache web page does not exist.
                // cannot find a work around at the moment
                WebView.LoadRequest(new NSUrlRequest(new NSUrl(Uri), NSUrlRequestCachePolicy.ReturnCacheDataDoNotLoad, 60));
                WebView.SizeToFit();
            }

            WebView.Frame = View.Bounds;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            WebView.LoadFinished -= WebViewLoadFinished;
        }

        private void WebViewLoadFinished(object sender, EventArgs e)
        {
            LoadingIndicator.StopAnimating();
        }
    }
}