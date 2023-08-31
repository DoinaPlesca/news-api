using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repository;

namespace service;

public class ArticlesService
{
    private readonly ArticleRepository _articleRepository;

    public ArticlesService(ArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    public IEnumerable<ArticleFeedQuery> SearchArticles(string searchDtoSearchTerm, int searchDtoPageSize)
    {
        return _articleRepository.SearchArticles(searchDtoSearchTerm, searchDtoPageSize);
    }

    public IEnumerable<ArticleFeedQuery> GetArticlesForFeed()
    {
        return _articleRepository.GetArticlesForFeed();
    }
    
    public Article GetArticleById(int articleId)
    {
        return _articleRepository.GetArticleById(articleId);
    }

    public Article CreateArticle(string headline, string body, string author,
        string articleImgUrl)
    {
        return _articleRepository.CreateArticle(headline,body,author, articleImgUrl);
    }

    // needs to be fixed as well :) 
    public Article UpdateArticle( int articleId,string headline, string body, string author,
        string articleImgUrl)
    {
        return _articleRepository.UpdateArticle(headline, body, author, articleImgUrl);
        
    }

    public bool DeleteArticle(int articleId)
    {
        return _articleRepository.DeleteArticle(articleId);
    }

}
