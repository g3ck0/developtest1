using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookstore.Solution.Models;
using Bookstore.DAL;
using System.Web.Configuration;
using System.Collections;

namespace Bookstore.Solution.Controllers
{
    public class BooksController : Controller
    {
        private bookstoreEntities1 db = new bookstoreEntities1();

        //
        // GET: /Books/

        public ActionResult Index()
        {
            ArrayList items = new ArrayList();
            foreach (var item in db.Books.ToList())
            {
                BookModel bm = new BookModel();
                bm.Id = item.Id;
                bm.Title = item.Title;
                items.Add(bm);
            }
            return View(items.Cast<BookModel>().ToList());
        }

        //
        // GET: /Books/Details/5

        public ActionResult Details(long id = 0)
        {
            Books bookmodel = db.Books.Find(id);
            if (bookmodel != null)
            {
                BookModel bm = new BookModel();
                bm.Id = bookmodel.Id;
                bm.Title = bookmodel.Title;
                ViewBag.Authors = bookmodel.Authors.Select(c => c.Author).ToArray();
                ViewBag.Categories = bookmodel.Categories.Select(c => c.Category).ToArray();
                return View(bm);
            }
            return HttpNotFound();
        }

        //
        // GET: /Books/Create

        public ActionResult Create()
        {
            ViewBag.Authors = from a in db.Authors select new AuthorModel { 
                Id = a.Id,
                Author = a.Author
            };
            ViewBag.Categories = from a in db.Categories
                                 select new CategoryModel
                                 {
                                     Id = a.Id,
                                     Category = a.Category
                                 };
            return View();
        }

        //
        // POST: /Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(BookModel bookmodel)
        public ActionResult Create(FormCollection collection)
        {
            
            Books bookModel = new Books();
            bookModel.Title = collection.GetValue("Title").AttemptedValue;
            if (collection.GetValue("BookCategories").AttemptedValue != "")
            {
                var splitCategories = collection.GetValue("BookCategories").AttemptedValue.Split(',');
                
                foreach(var i in splitCategories)
                {
                    var c = db.Categories.Find(int.Parse(i));
                    bookModel.Categories.Add(c);
                }
            }
            if (collection.GetValue("BookAuthors").AttemptedValue != "")
            {
                var splitAuthors = collection.GetValue("BookAuthors").AttemptedValue.Split(',');
                foreach (var i in splitAuthors)
                {
                    var c = db.Authors.Find(int.Parse(i));
                    bookModel.Authors.Add(c);
                }
            }

            db.Books.Add(bookModel);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //
        // GET: /Books/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Books bookmodel = db.Books.Find(id);
            if (bookmodel != null)
            {
                BookModel bm = new BookModel();
                bm.Id = bookmodel.Id;
                bm.Title = bookmodel.Title;
                bm.BookAuthor = bookmodel.Authors.Select(c => c.Id).ToArray();
                bm.BookCategory = bookmodel.Categories.Select(c => c.Id).ToArray();

                ViewBag.Authors = from a in db.Authors
                                  select new AuthorModel
                                  {
                                      Id = a.Id,
                                      Author = a.Author
                                  };
                ViewBag.Categories = from a in db.Categories
                                     select new CategoryModel
                                     {
                                         Id = a.Id,
                                         Category = a.Category
                                     };

                return View(bm);        
            }
            return HttpNotFound();
            
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookModel bookmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmodel).State = System.Data.Entity.EntityState.Modified ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookmodel);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}