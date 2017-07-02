using System;
using System.Text;
using System.Xml.Linq;

namespace SimpleFeedReader.Feeds
{
    public class RssFeedItem
    {
        public string Guid { get;  set; }
        public string Title { get;  set; }
        public string Description { get;   set; }
        public string Thumbnail { get;   set; }
        public DateTime PublishDate { get;  set; }
        public string Link { get;  set; }
        public string Category { get; set; }

        public RssFeedItem() { }

        public RssFeedItem(XElement feedItemElement)
        {
            Title = feedItemElement.Element("title")?.Value;
            Description = RemoveHtmlTags(feedItemElement.Element("description")?.Value);
            Thumbnail = feedItemElement.Element("thumbnail")?.Value; //todo
            ParsePublishDate(feedItemElement);
            Link = feedItemElement.Element("link")?.Value;
            Guid = feedItemElement.Element("guid")?.Value;
            Category = feedItemElement.Element("category")?.Value;
        }

        private void ParsePublishDate(XElement feedItemElement)
        {
            DateTime parsedDate;
            DateTime.TryParse(feedItemElement.Element("pubDate")?.Value, out parsedDate);

            PublishDate = parsedDate;
        }

        private string RemoveHtmlTags(string description)
        {
            var noHtml = new StringBuilder();

            bool copyWord = true;
            for (int i = 0; i < description.Length; i++)
            {
                if (description[i] == '<')
                {
                    copyWord = false;
                }

                if (copyWord)
                {
                    noHtml.Append(description[i]);
                }

                if (description[i] == '>')
                {
                    copyWord = true;
                }
            }

            return noHtml.ToString().Trim();
        }
   }
}
