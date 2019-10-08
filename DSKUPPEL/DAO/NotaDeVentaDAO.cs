using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;

namespace DSKUPPEL.DAO
{
    public class NotaDeVentaDAO : BaseDAO
    {
        public static List<NotadeVentaCabeceraModels> AgregarNV(NotadeVentaCabeceraModels NVC)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_AgregarNVCabecera", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("NVNumero", NVC.NVNumero);
                    dc.parameters.AddWithValue("NumOC", NVC.NumOC);
                    dc.parameters.AddWithValue("NumReq", NVC.NumReq);

                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }

        }

        public static List<NotadeVentaCabeceraModels> EditarNV(NotadeVentaCabeceraModels NVC)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ModificarNVCabecera", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("NVNumero", NVC.NVNumero);
                    dc.parameters.AddWithValue("nvFem", NVC.nvFem);
                    dc.parameters.AddWithValue("nvEstado", NVC.nvEstado);
                    dc.parameters.AddWithValue("nvEstFact", NVC.nvEstFact);
                    dc.parameters.AddWithValue("nvEstDesp", NVC.nvEstDesp);
                    dc.parameters.AddWithValue("nvEstRese", NVC.nvEstRese);
                    dc.parameters.AddWithValue("nvEstConc", NVC.nvEstConc);
                    dc.parameters.AddWithValue("nvFeEnt", NVC.nvFeEnt);
                    dc.parameters.AddWithValue("CodAux", NVC.CodAux);
                    dc.parameters.AddWithValue("VenCod", NVC.VenCod);
                    dc.parameters.AddWithValue("CodMon", NVC.CodMon);
                    dc.parameters.AddWithValue("CodLista", NVC.CodLista);
                    dc.parameters.AddWithValue("nvObser", NVC.nvObser);
                    dc.parameters.AddWithValue("CveCod", NVC.CveCod);
                    dc.parameters.AddWithValue("NomCon", NVC.NomCon);
                    dc.parameters.AddWithValue("CodiCC", NVC.CodiCC);
                    dc.parameters.AddWithValue("nvSubTotal", NVC.nvSubTotal);
                    dc.parameters.AddWithValue("nvPorcDesc01", NVC.nvPorcDesc01);
                    dc.parameters.AddWithValue("nvDescto01", NVC.nvDescto01);
                    dc.parameters.AddWithValue("nvPorcDesc02", NVC.nvPorcDesc02);
                    dc.parameters.AddWithValue("nvDescto02", NVC.nvDescto02);
                    dc.parameters.AddWithValue("nvPorcDesc03", NVC.nvPorcDesc03);
                    dc.parameters.AddWithValue("nvDescto03", NVC.nvDescto03);
                    dc.parameters.AddWithValue("nvPorcDesc04", NVC.nvPorcDesc04);
                    dc.parameters.AddWithValue("nvDescto04", NVC.nvDescto04);
                    dc.parameters.AddWithValue("nvPorcDesc05", NVC.nvPorcDesc05);
                    dc.parameters.AddWithValue("nvDescto05", NVC.nvDescto05);
                    dc.parameters.AddWithValue("nvMonto", NVC.nvMonto);
                    dc.parameters.AddWithValue("NumGuiaRes", NVC.NumGuiaRes);
                    dc.parameters.AddWithValue("nvPorcFlete", NVC.nvPorcFlete);
                    dc.parameters.AddWithValue("nvValflete", NVC.nvValflete);
                    dc.parameters.AddWithValue("nvPorcEmb", NVC.nvPorcEmb);
                    dc.parameters.AddWithValue("nvEquiv", NVC.nvEquiv);
                    dc.parameters.AddWithValue("nvNetoExento", NVC.nvNetoExento);
                    dc.parameters.AddWithValue("nvNetoAfecto", NVC.nvNetoAfecto);
                    dc.parameters.AddWithValue("nvTotalDesc", NVC.nvTotalDesc);
                    dc.parameters.AddWithValue("ConcAuto", NVC.ConcAuto);
                    dc.parameters.AddWithValue("CheckeoPorAlarmaVtas", NVC.CheckeoPorAlarmaVtas);
                    dc.parameters.AddWithValue("EnMantencion", NVC.EnMantencion);
                    dc.parameters.AddWithValue("Usuario", NVC.Usuario);
                    dc.parameters.AddWithValue("UsuarioGeneraDocto", NVC.UsuarioGeneraDocto);
                    dc.parameters.AddWithValue("FechaHoraCreacion", NVC.FechaHoraCreacion);
                    dc.parameters.AddWithValue("Sistema", NVC.Sistema);
                    dc.parameters.AddWithValue("ConcManual", NVC.ConcManual);
                    dc.parameters.AddWithValue("proceso", NVC.proceso);
                    dc.parameters.AddWithValue("TotalBoleta", NVC.TotalBoleta);
                    dc.parameters.AddWithValue("NumReq", NVC.NumReq);
                    dc.parameters.AddWithValue("CodVenWeb", NVC.CodVenWeb);
                    dc.parameters.AddWithValue("EstadoNP", NVC.EstadoNP);
                    dc.parameters.AddWithValue("CodLugarDesp", NVC.CodLugarDesp);

                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw ex;
            }

        }

        public static List<NotaDeVentaDetalleModels> DetalleNV(NotaDeVentaDetalleModels NVD)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_AgregarNVDetalle", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("NVNumero", NVD.NVNumero);
                    dc.parameters.AddWithValue("nvLinea", NVD.nvLinea);
                    dc.parameters.AddWithValue("nvCorrela", NVD.nvCorrela);
                    dc.parameters.AddWithValue("nvFecCompr", NVD.nvFecCompr);
                    dc.parameters.AddWithValue("CodProd", NVD.CodProd);
                    dc.parameters.AddWithValue("nvCant", NVD.nvCant);
                    dc.parameters.AddWithValue("nvPrecio", NVD.nvPrecio);
                    dc.parameters.AddWithValue("nvEquiv", NVD.nvEquiv);
                    dc.parameters.AddWithValue("nvSubTotal", NVD.nvSubTotal);
                    dc.parameters.AddWithValue("nvDPorcDesc01", NVD.nvDPorcDesc01);
                    dc.parameters.AddWithValue("nvDDescto01", NVD.nvDDescto01);
                    dc.parameters.AddWithValue("nvDPorcDesc02", NVD.nvDPorcDesc02);
                    dc.parameters.AddWithValue("nvDDescto02", NVD.nvDDescto02);
                    dc.parameters.AddWithValue("nvDPorcDesc03", NVD.nvDPorcDesc03);
                    dc.parameters.AddWithValue("nvDDescto03", NVD.nvDDescto03);
                    dc.parameters.AddWithValue("nvDPorcDesc04", NVD.nvDPorcDesc04);
                    dc.parameters.AddWithValue("nvDDescto04", NVD.nvDDescto04);
                    dc.parameters.AddWithValue("nvDPorcDesc05", NVD.nvDPorcDesc05);
                    dc.parameters.AddWithValue("nvDDescto05", NVD.nvDDescto05);
                    dc.parameters.AddWithValue("nvTotDesc", NVD.nvTotDesc);
                    dc.parameters.AddWithValue("nvTotLinea", NVD.nvTotLinea);
                    dc.parameters.AddWithValue("nvCantDesp", NVD.nvCantDesp);
                    dc.parameters.AddWithValue("nvCantProd", NVD.nvCantProd);
                    dc.parameters.AddWithValue("nvCantFact", NVD.nvCantFact);
                    dc.parameters.AddWithValue("nvCantDevuelto", NVD.nvCantDevuelto);
                    dc.parameters.AddWithValue("nvCantNC", NVD.nvCantNC);
                    dc.parameters.AddWithValue("nvCantBoleta", NVD.nvCantBoleta);
                    dc.parameters.AddWithValue("nvCantOC", NVD.nvCantOC);
                    dc.parameters.AddWithValue("DetProd", NVD.DetProd);
                    dc.parameters.AddWithValue("CheckeoMovporAlarmaVtas", NVD.CheckeoMovporAlarmaVtas);
                    dc.parameters.AddWithValue("KIT", NVD.KIT);
                    dc.parameters.AddWithValue("CodPromocion", NVD.CodPromocion);
                    dc.parameters.AddWithValue("CodUMed", NVD.CodUMed);
                    dc.parameters.AddWithValue("CantUVta", NVD.CantUVta);
                    dc.parameters.AddWithValue("Partida", NVD.Partida);
                    dc.parameters.AddWithValue("Pieza", NVD.Pieza);

                    return dc.executeQuery<NotaDeVentaDetalleModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<NotadeVentaCabeceraModels> BuscarNVPorNumero(NotadeVentaCabeceraModels NVC)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "JS_ListarNVNM", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("intNVNumero", NVC.NVNumero);
                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }

        }

        public static List<NotadeVentaCabeceraModels> BuscarNVNum(NotadeVentaCabeceraModels NVSoft)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "SP_GET_BuscaNVNumSoft", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("NVNum", NVSoft.NVNumero);
                    return dc.executeQuery<NotadeVentaCabeceraModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }

        }

        public static List<NotaDeVentaDetalleModels> BuscarNVDETALLEPorNumero(NotadeVentaCabeceraModels NVC)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "JS_ListarNVDETALLENM", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("intNVNumero", NVC.NVNumero);
                    return dc.executeQuery<NotaDeVentaDetalleModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }

        }

        public static List<NotadeVentaCabeceraModels> InsertarNvSoftland(NotadeVentaCabeceraModels NVC)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "insertaNVSoftland", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("nvNumero", NVC.NVNumero);

                    return dc.executeQuery<NotadeVentaCabeceraModels>();
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