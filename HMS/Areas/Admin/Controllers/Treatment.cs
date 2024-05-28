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
    public class Treatment : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Treatment(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddTreatment(int? id)
        {
            AVM = new AdminViewModels()
            {
                Treatment = new Models.Treatment(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                ProceduresList = _unitofWork.Procedure.GetDropDownListForProcedure()
            };

            if(id != null)
            {
                AVM.Treatment = _unitofWork.Treatment.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTreatment()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Treatment.Id == 0)
                {
                    _unitofWork.Treatment.Add(AVM.Treatment);
                }
                else
                {
                    _unitofWork.Treatment.Update(AVM.Treatment);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors();
                AVM.ProceduresList = _unitofWork.Procedure.GetDropDownListForProcedure();

                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Treatment.GetAll(includeProperties: "Doctors,Procedure") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var tFromDb = _unitofWork.Treatment.Get(id);

            if(tFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Treatment Record!" });
            }

            _unitofWork.Treatment.Remove(tFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Treatment Record Deleted Successfully!" });
        }

        #endregion
    }
}
