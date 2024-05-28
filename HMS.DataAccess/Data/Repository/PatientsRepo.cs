using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class PatientsRepo : Repository<Patients>, IPatientsRepo
    {
        private readonly ApplicationDbContext _db;

        public PatientsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForPatients()
        {
            return _db.Patients.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Patients patients)
        {
            var pFromDb = _db.Patients.FirstOrDefault(i => i.Id == patients.Id);

            pFromDb.Name = patients.Name;
            pFromDb.Gender = patients.Gender;
            pFromDb.ImageUrl = patients.ImageUrl;
            pFromDb.Mobile = patients.Mobile;
            pFromDb.ICompany = patients.ICompany;
            pFromDb.INo = patients.INo;

            _db.SaveChanges();
        }
    }
}
