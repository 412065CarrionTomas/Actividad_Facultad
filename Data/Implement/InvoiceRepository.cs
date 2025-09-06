using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Utils;
using Actividad_Facultad.Domain;
using Actividad_Facultad.Service;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Repositories
{
    public class InvoiceRepository : IGenericRepository<Invoice>
    {
        //AGREGAR VARIABLE CLASE DE LA CLASE
        private IUnitOfWork? _unitOfWork;
        //DI
        public InvoiceRepository(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public int Delete(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroFactura",
                    valor = id
                }
            };
            return _unitOfWork.ExecuteSPNonQuery("sp_Factura_Delete", parameters);
            
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> invoices = new List<Invoice>();
            var dt = _unitOfWork.ExecuteSPQuery("sp_Factura_Get");
            foreach(DataRow row in dt.Rows) 
            {
                Invoice i = new Invoice()
                {
                    NroFactura = (int)row["nroFactura"],
                    Cliente = (string)row["cliente"],
                    Fecha = (DateTime)row["fecha"],
                    paymentMethod = new PaymentMethod()
                    {
                        FormaPagoID = (int)row["formaPagoID"]
                    }
                };
                invoices.Add(i);
            }
            return invoices;
        }

        public Invoice? GetById(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroFactura",
                    valor = id
                }
            };
            var dt = _unitOfWork.ExecuteSPQuery("sp_Factura_Get", parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Invoice i = new Invoice()
                    {
                        NroFactura = (int)row["nroFactura"],
                        Cliente = (string)row["cliente"],
                        Fecha = (DateTime)row["fecha"],
                        paymentMethod = new PaymentMethod()
                        {
                            FormaPagoID = (int)row["formaPagoID"]
                        }
                    };
                    return i;
                }
            }
            return null;
        }
        public int Save(Invoice invoice)
        {
            int idFactura = -1;
            int resultOk = -1;
            try
            {
                List<ParameterSP> paramInvoice = new List<ParameterSP>()
                {
                    new ParameterSP()
                    {
                        nombre = "@formaPagoID",
                        valor = invoice.paymentMethod.FormaPagoID
                    },
                    new ParameterSP()
                    {
                        nombre = "@cliente",
                        valor = invoice.Cliente
                    }
                };
                idFactura = _unitOfWork.SaveChangesWhitOutput("sp_INSERTAR_MAESTRO","@nroFactura", paramInvoice);

                foreach(var invoiceDetail in invoice.invoiceDetailsList)
                {
                    List<ParameterSP> paramDetails = new List<ParameterSP>()
                    {
                        new ParameterSP()
                        {
                            nombre = "@nroFactura",
                            valor = idFactura
                        },
                        new ParameterSP()
                        {
                            nombre = "@articuloID",
                            valor = invoiceDetail.article.ArticuloID
                        },
                        new ParameterSP()
                        {
                            nombre = "@cantidad",
                            valor = invoiceDetail.cantidad
                        }
                    };
                    resultOk = _unitOfWork.SaveChanges("sp_INSERTAR_ALUMNO", paramDetails);
                }
                
                return idFactura;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
