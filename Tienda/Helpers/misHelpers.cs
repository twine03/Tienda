using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Tienda.Helpers
{
    public static class misHelpers
    {
        public static MvcHtmlString alerta_roja(
            this HtmlHelper htmlHelper,
            String mensaje)
        {
            return MvcHtmlString.Create("<div class='alert alert-danger'>"+mensaje+"</div>");
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text, 
            string action,
            string controler,
            object routeValues=null,
            object htmlAtributes=null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var AccionActual = routeData.GetRequiredString("action");
            var ControladorActual = routeData.GetRequiredString("controller");

            if(string.Equals(ControladorActual,controler, StringComparison.OrdinalIgnoreCase)
                &&
                string.Equals(AccionActual, action, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }

            if (routeValues != null)
            {
                li.InnerHtml = (htmlAtributes != null)
                    ? htmlHelper.ActionLink(
                        text, action, controler, routeValues, htmlAtributes
                        ).ToHtmlString()
                    : htmlHelper.ActionLink(
                        text, action, controler, routeValues
                        ).ToHtmlString();
            }
            else
            {
                li.InnerHtml = (htmlAtributes != null)
                    ? htmlHelper.ActionLink(
                        text, action, controler, null, htmlAtributes
                        ).ToHtmlString()
                    : htmlHelper.ActionLink(
                        text, action, controler
                        ).ToHtmlString();
            }


            return MvcHtmlString.Create(li.ToString());

        }

        public static MvcHtmlString BootstrapIcon_Button(
            this HtmlHelper htmlHelper,
            string text,
            string title,
            string action,
            string controler,
            string btnType = null,
            string icon = null,
            object routeValues = null,
            object htmlAtributes = null
            )
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            if (btnType == null)
                btnType = "btn-default";
           
            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.AddCssClass("btn");
            builder.AddCssClass(btnType);

            if (icon != null)
            {
                TagBuilder spanbuilder = new TagBuilder("span");
                if (icon.Contains("fa-"))
                    spanbuilder.AddCssClass("fa " + icon);
                if(icon.Contains("glyphicon-"))
                    spanbuilder.AddCssClass("glyphicon " + icon);

                builder.InnerHtml = spanbuilder.ToString() + text;
            }

            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action,controler,routeValues);

            builder.MergeAttributes(new RouteValueDictionary(
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAtributes
                )));
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}