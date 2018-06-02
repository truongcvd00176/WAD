using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        ExamContext db = new ExamContext();
        public ActionResult Index()
        {
            return View(db.TableExams.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TableExam exam)
        {
            if (ModelState.IsValid)
            {
                exam.status = "up coming";
                db.TableExams.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exam);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableExam exam = db.TableExams.Find(id);
            return View(exam);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableExam exam = db.TableExams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id, subject, startTime, examDate, duration, classRoom, faculty, status")] TableExam exam)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    db.Entry(exam).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(exam);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableExam exam = db.TableExams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                TableExam exam = db.TableExams.Find(id);
                db.TableExams.Remove(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}