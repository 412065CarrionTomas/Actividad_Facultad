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
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return $"Codigo: {ArticuloID}, Nombre: {Descripcion}";
        }
    }
}
