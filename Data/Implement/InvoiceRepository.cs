using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private readonly UnitOfWork _unitOfWork;

        public InvoiceRepository(UnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public InvoiceRepository(SqlConnection cnn, SqlTransaction t)
        {
            _connection = cnn;
            _transaction = t;
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
            return DataHelper.GetInstance().ExecuteSPNoQuery("sp_Factura_Delete", parameters);
            
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> invoices = new List<Invoice>();
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("sp_Factura_Get");
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
                        paymentMethod = new PaymentMethod()
                        {
                            FormaPagoID = (int)row["formaPagoID"]
                        }
                    };
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
                idFactura = _unitOfWork.SaveChangesWithOutput("sp_INSERTAR_MAESTRO", paramInvoice, "@nroFactura");

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
                _unitOfWork.Commit();
                return idFactura;

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
            return idFactura;
        }
        //public int Save(Invoice invoice)
        //{
        //    int result = -1;
        //    SqlConnection cnn = null;
        //    SqlTransaction t = null;
        //    try
        //    {
        //        cnn = DataHelper.GetConnection();
        //        cnn.Open();
        //        t = cnn.BeginTransaction();
        //        var cmd = new SqlCommand("sp_INSERTAR_MAESTRO", cnn, t);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@cliente", invoice.Cliente); 
        //        cmd.Parameters.AddWithValue("@formaPagoID", invoice.paymentMethod.FormaPagoID);
        //        cmd.Parameters.AddWithValue("@nroFactura", invoice.NroFactura);
        //        var param = new SqlParameter()
        //        {
        //            ParameterName = "@nroFactura",
        //            Direction = ParameterDirection.Output,
        //            SqlDbType = SqlDbType.Int
        //        };
        //        cmd.Parameters.Add(param);
        //        cmd.ExecuteNonQuery();
        //        int invoiceDetailID = (int)param.Value;
        //        foreach(var invoiceDetail in invoice.invoiceDetailsList)
        //        {
        //            var cmdDetail = new SqlCommand("sp_INSERTAR_ALUMNO", cnn, t);
        //            cmdDetail.CommandType = CommandType.StoredProcedure;
        //            cmdDetail.Parameters.AddWithValue("@nroFactura", invoiceDetail.nroFactura);
        //            cmdDetail.Parameters.AddWithValue("@articuloID", invoiceDetail.article.ArticuloID);
        //            cmdDetail.Parameters.AddWithValue("@cantidad", invoiceDetail.cantidad);
        //            cmdDetail.ExecuteNonQuery();
        //        }
        //        t.Commit(); 
        //    }
        //    catch (SqlException e)
        //    {
        //        if (t != null)
        //        {
        //            t.Rollback();
        //        }
                
        //        return -1;
        //    }
        //    finally
        //    {
        //        if(cnn != null && cnn.State == ConnectionState.Open)
        //        {
        //            cnn.Close();
        //        }
        //        cnn.Close();
        //    }
             
        //    List<ParameterSP> parameters = new List<ParameterSP>()
        //    {
        //        new ParameterSP()
        //        {
        //            nombre = "@nroFactura",
        //            valor = invoice.NroFactura
        //        },
        //        new ParameterSP()
        //        {
        //            nombre = "@fecha",
        //            valor = invoice.Fecha
        //        },
        //        new ParameterSP()
        //        {
        //            nombre = "@formaPagoID",
        //            valor = invoice.paymentMethod.FormaPagoID
        //        },
        //        new ParameterSP()
        //        {
        //            nombre = "@cliente",
        //            valor = invoice.Cliente
        //        }

        //    };
        //    return DataHelper.GetInstance().ExcecuteSPCatchInt("SP_GUARDAR_FACTURA", parameters);
        //}
    }
}
