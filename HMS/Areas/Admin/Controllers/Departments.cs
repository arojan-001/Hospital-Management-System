using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.DataAccess.Data.IRepository;
using HMS.Models.ViewModels;
using HMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class Departments : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Departments(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddDepartment(int? id)
        {
            AVM = new AdminViewModels()
            {
                Department = new Models.Department(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors()
            };

            if(id != null)
            {
                AVM.Department = _unitofWork.Department.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Department.Id == 0)
                {
                    _unitofWork.Department.Add(AVM.Department);
                }
                else
                {
                    _unitofWork.Department.Update(AVM.Department);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors();

                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Department.GetAll(includeProperties: "Doctors") });
        }

        #endregion
    }
}
