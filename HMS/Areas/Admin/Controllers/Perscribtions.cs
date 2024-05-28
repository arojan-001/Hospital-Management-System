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
    public class Perscribtions : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Perscribtions(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPerscribtion(int? id)
        {
            AVM = new AdminViewModels()
            {
                Perscribtion = new Models.Perscribtion(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                PatientsList = _unitofWork.Patients.GetDropDownListForPatients(),
                MedicationsList = _unitofWork.Medication.GetDropDownListForMedications()
            };

            if(id != null)
            {
                AVM.Perscribtion = _unitofWork.Perscribtion.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPerscribtion()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Perscribtion.Id == 0)
                {
                    _unitofWork.Perscribtion.Add(AVM.Perscribtion);
                }
                else
                {
                    _unitofWork.Perscribtion.Update(AVM.Perscribtion);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors();
                AVM.PatientsList = _unitofWork.Patients.GetDropDownListForPatients();
                AVM.MedicationsList = _unitofWork.Medication.GetDropDownListForMedications();

                return View(AVM);
            }
        }

        public IActionResult Detail(int id)
        {
            var pFromDb = _unitofWork.Perscribtion.GetFirstOrDefault(includeProperties: "Doctors,Patients,Medication", filter: i => i.Id == id);

            return View(pFromDb);
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Perscribtion.GetAll(includeProperties: "Doctors,Patients,Medication") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pFromDb = _unitofWork.Perscribtion.Get(id);

            if(pFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Perscription!" });
            }

            _unitofWork.Perscribtion.Remove(pFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Perscription Deleted Successfully!" });
        }

        #endregion
    }
}
