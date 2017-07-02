using System.Collections.Generic;
using SimpleFeedReader.Feeds;

namespace SimpleFeedReader
{
    public class FeedItemsViewModel
    {
        public List<RssFeedItem> FeedItems { get; set; }

        public List<string> CategoriesFilter { get; private set; } = new List<string>();

        public void ClearFilter()
        {
            CategoriesFilter.Clear();
        }

        public void AddFilter(string category)
        {
            CategoriesFilter.Add(category);
        }

        public List<RssFeedItem> GetFilteredItems()
        {
            var filteredItems = new List<RssFeedItem>();

            if (CategoriesFilter.Contains("All Categories"))
            {
                return filteredItems;
            }

            GetItemsInCategories(filteredItems);

            return filteredItems;
        }

        private void GetItemsInCategories(List<RssFeedItem> filteredItems)
        {
            foreach (var item in FeedItems)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    foreach (var category in CategoriesFilter)
                    {
                        if (CategoriesFilter.Contains(category))
                        {
                            filteredItems.Add(item);
                        }
                    }
                }
            }
        }
   }
}
