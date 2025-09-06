using Actividad_Facultad.Data.Repositories;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensibility;
using Actividad_Facultad.Data;
using System.IO.Pipes;

namespace Actividad_Facultad.Service
{
    public class ArticleService
    {
        //AGREGAR VARIABLE CLASE DE LA CLASE
        private readonly IUnitOfWork _unitOfWork;
        //INYECCION DE DEPENDENCIAS
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //LOGICA DE NEGOCIO
        public List<Article> GetArticlesLts()
        {
            List<Article> lts = _unitOfWork.ArticleRepository.GetAll();
            return lts;
        }
        public Article GetArticle(int id)
        {
            Article a = _unitOfWork.ArticleRepository.GetById(id);
            return a;
        }
        public int DeleteArticle(int id)
        {
            int result = _unitOfWork.ArticleRepository.Delete(id);
            _unitOfWork.Commit();
            return result;
        }
        public int SaveArticle(Article a)
        {
            int result = _unitOfWork.ArticleRepository.Save(a);
            _unitOfWork.Commit();
            return result;
        }


    }
}
