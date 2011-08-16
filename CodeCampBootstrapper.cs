using Nancy;
using System.Configuration;

namespace CodeCampService
{
    public class CodeCampBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
			
			string mongoConnectionString = ConfigurationManager.AppSettings.Get("MONGOHQ_URL");
            container.Register<CampProvider>(new CampProvider(mongoConnectionString));
        }
    }
}