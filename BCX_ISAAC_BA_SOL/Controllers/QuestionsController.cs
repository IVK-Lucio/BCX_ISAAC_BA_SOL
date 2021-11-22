﻿using System;
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
                question.Rank = count + 1;
                db.Questions.Add(question);
                db.SaveChanges();
                string add = "/Jobs/Details/" + question.JobId;
                return Redirect(add);
            }

            ViewBag.JobId = question.JobId;
            return View(question);
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
