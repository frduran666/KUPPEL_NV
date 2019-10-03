using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class ParametrosDAO : BaseDAO
    {
        public static List<ParametrosModels> BuscarParametros()
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarParametrosUsuarios", CommandType.StoredProcedure))
                {
                    return dc.executeQuery<ParametrosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<ParametrosModels> ModificarParametros(ParametrosModels Aprobador)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ModificarParametrosUsuarios", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("Aprobador", Aprobador.Aprobador);
                    return dc.executeQuery<ParametrosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }
    }
}