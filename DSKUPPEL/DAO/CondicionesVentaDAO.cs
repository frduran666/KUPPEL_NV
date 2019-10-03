using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class CondicionesVentaDAO : BaseDAO
    {
        public static List<CondicionVentasModels> listarConVen(CondicionVentasModels conven)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarCondicionesDeVenta", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("CodAux", conven.CodAux);
                    return dc.executeQuery<CondicionVentasModels>();
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