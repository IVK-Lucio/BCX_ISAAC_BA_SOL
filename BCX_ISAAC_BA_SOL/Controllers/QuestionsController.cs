using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCX_ISAAC_BA_SOL.Models;

namespace BCX_ISAAC_BA_SOL.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Job);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create(string Id)
        {
            //ViewBag.JobId = new SelectList(db.Jobs, "Id", "Position");
            ViewBag.JobId = Id;
            string position = db.Jobs.Where(j => j.Id == Id).Select(j => j.Position).FirstOrDefault();
            ViewBag.Position = position;
            return View();
        }

        public ActionResult AnswerQuestions(string JobId,string JobApplicationId)
        {
            ViewBag.JobId = JobId;
            ViewBag.JobApplicationId = JobApplicationId;
            ViewBag.ApplicantName = (from s in db.JobApplications where s.Id == JobApplicationId select new { Name = s.LastName.ToUpper() + ", " + s.FirstName + " " + s.OtherNames }).Select(j => j.Name).FirstOrDefault();
            ViewBag.Position = db.Jobs.Find(JobId).Position;
            ViewBag.QuestionTime = db.Jobs.Find(JobId).QuestionTime;
            var questions = db.Questions.Where(q => q.JobId == JobId).OrderBy(q=>q.Rank).ToList();

            return View(questions);
        }
        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobId,QText,Rank")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid().ToString();
                int count = db.Questions.Where(q => q.JobId == question.JobId).Count();
                question.Rank = getNextRank(question.JobId);
                db.Questions.Add(question);
                db.SaveChanges();
                string add = "/Jobs/Details/" + question.JobId;
                return Redirect(add);
            }

            ViewBag.JobId = question.JobId;
            return View(question);
        }

        public int getNextRank(string jobId)
        {
            //get count;
            int count = db.Questions.Where(q => q.JobId == jobId).Count();
            //get maximum value;
            int maxvalue = (count==0) ? 0: db.Questions.Where(q => q.JobId == jobId).Max(q => q.Rank).Value;
            
           
            int retValue = 0;
            if (maxvalue > count)
            {
                for(int xt=1; xt<maxvalue; xt++)
                {
                   
                        int? countvalue = db.Questions.Where(q => q.Rank == xt && q.JobId==jobId).Select(q => q.Rank).FirstOrDefault();
                        if (countvalue == null)
                        {
                            retValue = xt;
                        }
                   
                }
            }
            else
            {
                retValue = count + 1;
            }
            return retValue;
        }
        // GET: Questions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobId =  question.JobId;
            
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobId,QText,Rank")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                string add = "/Jobs/Details/" + question.JobId;
                return Redirect(add);
            }
            ViewBag.JobId = question.JobId;
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Question question = db.Questions.Find(id);
            string ji = question.JobId;
            db.Questions.Remove(question);
            db.SaveChanges();
            string add = "/Jobs/Details/" + ji;
            return Redirect(add);
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
