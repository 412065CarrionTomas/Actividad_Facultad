using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Repositories;
using Actividad_Facultad.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Actividad_Facultad.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private InvoiceRepository _InvoiceRepository;

        public UnitOfWork()
        {
            _connection = new SqlConnection(Properties.Resources.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IInvoiceRepository InvoiceRepository
        {
            get
            {
                if(_InvoiceRepository == null)
                {
                    _InvoiceRepository = new InvoiceRepository(_connection, _transaction);
                }
                return _InvoiceRepository;
            }
        }

        //TRANSACCION CON OUTPUT
        public int SaveChangesWithOutput(string sp, List<ParameterSP>? parameterSPs, string outPutName)
        {
            int result = -1;
            try
            {
                using (var cmdMaestro = new SqlCommand(sp, _connection, _transaction))
                {
                    cmdMaestro.CommandType = CommandType.StoredProcedure;
                    if (parameterSPs != null)
                    {
                        foreach (ParameterSP param in parameterSPs)
                        {
                            cmdMaestro.Parameters.AddWithValue(param.nombre, param.valor);
                        }
                    }
                    var idOutput = new SqlParameter()
                    {
                        ParameterName = outPutName,
                        Direction = ParameterDirection.Output,
                        DbType = DbType.Int32
                    };
                    cmdMaestro.Parameters.Add(idOutput);
                    cmdMaestro.ExecuteNonQuery();
                    var idVariable = (int)idOutput.Value;
                    if(idOutput.Value != DBNull.Value)
                    {
                        return idVariable;
                    }
                    else { return -1; }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TRANSACCION SIN DEVOLUCION
        public int SaveChanges(string sp, List<ParameterSP>? parameters)
        {
            int result = -1;
            try
            {
                using(var cmdAlumno = new SqlCommand(sp, _connection, _transaction))
                {
                    cmdAlumno.CommandType = CommandType.StoredProcedure;
                    if(parameters != null)
                    {
                        foreach(ParameterSP param in parameters)
                        {
                            cmdAlumno.Parameters.AddWithValue(param.nombre, param.valor);
                        }
                    }
                    result = cmdAlumno.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
            }
            if(_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
