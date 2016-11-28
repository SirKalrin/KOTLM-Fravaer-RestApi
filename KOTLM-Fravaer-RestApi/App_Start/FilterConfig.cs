using System.Web;
using System.Web.Mvc;

namespace KOTLM_Fravaer_RestApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
