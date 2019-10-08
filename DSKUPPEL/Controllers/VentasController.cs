using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DSKUPPEL.Models;
using DSKUPPEL.DAO;
using ET.Objetos;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using BLL;
using DS_NotaVenta.Util.Archivo;
using System.Web.Script.Serialization;

namespace DSKUPPEL.Controllers
{
    public class VentasController : Controller
    {
        public String ImagePath
        {
            get
            {
                return "~/DSKUPPEL/Image/logo.JPG";
            }

        }

        string MakeImageSrcData(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
              Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        public ActionResult Todolosclientes()
        {
            List<ClientesModels> lclientes = new List<ClientesModels>();
            var todosClientes = ClientesDAO.ListarClientesTodos();

            if (lclientes != null)
            {
                lclientes = todosClientes;
            }

            ViewBag.clientes = lclientes;

            return View();
        }

        public ActionResult MisClientes(string cod, int ID)
        {
            UsuariosModels usr = new UsuariosModels();

            List<ClientesModels> lclientes = new List<ClientesModels>();


            usr.VenCod = cod;
            usr.id = ID;
            var misClientes = ClientesDAO.BuscarMisClientesVenCod(usr);

            if (misClientes != null)
            {
                lclientes = misClientes;
            }

            ViewBag.clientes = lclientes;

            return View();
        }

        public ActionResult NotaDeVenta(string CodAux, string NomAux, string anterior)
        {
            var id_ = Session["ID"].ToString();
            var VenCod = Session["VenCod"].ToString();
            var id = id_; 

            NotadeVentaCabeceraModels NVC = new NotadeVentaCabeceraModels();
            ViewBag.CodAux = CodAux;
            ViewBag.NomAux = NomAux;
            ViewBag.Anterior = anterior;
            NVC.NVNumero = 0;
            NVC.NumOC = "";
            NVC.NumReq = 0;

            ClientesModels cliente = new ClientesModels
            {
                CodAux = CodAux
            };

            List<ClientesModels> contactoCorreos = ClientesDAO.GetContacto(cliente);
            List<ClientesModels> clientes = ClientesDAO.GetClientes(cliente);

            if (contactoCorreos == null || contactoCorreos[0].EMail == "")
            {
                if (clientes == null || clientes[0].EMail == "")
                {
                    TempData["Mensaje"] = "CLIENTE SELECCIONADO NO CUENTA CON CORREO NI CONTACTOS. <br>";
                    return RedirectToAction("MisClientes", "Ventas", new { cod = VenCod, ID = id });
                }
                else
                {
                    ViewBag.CorreoCliente = clientes[0].EMail;
                }

            }
            else
            {
                ViewBag.CorreoCliente = contactoCorreos[0].EMail;
            }

            //Se agrega la cabecera
            List<NotadeVentaCabeceraModels> lnvc = NotaDeVentaDAO.AgregarNV(NVC);


            ViewBag.numeronota = lnvc;

            CondicionVentasModels conven = new CondicionVentasModels();

            conven.CodAux = CodAux.ToString();

            //Se lista(n) la(s) condicion(es) de venta(s)
            List<CondicionVentasModels> lcondicion = CondicionesVentaDAO.listarConVen(conven);

            ViewBag.condicion = lcondicion;

            ClientesModels contacto = new ClientesModels();

            contacto.CodAux = CodAux.ToString();
            contacto.NomAux = Session["nombre"].ToString();

            //Se ubica la lista de contactos
            List<ClientesModels> contactos = ClientesDAO.BuscarContacto(contacto);

            if (contactos == null)
            {
                ViewBag.contactos = contactos;
                ViewBag.vcontactos = 0;
            }
            else
            {
                ViewBag.contactos = contactos;
                ViewBag.vcontactos = 1;
            }

            DireccionDespachoModels direc = new DireccionDespachoModels();

            direc.CodAxD = CodAux.ToString();

            //Se lista(n) la(s) dirección(es) de despacho
            List<DireccionDespachoModels> direciones = ClientesDAO.BuscarDirecDespach(direc);

            if (direciones == null)
            {
                ViewBag.vdirecc = 0;
            }
            else
            {
                ViewBag.vdirecc = 1;
            }

            ViewBag.direccion = direciones;
            ViewBag.codigo = CodAux;
            ViewBag.nombre = NomAux;

            if (anterior == "1")
            {
                ViewBag.anterior = "Mis Clientes";
                ViewBag.page = "Misclientes";
            }
            else if (anterior == "2")
            {
                ViewBag.anterior = "Mis Clientes";
                ViewBag.page = "";
            }
            else if (anterior == "3")
            {
                ViewBag.anterior = "Ruta";
                ViewBag.page = "";
            }

            ListaDePrecioModels ListPrecio = new ListaDePrecioModels();

            ListPrecio.CodAux = CodAux.ToString();

            //Se listan los precios
            List<ListaDePrecioModels> ListDePrecios = ListaDePrecioDAO.listarListaDePrecio(ListPrecio);

            ViewBag.lista = ListDePrecios;

            //Se listan los centros de costos
            List<CentrodeCostoModels> lcc = CentroDeCostoDAO.listarCC();
            ViewBag.cc = lcc;

            return View();
        }

        public void Detallenv(string CodProd, string DetProd, double nvCant, string CodUMed, double nvPrecio, string CodLista,
        double nvSubTotal, double nvTotLinea, int NVNumero, double nvLinea, DateTime nvFecCompr)
        {
            double porcentajefinal = 0;
            double descuento = 0;

            if (nvTotLinea != nvSubTotal)
            {
                porcentajefinal = 100 - ((nvTotLinea * 100) / nvSubTotal);
                descuento = nvSubTotal - nvTotLinea;
            }

            #region"NVD"
            var NVD = new NotaDeVentaDetalleModels
            {

                CodProd = CodProd,
                NVNumero = NVNumero,
                nvLinea = nvLinea,
                nvCorrela = 0,
                nvFecCompr = nvFecCompr,
                nvCant = nvCant,
                nvPrecio = nvPrecio,
                nvEquiv = 1,
                nvSubTotal = nvSubTotal,
                nvDPorcDesc01 = porcentajefinal,
                nvDDescto01 = descuento,
                nvDPorcDesc02 = 0,
                nvDDescto02 = 0,
                nvDPorcDesc03 = 0,
                nvDDescto03 = 0,
                nvDPorcDesc04 = 0,
                nvDDescto04 = 0,
                nvDPorcDesc05 = 0,
                nvDDescto05 = 0,
                nvTotDesc = 0,
                nvTotLinea = nvTotLinea,
                nvCantDesp = 0,
                nvCantProd = 0,
                nvCantFact = 0,
                nvCantDevuelto = 0,
                nvCantNC = 0,
                nvCantBoleta = 0,
                nvCantOC = 0,
                DetProd = DetProd,
                CheckeoMovporAlarmaVtas = "N",
                KIT = "",
                CodPromocion = 0,
                CodUMed = CodUMed,
                CantUVta = nvCant,
                Partida = "",
                Pieza = ""
            };
            #endregion

            List<NotaDeVentaDetalleModels> nv = NotaDeVentaDAO.DetalleNV(NVD);
        }

        [NonAction]
        public void VerificationEmail(int nvnumero, string NomCon)
        {

            Control Acceso = new Control();
            var de = "";
            var clavecorreo = "";
            IEnumerable<_NotaDeVentaDetalleModels> datosUser = Acceso.DatosCorreoVend(nvnumero);
            foreach (_NotaDeVentaDetalleModels ot in datosUser)
            {
                de = ot.EmailVend;
                clavecorreo = ot.PassCorreo;
            }


            string to = System.Configuration.ConfigurationManager.AppSettings.Get("Para");
            string from = de;
            string displayName = System.Configuration.ConfigurationManager.AppSettings.Get("Remitente");
            string password = clavecorreo;
            string host = System.Configuration.ConfigurationManager.AppSettings.Get("Host");
            int port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("Port"));
            bool enableSs1 = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("EnableSsl"));
            bool useDefaultCredentials = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("UseDefaultCredentials"));


