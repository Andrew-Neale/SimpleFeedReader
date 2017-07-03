// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SimpleFeedReader
{
    [Register ("MasterViewController")]
    partial class MasterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SimpleFeedReader.FeedTable FeedTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FeedTable != null) {
                FeedTable.Dispose ();
                FeedTable = null;
            }

            if (LoadingIndicator != null) {
                LoadingIndicator.Dispose ();
                LoadingIndicator = null;
            }
        }
    }
}