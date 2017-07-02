// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SimpleFeedReader
{
    [Register ("CategoryTableViewController")]
    partial class CategoryTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SimpleFeedReader.CategoryTableView CategoryTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CategoryTableView != null) {
                CategoryTableView.Dispose ();
                CategoryTableView = null;
            }
        }
    }
}