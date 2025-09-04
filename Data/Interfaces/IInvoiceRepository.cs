using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Interfaces
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice GetById(int id);
        int Save(Invoice invoice);
        int Delete(int id);
    }
}
