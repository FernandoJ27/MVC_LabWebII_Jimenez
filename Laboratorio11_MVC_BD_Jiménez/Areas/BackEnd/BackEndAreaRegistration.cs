using System.Web.Mvc;

namespace Laboratorio11_MVC_BD_Jiménez.Areas.BackEnd
{
    public class BackEndAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BackEnd";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BackEnd_default",
                "BackEnd/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}