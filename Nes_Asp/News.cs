namespace Nes_Asp
{
    public class News
    {
        public int NewsID;
        public string Title;
        public string Description;
        public string Category;
        public string Author;
        public string PubDate;
        public string ImageUrl;
        public News(int NewsID, string Title, string Description,
            string Category, string Author, string PubDate, string ImageUrl)
        {
            this.NewsID = NewsID;
            this.Title = Title;
            this.Description = Description;
            this.Category = Category;
            this.Author = Author;
            this.PubDate = PubDate;
            this.ImageUrl = ImageUrl;
        }
    }
}