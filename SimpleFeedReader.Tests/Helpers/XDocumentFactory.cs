using System.IO;
using System.Xml.Linq;

namespace SimpleFeedReader.Tests
{
    public static class XDocumentFactory
    {
        public static XDocument CreateSampleFeed()
        {
            var xml = File.ReadAllText(Path.Combine("XmlFiles", "sampleXml.xml"));
            return XDocument.Parse(xml);
        }
    }
}
