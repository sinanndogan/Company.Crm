using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Company.Crm.Web.Mvc.TagHelpers;

[HtmlTargetElement(Attributes = "cmg-if")]
public class ConditionTagHelper : TagHelper
{
    [HtmlAttributeName("cmg-if")]
    public bool Condition { get; set; } = true;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Condition)
        {
            output.TagName = null;

            output.SuppressOutput();
        }
    }
}
