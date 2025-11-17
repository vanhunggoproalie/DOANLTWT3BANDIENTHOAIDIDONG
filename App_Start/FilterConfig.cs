using System.Web;
using System.Web.Mvc;

namespace DOANLTWT3BANDIENTHOAIDIDONG
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
