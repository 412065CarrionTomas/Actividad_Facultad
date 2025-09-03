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
        IGenericRepository<Invoice> _InvoiceRepository;

        public InvoiceServices()
        {
            _InvoiceRepository = new InvoiceRepository();
        }
    }
}