            var fromEmail = new MailAddress(from, displayName);
            var toEmail = new MailAddress(to);

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSs1,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new NetworkCredential(fromEmail.Address, password)
            };

            var mailWithImg = GetMailWithImg(nvnumero,NomCon);

            if (mailWithImg != null)
            {
                smtp.Send(mailWithImg);
                //smtp.SendAsync(mailWithImg, mailWithImg);
            }
            
        }

        private MailMessage GetMailWithImg(int nvnumero,string NomCon)
        {
            Control Acceso = new Control();
            var de = "";
            var clavecorreo = "";
            IEnumerable<_NotaDeVentaDetalleModels> datosUser = Acceso.DatosCorreoVend(nvnumero);
            foreach (_NotaDeVentaDetalleModels ot in datosUser)
            {
                de = ot.EmailVend;
                clavecorreo = ot.PassCorreo;
            }
            string from = de;
            string subject = string.Format("Cotizacion {0}", nvnumero);

            NotadeVentaCabeceraModels NVentaCabecera = new NotadeVentaCabeceraModels
            {
                NVNumero = nvnumero
            };
            List<NotadeVentaCabeceraModels> NVentaCabeceras = NotaDeVentaDAO.BuscarNVPorNumero(NVentaCabecera);

            List<NotaDeVentaDetalleModels> NVentaDetalles = NotaDeVentaDAO.BuscarNVDETALLEPorNumero(NVentaCabecera);

            ClientesModels cliente = new ClientesModels
            {
                CodAux = NVentaCabeceras[0].CodAux,
                NomCon = NomCon
            };

            List<ClientesModels> contacto = ClientesDAO.GetContacto(cliente);

            List<ClientesModels> clientes = ClientesDAO.GetClientes(cliente);

            ClientesModels Vendedor = new ClientesModels
            {
                VenCod = NVentaCabeceras[0].VenCod
            };

            List<ClientesModels> vendedores = VendedoresSoftlandDAO.GetVendedores(Vendedor);

            MailMessage mail = new MailMessage
            {
                IsBodyHtml = true
            };

            mail.AlternateViews.Add(GetEmbeddedImage(NVentaCabeceras, NVentaDetalles, clientes));
            mail.From = new MailAddress(from);

            if (clientes != null)
            {
                mail.To.Add(vendedores[0].EMail);

                if (contacto[0].EMail == null || contacto[0].EMail.Trim() == "")
                {
                    mail.To.Add(clientes[0].EMail);
                }
                else
                {
                    mail.To.Add(contacto[0].EMail);
                }
                mail.Subject = subject;
                return mail;
            }
            else
            {
                return null;
            }
        }
        /*
        public void SendEmail(string pvarPara, string pvarsAsunto, string pvarsMensaje, string pvarFrom, bool pvarisHtml = false)
        {

            try
            {

                bool wvarEnvia = false;
                MailMessage myMail = new MailMessage();

                myMail.From = new MailAddress("rhurtado@disofi.cl");


                if (pvarPara.Trim() != "")
                {
                    pvarPara = pvarPara + ",";

                    string[] wvarPP = pvarPara.Split(Convert.ToChar(","));

                    foreach (string wvarPara in wvarPP)
                    {
                        if (wvarPara != "")
                        {
                            myMail.To.Add(wvarPara);
                            wvarEnvia = true;
                        }
                    }
                }

                myMail.Subject = pvarsAsunto;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                myMail.Body = pvarsMensaje;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = pvarisHtml;

                SmtpClient mySmtpClient = new SmtpClient();
                mySmtpClient.Host = "mail.disofi.cl";
                mySmtpClient.Port = 25;
                mySmtpClient.EnableSsl = true;


                mySmtpClient.UseDefaultCredentials = false;
                NetworkCredential MyCredentials = new NetworkCredential("fduran@disofi.cl", "D1s0f12019");
                mySmtpClient.Credentials = MyCredentials;

                if (wvarEnvia == true)
                {
                    mySmtpClient.Send(myMail);

                    //ReleaseObject(mySmtpClient);
                    //ReleaseObject(myMail);
                }

            }

            catch (SmtpException ex)
            {
                //WriteLog("SmtpException has occured: " + ex.Message);
            }

        }
        */



        private AlternateView GetEmbeddedImage(List<NotadeVentaCabeceraModels> NVentaCabeceras,
        List<NotaDeVentaDetalleModels> NVentaDetalles, List<ClientesModels> Clientes)
        {
            //var connectionString = "";
            //byte[] arrImage = ms.GetBuffer();
            /*
            using (SqlConnection cn = new SqlConnection(connectionString = "Data Source=SRV-DISOFI; Initial Catalog=DSKUPPEL; User ID=sa; Password=Softland2018; MultipleActiveResultSets=True; TrustServerCertificate =True"))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select Imagen from DS_Imagenes", cn);
                byte[] datosImagen = (byte[])cmd.ExecuteScalar();
                MemoryStream ms = new MemoryStream(datosImagen);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                //arrImage = img;
                
            }
            */
            char[] blanco = { ' ' };

            string htmlBody = String.Format(
            "<html><body>" +
            //Cabecera
            /*"<table width ='400' cellspacing ='1' cellpadding ='3' border ='0' bgcolor ='#000000' align ='center'>" +
            "<tr>" +
            "<td width ='120' align ='left' bgcolor ='#ffffff'>" +
            "<div class='custom - banner'>" +

            "</div>" +
            "</td>" +
            "</tr>" +
            "</table>" +*/
            //
            "<img src='~/Image/logo.png' />" +
            "<H1> COTIZACIÓN </H1>" +
            @"<H4> Nº de Cotización: " + NVentaCabeceras[0].NVNumero + @" </H4>" +
            @"<H4> Fecha Pedido: " + NVentaCabeceras[0].nvFem.ToShortDateString() + @" </H4>" +
            @"<H4> Cliente: " + NVentaCabeceras[0].NomAux + @" </H4>" +
            @"<H4> Dirección: " + Clientes[0].DirAux + @" </H4>" +
            @"<H4> Fecha Entrega: " + NVentaCabeceras[0].nvFeEnt.ToShortDateString() + @" </H4>" +
            @"<H4> Observaciones: " + NVentaCabeceras[0].nvObser + @" </H4>" +
            @"<H4> Vendedor: " + Session["VenDes"].ToString() + @" </H4>" +
            @"<table border = ""1"" >" +
            @"<tr>" +
            @"<td>ID</td>" +
            @"<th nowrap=""nowrap"">Codigo Producto</th>" +
            //@"<th>Imagen</th>" +
            @"<th>Descripcion</th>" +
            @"<th>Cantidad</th>" +
            @"<th>Precio</th>" +
            @"<th>Sub-Total</th>" +
            @"<th>Iva    </th>" +
            @"<th>Total   </th>" +
            @"</tr>");

            ProductosModels producto = new ProductosModels();
            List<LinkedResource> resources = new List<LinkedResource>();
            double Iva = 0;
            double subtotal = 0;
            double ivaux = 0;
            foreach (NotaDeVentaDetalleModels nvd in NVentaDetalles)
            {
                double precioConIVa = Math.Round(nvd.nvSubTotal * 1.19);
                subtotal = subtotal + nvd.nvSubTotal;
                Iva = (precioConIVa - nvd.nvSubTotal);
                ivaux = ivaux + Iva;
                producto.CodProd = nvd.CodProd;
                htmlBody += @"<tr>" +
                           @"<td>" + nvd.nvLinea + @"</td>" +
                           @"<td>" + nvd.CodProd + @"</td>" +
                           @"<td>" + nvd.DesProd + @"</td>" +
                           @"<td style='text-align: right;'>" + nvd.nvCant + @"</td>" +
                           @"<td style='text-align: right;'>" + nvd.nvPrecio + @"</td>" +
                           @"<td style='text-align: right;'>" + nvd.nvSubTotal + @"</td>" +
                           @"<td style='text-align: right;'>" + Iva + @"</td>" +
                           @"<td style='text-align: right;'>" + precioConIVa + @"</td>" +
                           @"</tr>";
            }
            htmlBody += @"<tr><th style='text-align: right;' colspan =" + 7 + @">Sub Total</th><td style='text-align: right;'>" + subtotal  + @"</td></tr>" +
                        @"<tr><th style='text-align: right;' colspan =" + 7 + @">Total Iva</th><td style='text-align: right;'>" + ivaux + @"</td></tr>" +
                        @"<tr><th style='text-align: right;' colspan =" + 7 + @">Total</th><td style='text-align: right;'>" + NVentaCabeceras[0].TotalBoleta + @"</td></tr>";
            htmlBody += @" </body></html>";

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            foreach (LinkedResource r in resources)
            {
                alternateView.LinkedResources.Add(r);
            }
            return alternateView;
        }

        public void AgregarContacto(string CodAux, string NomCon)
        {
            ClientesModels cli = new ClientesModels();
            cli.CodAux = CodAux;
            cli.NomCon = NomCon;

            List<ClientesModels> nv = ClientesDAO.AgregarContacto(cli);
        }



        #region"--- Web Métodos ---"
        //[HttpPost, ValidateInput(false)]
        //public JsonResult AgregarNV(FormCollection frm, int NVNumero, DateTime nvFem, DateTime nvFeEnt, string CodAux, string VenCod,
        //string CodLista, string nvObser, string CveCod, string NomCon, string CodiCC, double nvSubTotal, double nvMonto,
        //double nvNetoAfecto, string Usuario, string UsuarioGeneraDocto, DateTime FechaHoraCreacion, double TotalBoleta,
        //string id, string CodLugarDesp)
        //{
        //    int numSoft = 0;
        //    try
        //    {
        //        #region"NVC"
        //        NotadeVentaCabeceraModels NVC = new NotadeVentaCabeceraModels
        //        {
        //            NVNumero = NVNumero,
        //            nvFem = nvFem,
        //            nvEstado = "A",
        //            nvEstFact = 0,
        //            nvEstDesp = 0,
        //            nvEstRese = 0,
        //            nvEstConc = 0,
        //            nvFeEnt = nvFeEnt,
        //            CodAux = CodAux,
        //            VenCod = VenCod,
        //            CodMon = "01",
        //            CodLista = CodLista,
        //            nvObser = nvObser,
        //            CveCod = CveCod,
        //            NomCon = NomCon,
        //            CodiCC = CodiCC,
        //            nvSubTotal = nvSubTotal,
        //            nvPorcDesc01 = 0,
        //            nvDescto01 = 0,
        //            nvPorcDesc02 = 0,
        //            nvDescto02 = 0,
        //            nvPorcDesc03 = 0,
        //            nvDescto03 = 0,
        //            nvPorcDesc04 = 0,
        //            nvDescto04 = 0,
        //            nvPorcDesc05 = 0,
        //            nvDescto05 = 0,
        //            nvMonto = nvMonto,
        //            NumGuiaRes = 0,
        //            nvPorcFlete = 0,
        //            nvValflete = 0,
        //            nvPorcEmb = 0,
        //            nvEquiv = 1,
        //            nvNetoExento = 0,
        //            nvNetoAfecto = nvNetoAfecto,
        //            nvTotalDesc = 0,
        //            ConcAuto = "N",
        //            CheckeoPorAlarmaVtas = "N",
        //            EnMantencion = 0,
        //            Usuario = Usuario,
        //            UsuarioGeneraDocto = UsuarioGeneraDocto,
        //            FechaHoraCreacion = FechaHoraCreacion,
        //            Sistema = "NW",
        //            ConcManual = "N",
        //            proceso = "Notas de Venta",
        //            TotalBoleta = TotalBoleta,
        //            NumReq = 0,
        //            CodVenWeb = "5",
        //            CodLugarDesp = CodLugarDesp
        //        };
        //        #endregion

        //        List<ParametrosModels> para = ParametrosDAO.BuscarParametros();

        //        if (para[0].Aprobador == 1)
        //        {
        //            NVC.EstadoNP = "P";
        //        }
        //        else
        //        {
        //            NVC.EstadoNP = "A";
        //        }

        //        if (para[0].Aprobador == 1)
        //        {
        //            List<NotadeVentaCabeceraModels> NVSoft = NotaDeVentaDAO.EditarNV(NVC);
        //            ViewBag.NVnum = NVC.NVNumero;
        //            numSoft = NVC.NVNumero;
        //        }
        //        else
        //        {
        //            List<NotadeVentaCabeceraModels> NVSoft = NotaDeVentaDAO.InsertarNvSoftland(NVC);
        //            ViewBag.NVnum = NVSoft[0].NVNumero;
        //            numSoft = NVSoft[0].NVNumero;
        //        }


        //        //EMail
        //        VerificationEmail(NVNumero,NomCon);
        //        //return Json(NV);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw(ex);
        //    }

        //    return Json(new { ID = id, NVNUM = numSoft });

        //}

        [HttpPost, ValidateInput(false)]
        public JsonResult AgregarNV(FormCollection frm, int NVNumero, DateTime nvFem, DateTime nvFeEnt, string CodAux, string VenCod,
        string CodLista, string nvObser, string CveCod, string NomCon, string CodiCC, double nvSubTotal, double nvMonto,
        double nvNetoAfecto, string Usuario, string UsuarioGeneraDocto, DateTime FechaHoraCreacion, double TotalBoleta,
        string id, string CodLugarDesp)
        {
            #region"NVC"
            try
            {
                NotadeVentaCabeceraModels NVC = new NotadeVentaCabeceraModels
                {
                    NVNumero = NVNumero,
                    nvFem = nvFem,
                    nvEstado = "P",
                    nvEstFact = 0,
                    nvEstDesp = 0,
                    nvEstRese = 0,
                    nvEstConc = 0,
                    nvFeEnt = nvFeEnt,
                    CodAux = CodAux,
                    VenCod = VenCod,
                    CodMon = "01",
                    CodLista = CodLista,
                    nvObser = nvObser,
                    CveCod = CveCod,
                    NomCon = NomCon,
                    CodiCC = CodiCC,
                    nvSubTotal = nvSubTotal,
                    nvPorcDesc01 = 0,
                    nvDescto01 = 0,
                    nvPorcDesc02 = 0,
                    nvDescto02 = 0,
                    nvPorcDesc03 = 0,
                    nvDescto03 = 0,
                    nvPorcDesc04 = 0,
                    nvDescto04 = 0,
                    nvPorcDesc05 = 0,
                    nvDescto05 = 0,
                    nvMonto = nvMonto,
                    NumGuiaRes = 0,
                    nvPorcFlete = 0,
                    nvValflete = 0,
                    nvPorcEmb = 0,
                    nvEquiv = 1,
                    nvNetoExento = 0,
                    nvNetoAfecto = nvNetoAfecto,
                    nvTotalDesc = 0,
                    ConcAuto = "N",
                    CheckeoPorAlarmaVtas = "N",
                    EnMantencion = 0,
                    Usuario = Usuario,
                    UsuarioGeneraDocto = UsuarioGeneraDocto,
                    FechaHoraCreacion = FechaHoraCreacion,
                    Sistema = "NW",
                    ConcManual = "N",
                    proceso = "Notas de Venta",
                    TotalBoleta = TotalBoleta,
                    NumReq = 0,
                    CodVenWeb = "5",
                    CodLugarDesp = CodLugarDesp
                };
                #endregion

                List<ParametrosModels> para = ParametrosDAO.BuscarParametros();

                if (para[0].Aprobador == 1)
                {
                    NVC.EstadoNP = "P";
                }
                else
                {
                    NVC.EstadoNP = "P";
                }

                if (para[0].Aprobador == 1)
                {
                    List<NotadeVentaCabeceraModels> NV = NotaDeVentaDAO.EditarNV(NVC);
                }
                else
                {

                }
                //EMail
                VerificationEmail(NVNumero, NomCon);
                return Json(new { ID = id });
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            //return RedirectToAction("Misclientes", "Ventas", new { ID = id });

        }

        [HttpPost]
        public JsonResult ProductSearch(string CodProd, string CodLista)
        {
            ProductosModels prod = new ProductosModels();
            prod.CodProd = CodProd;
            prod.DesProd = CodProd;
            prod.CodLista = CodLista;

            List<ProductosModels> pro = ProductosDAO.BuscarProducto(prod);

            return Json(pro);
        }

        [HttpPost]
        public JsonResult QuickProductSearch(string CodRapido, string CodLista)
        {
            ProductosModels prod = new ProductosModels();
            prod.CodRapido = CodRapido;
            prod.CodLista = CodLista;

            List<ProductosModels> pro = ProductosDAO.BuscarProductoRapido(prod);

            return Json(pro);
        }

        [HttpPost]
        public JsonResult Multiplicar(string punitario, string cantidad)
        {
            double u = double.Parse(punitario);
            double c = double.Parse(cantidad);

            double t = u * c;

            return Json(t.ToString());
        }

        [HttpPost]
        public JsonResult Porcentaje(string valor, string porcenta)
        {
            CultureInfo culture = new CultureInfo("en-US");

            double u = double.Parse(valor);
            double c = double.Parse(porcenta, CultureInfo.InvariantCulture);
            double Decimal = c / 100;
            double t = u * Decimal;
            double total = u - t;
            total = Math.Round(total);

            return Json(total.ToString());
        }
        #endregion

    }
}
