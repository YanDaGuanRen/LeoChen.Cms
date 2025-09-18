using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.ArticleContent;

[DisplayName("文章内容")]
public class ArticleContentArea : AreaBase
{
    public ArticleContentArea() : base(nameof(ArticleContentArea).TrimEnd("Area")) { }
}