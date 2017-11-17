using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatterProject.Models;

namespace ChatterProject.Controllers
{
    public class ChattersController : Controller
    {
        private ChatterDatabaseEntities db = new ChatterDatabaseEntities();

        // GET: Chatters
        public ActionResult Index()
        {
            var chatters = db.Chatters.Include(c => c.AspNetUser);
            return View(chatters.ToList());
        }

        // GET: Chatters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatter chatter = db.Chatters.Find(id);
            if (chatter == null)
            {
                return HttpNotFound();
            }
            return View(chatter);
        }

        // GET: Chatters/Create
        public ActionResult Create()
        {
            ViewBag.UserNameID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Chatters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatterID,ChatMsg,DateStamp,UserNameID")] Chatter chatter)
        {
            if (ModelState.IsValid)
            {
                db.Chatters.Add(chatter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserNameID = new SelectList(db.AspNetUsers, "Id", "Email", chatter.UserNameID);
            return View(chatter);
        }

        // GET: Chatters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatter chatter = db.Chatters.Find(id);
            if (chatter == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserNameID = new SelectList(db.AspNetUsers, "Id", "Email", chatter.UserNameID);
            return View(chatter);
        }

        // POST: Chatters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatterID,ChatMsg,DateStamp,UserNameID")] Chatter chatter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserNameID = new SelectList(db.AspNetUsers, "Id", "Email", chatter.UserNameID);
            return View(chatter);
        }

        // GET: Chatters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatter chatter = db.Chatters.Find(id);
            if (chatter == null)
            {
                return HttpNotFound();
            }
            return View(chatter);
        }

        // POST: Chatters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chatter chatter = db.Chatters.Find(id);
            db.Chatters.Remove(chatter);
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
