using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleFeedReader.Feeds;
using SimpleFeedReader.Interfaces;

namespace SimpleFeedReader.ViewModels
{
    public class FeedViewModel
    {
        private readonly IFeedRequest _feedRequest;
        private readonly ICache _cache;
        private readonly List<RssFeed> _rssFeed = new List<RssFeed>();

        private readonly List<string> _feedSources = new List<string>()
        {
            "http://feeds.reuters.com/reuters/UKdomesticNews?format=xml",
            "http://feeds.bbci.co.uk/news/uk/rss.xml"
        };

        public List<RssFeed> Feeds
        {
            get
            {
                return _rssFeed;
            }
        }

        public FeedViewModel(IFeedRequest feedRequest, ICache cache)
        {
            _feedRequest = feedRequest;
            _cache = cache;
        }

        public async Task LoadFeeds()
        {
            foreach (var feed in _feedSources)
            {
                XDocument feedDoc = null;

                feedDoc = await _feedRequest.MakeRequest(feed);

                var rssFeed = new RssFeed(_cache);

                await rssFeed.LoadFeed(feed, feedDoc);

                _rssFeed.Add(rssFeed);
            }
        }
    }
}
