using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IUserServices : IBaseServices<UserBase>
    {
        Task<UserResponse> Login(LoginUserRequest request);
        Task<UserResponse> Register(RegisterUserRequest request);
        Task<IEnumerable<Employee>> GetEmployee(int limit, int offset);
        Task<IEnumerable<HRUser>> GetHR(int limit, int offset);

    }
}
