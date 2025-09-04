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
    public class PaymentMethodService
    {
        IPaymentMethod _PaymentMethodService;
        public PaymentMethodService()
        {
            _PaymentMethodService = new PaymentMethodRepository();
        }
    }
}
