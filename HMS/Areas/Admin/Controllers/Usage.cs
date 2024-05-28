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
    public class Usage : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Usage(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRoomUsage(int? id)
        {
            AVM = new AdminViewModels()
            {
                Usage = new Models.Usage(),
                PatientsList = _unitofWork.Patients.GetDropDownListForPatients(),
                RoomsList = _unitofWork.Room.GetDropDownListForRoom()
            };

            if (id != null)
            {
                AVM.Usage = _unitofWork.Usage.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoomUsage()
        {
            if (ModelState.IsValid)
            {
                if (AVM.Usage.Id == 0)
                {
                    _unitofWork.Usage.Add(AVM.Usage);
                }
                else
                {
                    _unitofWork.Usage.Update(AVM.Usage);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.PatientsList = _unitofWork.Patients.GetDropDownListForPatients();
                AVM.RoomsList = _unitofWork.Room.GetDropDownListForRoom();

                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Usage.GetAll(includeProperties: "Patients,Room") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var uFromDb = _unitofWork.Usage.Get(id);

            if (uFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Room Usage Record!" });
            }

            _unitofWork.Usage.Remove(uFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Room Usage Record Deleted Suucessfully!" });
        }

        #endregion
    }
}
