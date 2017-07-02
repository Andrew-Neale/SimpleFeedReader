using System;
using System.Collections.Generic;
using SimpleFeedReader.Feeds;
using SimpleFeedReader.iOS.Views;
using UIKit;

namespace SimpleFeedReader
{
    public partial class DetailViewController : UIViewController
    {
        private readonly FeedItemsViewModel _feedViewModel = new FeedItemsViewModel();

        public FeedItemsViewModel ViewModel
        {
            get { return _feedViewModel; }
        }

        protected DetailViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetDetailItem(List<RssFeedItem> feedItems)
        {
            ViewModel.FeedItems = feedItems;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Stories";

            FeedITemsTable.ShowsVerticalScrollIndicator = true;
            FeedITemsTable.RegisterNibForCellReuse(FeedItemTableCell.Nib, "Cell");
            FeedITemsTable.Source = new FeedItemTableSource(ViewModel.FeedItems, this);
            FeedITemsTable.RowHeight = UITableView.AutomaticDimension;
            FeedITemsTable.EstimatedRowHeight = 90;
            FeedITemsTable.Frame = View.Bounds;

            InvokeOnMainThread(() =>
            {
                this.FeedITemsTable.ReloadData();
            });

            var categoryButton = new UIBarButtonItem("Filter", UIBarButtonItemStyle.Bordered, FilterByCategory);
            categoryButton.AccessibilityLabel = "filterCategory";
            NavigationItem.RightBarButtonItem = categoryButton;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.Identifier == "ShowStory")
            {
                var storyViewController = segue.DestinationViewController as StoryViewController;
                var itemTableSource = sender as FeedItemTableSource;

                if (storyViewController != null)
                {
                    storyViewController.Uri = itemTableSource.SeletecedStory;
                }
            }
            else if (segue.Identifier == "ShowCategories")
            {
                var categorryController = segue.DestinationViewController as CategoryTableViewController;
                ViewModel.ClearFilter();

                categorryController.CategorySelected += OnCategorySelected;

                if (categorryController != null)
                {
                    categorryController.SetupCategories(ViewModel.FeedItems);
                }
            }
        }

        private void BindFilteredListToTable(List<RssFeedItem> filteredList)
        {
            if (filteredList.Count > 0)
            {
                FeedITemsTable.Source = new FeedItemTableSource(filteredList, this);

                InvokeOnMainThread(() =>
                {
                    this.FeedITemsTable.ReloadData();
                });
            }
        }

        private void FilterByCategory(object sender, EventArgs args)
        {
            PerformSegue("ShowCategories", this);
        }

        private void OnCategorySelected(object sender, string e)
        {
            ViewModel.AddFilter(e);

            var filteredList = ViewModel.GetFilteredItems();

            BindFilteredListToTable(filteredList);
        }
    }
}

