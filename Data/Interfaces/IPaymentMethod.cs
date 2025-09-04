using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Interfaces
{
    public interface IPaymentMethod
    {
        List<PaymentMethod> GetAll();
        PaymentMethod GetById(int id);
        int Save(PaymentMethod paymentMethod);
        int Delete(int id);
    }
}
