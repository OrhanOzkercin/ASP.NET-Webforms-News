using System;
using System.Collections;
using System.Data.OleDb;
using Serilog;

namespace Nes_Asp
{
    public class DatabaseHandler
    {
        private static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\egil_\Desktop\Nes_Asp\Nes_Asp\news.mdb;Persist Security Info=True"); // You need to change database file address
        private static DatabaseHandler dh;
        private DatabaseHandler() { }


        public static DatabaseHandler getInitial()
        {
            if (dh == null)
            {
                dh = new DatabaseHandler();
                return dh;
            }
            else
            {
                return dh;
            }
        }

        public void addNews(News news)
        {
            try
            {
                OleDbCommand insert = new OleDbCommand("INSERT INTO News (newsId,title,description,category,author,pubDate,imageUrl)" +
                                                       "VALUES (@newsId,@title,@description,@category,@author,@pubDate,@imageUrl)", conn);
                conn.Open();

                insert.Parameters.AddWithValue("@newsId", news.NewsID);
                insert.Parameters.AddWithValue("@title", news.Title);
                insert.Parameters.AddWithValue("@description", news.Description);
                insert.Parameters.AddWithValue("@category", news.Category);
                insert.Parameters.AddWithValue("@author", news.Author);
                insert.Parameters.AddWithValue("@pubDate", news.PubDate);
                insert.Parameters.AddWithValue("@imageUrl", news.ImageUrl);
                insert.ExecuteNonQuery();
                conn.Close();
                Log.Debug("Veritabanına haber eklendi");
            }
            catch (Exception e)
            {
                Log.Debug("Veritabanına haber eklenirken bir hata oluştu.");
                Console.WriteLine(e);
                throw;
            }
            
           
        }

        public bool isNewsExist(News news)
        {
            try
            {
                string title = news.Title;
                OleDbCommand select = new OleDbCommand("Select title from News where title = @title", conn);
                conn.Open();
                select.Parameters.AddWithValue("@title", title);
                OleDbDataReader reader = select.ExecuteReader();
                if (reader.Read())
                {
                    conn.Close();
                    Log.Debug("Database de haber daha önceden eklediği verisi okundu");
                    return false;
                }
                conn.Close();
                Log.Debug("Database de haber daha önceden eklenmediği verisi okundu");
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
       
        }

        public ArrayList GetAllNews()
        {
            try
            {
                ArrayList allNews = new ArrayList();
                OleDbCommand select = new OleDbCommand("Select * from News", conn);
                conn.Open();

                OleDbDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    News news = new News(int.Parse(reader["newsId"].ToString()), reader["title"].ToString(),
                        reader["description"].ToString(), reader["category"].ToString(),
                        reader["author"].ToString(), reader["pubdate"].ToString(), reader["imageUrl"].ToString());
                    allNews.Add(news);
                }
                conn.Close();
                Log.Debug("Tüm haberler databaseden çekildi.");
                return allNews;
            }
            catch (Exception e)
            {
                Log.Debug("Tüm haberler databaseden çekilirken bir hata meydana geldi.");
                Console.WriteLine(e);
                throw;
            }
            
            
        }

        public News getSingleNewsbyId(int newsId )
        {
            try
            {
               OleDbCommand select = new OleDbCommand("Select * from News where newsId = @newsId", conn);
                conn.Open();
                select.Parameters.AddWithValue("@newsId", newsId);
                OleDbDataReader reader = select.ExecuteReader();
                if (reader.Read())
                {
                   
                    News news = new News(newsId, reader["title"].ToString(),
                        reader["description"].ToString(), reader["category"].ToString(),
                        reader["author"].ToString(), reader["pubdate"].ToString(), reader["imageUrl"].ToString());
                    conn.Close();
                    return news;
                }
                conn.Close();
                Log.Debug("Id ile haber databaseden çekildi.");
            }
            catch (Exception e)
            {
                Log.Debug("Id ile haber databaseden çekilirken bir hata meydana geldi");
            }
           
            return null;
        }
        public News getSingleNewsbyTitle(string title)
        {
            try
            {
                OleDbCommand select = new OleDbCommand("Select * from News where title = @title", conn);
                conn.Open();
                select.Parameters.AddWithValue("@title", title);
                OleDbDataReader reader = select.ExecuteReader();
                if (reader.Read())
                {
                    News news = new News(int.Parse(reader["newsId"].ToString()), reader["title"].ToString(),
                        reader["description"].ToString(), reader["category"].ToString(),
                        reader["author"].ToString(), reader["pubdate"].ToString(), reader["imageUrl"].ToString());
                    conn.Close();
                    return news;
                }
                conn.Close();
                Log.Debug("Title ile haber databaseden çekildi.");
            }
            catch (Exception e)
            {
                Log.Debug("Title ile haber databaseden çekilirken bir hata meydana geldi");
                Console.WriteLine(e);
                throw;
            }

            
            return null;
        }
    }
}