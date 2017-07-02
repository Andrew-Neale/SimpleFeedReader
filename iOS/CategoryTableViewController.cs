using System;
using System.Collections.Generic;
using Foundation;
using SimpleFeedReader.Feeds;
using UIKit;

namespace SimpleFeedReader
{
    public partial class CategoryTableViewController : UITableViewController
    {
        public event EventHandler<string> CategorySelected;
        
        private List<string> _allCategories;
        private List<string> _selectedCategories = new List<string>();

        public CategoryTableViewController (IntPtr handle) : base (handle)
        {

        }

        public void SetupCategories(List<RssFeedItem> items)
        {
            _allCategories = GetAllCategories(items);
        }

       public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Category Filter";

            TableView.Source = new CategoryTableSource(_allCategories, null);

            InvokeOnMainThread(() =>
            {
				this.TableView.ReloadData();
            });
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            System.Diagnostics.Debug.WriteLine("Clicked - " + _allCategories[indexPath.Row]);

            // Indicate selection of cell
	        var cell = tableView.CellAt(indexPath);
            cell.Highlighted = true;    

            // Let controller know of selected category
            var catSelectedEvent = CategorySelected;
            if (catSelectedEvent != null)
            {
                catSelectedEvent(this, _allCategories[indexPath.Row]);
            }
        }

        private static List<string> GetAllCategories(List<RssFeedItem> items)
        {
            var categories = new List<string>();

            categories.Add("All Categories");

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    if (categories.IndexOf(item.Category) < 0)
                    {
                        categories.Add(item.Category);
                    }
                }
            }

            return categories;
        }

        private void AddSelectedCategory(string category)
        {
            _selectedCategories.Add(category);
        }
    }
}