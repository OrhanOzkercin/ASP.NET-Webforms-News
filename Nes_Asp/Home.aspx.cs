using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Serilog;

namespace Nes_Asp
{
    public partial class Home : System.Web.UI.Page
    {
        DatabaseHandler dh = DatabaseHandler.getInitial();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.QueryString["category"] != null)
            {
                string newsCategory = (Request.QueryString["category"]);
                
                if (newsCategory == "getDishaber")
                {
                    getDishaber(sender, e);
                }
                else if (newsCategory == "getEgitim")
                {
                    getEgitim(sender, e);
                }
                else if (newsCategory == "getEkonomi")
                {
                    getEkonomi(sender, e);
                }
                else if (newsCategory == "getGundem")
                {
                    getGundem(sender, e);
                }
                else if (newsCategory == "getMagazin")
                {
                    getMagazin(sender, e);
                }
                else if (newsCategory == "getSiyaset")
                {
                    getSiyaset(sender, e);
                }
                else
                {
                    getSpor(sender, e);
                }

                PropertyInfo isreadonly =
                    typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
                        "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Remove("category");
            }
            Dishaberler.ServerClick += new System.EventHandler(getDishaber);
            Egitim.ServerClick += new System.EventHandler(getEgitim);
            Ekonomi.ServerClick += new System.EventHandler(getEkonomi);
            Gundem.ServerClick += new System.EventHandler(getGundem);
            Magazin.ServerClick += new System.EventHandler(getMagazin);
            Siyaset.ServerClick += new System.EventHandler(getSiyaset);
            Spor.ServerClick += new System.EventHandler(getSpor);
            
            ArrayList arrayList = (ArrayList)Session["todayNews"];
            if (arrayList != null)
            {
                for (int i = 0; i < arrayList.Count; i++)
                {
                    newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)arrayList[i]));
                }
            }

            time.InnerText = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            Log.Debug("Tüm haberler anasayfa gösterildi.");

        }

        string cleanHtml(string stringHtml)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            stringHtml = regex.Replace(stringHtml, String.Empty);
            return (String.Format(stringHtml));// Plain Text as a OUTPUT
        }

        private HtmlGenericControl createDOMElements(object sender, EventArgs e, News singLeNews)
        {
            HtmlGenericControl card = new HtmlGenericControl("div");
            card.Attributes.Add("class", "card");

            HtmlGenericControl cardHeader = new HtmlGenericControl("div");
            cardHeader.Attributes.Add("class", "card-header");

            card.Controls.Add(cardHeader);

            HtmlGenericControl cardImage = new HtmlGenericControl("image");
            cardImage.Attributes.Add("src", singLeNews.ImageUrl);

            cardHeader.Controls.Add(cardImage);

            HtmlGenericControl cardBody = new HtmlGenericControl("div");
            cardBody.Attributes.Add("class", "card-body");

            card.Controls.Add(cardBody);

            HtmlGenericControl cardTitle = new HtmlGenericControl("div");
            cardTitle.Attributes.Add("class", "title");

            cardBody.Controls.Add(cardTitle);

            HtmlGenericControl title = new HtmlGenericControl("h2");
            title.InnerHtml = singLeNews.Title;
            cardTitle.Controls.Add(title);

            HtmlGenericControl cardContent = new HtmlGenericControl("div");
            cardContent.Attributes.Add("class", "content");

            cardBody.Controls.Add(cardContent);

            HtmlGenericControl contentParagraph = new HtmlGenericControl("p");
            contentParagraph.InnerText = cleanHtml(cleanHtml(singLeNews.Description).Substring(0, 250) + "...");
            cardContent.Controls.Add(contentParagraph);

            HtmlGenericControl pubDate = new HtmlGenericControl("p");
            pubDate.Attributes.Add("class", "pub-date");
            pubDate.InnerText += singLeNews.PubDate;
            cardContent.Controls.Add(pubDate);



            Button readMore = new Button();
            readMore.Attributes.Add("onclick", "");
            readMore.ID = singLeNews.NewsID.ToString();
            readMore.Text = "Devamını oku!";
            readMore.CssClass = "btn";
            readMore.Click += yonlendir;

            cardBody.Controls.Add(readMore);

            return card;

        }

        public void yonlendir(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonId = button.ID;
            Response.Redirect("NewsDetail.aspx?id=" + buttonId);

        }

        public void getSpor(object sender, EventArgs e)
        {
            
            ArrayList news = getNewsAccCategory("Spor");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {
                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getSiyaset(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Siyaset");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getMagazin(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Magazin");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getGundem(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Genel");
           
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getEkonomi(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Ekonomi");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getEgitim(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Eğitim");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
        }

        public void getDishaber(object sender, EventArgs e)
        {
            ArrayList news = getNewsAccCategory("Dış Haber");
            newsPanel.Controls.Clear();
            if (news.Count == 0)
            {
                HtmlGenericControl title = new HtmlGenericControl("h1");
                title.InnerText = "Herhangi bir içerik bulunamadı!";
                newsPanel.Controls.Add(title);
            }
            for (int i = 0; i < news.Count; i++)
            {

                newsPanel.Controls.Add(createDOMElements(sender, e, (Nes_Asp.News)news[i]));
            }
            
        }

        private ArrayList getNewsAccCategory(string category)
        {
            ArrayList returnList = new ArrayList();
            ArrayList news = dh.GetAllNews();
            for (int i = 0; i < news.Count; i++)
            {
                News singleNews = (Nes_Asp.News)news[i];
                if (singleNews.Category == category)
                {
                    returnList.Add(singleNews);
                }
            }

            return returnList;
        }


    }
}