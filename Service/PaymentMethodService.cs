using Actividad_Facultad.Data;
using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Service
{
    public class PaymentMethodService
    {
        //agregar variable clase de la clase 
        private readonly IUnitOfWork _UnitOfWork;
        //inyeccion de la DI
        public PaymentMethodService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        //LOGICA NEGOCIO
        public List<PaymentMethod> GetPaymentMethodsLts()
        {
            List<PaymentMethod> lts = _UnitOfWork.PaymentMethodRepository.GetAll();
            return lts;
        }
        public PaymentMethod GetPaymenMethod(int id)
        {
            return _UnitOfWork.PaymentMethodRepository.GetById(id);
        }
        public int DeletePaymentMethod(int id)
        {
            int result = _UnitOfWork.PaymentMethodRepository.Delete(id);
            _UnitOfWork.Commit();
            return result;
        }
        public int SavePaymentMethod(PaymentMethod paymentMethod)
        {
            int result = _UnitOfWork.PaymentMethodRepository.Save(paymentMethod);
            _UnitOfWork.Commit();
            return result;
        }
    }
}
