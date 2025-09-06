using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Domain
{
    public class Article
    {
        public int ArticuloID { get; set; }
        public string NombreArticulo { get; set; }

        public Decimal PrecioUnitario { get; set; }

        public override string ToString()
        {
            return $"Codigo: {ArticuloID}, Nombre: {NombreArticulo}, Precio Unitario: {PrecioUnitario}";
        }
    }
}
