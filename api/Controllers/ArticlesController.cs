using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api.Controllers;



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
    
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched article",
            ResponseData = _articlesService.GetArticlesForFeed()
        };
    
    }


    [HttpPost]
    [ValidateModel]
    [Route("/api/articles")]
    
    public ResponseDto  Post([FromBody] CreateArticleRequest dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a article",
            ResponseData = _articlesService.CreateArticle(dto.Headline,dto.Body, dto.Author, dto.ArticleImgUrl)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/articles/{articleId}")]
    public ResponseDto Put([FromRoute] int articleId,
        [FromBody] UpdateArticleRequest dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated",
            ResponseData = _articlesService.UpdateArticle( dto.ArticleId,dto.Headline, dto.Body,
                dto.Author, dto.ArticleImgUrl)
        };

    }

    [HttpDelete]
    [Route("/api/articles/{articleId}")]
    public ResponseDto Delete([FromRoute] int articleId)
    {
        _articlesService.DeleteArticle(articleId);
        return new ResponseDto()
        {
            MessageToClient = "Succesfully deleted"
        };
    }
}









