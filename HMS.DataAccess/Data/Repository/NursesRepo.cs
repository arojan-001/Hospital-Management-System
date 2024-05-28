using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class NursesRepo : Repository<Nurses>, INursesRepo
    {
        private readonly ApplicationDbContext _db;

        public NursesRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForNurses()
        {
            return _db.Nurses.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Nurses nurses)
        {
            var nFromDb = _db.Nurses.FirstOrDefault(i => i.Id == nurses.Id);

            nFromDb.Name = nurses.Name;
            nFromDb.Gender = nurses.Gender;
            nFromDb.ImageUrl = nurses.ImageUrl;
            nFromDb.Mobile = nurses.Mobile;
            nFromDb.Position = nurses.Position;
            nFromDb.Registered = nurses.Registered;

            _db.SaveChanges();
        }
    }
}
