using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookstore.Solution.Models;
using Bookstore.DAL;
using System.Collections;

namespace Bookstore.Solution.Controllers
{
    public class AuthorsController : Controller
    {
        private bookstoreEntities1 db = new bookstoreEntities1();

        //
        // GET: /Authors/

        public ActionResult Index()
        {
            ArrayList items = new ArrayList();
            foreach (var item in db.Authors.ToList())
            {
                AuthorModel am = new AuthorModel();
                am.Id = item.Id;
                am.Author = item.Author;
                items.Add(am);
            }
            return View(items.Cast<AuthorModel>().ToList());
        }

        //
        // GET: /Authors/Details/5

        public ActionResult Details(long id = 0)
        {
            Authors aut = db.Authors.Find(id);
            if (aut != null)
            {
                AuthorModel authormodel = new AuthorModel();
                authormodel.Id = aut.Id;
                authormodel.Author = aut.Author;
                return View(authormodel);    
            }
            
            return HttpNotFound();
        }

        //
        // GET: /Authors/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Authors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorModel authormodel)
        {
            if (ModelState.IsValid)
            {
                Authors aut = new Authors();
                aut.Author = authormodel.Author;
                db.Authors.Add(aut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authormodel);
        }

        //
        // GET: /Authors/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Authors aut = db.Authors.Find(id);
            if (aut != null)
            {
                AuthorModel authormodel = new AuthorModel();
                authormodel.Id = aut.Id;
                authormodel.Author = aut.Author;
                return View(authormodel);
            }

            return HttpNotFound();
        }

        //
        // POST: /Authors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorModel authormodel)
        {
            if (ModelState.IsValid)
            {
                Authors aut = db.Authors.Find(authormodel.Id);
                if (aut != null)
                {
                    aut.Author = authormodel.Author;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(authormodel);

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}