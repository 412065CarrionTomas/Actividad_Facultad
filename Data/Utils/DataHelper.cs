using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_Facultad.Data.Utils
{

    /// <summary>
    /// SIN USO
    /// POSIBLE IMPLEMENTACION PARA VALIDACIONES, LECTURAS, PRUEBAS, ETC.
    /// Decidi no darle uso. UnitOfWork es el unico que maneja conexiones y transacciones. Si dividia conexiones,
    /// la legibilidad y responsabilidad estarian mezcladas. 
    /// En lo que base mi codigo. UnitOfWork crea conexion y transaccion. Utiliza la misma hasta un Commit() o Rollback().
    /// No manejo de Singleton por que rompe la logica del UnitOfWork. Ademas dicidi implementar una interfas(IUnitOfWork)
    /// para mantener la abstraccion entre los repo de acceso a datos.
    /// </summary>
    public class DataHelper
    {
        //private static DataHelper _instance;
        //private static SqlConnection _connection;

        //private DataHelper()
        //{
        //    _connection = new SqlConnection(Properties.Resources.ConnectionString);
        //}

        //public static DataHelper GetInstance()
        //{
        //    if(_instance == null)
        //    {
        //        _instance = new DataHelper(); 
        //    }
        //    return _instance;
        //}
        
        //public static SqlConnection GetConnection()
        //{
        //    if(_connection == null)
        //    {
        //        GetInstance();
        //    }
        //    return _connection;
        //}
    }
}
