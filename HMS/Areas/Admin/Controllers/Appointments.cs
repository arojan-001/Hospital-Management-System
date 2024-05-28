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
    public class Appointments : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Appointments(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAppointment(int? id)
        {
            AVM = new AdminViewModels()
            {
                Appointments = new Models.Appointments(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                PatientsList = _unitofWork.Patients.GetDropDownListForPatients()
            };

            if(id != null)
            {
                AVM.Appointments = _unitofWork.Appointments.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Appointments.Id == 0)
                {
                    _unitofWork.Appointments.Add(AVM.Appointments);
                }
                else
                {
                    _unitofWork.Appointments.Update(AVM.Appointments);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors();
                AVM.PatientsList = _unitofWork.Patients.GetDropDownListForPatients();

                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Appointments.GetAll(includeProperties: "Doctors,Patients") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var aFromDb = _unitofWork.Appointments.Get(id);

            if(aFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Appointment!" });
            }

            _unitofWork.Appointments.Remove(aFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Appointment Deleted Successfully!" });
        }

        #endregion
    }
}
