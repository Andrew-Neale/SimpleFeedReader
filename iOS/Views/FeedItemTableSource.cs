using System;
using System.Collections.Generic;
using Foundation;
using SimpleFeedReader.Feeds;
using UIKit;

namespace SimpleFeedReader.iOS.Views
{
    public class FeedItemTableSource : UITableViewSource
    {
        private readonly List<RssFeedItem> _items;
        private readonly DetailViewController _controller;
        private readonly NSString CellIdentifier = new NSString("Cell");

        public string SeletecedStory { get; private set; }

        public FeedItemTableSource(List<RssFeedItem> items, DetailViewController controller)
        {
            _items = items;
            _controller = controller;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath) as FeedItemTableCell;

            cell.UpdateCell(_items[indexPath.Row]);
                                   
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (_items == null)
            {
                return 0;
            }

            return _items.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            System.Diagnostics.Debug.WriteLine("Clicked - " + _items[indexPath.Row].Title);

            SeletecedStory = _items[indexPath.Row].Link;
            _controller.PerformSegue("ShowStory", this);
        }
    }
}
