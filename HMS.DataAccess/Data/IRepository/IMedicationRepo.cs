using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.DataAccess.Data.IRepository
{
    public interface IMedicationRepo : IRepository<Medication>
    {
        IEnumerable<SelectListItem> GetDropDownListForMedications();

        void Update(Medication medication);
    }
}
