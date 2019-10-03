using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class ListaDePrecioDAO : BaseDAO
    {
        public static List<ListaDePrecioModels> listarListaDePrecio(ListaDePrecioModels lista)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarListaDePrecio", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("CodAux", lista.CodAux);
                    return dc.executeQuery<ListaDePrecioModels>();
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