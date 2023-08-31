using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repository;

public class ArticleRepository
{
    private NpgsqlDataSource _dataSource;

    public ArticleRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }


    public IEnumerable<ArticleFeedQuery> GetArticlesForFeed()
    {
        string sql = $@"
SELECT articleId AS {nameof(ArticleFeedQuery.ArticleId)},
    headline AS {nameof(ArticleFeedQuery.Headline)},
    body AS {nameof(ArticleFeedQuery.Body)},
    author AS {nameof(ArticleFeedQuery.Author)},
    articleimgurl AS {nameof(ArticleFeedQuery.ArticleImgUrl)}
FROM news.articles;
   ";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<ArticleFeedQuery>(sql).ToList();
        }
    }
    

    public Article UpdateArticle(string headline, string body, string author, string articleImgUrl)
    {
        var sql = $@"
UPDATE news.articles SET headline = @headline, body = @body, author= @author, articleImgUrl = @articleImgUrl
WHERE articleId = @articleId
RETURNING articleId AS {nameof(Article.ArticleId)},
        headline AS {nameof(Article.Headline)},
        body AS {nameof(Article.Body)},
       author AS {nameof(Article.Author)},
        articleImgUrl AS {nameof(Article.ArticleImgUrl)};
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new {headline,body,author, articleImgUrl });
        }
    }
    

    public Article CreateArticle(string headline, string body, string author, string articleImgUrl)
    {
        var sql = $@"
INSERT INTO news.articles (headline, body, author, articleImgUrl) 
VALUES (@headline, @body, @author, @articleImgUrl)
RETURNING articleId AS {nameof(Article.ArticleId)},
       headline AS {nameof(Article.Headline)},
        body AS {nameof(Article.Body)},
        author AS {nameof(Article.Author)}
        articleImgUrl AS {nameof(Article.ArticleImgUrl)};
       
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new { headline, body, author, articleImgUrl});
        }
    }

    public bool DeleteArticle(int articleId)
    {
        var sql = @"DELETE FROM news.articles WHERE articleid= @article_Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { articleId }) == 1;
        }
    }

    public bool DoesArticleWithHeadlineExist(string headline)
    {
        var sql = @"SELECT COUNT(*) FROM news.articles WHERE headline = @headline;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { headline }) == 1;
        }
    }
}


