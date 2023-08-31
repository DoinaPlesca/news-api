using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class UpdateArticleRequest
{
    
    public int ArticleId { get; set; }
    [MinLength(4)] 
    public string Headline { get; set; }
    public string Body { get; set; }
    public string Author { get; set; }
    public string ArticleImgUrl { get; set; }


   
}
