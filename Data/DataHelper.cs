using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.ConnectionString);
        }

        public static DataHelper GetInstance()
        {
            if(_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        //LECTURA DE DATOS EN TABLA
        public DataTable ExcecuteSPQuery(string sp, List<ParameterSP>? parametros = null)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if(parametros != null)
                {
                    foreach(ParameterSP p in parametros)
                    {
                        cmd.Parameters.AddWithValue(p.nombre, p.valor);
                    }
                }
                dt.Load(cmd.ExecuteReader());
                
            }
            catch (SqlException e)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        //OBTENCION DE FILAS AFECTADAS
        

        //OBTENCION DE RETURN EN SP
        public int ExcecuteSPCatchInt(string sp, List<ParameterSP>? parameters=null) 
        {
            int result = -1;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if(parameters != null)
                {
                    foreach(ParameterSP p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.nombre, p.valor);
                    }
                }

                var returnParameter = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue,
                    SqlDbType = SqlDbType.Int
                };
                cmd.Parameters.Add(returnParameter);
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
            }
            catch(SqlException)
            {
                result = -1;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        //OBTENCION DE FILAS AFECTADAS
        public int ExecuteSPNoQuery(string sp, List<ParameterSP> parameter)
        {
            int resultado = 0;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if(parameter != null)
                {
                    foreach(ParameterSP param in parameter)
                    {
                        cmd.Parameters.AddWithValue(param.nombre, param.valor);
                    }
                }
                resultado = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                resultado = 0;
            }
            finally
            {
                _connection.Close();
            }
            return resultado;
        }
    }
}
