using System.Linq;
using NUnit.Framework;
using SimpleFeedReader.Feeds;

namespace SimpleFeedReader.Tests
{
    [TestFixture()]
    public class RssFeedItemTests
    {
        [Test()]
        public void CanRemoveHtmlFromDescription()
        {
            // Setup
            var xDoc = XDocumentFactory.CreateSampleFeed();
            var firstEl = xDoc.Descendants("item").First();
            firstEl.Element("description").Value = "some element <b>no html</b>";

            // Act
            var rssFeedItem = new RssFeedItem(firstEl);

            // Verify
            Assert.AreEqual("some element no html", rssFeedItem.Description);
        }

        [Test()]
        public void RemovingHtmlCanHandleEmptyDescription()
        {
            // Setup
            var xDoc = XDocumentFactory.CreateSampleFeed();
            var firstEl = xDoc.Descendants("item").First();
            firstEl.Element("description").Value = string.Empty;

            // Act
            var rssFeedItem = new RssFeedItem(firstEl);

            // Verify
            Assert.AreEqual(string.Empty, rssFeedItem.Description);
        }
    }
}
