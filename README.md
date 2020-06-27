### Installation

  - Change log file path in RSSParsing.aspx.cs file to save some logs properly
  
```sh
   Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Change here!", rollingInterval: RollingInterval.Day)
                .CreateLogger();
```

 - Change database connection address in DatabaseHandler file to connect database properly
 
 ```sh
  private static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Change here!\news.mdb;Persist Security Info=True");  
```

- Run program with RSSParsing.aspx.cs


### Purpose of project and how to work

 In this final term project I got news from `https://ajanda.dha.com.tr/` address with xml format. In `RSSParsing.aspx.cs` file I got this news and parse it and convert it to News object which defined in `News.cs` class. After this operation I saved News to database and session on browser. While doing this operations, I made some logging. 
 
 After this logic operations, `RSSParsing.aspx.cs` Redirect page to `Homepage.aspx` and Homepage reads news from session and create HTML elements dynamically in that file and show that. Also there is a filter options on the left sidebar. You can filter topics of News. Last page is `NewsDetail.aspx` which shows the details of News such as author, pubDate, title and description.
 
 
![News Project](https://i.ibb.co/p2tYxKC/news.gif)