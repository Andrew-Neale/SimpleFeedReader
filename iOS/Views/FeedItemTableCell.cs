using System;

using Foundation;
using SimpleFeedReader.Feeds;
using UIKit;

namespace SimpleFeedReader
{
    public partial class FeedItemTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("FeedItemTableCell");
        public static readonly UINib Nib;


        static FeedItemTableCell()
        {
            Nib = UINib.FromName("FeedItemTableCell", NSBundle.MainBundle);
        }

        protected FeedItemTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initiallogic.
        }

        public void UpdateCell(RssFeedItem item)
        {
            Title.Text = item.Title;
            Description.Text = item.Description;
        }

    }
}
