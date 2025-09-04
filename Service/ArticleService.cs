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
        private IArticleRepository _ArticleRepository;

        public ArticleService()
        {
            _ArticleRepository = new ArticleRepository();
        }

        public List<Article> GetArticles()
        {
            return _ArticleRepository.GetAll();
        }

        public Article? GetArticleById(int id)
        {
            return _ArticleRepository.GetById(id);
        }

        public int articleSave(Article article1)
        {
            return _ArticleRepository.Save(article1);
        }
        public int articleDelete(int id)
        {
            return _ArticleRepository.Delete(id);
        }
    }
}
