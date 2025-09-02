using Actividad_Facultad.Data.Repositories;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Service
{
    public class ArticleService
    {
        private IGenericRepository<Article> _RepositoryArticle;

        public ArticleService()
        {
            _RepositoryArticle = new ArticleRepository();
        }

        public List<Article> GetArticles()
        {
            return _RepositoryArticle.GetAll();
        }

        public Article? GetArticleById(int id)
        {
            return _RepositoryArticle.GetById(id);
        }

        public int articleSave(Article article1)
        {
            return _RepositoryArticle.Save(article1);
        }
        public bool articleDelete(int id)
        {
            return _RepositoryArticle.Delete(id);
        }
    }
}
