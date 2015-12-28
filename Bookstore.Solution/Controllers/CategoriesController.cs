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
    public class CategoriesController : Controller
    {
        private bookstoreEntities1 db = new bookstoreEntities1();

        //
        // GET: /Categories/

        public ActionResult Index()
        {
            ArrayList items = new ArrayList();
            foreach (var item in db.Categories.ToList())
            {
                CategoryModel am = new CategoryModel();
                am.Id = item.Id;
                am.Category = item.Category;
                items.Add(am);
            }
            return View(items.Cast<CategoryModel>().ToList());
        }

        //
        // GET: /Categories/Details/5

        public ActionResult Details(long id = 0)
        {

            Categories cat = db.Categories.Find(id);
            if (cat != null)
            {
                CategoryModel cm = new CategoryModel();
                cm.Id = cat.Id;
                cm.Category = cat.Category;
                return View(cm);
            }

            return HttpNotFound();
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel categorymodel)
        {

            if (ModelState.IsValid)
            {
                Categories cat = new Categories();
                cat.Category = categorymodel.Category;
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorymodel);
        }

        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Categories cat = db.Categories.Find(id);
            if (cat != null)
            {
                CategoryModel cm = new CategoryModel();
                cm.Id = cat.Id;
                cm.Category = cat.Category;
                return View(cm);
            }

            return HttpNotFound();

        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel categorymodel)
        {
            if (ModelState.IsValid)
            {
                Categories cat = db.Categories.Find(categorymodel.Id);
                if (cat != null)
                {
                    cat.Category = categorymodel.Category;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(categorymodel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}