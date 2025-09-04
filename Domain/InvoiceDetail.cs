using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Domain
{
    public class InvoiceDetail
    {
        public int nroDetalle { get; set; }
        public int nroFactura { get; set; }
        public Article article { get; set; }
        public int cantidad { get; set; }
    }
}
