using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using BCX_ISAAC_BA_SOL.Models;

namespace BCX_ISAAC_BA_SOL.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Job_name" ? "Job_desc" : "job_name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            var jobs = from s in db.Jobs
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(s => s.Position.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    jobs = jobs.OrderByDescending(s => s.Position);
                    break;
                case "job_name":
                    jobs = jobs.OrderBy(s => s.Position);
                    break;

                default:  // Name ascending 
                    jobs = jobs.OrderBy(s => s.Position);
                    break;
            }
            ViewBag.JobsCount = jobs.Count();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
            //return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Position,Description,CompanyName,Location,Designation,EngagementType,CompanyWebsite,UserName,ActiveStatus")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.Id = Guid.NewGuid().ToString();
                job.UserName = User.Identity.Name;
                job.ActiveStatus = true;
                job.DatePosted = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Position,Description,CompanyName,Location,Designation,EngagementType,CompanyWebsite,UserName,ActiveStatus,DatePosted")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
