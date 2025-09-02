using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Repositories
{
    internal class ArticleRepository : IGenericRepository<Article>
    {
        public bool Delete(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Articulo_Delete", parameters);
            if(dt != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Article> GetAll()
        {
            List<Article> lista = new List<Article>();
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Articulo_Get");
            foreach(DataRow row in dt.Rows)
            {
                Article a = new Article();
                a.ArticuloID = (int)row[0];
                a.Descripcion = (string)row[1];
                lista.Add(a);
            }
            return lista;
            
        }

        public Article? GetById(int id)
        {
            List<ParameterSP> parametro = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = id
                }
            };
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Articulo_Get", parametro);
            if(dt != null && dt.Rows.Count > 0)
            {
                Article a = new Article();
                a.ArticuloID = (int)dt.Rows[0]["articuloID"];
                a.Descripcion = (string)dt.Rows[0]["descripcion"];
                return a;
            }
            return null;
        }

        public int Save(Article article)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = article.ArticuloID
                },
                new ParameterSP()
                {
                    nombre = "@descripcion",
                    valor = article.Descripcion
                }
            };
            return DataHelper.GetInstance().ExcecuteSPCatchInt("SP_GUARDAR_ARTICULO", parameters);
        }
    }
}
