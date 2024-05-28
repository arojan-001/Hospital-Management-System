using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class OnCallRepo : Repository<OnCall>, IOnCallRepo
    {
        private readonly ApplicationDbContext _db;

        public OnCallRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OnCall onCall)
        {
            var ocFromDb = _db.OnCalls.FirstOrDefault(i => i.Id == onCall.Id);

            ocFromDb.NurseId = onCall.NurseId;
            ocFromDb.RoomId = onCall.RoomId;
            ocFromDb.STime = onCall.STime;
            ocFromDb.ETime = onCall.ETime;

            _db.SaveChanges();
        }
    }
}
