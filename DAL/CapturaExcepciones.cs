using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class CapturaExcepciones : Exception
    {
        public CapturaExcepciones(Exception e)
        {
            var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Fatal(e.Message);
            log.Error("Error no controlado: " + e);
            log.ErrorFormat(e.StackTrace);
        }


    }
}
