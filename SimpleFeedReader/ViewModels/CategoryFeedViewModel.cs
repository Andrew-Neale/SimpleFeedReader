using System.Collections.Generic;
using SimpleFeedReader.Feeds;

namespace SimpleFeedReader
{
    public class CategoryFeedViewModel
    {
        private List<string> _categories;

        public List<string> Categories
        {
            get
            {
                return _categories;
            }
        }

        public void Initialise(List<RssFeedItem> items)
        {
            _categories = GetAllCategories(items);
        }

        private static List<string> GetAllCategories(List<RssFeedItem> items)
        {
            var categories = new List<string>();

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
    }
}
