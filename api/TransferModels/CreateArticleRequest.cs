using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using api.CustomDataAnnotations;

namespace api.TransferModels;

// I have added couple of annotation that you can look into to ease the validation
public class CreateArticleRequest
{
    [NotNull]
    [Required]
    [StringLength(30, MinimumLength = 5)]
    public string? Headline { get; set; }
    
    [NotNull]
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string? Body { get; set; }
    
    [NotNull]
    [Required]
    [Url]
    public string? ArticleImgUrl { get; set; }

    [NotNull]
    [Required]
    // Here you can use the regular expression instead what you had before to ease the validation
    [RegularExpression("^(Bob|Rob|Dob|Lob)$")]
    public string? Author { get; set; }
}
