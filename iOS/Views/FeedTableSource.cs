using System;
using System.Collections.Generic;
using Foundation;
using SimpleFeedReader.Feeds;
using UIKit;

namespace SimpleFeedReader.iOS.Views
{
    public class FeedTableSource : UITableViewSource
    {
        private readonly List<RssFeed> _feeds;
        private readonly MasterViewController _controller;
        private string CellIdentifier = "Cell";

        public FeedTableSource(List<RssFeed> feeds, MasterViewController controller)
        {
            _feeds = feeds;
            _controller = controller;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

            cell.TextLabel.Text = _feeds[indexPath.Row].Title;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _feeds.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            System.Diagnostics.Debug.WriteLine("Clicked - " + _feeds[indexPath.Row].Title);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                _controller.DetailViewController.SetDetailItem(_feeds[indexPath.Row].FeedItems);
            }
        }
    }
}
