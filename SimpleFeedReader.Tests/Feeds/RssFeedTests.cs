using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleFeedReader.Feeds;

namespace SimpleFeedReader.Tests
{
    [TestFixture()]
    public class RssFeedTests
    {
        [Test()]
        public async Task CanDownloadFeed()
        {
            // Setup
            var cacheMock = new Mock<ICache>();
            var rssFeed = new RssFeed(cacheMock.Object);

            // Act
            await rssFeed.LoadFeed("http://feed.com/rss", XDocumentFactory.CreateSampleFeed());

            // Verify
            Assert.IsNotNull(rssFeed.FeedItems);
            Assert.AreEqual(10, rssFeed.FeedItems.Count);
            Assert.AreEqual("BBC News - UK", rssFeed.Title);
        }

        [Test()]
        public async Task NoDuplicationsWhenUsingCache()
        {
            // Setup
            var cacheMock = new Mock<ICache>();
            var rssFeed = new RssFeed(cacheMock.Object);

            var cachedFeed = new RssFeed() { Title = "BBC News - UK" };
            await rssFeed.LoadFeed("http://feed.com/rss", XDocumentFactory.CreateSampleFeed());
            cacheMock.Setup(x => x.Get<RssFeed>(It.IsAny<string>()))
                     .ReturnsAsync(cachedFeed);

            // Act
            await rssFeed.LoadFeed("http://feed.com/rss", XDocumentFactory.CreateSampleFeed());

            // Verify
            Assert.IsNotNull(rssFeed.FeedItems);
            Assert.AreEqual(10, rssFeed.FeedItems.Count);
            Assert.AreEqual("BBC News - UK", rssFeed.Title);
        }
    }
}
