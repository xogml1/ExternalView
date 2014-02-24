using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExternalView.MainWeb
{
    // 참고: IIS6 또는 IIS7 클래식 모드를 사용하도록 설정하는 지침을 보려면 
    // http://go.microsoft.com/?LinkId=9394801을 방문하십시오.

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 경로 이름
                "{controller}/{action}/{id}", // 매개 변수가 있는 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "ExternalView.MainWeb.Controllers" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // 포함 리소스로 프로젝트 첨부된 리소스 Path 에서 렌더링하는 Razor View Engine 등록
            ViewEngines.Engines.Insert(0, new ExternalView.Mvc.EmbeddedResourceViewEngine(typeof(ExternalView.EmbeddedResourceViews.Resource)));
        }
    }
}