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
    public class Patients : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Patients(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPatient(int? id)
        {
            AVM = new AdminViewModels() { Patients = new Models.Patients() };

            if(id != null)
            {
                AVM.Patients = _unitofWork.Patients.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(AVM.Patients.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Patients");
                    var extension = Path.GetExtension(files[0].FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    AVM.Patients.ImageUrl = @"\Images\Patients\" + fileName + extension;

                    _unitofWork.Patients.Add(AVM.Patients);
                }
                else
                {
                    var pFromdb = _unitofWork.Patients.Get(AVM.Patients.Id);

                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Images\Patients");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRootPath, pFromdb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        AVM.Patients.ImageUrl = @"\Images\Patients\" + fileName + extension_new;
                    }
                    else
                    {
                        AVM.Patients.ImageUrl = pFromdb.ImageUrl;
                    }

                    _unitofWork.Patients.Update(AVM.Patients);
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
            var pFromDb = _unitofWork.Patients.Get(id);

            return View(pFromDb);
        }


        #region API CALLS

        public IActionResult GetAllP()
        {
            return Json(new { data = _unitofWork.Patients.GetAll() });
        }

        [HttpDelete]
        public IActionResult DeleteP(int id)
        {
            var pFromDb = _unitofWork.Patients.Get(id);

            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, pFromDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if(pFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Patient Record!" });
            }

            _unitofWork.Patients.Remove(pFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Patient Record Deleted Suucessfully!" });
        }


        #endregion
    }
}
