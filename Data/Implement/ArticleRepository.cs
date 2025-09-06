using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Utils;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Repositories
{
    public class ArticleRepository : IGenericRepository<Article>
    {
        //AGREGAR VARIABLE CLASE DE LA CLASE
        private IUnitOfWork _unitOfWork;
        //DI
        public ArticleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int Delete(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = id
                }
            };

            return _unitOfWork.ExecuteSPNonQuery("sp_Articulo_Delete", parameters);
            
        }

        public List<Article> GetAll()
        {
            List<Article> article = new List<Article>();
            var dt = _unitOfWork.ExecuteSPQuery("sp_Articulo_Get");
            foreach(DataRow row in dt.Rows)
            {
                Article a = new Article();
                a.ArticuloID = (int)row["articuloID"];
                a.NombreArticulo = (string)row["nombreArticulo"];
                a.PrecioUnitario = (decimal)row["precioUnitario"];
                article.Add(a);
            }
            return article;
            
        }

        public Article? GetById(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = id
                }
            };
            var dt = _unitOfWork.ExecuteSPQuery("sp_Articulo_Get", parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                Article a = new Article();
                a.ArticuloID = (int)dt.Rows[0]["articuloID"];
                a.NombreArticulo = (string)dt.Rows[0]["nombreArticulo"];
                a.PrecioUnitario = (decimal)dt.Rows[0]["precioUnitario"];
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
                    nombre = "@nombreArticulo",
                    valor = article.NombreArticulo
                },
                new ParameterSP()
                {
                    nombre = "@precioUnitario",
                    valor = article.PrecioUnitario
                }
            };
            return _unitOfWork.ExecuteSPWithReturn("sp_articulo_UPSERT", parameters);
        }
    }
}
