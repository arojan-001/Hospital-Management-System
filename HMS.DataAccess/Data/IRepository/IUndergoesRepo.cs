using HMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IUndergoesRepo : IRepository<Undergoes>
    {
        void Update(Undergoes undergoes);
    }
}
