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
    public class Undergoes : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Undergoes(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUndergoes(int? id)
        {
            AVM = new AdminViewModels()
            {
                Undergoes = new Models.Undergoes(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                PatientsList = _unitofWork.Patients.GetDropDownListForPatients(),
                NursesList = _unitofWork.Patients.GetDropDownListForPatients(),
                UsageList = _unitofWork.Usage.GetDropDownListForUsage(),
                ProceduresList = _unitofWork.Procedure.GetDropDownListForProcedure()
            };

            if(id != null)
            {
                AVM.Undergoes = _unitofWork.Undergoes.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUndergoes()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Undergoes.Id == 0)
                {
                    _unitofWork.Undergoes.Add(AVM.Undergoes);
                }
                else
                {
                    _unitofWork.Undergoes.Update(AVM.Undergoes);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors();
                AVM.PatientsList = _unitofWork.Patients.GetDropDownListForPatients();
                AVM.NursesList = _unitofWork.Patients.GetDropDownListForPatients();
                AVM.UsageList = _unitofWork.Usage.GetDropDownListForUsage();
                AVM.ProceduresList = _unitofWork.Procedure.GetDropDownListForProcedure();

                return View(AVM);
            }
        }

        public IActionResult Detail(int id)
        {
            var uFromDb = _unitofWork.Undergoes.GetFirstOrDefault(
                includeProperties: "Doctors,Patients,Nurses,Usage,Procedure",
                filter: i => i.Id == id
                );

            return View(uFromDb);
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Undergoes.GetAll(includeProperties: "Doctors,Procedure,Patients") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var uFromDb = _unitofWork.Undergoes.Get(id);

            if(uFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Undergoes Record!" });
            }

            _unitofWork.Undergoes.Remove(uFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Undergoes Record Deleted Successfully!" });
        }

        #endregion
    }
}
