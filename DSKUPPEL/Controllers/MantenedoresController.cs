using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DSKUPPEL.Models;
using DSKUPPEL.DAO;

namespace DSKUPPEL.Controllers
{
    public class MantenedoresController : Controller
    {
        public ActionResult Clientes()
        {
            if (Session["VenCod"] == null)
            {
                return RedirectToAction("SesionExpirada", "Error");
            }
            List<ClientesModels> ListClientes = new List<ClientesModels>();
            var listClientes = ClientesDAO.listarClientes();

            if (listClientes != null)
            {
                ListClientes = listClientes;
            }

            ViewBag.clientes = ListClientes;

            return View();
        }

        public ActionResult EditCliente(string rutAux, string codAux)
        {

            try
            {
                if (rutAux == null)
                {
                    return View();
                }

                if (codAux == null) codAux = string.Empty;

                ClientesModels cliente = new ClientesModels();

                cliente.RutAux = rutAux.ToString();
                cliente.CodAux = codAux.ToString();

                List<ClientesModels> bclientes = ClientesDAO.BuscarClientes(cliente);

                if (Session["buscarCliente"] != null)
                {
                    Session["buscarCliente"] = null;
                    Session.Add("buscarCliente", bclientes);
                }
                else
                {
                    Session.Add("buscarCliente", bclientes);
                }

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult EditUsuarios(int idUsuario)
        {
            UsuariosModels Usuario = new UsuariosModels();
            Usuario.id = idUsuario;
            List<UsuariosModels> busuario = UsuariosDAO.BuscarUsuario(Usuario);
            ViewBag.buscarusuarios = busuario;

            List<UsuariosTiposModels> ltipo = UsuariosTiposDAO.listarTipo();
            ViewBag.tipo = ltipo;

            List<VendedoresSoftlandModels> LisVendedoresSoftland = VendedoresSoftlandDAO.ListarVendedoresSoftland(busuario[0].VenCod.Trim());
            ViewBag.vendedorSoftland = LisVendedoresSoftland;

            return View();
        }

        public ActionResult Usuarios()
        {
            if (Session["VenCod"] == null)
            {
                return RedirectToAction("SesionExpirada", "Error");
            }
            List<UsuariosModels> lUsuarios = UsuariosDAO.listarUsuarios();
            ViewBag.usuarios = lUsuarios;
            return View();
        }

        public ActionResult AddUsuario()
        {
            if (Session["VenCod"] == null)
            {
                return RedirectToAction("SesionExpirada", "Error");
            }
            List<UsuariosTiposModels> ltipo = UsuariosTiposDAO.listarTipo();
            ViewBag.tipo = ltipo;

            List<VendedoresSoftlandModels> lvendedor = VendedoresSoftlandDAO.listarVendedoresSoftland();
            ViewBag.vendedor = lvendedor;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddUsuario(FormCollection frm)
        {
            UsuariosModels Usuarios = new UsuariosModels();
            Usuarios.Usuario = Request.Form["txtUsuario"];
            Usuarios.email = Request.Form["txtEmail"];
            Usuarios.tipoUsuario = Request.Form["cbxTipo"];
            Usuarios.VenCod = Request.Form["cbxUsuarioSoftland"];
            Usuarios.Password = Request.Form["txtContrasena"];

            List<UsuariosModels> busuario = UsuariosDAO.AgregarUsuario(Usuarios);
            if (busuario == null || busuario.Count == 0)
            {
                TempData["Mensaje"] = "ERROR - Usuario Creado Correctamente. <br>";
                return RedirectToAction("AddUsuario", "Mantenedores");
            }
            else
            {
                TempData["Mensaje"] = "ERROR - Codigo de vendedor Repetido. <br>";
                return RedirectToAction("AddUsuario", "Mantenedores");
            }
        }

        public ActionResult Parametros()
        {
            List<ParametrosModels> para = ParametrosDAO.BuscarParametros();

            ViewBag.Parametros = para;

            return View();
        }

        public JsonResult EliminarUsuario(int _Id)
        {
            UsuariosModels usuarios = new UsuariosModels();
            usuarios.id = _Id;
            bool result = UsuariosDAO.EliminarUsuario(usuarios);

            return (Json(result));
        }

        #region"--- Web Methods ---"


        [HttpPost, ValidateInput(false)]
        public ActionResult EditUsuarios(FormCollection frm)
        {
            UsuariosModels Usuario = new UsuariosModels();

            Usuario.id = Convert.ToInt32(Request.Form["hdCodigo"]);
            Usuario.Usuario = Request.Form["txtUsuario"];
            Usuario.email = Request.Form["txtEmail"];
            Usuario.tipoUsuario = Request.Form["perfil"];
            Usuario.VenCod = Request.Form["vendedor"];

            bool result = UsuariosDAO.ActualizarUsuario(Usuario);

            List<UsuariosModels> busuario = UsuariosDAO.BuscarUsuario(Usuario);
            ViewBag.buscarusuarios = busuario;

            List<UsuariosTiposModels> ltipo = UsuariosTiposDAO.listarTipo();
            ViewBag.tipo = ltipo;

            List<VendedoresSoftlandModels> LisVendedoresSoftland = VendedoresSoftlandDAO.ListarVendedoresSoftland(busuario[0].VenCod);
            ViewBag.vendedorSoftland = LisVendedoresSoftland;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Parametros(FormCollection frm)
        {
            int numero;
            if (Request.Form["valorRadio"].ToString() == "P")
            {
                numero = 0;
            }
            else
            {
                numero = 1;
            }
            ParametrosModels apo = new ParametrosModels();
            apo.Aprobador = numero;

            List<ParametrosModels> lis = ParametrosDAO.ModificarParametros(apo);

            List<ParametrosModels> para = ParametrosDAO.BuscarParametros();

            ViewBag.Parametros = para;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Clientes(FormCollection frm)
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditCliente(FormCollection frm)
        {
            //ClientesModels cliente = new ClientesModels();
            //cliente.RutAux = Request.Form["txtrut"];

            //return View();
            ClientesModels cliente = new ClientesModels();

            cliente.CodAux = Request.Form["txtcodAux"];
            cliente.RutAux = Request.Form["txtrut"];
            cliente.NomAux = Request.Form["txtnombre"];
            cliente.NomCon = Request.Form["txtcontacto"];
            cliente.FonCon = Request.Form["txttelefono"];
            cliente.DirAux = Request.Form["txtdireccion"];
            cliente.EMail = Request.Form["txtemail"];

            bool result = ClientesDAO.ActualizarCliente(cliente);

            List<ClientesModels> bclientes = ClientesDAO.BuscarClientes(cliente);

            if (Session["buscarCliente"] != null)
            {
                Session["buscarCliente"] = null;
                Session.Add("buscarCliente", bclientes);
            }
            else
            {
                Session.Add("buscarCliente", bclientes);
            }

            return View();
        }

        #endregion
    }
}

