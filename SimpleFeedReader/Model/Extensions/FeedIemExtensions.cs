using SimpleFeedReader.Feeds;

namespace SimpleFeedReader.Extensions
{
    public static class FeedExtensions
    {
        public static bool IsSameFeedItem(this RssFeedItem left, RssFeedItem right)
        {
            return left.Guid == right.Guid;
        }
    }
}
