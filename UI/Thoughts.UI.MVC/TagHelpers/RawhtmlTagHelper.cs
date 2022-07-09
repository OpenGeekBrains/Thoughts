using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Thoughts.UI.MVC.TagHelpers;

/// <summary>
/// Таг-хелпер отображения html в сыром виде
/// </summary>
public class RawhtmlTagHelper : TagHelper
{
    private IHtmlHelper _htmlHelper;

    public RawhtmlTagHelper(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    /// <summary>
    /// Текст html
    /// </summary>
    public string Text { get; set; }

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var content_html = content.GetContent();

        if (content_html is { Length: > 0 })
        {
            //output.Content.AppendHtml(content_html);
            output.Content.SetHtmlContent(new HtmlString(content_html));
            return;
        }

        output.TagName = null;
        var text = _htmlHelper.Raw(Text);
        output.Content.SetHtmlContent(text);
    }
}
