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
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answers
        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.JobApplication).Include(a => a.Question);
            return View(answers.ToList());
        }

        // GET: Answers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.JobApplicationId = new SelectList(db.JobApplications, "Id", "JobId");
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "JobId");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuestionId,AText,JobApplicationId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobApplicationId = new SelectList(db.JobApplications, "Id", "JobId", answer.JobApplicationId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "JobId", answer.QuestionId);
            return View(answer);
        }
        public ActionResult SubmitAnswers()
        {
            string jobapplicationId = Request.Form["JobapplicationId"];
            string[] questionId = Request.Form.GetValues("QuestionId");
            string[] answer = Request.Form.GetValues("Answer");
            var count = (questionId!=null)?questionId.Count():0;
            if (count != 0) {
                for (int n=0; n < count; n++)
                {
                    Answer an = new Answer();
                    an.Id = Guid.NewGuid().ToString();
                    an.QuestionId = questionId[n];
                    an.AText = answer[n];
                    an.JobApplicationId = jobapplicationId;
                    db.Answers.Add(an);
                    db.SaveChanges();
                }
            }
            return View();
        }
        // GET: Answers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobApplicationId = new SelectList(db.JobApplications, "Id", "JobId", answer.JobApplicationId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "JobId", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionId,AText,JobApplicationId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobApplicationId = new SelectList(db.JobApplications, "Id", "JobId", answer.JobApplicationId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "JobId", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
