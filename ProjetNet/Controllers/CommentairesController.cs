using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetNet.Models;

namespace ProjetNet.Controllers
{
    public class CommentairesController : Controller
    {
        private CommentaireDBContext db = new CommentaireDBContext();

        // GET: Commentaires
        public ActionResult Index()
        {
            return View(db.Commentaires.ToList());
        }

        // GET: Commentaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        // GET: Commentaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commentaires/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Texte,Date,Posteur,id_message")] Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                db.Commentaires.Add(commentaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentaire);
        }

        // GET: Commentaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        // POST: Commentaires/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Texte,Date,Posteur,id_message")] Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentaire);
        }

        // GET: Commentaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        // POST: Commentaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commentaire commentaire = db.Commentaires.Find(id);
            db.Commentaires.Remove(commentaire);
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
