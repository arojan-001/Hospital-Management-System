﻿using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface INursesRepo : IRepository<Nurses>
    {
        IEnumerable<SelectListItem> GetDropDownListForNurses();

        void Update(Nurses nurses);
    }
}
