using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api.Controllers;



// Comments to help you more :) 
// 1. I would recommend to use the same naming convention for all your controllers and usually is singular (ArticleController) or whatever model you are working with.
// 2. Check all the validation and also so that it will by the requirements 
// When failing data validation, the client expects a status code 400 (automatically done when using Data Annotations). For internal server errors such as database problems, throw an exception resulting in status code 500.
// if you are lost just let me know or check the final solution or we can talk about it and find solution
public class ArticlesController : ControllerBase
{
    private readonly ILogger<ArticlesController> _logger;
    private readonly ArticlesService _articlesService;

    public ArticlesController(ILogger<ArticlesController> logger,ArticlesService articlesService)
    {
        _logger = logger;
        _articlesService = articlesService;
    }
    
    [HttpGet]
    [Route("/api/feed")]
    public IEnumerable<ArticleFeedQuery> GetArticlesForFeed() // TODO: Changed to GetArticlesForFeed (this is better naming convention :) )
    { 
        return _articlesService.GetArticlesForFeed();
    }
    [HttpGet]
    [ValidateModel]
    [Route("/api/articles")]
    public IEnumerable<ArticleFeedQuery> SearchArticles([FromQuery] SearchArticlesDto searchDto)
    {
        return _articlesService.SearchArticles(searchDto.SearchTerm, searchDto.PageSize);
    }

    [HttpGet]
    [Route("/api/articles/{articleId}")]
    public Article GetArticleById([FromRoute] int articleId) // TODO: Changed to GetArticleById (this is better naming convention :) )
    {
        var article = _articlesService.GetArticleById(articleId);
        return article;
    }
    
    [HttpPost]
    [ValidateModel]
    [Route("/api/articles")]
    public Article CreateArticle([FromBody] CreateArticleRequest dto)
    {
        return _articlesService.CreateArticle(dto.Headline, dto.Body, dto.Author, dto.ArticleImgUrl);
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/articles/{articleId}")]
    public Article UpdateArticleById([FromRoute] int articleId,
        [FromBody] UpdateArticleRequest dto)
    {
        return _articlesService.UpdateArticle(dto.ArticleId, dto.Headline, dto.Body,
            dto.Author, dto.ArticleImgUrl);
    }

    // I fixed this to return bool instead of void, because it's better to return a response to the client
    // and if you check test or even open the swagger endpoint you have there what is expeceted to be returned from the endpoint
    [HttpDelete]
    [Route("/api/articles/{articleId}")]
    public bool Delete([FromRoute] int articleId)
    {
        return _articlesService.DeleteArticle(articleId);
    }
}









