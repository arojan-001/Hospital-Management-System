using HMS.DataAccess.Data.IRepository;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMS.DataAccess.Data.Repository
{
    public class DepartmentRepo : Repository<Department>, IDepartmentRepo
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForDepartments()
        {
            return _db.Departments.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Department department)
        {
            var dFromDb = _db.Departments.FirstOrDefault(i => i.Id == department.Id);

            dFromDb.Name = department.Name;
            dFromDb.DoctorId = department.DoctorId;

            _db.SaveChanges();
        }
    }
}
