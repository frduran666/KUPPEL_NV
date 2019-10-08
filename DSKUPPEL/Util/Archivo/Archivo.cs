using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DS_NotaVenta.Util.Archivo
{
    public class Archivo
    {
        public static RetornoCrearArchivo CrearArchivo(HttpServerUtilityBase serverUtilityBase, string texto)
        {
            string nombreArchivo = "error_"
                + DateTime.Now.ToString("yyyyMMdd HHmmss.fff");

            string pathArchivo = serverUtilityBase.MapPath("~/ArchivosTemporales/" + nombreArchivo + ".txt");


            if (File.Exists(pathArchivo))
            {
                File.WriteAllText(pathArchivo, String.Empty);
            }

            using (var tw = new StreamWriter(pathArchivo, true))
            {
                tw.WriteLine(texto);
            }

            RetornoCrearArchivo retornoCrearArchivo = new RetornoCrearArchivo();
            retornoCrearArchivo.Id = nombreArchivo;
            retornoCrearArchivo.Path = pathArchivo;

            return retornoCrearArchivo;
        }
    }
    public class RetornoCrearArchivo
    {
        public string Id { get; set; }
        public string Path { get; set; }
    }
}