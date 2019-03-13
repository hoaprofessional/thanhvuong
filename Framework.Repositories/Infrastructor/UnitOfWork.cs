using Framework.Context;
using Framework.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.Infrastructor
{
    public interface IUnitOfWork
    {
        void Commit();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
    public class UnitOfWork : IUnitOfWork
    {
        FrameworkDbContext dataContext;
        IDbContextTransaction dbTransaction;
        public UnitOfWork(FrameworkDbContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public void BeginTransaction()
        {
            dbTransaction = dataContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Commit();
                dbTransaction = null;
            }
        }

        public void Commit()
        {
            this.dataContext.SaveChanges();
        }

        public void RollbackTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
            }
        }
    }
}
