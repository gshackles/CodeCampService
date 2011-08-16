using Nancy;

namespace CodeCampService.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule(CampProvider campProvider)
        {
            Get["/"] = _ => "o hai";
            Get["/v1/{CampKey}/Version"] = parameters => campProvider.GetVersionNumber(parameters.CampKey).ToString();
            Get["/v1/{CampKey}/Xml"] = parameters => campProvider.GetXml(parameters.CampKey);
			Get["/v1/{CampKey}/Reset"] = parameters => campProvider.ResetCache(parameters.CampKey);
        }
    }
}