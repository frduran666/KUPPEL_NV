using cl.riobaker.DAL.Data;
using DSKUPPEL.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSKUPPEL.DAO
{
    public class LoginDAO : BaseDAO
    {
        public static List<UsuariosModels> Login(UsuariosModels usuario)
        {
            try
            {
                using (DataContext dc = new DataContext(catalogo, "SP_Login", CommandType.StoredProcedure))
                {
                    dc.parameters.AddWithValue("usuario", usuario.Usuario);
                    dc.parameters.AddWithValue("password", usuario.Password);
                    return dc.executeQuery<UsuariosModels>();
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