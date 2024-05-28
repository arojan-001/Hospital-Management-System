using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class UndergoesRepo : Repository<Undergoes>, IUndergoesRepo
    {
        private readonly ApplicationDbContext _db;

        public UndergoesRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Undergoes undergoes)
        {
            var uFromDb = _db.Undergoes.FirstOrDefault(i => i.Id == undergoes.Id);

            uFromDb.PatientId = undergoes.PatientId;
            uFromDb.DoctorId = undergoes.DoctorId;
            uFromDb.NurseId = undergoes.NurseId;
            uFromDb.UsageId = undergoes.UsageId;
            uFromDb.ProcedureId = undergoes.ProcedureId;
            uFromDb.Date = undergoes.Date;

            _db.SaveChanges();
        }
    }
}
