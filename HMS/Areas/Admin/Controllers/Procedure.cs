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
    public class Procedure : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Procedure(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProcedure(int? id)
        {
            AVM = new AdminViewModels() { Procedure = new Models.Procedure() };

            if (id != null)
            {
                AVM.Procedure = _unitofWork.Procedure.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProcedure()
        {
            if (ModelState.IsValid)
            {
                if (AVM.Procedure.Id == 0)
                {
                    _unitofWork.Procedure.Add(AVM.Procedure);
                }
                else
                {
                    _unitofWork.Procedure.Update(AVM.Procedure);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Procedure.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pFromDb = _unitofWork.Procedure.Get(id);

            if (pFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Procedure Record!" });
            }

            _unitofWork.Procedure.Remove(pFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Procedure Record Deleted Successfully!" });
        }

        #endregion
    }
}
