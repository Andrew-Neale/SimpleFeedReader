using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace SimpleFeedReader
{
    public class CategoryTableSource : UITableViewSource
    {        
        private readonly List<string> _categories;
        private readonly CategoryTableViewController _controller;
        private string CellIdentifier = "CategoryCell";

        public CategoryTableSource(List<string> categories, CategoryTableViewController controller)
        {
            _categories = categories;
            _controller = controller;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

            cell.TextLabel.Text = _categories[indexPath.Row];

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _categories.Count;
        }

       
    }
}
