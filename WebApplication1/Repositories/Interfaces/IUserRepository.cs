using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    interface IUserRepository
    {
        IEnumerable<USER> Users { get; }
        void AddUserToDb(USER user);
        bool ValidateData(USER user);
        int LoginUsingPassword(string username , string password );
    }
}
