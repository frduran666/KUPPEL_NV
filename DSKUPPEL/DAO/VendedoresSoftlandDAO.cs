using cl.riobaker.DAL.Data;
using DSKUPPEL.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSKUPPEL.DAO
{
    public class VendedoresSoftlandDAO : BaseDAO
    {
        public static List<VendedoresSoftlandModels> ListarVendedoresSoftland(string venCod)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarVendedorSoftland", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("venCod", venCod);
                    return dc.executeQuery<VendedoresSoftlandModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<VendedoresSoftlandModels> listarVendedoresSoftland()
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarVendedorSoftland2", CommandType.StoredProcedure))
                {
                    return dc.executeQuery<VendedoresSoftlandModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<ClientesModels> GetVendedores(ClientesModels cliente = null)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "JS_ListarVendorVenCod", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("VenCod", cliente.VenCod);
                    return dc.executeQuery<ClientesModels>();
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