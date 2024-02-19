using CleanCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Core.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
