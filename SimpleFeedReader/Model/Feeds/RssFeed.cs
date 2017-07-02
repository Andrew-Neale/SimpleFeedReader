using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleFeedReader.Feeds
{
    public class RssFeed
    {
        private const double CACHE_TIMEOUT_IN_DAYS = 7;
        
        private readonly ICache _cache;
        private readonly List<RssFeedItem> _feedItems = new List<RssFeedItem>();
        
        public string Title { get; set; }

        public List<RssFeedItem> FeedItems
        {
            get
            {
                return _feedItems;
            }
        }

        public RssFeed()
        {
            // Required for deserialisation
        }

        public RssFeed(ICache cache)
        {
            _cache = cache;
        }

        public async Task LoadFeed(string uri, XDocument feed)
        {
            await LoadCache(uri);

            if (feed != null)
            {
                Title = feed.Element("rss").Element("channel").Element("title").Value;
            }

            var latestCachedItem = _feedItems.FirstOrDefault();
            var newFeedItems = new List<RssFeedItem>();

            AddFeedItems( feed, newFeedItems);

            await AddAllItemsToCache(uri, latestCachedItem, newFeedItems);
        }


        async Task AddAllItemsToCache(string key, RssFeedItem latestCachedItem, List<RssFeedItem> newFeedItems)
        {
            // Nothing in cache, just add everything
            if (latestCachedItem == null)
            {
                _feedItems.AddRange(newFeedItems);
                await _cache.Insert<RssFeed>(key, this, DateTimeOffset.UtcNow.AddDays(CACHE_TIMEOUT_IN_DAYS));

                return;
            }

            var matchedFeedItem = newFeedItems.FirstOrDefault((arg) => arg.Guid == latestCachedItem.Guid);
           
            // Nothing matches in cache compared to downloaded feed add everything
            if (matchedFeedItem == null)
            {
                _feedItems.AddRange(newFeedItems);
                await _cache.Insert<RssFeed>(key, this, DateTimeOffset.UtcNow.AddDays(CACHE_TIMEOUT_IN_DAYS));

                return;
            }

            // Only add feed items we don't have in cache
            var index = newFeedItems.IndexOf(matchedFeedItem);
            for (int i = index; i == newFeedItems.Count; i--)
            {
                _feedItems.Add(newFeedItems[i]);
            }

            await _cache.Insert<RssFeed>(key, this, DateTimeOffset.UtcNow.AddDays(CACHE_TIMEOUT_IN_DAYS));
        }

        private static void AddFeedItems(XDocument feed, List<RssFeedItem> newFeedItems)
        {
            if (feed != null)
            {
                foreach (var item in feed.Descendants("item"))
                {
                    newFeedItems.Add(new RssFeedItem(item));
                }
            }
        }

        private async Task LoadCache(string key)
        {
            var cachedFeed = await _cache.Get<RssFeed>(key);

            if (cachedFeed != null)
            {
                Title = cachedFeed.Title;
                _feedItems.AddRange(cachedFeed.FeedItems);
            }
        }
    }
}
