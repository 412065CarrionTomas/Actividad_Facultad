using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Repositories
{
    public class InvoiceRepository : IGenericRepository<Invoice>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
