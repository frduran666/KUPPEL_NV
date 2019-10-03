using System;
using System.Collections.Generic;
using cl.riobaker.DAL.Data;
using System.Data;
using DSKUPPEL.Models;
using DAL;

namespace DSKUPPEL.DAO
{
    public class UsuariosDAO : BaseDAO
    {
        public static List<UsuariosModels> listarUsuarios()
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ListarUsuarios", CommandType.StoredProcedure))
                {
                    return dc.executeQuery<UsuariosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static List<UsuariosModels> BuscarUsuario(UsuariosModels usuario)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_BuscarUsuarios", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("id", usuario.id);

                    return dc.executeQuery<UsuariosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public List<NotaDeVentaDetalleModels> DatosCorreoVend(int NvNUmero)
        {
            var DatosUser = new List<NotaDeVentaDetalleModels>();
            var data = new DBConector().EjecutarProcedimientoAlmacenado("SP_GET_DatosCorreoVend", new System.Collections.Hashtable()
                                                                                            {
                                                                                                {"NvNumero", NvNUmero}
                                                                                            });

            if (data.Rows.Count > 0)
            {
                for (var i = 0; i < data.Rows.Count; i++)
                {
                    var validador = new object();
                    var resultadoListado = new NotaDeVentaDetalleModels();

                    validador = data.Rows[i].Field<object>("email");
                    resultadoListado.EmailVend = validador != null ? data.Rows[i].Field<string>("email") : "NO ASIGNADO";


                    validador = data.Rows[i].Field<object>("ContrasenaCorreo");
                    resultadoListado.PassCorreo = validador != null ? data.Rows[i].Field<string>("ContrasenaCorreo") : "NO ASIGNADO";

                    DatosUser.Add(resultadoListado);
                }
            }
            return DatosUser;
        }











        public static bool ActualizarUsuario(UsuariosModels usuario)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_ActualizarUsuario", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("id", usuario.id);
                    dc.parameters.AddWithValue("usuario", usuario.Usuario);
                    dc.parameters.AddWithValue("email", usuario.email);
                    dc.parameters.AddWithValue("tipoUsuario", usuario.tipoUsuario);
                    dc.parameters.AddWithValue("venCod", usuario.VenCod);
                    return dc.executeNonQuery();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
        }
        public static List<UsuariosModels> AgregarUsuario(UsuariosModels usuario)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_AgregarUsuario", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("Usuario", usuario.Usuario);
                    dc.parameters.AddWithValue("email", usuario.email);
                    dc.parameters.AddWithValue("tipoUsuario", usuario.tipoUsuario);
                    dc.parameters.AddWithValue("VenCod", usuario.VenCod);
                    dc.parameters.AddWithValue("Contrasena", usuario.Password);
                    return dc.executeQuery<UsuariosModels>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static bool EliminarUsuario(UsuariosModels usuarios)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "FR_EliminarUsuario", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("Id", usuarios.id);
                    return dc.executeNonQuery();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
        }
    }
}