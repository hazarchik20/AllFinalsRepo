using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.Base
{
    public interface IBaseServices<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int limit, int offset);
        Task<T?> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
