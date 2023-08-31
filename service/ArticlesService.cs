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

    public IEnumerable<ArticleFeedQuery> GetArticlesForFeed()
    {
        return _articleRepository.GetArticlesForFeed();
    }

    public Article CreateArticle(string headline, string body, string author,
        string articleImgUrl)
    {
        var doesArticleExist = _articleRepository.DoesArticleWithHeadlineExist(headline);
        if (!doesArticleExist)
        {
            throw new ValidationException("Article already exist!");
        }
        return _articleRepository.CreateArticle(headline,body,author, articleImgUrl);
    }

    public Article UpdateArticle( int articleId,string headline, string body, string author,
        string articleImgUrl)
    {
        return _articleRepository.UpdateArticle(headline, body, author, articleImgUrl);
        
    }

    public void DeleteArticle(int articleId)
    {
        var result = _articleRepository.DeleteArticle(articleId);
        if (!result)
        {
            throw new Exception("Could not insert article");
        }
        
    }
    
}
