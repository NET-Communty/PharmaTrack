using Service.Abstractions.Dtos.AccountDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.IServices
{
    public interface IAccountServices
    {
        Task<AuthModel> Login(LoginDto loginDto);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
