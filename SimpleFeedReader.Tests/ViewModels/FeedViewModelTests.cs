using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleFeedReader.Feeds;
using SimpleFeedReader.Interfaces;
using SimpleFeedReader.ViewModels;

namespace SimpleFeedReader.Tests
{
    [TestFixture]
    public class FeedViewModelTests
    {
        [Test]
        public async Task CanLoadFeedsSuccessfullyFromNetworkOnly()
        {
            // Setup
            var cacheMock = new Mock<ICache>();
            var requestMock = new Mock<IFeedRequest>();
            var feedViewModel = new FeedViewModel(requestMock.Object, cacheMock.Object);

            requestMock.Setup(x => x.MakeRequest(It.IsAny<string>()))
                       .ReturnsAsync(XDocumentFactory.CreateSampleFeed());

            // Act
            await feedViewModel.LoadFeeds();

            // Verify
            Assert.IsNotNull(feedViewModel.Feeds);
            Assert.AreEqual(2, feedViewModel.Feeds.Count);
            Assert.AreEqual("BBC News - UK", feedViewModel.Feeds[0].Title);
            Assert.AreEqual(10, feedViewModel.Feeds[0].FeedItems.Count);
        }

        [Test]
        public async Task CanLoadFeedsSuccessfullyFromCacheOnly()
        {
            // Setup
            var cacheMock = new Mock<ICache>();
            var requestMock = new Mock<IFeedRequest>();
            var feedViewModel = new FeedViewModel(requestMock.Object, cacheMock.Object);

            var cachedFeed = new RssFeed() { Title = "BBC News - UK" };
            cacheMock.Setup(x => x.Get<RssFeed>(It.IsAny<string>()))
                     .ReturnsAsync(cachedFeed);

            // Act
            await feedViewModel.LoadFeeds();

            // Verify
            Assert.IsNotNull(feedViewModel.Feeds);
            Assert.AreEqual(2, feedViewModel.Feeds.Count);
            Assert.AreEqual("BBC News - UK", feedViewModel.Feeds[0].Title);
             Assert.AreEqual(0, feedViewModel.Feeds[0].FeedItems.Count);
        }
    }
}
