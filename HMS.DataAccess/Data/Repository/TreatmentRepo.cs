using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class TreatmentRepo : Repository<Treatment>, ITreatmentRepo
    {
        private readonly ApplicationDbContext _db;

        public TreatmentRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Treatment treatment)
        {
            var tFromDb = _db.Treatments.FirstOrDefault(i => i.Id == treatment.Id);

            tFromDb.ProcedureId = treatment.ProcedureId;
            tFromDb.DoctorId = treatment.DoctorId;

            _db.SaveChanges();
        }
    }
}
