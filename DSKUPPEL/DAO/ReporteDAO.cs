using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class ReporteDAO : BaseDAO
    {
        public static List<NotadeVentaCabeceraModels> listarDocAprobados()
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarDocumentosAprobados", CommandType.StoredProcedure))
                {
                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotadeVentaCabeceraModels> listarDocPendientes()
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarDocumentosPendientes", CommandType.StoredProcedure))
                {
                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotadeVentaCabeceraModels> actualizaEstado(NotadeVentaCabeceraModels nw)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "RRA_ActualizaEstadoNW", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("nvNumero", nw.NVNumero);
                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotadeVentaCabeceraModels> BuscarNVC(NotadeVentaCabeceraModels nw)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarNVCabecera", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("nvNumero", nw.NVNumero);

                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotaDeVentaDetalleModels> BuscarNVD(NotaDeVentaDetalleModels nw)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarNVDetalle", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("nvNumero", nw.NVNumero);

                    return dc.executeQuery<NotaDeVentaDetalleModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotaDeVentaDetalleModels> ListarNotaDetalle(NotaDeVentaDetalleModels nw)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarNVDetalleStock", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("nvNumero", nw.NVNumero);

                    return dc.executeQuery<NotaDeVentaDetalleModels>();
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











