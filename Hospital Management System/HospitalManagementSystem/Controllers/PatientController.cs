
using HospitalManagementSystem.Models;
using PatientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        PatientDBHandle sdb = new PatientDBHandle();
        DoctorList doctors = new DoctorList();
        // GET: Patient
        public ActionResult Index()
       {
            return View();
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            ViewBag.doctors = doctors.Doctors;
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sdb.AddPatient(patient))
                    {
                        ModelState.Clear();
                    }
                    TempData["msg"] = "<script>alert('Patient Created Sucessfully')</script>";
                    return RedirectToAction("Index");
                }
                return View(patient);
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message+" "+ex.StackTrace);
                ViewBag.doctors = doctors.Doctors;
                return View(patient);
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.doctors = doctors.Doctors;
                return View(sdb.GetPatient(id));
            }
            catch(Exception ex)
            {
                TempData["error"] = "<script>alert("+ex.Message + " " + ex.StackTrace+ ")</script>";
                return RedirectToAction("index");
            }
            
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Patient patient)
        {
            try
            {
                sdb.UpdateDetails(patient);
                TempData["msg"] = "<script>alert('Patient Edited Sucessfully')</script>";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message + " " + ex.StackTrace);
                return View(patient);
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                return View(sdb.GetPatient(id));
            }
            catch(Exception ex)
            {

                TempData["error"] = "<script>alert("+ ex.Message + " " + ex.StackTrace+ ")</script>";
                return RedirectToAction("index");
            }
            
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Patient patient)
        {
            try
            {
                if (sdb.DeletePatient(id))
                {
                    TempData["msg"] = "<script>alert('Student Deleted Successfully')</script>";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("","Unadle to Delete");
                return View(patient);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message+" "+ex.StackTrace);
                return View();
            }
        }

        // Function For Loading data to Jquery Datable
        //GET :Patient/PatientData
        [HttpPost]
        public ActionResult PatientData()
        {
            try
            {
                
                var start = Convert.ToInt32(Request.Form["start"]);
                var length = Convert.ToInt32(Request.Form["length"]);
                var searchValue = Convert.ToString(Request.Form["search[value]"]);
                List<Patient> patientList = sdb.GetPatients(start,length,searchValue);
                return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return Json(null);
            }

        }
    }
}
