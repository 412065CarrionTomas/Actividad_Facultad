﻿using Actividad_Facultad.Data.Implement;
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
using Actividad_Facultad.Data.Utils;

namespace Actividad_Facultad.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private InvoiceRepository? _InvoiceRepository;
        private InvoiceDetailRepository? _InvoiceDetailRepository;
        private ArticleRepository? _ArticleRepository;
        private PaymentMethodRepository? _PaymentMethodRepository;

        public UnitOfWork()
        {
            _connection = new SqlConnection(Properties.Resources.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        //INSTANCIAS DE CADA REPO COMO PROPIEDADES (lazy initialization)
        public IGenericRepository<Invoice>? InvoiceRepository
        {
            get
            {
                if(_InvoiceRepository == null)
                {
                    _InvoiceRepository = new InvoiceRepository(this);
                }
                return _InvoiceRepository;
            }
        }
        public IGenericRepository<InvoiceDetail>? InvoicesDetailRepository
        {
            get
            {
                if(_InvoiceDetailRepository  == null)
                {
                    _InvoiceDetailRepository = new InvoiceDetailRepository(this);
                }
                return _InvoiceDetailRepository;
            }
        }
        public IGenericRepository<Article>? ArticleRepository
        {
            get
            {
                if(_ArticleRepository == null)
                {
                    _ArticleRepository = new ArticleRepository(this);
                }
                return _ArticleRepository;
            }
        }

        public IGenericRepository<PaymentMethod>? PaymentMethodRepository
        {
            get
            {
                if(_PaymentMethodRepository == null)
                {
                    _PaymentMethodRepository = new PaymentMethodRepository(this);
                }
                return _PaymentMethodRepository;
            }
        }

        //TRANSACCION CON OUTPUT
        public int SaveChangesWhitOutput(string sp, string paramOutput, List<ParameterSP>? parameters = null)
        {
            int result = -1;
            try
            {
                using var cmd = new SqlCommand(sp, _connection, _transaction) 
                { CommandType = CommandType.StoredProcedure };
                AddParameters(cmd, parameters);
                var uotp = new SqlParameter()
                {
                    ParameterName = paramOutput,
                    Direction = ParameterDirection.Output,
                    DbType = DbType.Int32
                };
                cmd.Parameters.Add(uotp);
                cmd.ExecuteNonQuery();
                var idVariable = (int)uotp.Value;
                if(uotp.Value != DBNull.Value)
                {
                    return idVariable;
                }
                else { return -1; }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TRANSACCION SIN DEVOLUCION
        public int SaveChanges(string sp, List<ParameterSP>? parameters = null)
        {
            int result = -1;
            try
            {
                using var cmd = new SqlCommand(sp, _connection, _transaction)
                { CommandType = CommandType.StoredProcedure };
                AddParameters(cmd, parameters);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        //DML
        //LECTURA DE DATOS EN TABLA
        public DataTable ExecuteSPQuery(string sp, List<ParameterSP>? parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using var cmd = new SqlCommand(sp, _connection, _transaction)
                { CommandType = CommandType.StoredProcedure };
                AddParameters(cmd, parameters);
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
                Console.WriteLine("Error SQL: " + ex.Message);
                throw;
            }
            return dt;
        }
        //DEVOLUCION DE RETURN - UTIL PARA SP DE SAVE O INSERT - UPSERT
        public int ExecuteSPWithReturn(string sp, List<ParameterSP>? parameters = null)
        {
            int result = -1;
            try
            {
                using var cmd = new SqlCommand(sp, _connection, _transaction)
                { CommandType = CommandType.StoredProcedure };
                AddParameters(cmd, parameters);
                //capturar valor de retorno
                var returnParameter = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue,
                    SqlDbType = SqlDbType.Int
                };
                cmd.Parameters.Add(returnParameter);
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
            }
            catch (SqlException ex)
            {
                result = -1;
                Console.WriteLine("Error SQL: " + ex.Message);
            }
            return result;
        }
        //EJECUTAR DEVOLVIENDO FILAS AFECTAS - VALOR DE RETONRO INT DE CONFIRMACION
        public int ExecuteSPNonQuery(string sp, List<ParameterSP>? parameters = null)
        {
            int resultado = 0;
            try
            {
                using var cmd = new SqlCommand(sp, _connection, _transaction)
                { CommandType = CommandType.StoredProcedure };
                AddParameters(cmd, parameters);
                resultado = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                resultado = 0;
            }
            return resultado;
        }
        //METODO PARA AGREGAR LIST DE PARAMETROS
        private static void AddParameters(SqlCommand cmd, List<ParameterSP>? parameters = null)
        {
            if (parameters == null)
            {
                return;
            }
            else
            {
                foreach(ParameterSP param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.nombre, param.valor);
                }
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
