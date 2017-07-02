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
    [Register ("DetailViewController")]
    partial class DetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView FeedITemsTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FeedITemsTable != null) {
                FeedITemsTable.Dispose ();
                FeedITemsTable = null;
            }
        }
    }
}