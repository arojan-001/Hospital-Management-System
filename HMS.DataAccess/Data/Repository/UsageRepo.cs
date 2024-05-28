using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class UsageRepo : Repository<Usage>, IUsageRepo
    {
        private readonly ApplicationDbContext _db;

        public UsageRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForUsage()
        {
            return _db.Usage.Select(i => new SelectListItem()
            {
                Text = i.Room.RNo.ToString(),
                Value = i.Id.ToString()
            });
        }

        public void Update(Usage usage)
        {
            var uFromDb = _db.Usage.FirstOrDefault(i => i.Id == usage.Id);

            uFromDb.PatientId = usage.PatientId;
            uFromDb.RoomId = usage.RoomId;
            uFromDb.SDate = usage.SDate;
            uFromDb.EDate = usage.EDate;

            _db.SaveChanges();
        }
    }
}
