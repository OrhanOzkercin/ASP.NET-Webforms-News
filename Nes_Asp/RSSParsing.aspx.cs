using Serilog;
using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Nes_Asp
{
    public partial class RSSParsing : System.Web.UI.Page
    {
        DatabaseHandler dh = DatabaseHandler.getInitial();
     
        public ArrayList ParseRssFile( string xmladress = "https://ajanda.dha.com.tr/feed/rss/")
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(xmladress);
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            
            ArrayList allNews = new ArrayList();


            for (int i = 0; i < rssNodes.Count; i++)
            {
                XmlNode rssNode = rssNodes.Item(i);

                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("category");
                string category = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("author");
                string author = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";
                var dateArray = pubDate.Split(' ');
                pubDate = dateArray[1] + " " + dateArray[2] + " " + dateArray[3];
                string image = "";
                rssSubNode = rssNode.SelectSingleNode("enclosure");
                var nodeAttr = rssSubNode != null ? rssSubNode.Attributes["url"] : null;
                if (nodeAttr != null)
                {
                    image = nodeAttr.InnerText;
                }
                else
                {
                    image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1200px-No_image_available.svg.png";
                }

                News singleNews = new News(generateNewsId(), title, description, category, author, pubDate, image);
                if (dh.isNewsExist(singleNews))
                {
                    dh.addNews(singleNews);
                    allNews.Add(singleNews);
                }
                else
                {
                    allNews.Add(dh.getSingleNewsbyTitle(title));
                }
            }
            return allNews;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\Users\\egil_\\Desktop\\Nes_Asp\\Nes_Asp\\logs\\log.txt", rollingInterval: RollingInterval.Day) // You need to change log file address
                .CreateLogger();
            try
            {
                ArrayList allNews = ParseRssFile();
                Session["todayNews"] = allNews;
                Log.Debug("Rssden veriler çekildi");
                Log.Debug("Session'a veriler eklendi");
            }
            catch (Exception exception)
            {
                Log.Debug("Rssden veriler çekilemedi ve session'a ekelenemedi. Bir hata oluştu.");
            }
            
            Response.Redirect("/Home.aspx");
        }
        
        private int generateNewsId()
        {
            Random random = new Random();
            int id = random.Next(1, 1000000);

            return id;

        }


    }
}