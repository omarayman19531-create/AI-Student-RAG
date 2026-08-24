using Domain.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repostry
{
    public interface IUserService
    {
        Task<Appuser?> GetUserByEmail(string email);
    }
}
