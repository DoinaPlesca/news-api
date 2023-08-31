using System.ComponentModel.DataAnnotations;
using System.Reflection;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateArticleRequest
{
    [MinLength(5)]
    public string Headline { get; set; }
    public string Author { get; set; }
    public string ArticleImgUrl { get; set; }
    
    [ValueIsOneOf(new string[]{"Bob", "Rob"},  "Must be one one ...")]
    
    public string Body { get; set; }
    
    
}
