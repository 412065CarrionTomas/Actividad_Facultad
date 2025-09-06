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
        //agregar variable de clase de la clase 
        private readonly IUnitOfWork _UnitOfWork;
        //Inyeccion DI
        public InvoiceServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        //LOGICA NEGOCIO
        public List<Invoice> GetInvoicesLts()
        {
            return _UnitOfWork.InvoiceRepository.GetAll();
        }
        public Invoice GetInvoice(int id)
        {
            return _UnitOfWork.InvoiceRepository.GetById(id);
        }
        public int DeleteInvoice(int id)
        {
            int result = _UnitOfWork.InvoiceRepository.Delete(id);
            _UnitOfWork.Commit();
            return result;
        }
        public int SaveInvoice(Invoice invoice)
        {
            try
            {
                int result = _UnitOfWork.InvoiceRepository.Save(invoice);
                _UnitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
