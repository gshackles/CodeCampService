using Nancy;

namespace CodeCampService.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters => "o hai";
        }
    }
}