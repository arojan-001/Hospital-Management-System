using HMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IUserRepo : IRepository<ApplicationUser>
    {
        void Lock(string userId);

        void UnLock(string userId);
    }
}
