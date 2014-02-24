using System.Web.Mvc;

namespace ExternalView.EmbeddedResourceViews.Areas.ERV
{
    public class ERVAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ERV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ERV_default",
                "ERV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "ExternalView.EmbeddedResourceViews.Areas.ERV.Controllers" }
            );
        }
    }
}
