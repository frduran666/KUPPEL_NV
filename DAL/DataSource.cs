using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class DataSource
    {
        public static string coneccionPrimaria = (ConfigurationManager.ConnectionStrings["DSAbutex"].ConnectionString);
        //public static string coneccionPrimaria = (ConfigurationManager.ConnectionStrings["KuppelBD"].ConnectionString);
        public static bool cache;

        public static void SetParametros(string conn1)
        {
            coneccionPrimaria = "Initial Catalog=DSKUPPEL;Data Source=srv-disofi;User ID=sa;password=Softland2018";
            //coneccionPrimaria = "Initial Catalog=DSKUPPEL;Data Source=SRVKUPPEL\\SQLEXPRESS;User ID=sa;password=Softland2018";
        }

        public static void SetCache(bool hasCache)
        {
            cache = hasCache;
        }

        public string ConeccionPrimaria { get { return coneccionPrimaria; } }
    }
}
