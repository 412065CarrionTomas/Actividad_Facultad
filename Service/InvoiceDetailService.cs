using Actividad_Facultad.Data;
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
        //agregar variable de clase de la clase 
        private readonly IUnitOfWork _UnitOfWork;
        //Inyeccion DI
        public InvoiceDetailService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        //LOGICA NEGOCIO
        public List<InvoiceDetail> GetInvoiceDetailsLts()
        {
            return _UnitOfWork.InvoicesDetailRepository.GetAll();
        }
        public InvoiceDetail GetInvoiceDetail(int id)
        {
            return _UnitOfWork.InvoicesDetailRepository.GetById(id);
        }
        public int DeleteInvoiceDetail(int id)
        {
            int result = _UnitOfWork.InvoicesDetailRepository.Delete(id);
            _UnitOfWork.Commit();
            return result;
        }
        public int SaveInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            int result = _UnitOfWork.InvoicesDetailRepository.Save(invoiceDetail);
            _UnitOfWork.Commit();
            return result;
        }
    }
}
