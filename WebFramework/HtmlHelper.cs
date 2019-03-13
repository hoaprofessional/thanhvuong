using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCore.Utils.ModelHelper;

namespace WebFramework
{
    public static class HtmlExtension
    {

        public static IHtmlContent SortingFor<TSource>(this IHtmlHelper helper, ISortingResultDto sortingResultDto, Expression<Func<TSource, object>> selector, object htmlAttributes = null)
        {
            // Create tag builder
            TagBuilder builder = new TagBuilder("th");

            string propertyName = GetCorrectPropertyName(selector);

            string[] sortings = sortingResultDto.Sorting.Split(' ');
            if (sortings.Count() != 2)
            {
                return helper.Raw("");
            }
            string sortingProperty = sortings[0];
            string sortingAction = sortings[1];

            builder.AddCssClass("sortable");

            if (propertyName == sortingProperty)
            {
                if (sortingAction == "asc")
                {
                    builder.AddCssClass("sort-asc");
                }
                if (sortingAction == "desc")
                {
                    builder.AddCssClass("sort-desc");
                }
            }

            // Add attributes
            builder.MergeAttribute("data-property", propertyName);
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            // Render tag
            string result = string.Join(" ", builder.Attributes.Select(x => $"{x.Key}=\"{x.Value}\""));
            return helper.Raw(result);
        }
        
        public static bool HasPermission(this IHtmlHelper helper, string permission)
        {
            IHttpContextAccessor httpContext = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            return httpContext.HttpContext.User.Claims.Any(x => x.Value == permission);
        }

        public static bool IsLogin(this IHtmlHelper helper)
        {
            IHttpContextAccessor httpContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            string name = httpContextAccessor.HttpContext.User?.Identity?.Name;
            return name != null;
        }

        public static string DisplayFromSelectList(this IHtmlHelper helper, SelectList selectList, string key)
        {
            SelectListItem value = selectList.Where(x => x.Value == key).FirstOrDefault();
            if (value == null)
            {
                return "";
            }
            return value.Text;
        }

        private static string GetCorrectPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                Expression op = ((UnaryExpression)expression.Body).Operand;
                return ((MemberExpression)op).Member.Name;
            }
        }
    }
}
