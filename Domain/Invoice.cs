using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Domain
{
    public class Invoice
    {
        public int NroFactura { get; set; }
        public DateOnly Fecha { get; set; }
        public int FormaPagoID { get; set; }
        public string Cliente { get; set; }


    }
}
