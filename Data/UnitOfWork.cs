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

namespace Actividad_Facultad.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private InvoiceRepository _InvoiceRepository;

        public UnitOfWork()
        {
            _connection = DataHelper.GetConnection();
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

        public void SaveChanges()
        {
            try
            {

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
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
    }
}
