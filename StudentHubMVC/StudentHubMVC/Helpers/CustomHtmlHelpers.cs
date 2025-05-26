using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString ProgressCircle(this HtmlHelper helper, string percent, string strokeclass, string sublinetext)
        {
            // @*<div class="circlechart" data-percentage="@mockTestDetails.PercentCompleted">@mockTestDetails.PercentCompleted%</div>*@
            return new MvcHtmlString(string.Format("<div class=\"circlechart\" data-percentage=\"{0}\"><svg class=\"circle-chart\" viewBox=\"0 0 33.83098862 33.83098862\" xmlns=\"http://www.w3.org/2000/svg\"><circle class=\"circle-chart__background\" cx=\"16.9\" cy=\"16.9\" r=\"15.9\"></circle><circle class=\"circle-chart__circle {1}\" stroke-dasharray=\"90,100\" cx=\"16.9\" cy=\"16.9\" r=\"15.9\"></circle><g class=\"circle-chart__info\"><text class=\"circle-chart__percent\" x=\"17.5\" y=\"17\">{0}%</text>@*<text class=\"circle-chart__subline\" x=\"16.91549431\" y=\"22\">{2}</text>*@</g></svg></div>", percent, strokeclass, sublinetext));
        }
    }
}