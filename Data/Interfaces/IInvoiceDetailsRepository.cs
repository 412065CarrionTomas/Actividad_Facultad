using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Interfaces
{
    public interface IInvoiceDetailsRepository
    {
        List<InvoiceDetail> GetAll();
        InvoiceDetail GetById(int id);
        int Save(InvoiceDetail invoiceDetail);
        int Delete(int id);
    }
}
