using Kalmeo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalmeo.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(KalmeoContext dbFactory)
        {
            DbContext = dbFactory;
        }

        public KalmeoContext DbContext { get; }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
