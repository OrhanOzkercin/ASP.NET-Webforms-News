using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Nes_Asp
{
    public partial class NewsDetail : System.Web.UI.Page
    {

        Home home = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            Dishaberler.ServerClick += new System.EventHandler(getDishaber);
            Egitim.ServerClick += new System.EventHandler(getEgitim);
            Ekonomi.ServerClick += new System.EventHandler(getEkonomi);
            Gundem.ServerClick += new System.EventHandler(getGundem);
            Magazin.ServerClick += new System.EventHandler(getMagazin);
            Siyaset.ServerClick += new System.EventHandler(getSiyaset);
            Spor.ServerClick += new System.EventHandler(getSpor);

            time.InnerText = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            DatabaseHandler dh = DatabaseHandler.getInitial();
            int newsId  = int.Parse(Request.QueryString["id"]);
            News newsDetail = dh.getSingleNewsbyId(newsId);
            newsDetailArea.Controls.Add(createDOMElements(newsDetail));
        }

        private void getSpor(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getSpor");
        }

        private void getSiyaset(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getSiyaset");
        }

        private void getMagazin(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getMagazin");
        }

        private void getGundem(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getGundem");
        }

        private void getEkonomi(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getEkonomi");
        }

        private void getEgitim(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getEgitim");
        }

        private void getDishaber(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?category=" + "getDishaber");
        }

        private HtmlGenericControl createDOMElements( News singLeNews)
        {
            HtmlGenericControl newsDetail = new HtmlGenericControl("section");
            newsDetail.Attributes.Add("class", "news-detail");

            HtmlGenericControl newsHeader = new HtmlGenericControl("div");
            newsHeader.Attributes.Add("class", "news-header");

            newsDetail.Controls.Add(newsHeader);

            HtmlGenericControl newsParagraph = new HtmlGenericControl("p");
            newsParagraph.InnerText = cleanHtml(singLeNews.Category);

            newsHeader.Controls.Add(newsParagraph);

            HtmlGenericControl newsH1= new HtmlGenericControl("h1");
            newsH1.InnerText = cleanHtml(singLeNews.Title);

            newsHeader.Controls.Add(newsH1);

            HtmlGenericControl newsBody = new HtmlGenericControl("div");
            newsBody.Attributes.Add("class", "news-body");

          

            HtmlGenericControl newsImage = new HtmlGenericControl("img");
            newsImage.Attributes.Add("src", singLeNews.ImageUrl);

            newsBody.Controls.Add(newsImage);

            HtmlGenericControl dateAndAuthorWrapper = new HtmlGenericControl("div");
            HtmlGenericControl newsDate = new HtmlGenericControl("p");
            newsDate.InnerText = singLeNews.PubDate;

            dateAndAuthorWrapper.Controls.Add(newsDate);

            HtmlGenericControl author = new HtmlGenericControl("p");
            author.InnerText = singLeNews.Author;

            dateAndAuthorWrapper.Controls.Add(author);

            newsBody.Controls.Add(dateAndAuthorWrapper);

            HtmlGenericControl descriptonWrapper = new HtmlGenericControl("div");
            descriptonWrapper.InnerHtml = singLeNews.Description;

            newsBody.Controls.Add(descriptonWrapper);
          

            newsDetail.Controls.Add(newsBody);


            return newsDetail;

        }

        string cleanHtml(string stringHtml)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            stringHtml = regex.Replace(stringHtml, String.Empty);
            return (String.Format(stringHtml));// Plain Text as a OUTPUT
        }
    }
}