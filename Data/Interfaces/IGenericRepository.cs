using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //OBTENER LISTA
        List<T> GetAll();
        //OBTENER POR ID
        T GetById(int id);
        //UPDATE OR INSERT
        int Save(T entity);
        //DELETE
        int Delete(int id);
    }
}
