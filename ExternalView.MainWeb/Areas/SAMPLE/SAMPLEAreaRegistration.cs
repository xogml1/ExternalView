using System.Web.Mvc;

namespace ExternalView.MainWeb.Areas.SAMPLE
{
    public class SAMPLEAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SAMPLE";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SAMPLE_default",
                "SAMPLE/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "ExternalView.MainWeb.Areas.SAMPLE.Controllers" }
            );
        }
    }
}
