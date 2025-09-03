using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Implement
{
    public class InvoiceDetailRepository : IGenericRepository<InvoiceDetail>
    {
        public int Delete(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroDetalle",
                    valor = id
                }
            };
            return DataHelper.GetInstance().ExecuteSPNoQuery("sp_DetalleFactura_Delete", parameters);
            
        }

        public List<InvoiceDetail> GetAll()
        {
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_DetalleFactura_Get");
            foreach(DataRow row in dt.Rows)
            {
                InvoiceDetail i = new InvoiceDetail()
                {
                    nroDetalle = (int)row["nroDetalle"],
                    nroFactura = (int)row["nroFactura"],
                    articuloID = (int)row["articuloID"],
                    cantidad = (int)row["cantidad"]
                };
                invoiceDetails.Add(i);
            }
            return invoiceDetails;
        }

        public InvoiceDetail GetById(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroDetalle",
                    valor = id
                }
            };
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_DetalleFactura_Get", parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    nroDetalle = (int)dt.Rows[0]["nroDetalle"],
                    nroFactura = (int)dt.Rows[1]["nroFactura"],
                    articuloID = (int)dt.Rows[2]["articuloID"],
                    cantidad = (int)dt.Rows[3]["cantidadID"]
                };
                return invoiceDetail;
            }
            return null;
        }

        public int Save(InvoiceDetail invoiceDetail)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroDetalle",
                    valor = invoiceDetail.nroDetalle
                },
                new ParameterSP()
                {
                    nombre = "@nroFactura",
                    valor = invoiceDetail.nroFactura
                },
                new ParameterSP()
                {
                    nombre = "@articuloID",
                    valor = invoiceDetail.articuloID
                },
                new ParameterSP()
                {
                    nombre = "@cantidad",
                    valor = invoiceDetail.cantidad
                }
            };
            return DataHelper.GetInstance().ExcecuteSPCatchInt("SP_GUARDAR_DETALLEFACTURA", parameters);
        }
    }
}
