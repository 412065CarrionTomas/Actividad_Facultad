using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Utils;
using Actividad_Facultad.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Quic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Implement
{
    public class InvoiceDetailRepository : IGenericRepository<InvoiceDetail>
    {
        //AGREGAR VARIABLE CLASE DE LA CLASE
        private IUnitOfWork? _unitOfWork;

        //CONSTRUCTOR DI
        public InvoiceDetailRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
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
            return _unitOfWork.ExecuteSPNonQuery("sp_DetalleFactura_Delete", parameters);
            
        }

        public List<InvoiceDetail> GetAll()
        {
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            var dt = _unitOfWork.ExecuteSPQuery("sp_DetalleFactura_Get");
            foreach(DataRow row in dt.Rows)
            {
                InvoiceDetail i = new InvoiceDetail()
                {
                    nroDetalle = (int)row["nroDetalle"],
                    nroFactura = (int)row["nroFactura"],
                    article = new Article()
                    {
                        ArticuloID = (int)row["articuloID"]
                    },
                    cantidad = (int)row["cantidad"]
                };
                invoiceDetails.Add(i);
            }
            return invoiceDetails;
        }

        public InvoiceDetail? GetById(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    nombre = "@nroDetalle",
                    valor = id
                }
            };
            var dt = _unitOfWork.ExecuteSPQuery("sp_DetalleFactura_Get", parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    nroDetalle = (int)dt.Rows[0]["nroDetalle"],
                    nroFactura = (int)dt.Rows[0]["nroFactura"],
                    article = new Article()
                    {
                        ArticuloID = (int)dt.Rows[0]["articuloID"]
                    },
                    cantidad = (int)dt.Rows[0]["cantidad"]
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
                    valor = invoiceDetail.article.ArticuloID
                },
                new ParameterSP()
                {
                    nombre = "@cantidad",
                    valor = invoiceDetail.cantidad
                }
            };
            return _unitOfWork.ExecuteSPWithReturn("sp_DetalleFactura_UPSERT", parameters);
        }
    }
}
