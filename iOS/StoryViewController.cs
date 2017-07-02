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
            WebView.ScalesPageToFit = true;

            WebView.ContentMode = UIViewContentMode.ScaleAspectFit;
           
            if (NetworkStatus.IsDataConnectionAvailable())
            {
                WebView.LoadRequest(new NSUrlRequest(new NSUrl(Uri)));
                WebView.SizeToFit();
            }
            else
            {
                // note this this will thorw a native exception if the cache web page does not exist
                WebView.LoadRequest(new NSUrlRequest(new NSUrl(Uri), NSUrlRequestCachePolicy.ReturnCacheDataDoNotLoad, 60));
                WebView.SizeToFit();
            }

            WebView.Frame = View.Bounds;
        }
    }
}