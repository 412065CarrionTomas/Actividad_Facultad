using Actividad_Facultad.Data;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Repositories;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Service
{
    public class InvoiceServices
    {
        IInvoiceRepository _InvoiceRepository;

        public InvoiceServices(UnitOfWork uof)
        {
            _InvoiceRepository = uof.InvoiceRepository;
        }
    }
}
