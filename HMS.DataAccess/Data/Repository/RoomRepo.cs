using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class RoomRepo : Repository<Room>, IRoomRepo
    {
        public readonly ApplicationDbContext _db;

        public RoomRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForRoom()
        {
            return _db.Rooms.Select(i => new SelectListItem()
            {
                Text = i.RNo.ToString(),
                Value = i.Id.ToString()
            });
        }

        public void Update(Room room)
        {
            var rFromDb = _db.Rooms.FirstOrDefault(i => i.Id == room.Id);

            rFromDb.FNo = room.FNo;
            rFromDb.RNo = room.RNo;
            rFromDb.RType = room.RType;
            rFromDb.Availability = room.Availability;

            _db.SaveChanges();
        }
    }
}
