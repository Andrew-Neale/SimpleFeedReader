using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleFeedReader.Interfaces;

namespace SimpleFeedReader
{
    public class FeedRequest : IFeedRequest
    {
        public async Task<XDocument> MakeRequest(string url)
        {
            var client = new HttpClient();
            XDocument xDoc = null;

            try
            {
                var feed = await client.GetAsync(url);
                xDoc = XDocument.Parse(await feed.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }

            return xDoc;
        }
    }
}
