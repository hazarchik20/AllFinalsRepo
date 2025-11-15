using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServices<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T value);
        void Delete(T value);
        void Update(T value);
        

    }
}
