using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IUsageRepo: IRepository<Usage>
    {
        IEnumerable<SelectListItem> GetDropDownListForUsage();

        void Update(Usage usage);
    }
}
