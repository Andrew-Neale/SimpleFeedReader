using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleFeedReader.Interfaces
{
    public interface IFeedRequest
    {
        Task<XDocument> MakeRequest(string url);
    }
}
