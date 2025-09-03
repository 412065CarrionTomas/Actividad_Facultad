using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
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
            return DataHelper.GetInstance().ExecuteSPNoQuery("sp_Factura_Delete", parameters);
            
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> invoices = new List<Invoice>();
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Factura_Get");
            foreach(DataRow row in dt.Rows) 
            {
                Invoice i =  new Invoice()
                {
                    NroFactura = (int)row["nroFactura"],
                    Cliente = (string)row["cliente"],
                    Fecha = (DateTime)row["fecha"],
                    FormaPagoID = (int)row["formaPagoID"],
                };
                invoices.Add(i);
            }
            return invoices;
        }

        public Invoice GetById(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroFactura",
                    valor = id
                }
            };
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Factura_Get", parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Invoice i = new Invoice()
                    {
                        NroFactura = (int)row["nroFactura"],
                        Cliente = (string)row["cliente"],
                        Fecha = (DateTime)row["fecha"],
                        FormaPagoID = (int)row["formaPagoID"],
                    };
                }
            }
            return null;
        }

        public int Save(Invoice invoice)
        {
            
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroFactura",
                    valor = invoice.NroFactura
                },
                new ParameterSP()
                {
                    nombre = "@fecha",
                    valor = invoice.Fecha
                },
                new ParameterSP()
                {
                    nombre = "@formaPagoID",
                    valor = invoice.FormaPagoID
                },
                new ParameterSP()
                {
                    nombre = "@cliente",
                    valor = invoice.Cliente
                }

            };
            return DataHelper.GetInstance().ExcecuteSPCatchInt("SP_GUARDAR_FACTURA", parameters);
        }
    }
}
