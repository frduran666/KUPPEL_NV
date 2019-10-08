using ET.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Control
    {
        private AccesoDatos _Control = new AccesoDatos();

        public List<_NotaDeVentaDetalleModels> DatosCorreoVend(int NvNUmero)
        {
            return _Control.DatosCorreoVend(NvNUmero);
        }

        public List<_NotaDeVentaDetalleModels> DatosCorreoAprobador(string vendCodi)
        {
            return _Control.DatosCorreoAprobador(vendCodi);
        }
        

        public bool ActualizaCorreo(_UsuariosModels Usuario)
        {
            return _Control.ActualizaCorreo(Usuario);
        }
    }
}
