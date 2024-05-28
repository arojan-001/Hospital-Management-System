using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class MedicationRepo : Repository<Medication>, IMedicationRepo
    {
        private readonly ApplicationDbContext _db;

        public MedicationRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForMedications()
        {
            return _db.Medications.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Medication medication)
        {
            var mFromDb = _db.Medications.FirstOrDefault(i => i.Id == medication.Id);

            mFromDb.Name = medication.Name;
            mFromDb.Description = medication.Description;
            mFromDb.Brand = medication.Brand;

            _db.SaveChanges();
        }
    }
}
