using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Interfaces
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        Article GetById(int id);
        int Save(Article article);
        int Delete(int id);
    }
}
