using System;
using Foundation;
using SimpleFeedReader.Cache;
using SimpleFeedReader.iOS.Views;
using SimpleFeedReader.ViewModels;
using UIKit;

namespace SimpleFeedReader
{
    public partial class MasterViewController : UITableViewController
    {
        //Ideally this should be done from Dependency injection framework
        private readonly FeedViewModel _feedViewModel = new FeedViewModel(new FeedRequest(), new FeedCache());

        public FeedViewModel ViewModel
        {
            get { return _feedViewModel; }
        }

        public DetailViewController DetailViewController { get; set; }

        protected MasterViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoadingIndicator.StartAnimating();
            Title = "RSS Feeds";

            DetailViewController = (DetailViewController)((UINavigationController)SplitViewController.ViewControllers[1]).TopViewController;

            await _feedViewModel.LoadFeeds();

            FeedTable.Source = new FeedTableSource(ViewModel.Feeds, this);

            InvokeOnMainThread(() =>
            {
                FeedTable.ReloadData();
            });

            HandleOffline();
            LoadingIndicator.StopAnimating();
            LoadingIndicator.Hidden = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            ClearsSelectionOnViewWillAppear = SplitViewController.Collapsed;
            base.ViewWillAppear(animated);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var controller = (DetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = _feedViewModel.Feeds[indexPath.Row].FeedItems;

                controller.SetDetailItem(item);
                controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
                controller.NavigationItem.LeftItemsSupplementBackButton = true;
            }
        }

        private void HandleOffline()
        {
            if (!NetworkStatus.IsDataConnectionAvailable())
            {
                var okAlertController = UIAlertController.Create("Info", "You are currently offline. Only displaying cached contents", UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

                PresentViewController(okAlertController, true, null);
            }
        }
    }
}
