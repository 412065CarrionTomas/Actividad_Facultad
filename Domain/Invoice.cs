using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Domain
{
    public class Invoice
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public PaymentMethod paymentMethod{ get; set; }
        public string Cliente { get; set; }

        public List<InvoiceDetail> invoiceDetailsList { get; set; }

        public Invoice()
        {
            invoiceDetailsList = new List<InvoiceDetail>(); 
        }
        public void AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            invoiceDetailsList.Add(invoiceDetail);
        }
        public void DeleteInvoiceDetail(int index)
        {
            invoiceDetailsList.RemoveAt(index);
        }
        public double Total()
        {
            double result = 0;
            foreach(InvoiceDetail invoiceDetail in invoiceDetailsList)
            {
                result += invoiceDetail.cantidad;//CAMBIAR POR PRECIO
            }
            return result;
        }
    }
}
