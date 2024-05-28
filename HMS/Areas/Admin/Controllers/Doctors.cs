using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HMS.DataAccess.Data.IRepository;
using HMS.Models.ViewModels;
using HMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class Doctors : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Doctors(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddDoctor(int? id)
        {
            AVM = new AdminViewModels() { Doctors = new Models.Doctors() };

            if(id != null)
            {
                AVM.Doctors = _unitofWork.Doctors.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDoctor()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(AVM.Doctors.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Doctors");
                    var extension = Path.GetExtension(files[0].FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    AVM.Doctors.ImageUrl = @"\Images\Doctors\" + fileName + extension;

                    _unitofWork.Doctors.Add(AVM.Doctors);
                }
                else
                {
                    var dFromDb = _unitofWork.Doctors.Get(AVM.Doctors.Id);

                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Images\Doctors");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRootPath, dFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        AVM.Doctors.ImageUrl = @"\Images\Doctors\" + fileName + extension_new;
                    }
                    else
                    {
                        AVM.Doctors.ImageUrl = dFromDb.ImageUrl;
                    }

                    _unitofWork.Doctors.Update(AVM.Doctors);
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
            var dFromDb = _unitofWork.Doctors.Get(id);

            return View(dFromDb);
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Doctors.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var dFromDb = _unitofWork.Doctors.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, dFromDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if(dFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Doctor Record!" });
            }

            _unitofWork.Doctors.Remove(dFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Doctor Record Deleted Successfully!" });
        }

        #endregion
    }
}
