using System.Configuration;

namespace DSKUPPEL.DAO
{
    public class BaseDAO
    {
        public static string catalogo = ConfigurationManager.AppSettings.Get("Catalogo");

    }
}