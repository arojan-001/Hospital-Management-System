using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IPatientsRepo : IRepository<Patients>
    {
        IEnumerable<SelectListItem> GetDropDownListForPatients();

        void Update(Patients patients);
    }
}
