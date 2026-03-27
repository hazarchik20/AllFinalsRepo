using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using FinalProjectASP_Net.Core.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Core.Abstractions.IServ
{
    public interface IUserServices : IBaseServices<UserBase, UserResponse, UserBase>
    {
        Task<UserResponse> Login(LoginUserRequest request);
        Task<UserResponse> Register(RegisterUserRequest request);
        Task<IEnumerable<UserResponse>> GetEmployee(int limit, int offset);
        Task<IEnumerable<UserResponse>> GetHR(int limit, int offset);


    }
}
