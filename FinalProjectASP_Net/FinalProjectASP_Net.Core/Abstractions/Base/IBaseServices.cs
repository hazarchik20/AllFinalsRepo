using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.Base
{
    public interface IBaseServices<MainModel,ResponseModel, ShortRequsetModel> where MainModel : class where ResponseModel : class where ShortRequsetModel : class
    {
        Task<IEnumerable<ResponseModel>> GetAll(int limit, int offset);
        Task<ResponseModel?> GetById(int id);
        Task Add(ShortRequsetModel entity);
        Task Update(int id, ShortRequsetModel entity);
        Task Delete(int id);

    }
}
