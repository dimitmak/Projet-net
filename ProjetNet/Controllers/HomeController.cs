using ProjetNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetNet.Controllers
{
    public class HomeController : Controller
    {
        private MessageDBContext dbm = new MessageDBContext();
        private CommentaireDBContext dbc = new CommentaireDBContext();
        private int message;

        public ActionResult Index()
        {
            ContentModelView vm = new ContentModelView();
            var lc = dbc.Commentaires.ToList();
            lc.Reverse();
            vm.Commentaires = lc;
            var lm = dbm.Messages.ToList();
            lm.Reverse();
            vm.Messages = lm;
            if (this.User.IsInRole("Admin"))
            {
                ViewBag.roles = "admin";
            }
            else
            {
                ViewBag.roles = "rien";
            }
            return View(vm);
           // return View(dbm.Messages.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult DetailMessage(int? id)
        {
            ViewBag.Message = "Your application description page.";
            Message m = dbm.Messages.Find(id);
            ViewBag.texte = m.Texte;
            ViewBag.posteur = m.Posteur;
            ViewBag.date = m.Date;
            ViewBag.id = id;
            var lc = dbc.Commentaires.ToList();
            lc.Reverse();
            return View(lc);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Messages/Create
        public ActionResult MCreate()
        {
            if(!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Messages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MCreate([Bind(Include = "ID,Texte,Date,Posteur")] Message message)
        {
            if (ModelState.IsValid)
            {
                dbm.Messages.Add(message);
                message.Date = DateTime.Now;
                message.Posteur = this.User.Identity.Name;
                dbm.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Commentaires/Create
        public ActionResult CCreate(int? id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            this.message = (int)id;
            this.Session.Add("idm",id);
            return View();
        }

        // POST: Commentaires/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CCreate([Bind(Include = "ID,Texte,Date,Posteur,id_message")] Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                dbc.Commentaires.Add(commentaire);
                commentaire.Date = DateTime.Now;
                commentaire.Posteur = this.User.Identity.Name;
                
                commentaire.id_message = this.Session["idm"].ToString();
                ViewBag.idm = message;
                dbc.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentaire);
        }

        // GET: Commentaires/Delete/5
        public ActionResult CDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaire commentaire = dbc.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        // POST: Commentaires/Delete/5
        [HttpPost, ActionName("CDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CDeleteConfirmed(int id)
        {
            Commentaire commentaire = dbc.Commentaires.Find(id);
            dbc.Commentaires.Remove(commentaire);
            dbc.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Messages/Delete/5
        public ActionResult MDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = dbm.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("MDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult MDeleteConfirmed(int id)
        {
            Message message = dbm.Messages.Find(id);
            dbm.Messages.Remove(message);
            dbm.SaveChanges();
            return RedirectToAction("Index");
        }

    }

    
}