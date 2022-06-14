using DapperDenemeleri.Application.Interfaces;
using System;
using System.Data;

namespace DapperDenemeleri.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        public IProductRepository Products { get; }
        public IDbTransaction DbTransaction { get; set; }

        public UnitOfWork(IProductRepository productRepository,IDbTransaction dbTransaction)
        {
            Products = productRepository;
            DbTransaction = dbTransaction;
        }
        
        public void Commit()
        {
            try
            {
                DbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                DbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                DbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            DbTransaction.Connection?.Close();
            DbTransaction.Connection?.Dispose();
            DbTransaction.Dispose();
        }
    }
}
