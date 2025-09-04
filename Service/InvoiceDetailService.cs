using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Service
{
    public class InvoiceDetailService
    {
        IInvoiceDetailsRepository _InvoiceDetailRepository;
        public InvoiceDetailService()
        {
            _InvoiceDetailRepository = new InvoiceDetailRepository();
        }
    }
}
