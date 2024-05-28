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
    public class Medications : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Medications(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMedication(int? id)
        {
            AVM = new AdminViewModels() { Medication = new Models.Medication() };

            if(id != null)
            {
                AVM.Medication = _unitofWork.Medication.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedication()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Medication.Id == 0)
                {
                    _unitofWork.Medication.Add(AVM.Medication);
                }
                else
                {
                    _unitofWork.Medication.Update(AVM.Medication);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(AVM);
            }
        }

        public IActionResult Detail(int id)
        {
            var mFromDb = _unitofWork.Medication.Get(id);

            return View(mFromDb);
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Medication.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var mFromDb = _unitofWork.Medication.Get(id);

            if(mFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Medication!" });
            }

            _unitofWork.Medication.Remove(mFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Medication Deleted Successfully!" });
        }

        #endregion
    }
}
