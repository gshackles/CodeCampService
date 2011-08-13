using Nancy;

namespace CodeCampService
{
    public class CodeCampBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<CampProvider>(new CampProvider(System.Web.HttpContext.Current.Server.MapPath("~/CampData")));
        }
    }
}