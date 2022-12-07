using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.API.Services.Interfaces
{
    public interface IService<TItem>
    {
        public Task<IEnumerable<TItem>> Gets(string include = null);
        public Task<TItem> Get(Guid ID, string include = null);
        public Task<TItem> Add(TItem TItem);
        public Task<TItem> Put(TItem TItem);
        public Task<bool> Delete(Guid ID);

    }
}
