using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repository;

public class ArticleRepository
{
    // the readonly is used to make sure that the value is not changed after the constructor is called :)
    private readonly NpgsqlDataSource _dataSource;

    public ArticleRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    // Add logic here to search for articles
    public IEnumerable<ArticleFeedQuery> SearchArticles(string searchDtoSearchTerm, int searchDtoPageSize)
    {
        throw new NotImplementedException();
    }

    // Added here the SUBSTRING(body, 1, 50) AS body to limit the body to less 51 characters in the feed as requested in the assignment
    // However make sure you have the same naming form your db
    public IEnumerable<ArticleFeedQuery> GetArticlesForFeed()
    {
        string sql = $@"
            SELECT articleId AS {nameof(ArticleFeedQuery.ArticleId)},
            headline AS {nameof(ArticleFeedQuery.Headline)},
            SUBSTRING(body, 1, 50) AS {nameof(ArticleFeedQuery.Body)},
            articleimgurl AS {nameof(ArticleFeedQuery.ArticleImgUrl)}
            FROM news.articles;
         ";
        
        using var
            conn = _dataSource
                .OpenConnection(); // here you can keep it or use the using statement (this is more modern way to do so) : PS. it will autoclose once the method is done :) 
        
        return conn.Query<ArticleFeedQuery>(sql);
    }
    
    // I finished this to show you how to do it, you can do the rest :)
    public Article GetArticleById(int articleId)
    {
        const string sql =
            $@"SELECT articleId AS ArticleId,author, headline, articleimgurl AS {nameof(Article.ArticleImgUrl)}, body FROM news.articles WHERE articleId = @articleId";
        
        using var
            conn = _dataSource
                .OpenConnection();
        
        return conn.QueryFirst<Article>(sql, new { articleId });
    }
    
    
    // I have fixed here some typos (articleimgurl) make sure that the naming is the same as you have in the database
    // the same goes for the other methods
    public Article CreateArticle(string headline, string body, string author, string articleImgUrl)
    {
        var sql = $@"
            INSERT INTO news.articles (headline, body, author, articleimgurl) 
            VALUES (@headline, @body, @author, @articleImgUrl)
            RETURNING articleId AS {nameof(Article.ArticleId)},
            headline AS {nameof(Article.Headline)},
            body AS {nameof(Article.Body)},
            author AS {nameof(Article.Author)},
            articleimgurl AS {nameof(Article.ArticleImgUrl)};
        ";

        using var conn = _dataSource.OpenConnection();
        
        return conn.QueryFirst<Article>(sql, new { headline, body, author, articleImgUrl });
    }

    // Fix this one if not correctly working 
    public Article UpdateArticle(string headline, string body, string author, string articleImgUrl)
    {
        var sql = $@"
            UPDATE news.articles SET headline = @headline, body = @body, author= @author, articleImgUrl = @articleImgUrl
            WHERE articleId = @articleId
            RETURNING articleId AS {nameof(Article.ArticleId)},
            headline AS {nameof(Article.Headline)},
            body AS {nameof(Article.Body)},
            author AS {nameof(Article.Author)},
            articleimgurl AS {nameof(Article.ArticleImgUrl)};
        ";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new { headline, body, author, articleImgUrl });
        }
    }
    
    // Fix this one if not correctly working 
    public bool DeleteArticle(int articleId)
    {
        var sql = @"DELETE FROM news.articles WHERE articleid= @articleId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { articleId }) > 0;
        }
    }
    
    // Not sure if this is needed ?
    public bool DoesArticleWithHeadlineExist(string headline)
    {
        var sql = @"SELECT COUNT(*) FROM news.articles WHERE headline = @headline;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { headline }) == 1;
        }
    }
}