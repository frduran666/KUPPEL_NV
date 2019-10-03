using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSKUPPEL.Models
{
    public class ProductosModels
    {
        public string CodProd { get; set; }
        public string DesProd { get; set; }
        public string CodGrupo { get; set; }
        public string CodSubGr { get; set; }
        public double PrecioVta { get; set; }
        public string codumed { get; set; }
        public string desumed { get; set; }
        public string CodLista { get; set; }
        public string CodRapido { get; set; }

        public double Stock { get; set; }
    }
}