using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Objetos
{
    public class _NotaDeVentaDetalleModels
    {
        public int NVNumero { get; set; }
        public double nvLinea { get; set; }
        public double nvCorrela { get; set; }
        public DateTime nvFecCompr { get; set; }
        public string CodProd { get; set; }
        public double nvCant { get; set; }
        public double nvPrecio { get; set; }
        public double nvEquiv { get; set; }
        public double nvSubTotal { get; set; }
        public double nvDPorcDesc01 { get; set; }
        public double nvDDescto01 { get; set; }
        public double nvDPorcDesc02 { get; set; }
        public double nvDDescto02 { get; set; }
        public double nvDPorcDesc03 { get; set; }
        public double nvDDescto03 { get; set; }
        public double nvDPorcDesc04 { get; set; }
        public double nvDDescto04 { get; set; }
        public double nvDPorcDesc05 { get; set; }
        public double nvDDescto05 { get; set; }
        public double nvTotDesc { get; set; }
        public double nvTotLinea { get; set; }
        public double nvCantDesp { get; set; }
        public double nvCantProd { get; set; }
        public double nvCantFact { get; set; }
        public double nvCantDevuelto { get; set; }
        public double nvCantNC { get; set; }
        public double nvCantBoleta { get; set; }
        public double nvCantOC { get; set; }
        public string DetProd { get; set; }
        public string CheckeoMovporAlarmaVtas { get; set; }
        public string KIT { get; set; }
        public int CodPromocion { get; set; }
        public string CodUMed { get; set; }
        public double CantUVta { get; set; }
        public string Partida { get; set; }
        public string Pieza { get; set; }
        public DateTime FechaVencto { get; set; }

        public string DesProd { get; set; }

        public double Stock { get; set; }

        public double Iva { get; set; }

        public string EmailVend { get; set; }

        public string PassCorreo { get; set; }

    }
}
