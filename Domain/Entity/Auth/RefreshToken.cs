using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Auth
{
    public class RefreshToken
    {
            public Guid Id { get; set; }

            public string Token { get; set; }

            public string UserId { get; set; }

            public DateTime Expiration { get; set; }


    }
}
