namespace MySurveys.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfiguration
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
