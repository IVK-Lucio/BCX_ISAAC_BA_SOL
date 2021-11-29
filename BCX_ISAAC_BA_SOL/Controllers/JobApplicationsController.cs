using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCX_ISAAC_BA_SOL.Models;

namespace BCX_ISAAC_BA_SOL.Controllers
{
    public class JobApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobApplications
        public ActionResult Index()
        {
            var jobApplications = db.JobApplications.Include(j => j.Job);
            return View(jobApplications.ToList());
        }
        [HttpPost]
        public ActionResult BeginApplication()
        {
            string jobId = Request.Form["JobId"];
            string emailaddress = Request.Form["emailaddress"];
            string addurl = "";
            if (jobId != null && emailaddress != null && jobId != "" && emailaddress != "")
            {
                JobApplication jap = db.JobApplications.Where(j => j.JobId == jobId && j.EmailAddress==emailaddress).FirstOrDefault();
                int count = db.JobApplications.Where(j => j.JobId == jobId && j.EmailAddress == emailaddress).Count();
                if (count >0)
                {
                    addurl = "/JobApplications/UploadResume/" + jap.Id;
                }
                else {
                JobApplication ja = new JobApplication();
                ja.EmailAddress = emailaddress;
                ja.Id = Guid.NewGuid().ToString();
                ja.JobId = jobId;
                db.JobApplications.Add(ja);
                db.SaveChanges();
              
                addurl = "/JobApplications/UploadResume/" + ja.Id;
                }
            }

            return Redirect(addurl);
        }
        
        // GET: JobApplications/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = db.JobApplications.Find(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            return View(jobApplication);
        }
        public ActionResult UploadResume(string id)
        {
            JobApplication ja = db.JobApplications.Find(id);
            ViewBag.JobApplicationId = id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadResume()
        {
            string addurl = "";
            string jobApplicationId = Request.Form["JobApplicationId"];
            //string filename = Request.Form["Resume"];
            //foreach (string upload in Request.Files)
            //{
                
                if (!HasFile(Request.Files["resume"])) { 
                var allowedFileExtensions = new[] { ".pdf" };
                string ext = Path.GetExtension(Request.Files["resume"].FileName).ToLower();
                if (allowedFileExtensions.Contains(ext))
                {
                    string fileName = jobApplicationId + ext;
                    string filepath = AppDomain.CurrentDomain.BaseDirectory + "Uploads";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Uploads", fileName);

                    JobApplication japp = db.JobApplications.Find(jobApplicationId);
                    japp.ResumeUrl = fileName;
                    
                    db.Entry(japp).State = EntityState.Modified;
                    db.SaveChanges();

                    Request.Files["resume"].SaveAs(path);
                    //addurl = "/JobApplications/UploadResume/" + jobApplicationId;
                    
                }
                else if (!allowedFileExtensions.Contains(ext))
                {
                    ViewBag.Msg = "Unsupported file format. Please upload only pdf files.";
                   
                }
            }
            //}
            addurl = "/JobApplications/UploadResume/" + jobApplicationId;
            return Redirect(addurl);
        }

        public static bool HasFile(HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
        // GET: JobApplications/Create
        public ActionResult Create(string Id)
        {

            ViewBag.JobId = Id;
            return View();
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobId,FirstName,LastName,OtherNames,Sex,MaritalStatus,DateOfBirth,ResumeUrl,EmailAddress,Status")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                db.JobApplications.Add(jobApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Position", jobApplication.JobId);
            return View(jobApplication);
        }

        // GET: JobApplications/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = db.JobApplications.Find(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Position", jobApplication.JobId);
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobId,FirstName,LastName,OtherNames,Sex,MaritalStatus,DateOfBirth,ResumeUrl,EmailAddress,Status")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Position", jobApplication.JobId);
            return View(jobApplication);
        }

        // GET: JobApplications/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = db.JobApplications.Find(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            JobApplication jobApplication = db.JobApplications.Find(id);
            db.JobApplications.Remove(jobApplication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
