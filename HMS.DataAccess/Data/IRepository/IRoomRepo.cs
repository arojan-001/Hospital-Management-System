using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IRoomRepo : IRepository<Room>
    {
        IEnumerable<SelectListItem> GetDropDownListForRoom();

        void Update(Room room);
    }
}
