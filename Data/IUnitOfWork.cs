using Actividad_Facultad.Data.Implement;
using Actividad_Facultad.Data.Interfaces;
using Actividad_Facultad.Data.Repositories;
using Actividad_Facultad.Data.Utils;
using Actividad_Facultad.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Actividad_Facultad.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        int SaveChangesWhitOutput(string sp, string paramOutput, List<ParameterSP>? parameters = null);
        int SaveChanges(string sp, List<ParameterSP>? parameters = null);
        DataTable ExecuteSPQuery(string sp, List<ParameterSP>? parameters = null);
        int ExecuteSPWithReturn(string sp, List<ParameterSP>? parameters = null);
        int ExecuteSPNonQuery(string sp, List<ParameterSP>? parameters = null);

        //para mayor simplicidad, decidi agregar los repository como propiedades de la interfaz
        //para luego dejar la capa services mas limpia, pero como desventaja,
        //compromenter mas a la interfaz
        IGenericRepository<Invoice>? InvoiceRepository { get;}
        IGenericRepository<InvoiceDetail>? InvoicesDetailRepository { get; }
        IGenericRepository<Article>? ArticleRepository { get; }
        IGenericRepository<PaymentMethod>? PaymentMethodRepository { get; }
    }
}
