using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class ProductosDAO : BaseDAO
    {
        public static List<ProductosModels> BuscarProducto(ProductosModels producto)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarProducto", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("DesProd", producto.DesProd);
                    //dc.parameters.AddWithValue("CodProd", producto.CodProd);
                    dc.parameters.AddWithValue("CodLista", producto.CodLista);
                    return dc.executeQuery<ProductosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<ProductosModels> BuscarProductoRapido(ProductosModels producto)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarProductoRapido", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("CodRapido", producto.CodRapido);
                    dc.parameters.AddWithValue("CodLista", producto.CodLista);
                    return dc.executeQuery<ProductosModels>();
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