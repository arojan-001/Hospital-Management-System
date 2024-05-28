using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class AppointmentsRepo : Repository<Appointments>, IAppointmentsRepo
    {
        private readonly ApplicationDbContext _db;

        public AppointmentsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForAppointments()
        {
            return _db.Appointments.Select(i => new SelectListItem()
            {
                Text = i.Id.ToString(),
                Value = i.Id.ToString()
            });

        }

        public void Update(Appointments appointments)
        {
            var aFromDb = _db.Appointments.FirstOrDefault(i => i.Id == appointments.Id);

            aFromDb.PatientId = appointments.PatientId;
            aFromDb.DoctorId = appointments.DoctorId;
            aFromDb.DTime = appointments.DTime;

            _db.SaveChanges();
        }
    }
}
