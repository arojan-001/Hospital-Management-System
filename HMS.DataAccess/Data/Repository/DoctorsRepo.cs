using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class DoctorsRepo : Repository<Doctors>, IDoctorsRepo
    {
        public readonly ApplicationDbContext _db;

        public DoctorsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForDoctors()
        {
            return _db.Doctors.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Doctors doctors)
        {
            var dFromDb = _db.Doctors.FirstOrDefault(i => i.Id == doctors.Id);

            dFromDb.Name = doctors.Name;
            dFromDb.Gender = doctors.Gender;
            dFromDb.ImageUrl = doctors.ImageUrl;
            dFromDb.Mobile = doctors.Mobile;
            dFromDb.Position = doctors.Position;

            _db.SaveChanges();
        }
    }
}
