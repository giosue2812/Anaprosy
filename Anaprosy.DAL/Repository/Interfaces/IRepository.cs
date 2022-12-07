using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anaprosy.DAL.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> Gets(string include = null);
        public Task<TEntity> Get(Guid ID, string include = null);
        public Task<TEntity> Add(TEntity TEntity);
        public Task<TEntity> Put(TEntity TEntity);
        public Task<bool> Delete(Guid TEntity);
    }
}
