using System.Web;
using System.Web.Mvc;

namespace BCX_ISAAC_BA_SOL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
