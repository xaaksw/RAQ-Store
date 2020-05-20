using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RAQ_Store.Models;
using RAQ_Store.ViewModels;


namespace RAQ_Store.Controllers
{
    public class ProductsController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult About() {

            return View();
        }

        // this is our index , to show all products 
        [HttpGet]
        public ActionResult ViewProduct()
        {

            ProductCategory prca = new ProductCategory
            {
                Product = db.Products.ToList(),
                Category = db.Categories.ToList(),
                cart = db.Cart.ToList()
            };
            return View(prca);
        }
        // this is index when hitting filter button 
        [HttpPost]
        public ActionResult ViewProduct(int? ID)
        {
            if ((ID ?? 0) == 0 || db.Categories.ToList().Where(s => s.id == ID).Count() == 0)
            {
                ProductCategory prca = new ProductCategory
                {
                    Product = db.Products.ToList(),
                    Category = db.Categories.ToList(),
                    cart = db.Cart.ToList()
                };
                return View(prca);
            }
            var product = db.Products.ToList().Where(c => c.category_id == ID);

            ProductCategory prc = new ProductCategory
            {
                Product = product.ToList(),
                Category = db.Categories.ToList()
              
            };


            return View(prc);
        }

        //open the cart action
        [ChildActionOnly]
        public ActionResult Cart()
        {

            ProductCategory prca = new ProductCategory
            {
                Product = db.Products.ToList(),
                cart = db.Cart.ToList()
            };

            return PartialView(prca);
        }


        // Add cart button action 
        [HttpGet]
        public ActionResult AddCart(int? ID, DateTime? time)
        {
            if (ID == null || db.Products.ToList().Where(s => s.id == ID).Count() == 0 || time == null)
            {

                return RedirectToAction("ViewProduct");
            }
            Cart adcrt = new Cart
            { added_at = time.Value,
                product_id = ID.Value
            };
                       
                db.Cart.Add(adcrt);
                db.SaveChanges();
            
            return RedirectToAction("ViewProduct");
        }

        // remove from cart button action 
        [HttpGet]
        public ActionResult RemoveCart(int? ID , DateTime? time)
        {
            if (ID == null || db.Products.ToList().Where(s => s.id == ID).Count() == 0 || time ==null) {

                return RedirectToAction("ViewProduct");
            }
            var car = db.Cart.Single(c => c.product_id == ID && c.added_at == time);
            db.Cart.Remove(car);
            db.SaveChanges();

            return RedirectToAction("ViewProduct");

        }

        // add product button which load add product page 
        [HttpGet]
        public ActionResult AddProduct()
        {

            ProductCategory prca = new ProductCategory
            {
                Category = db.Categories.ToList()
            };

            return View(prca);
        }
        // add proudct action button 
        [HttpPost]
        public ActionResult AddProduct(ProductCategory prca, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                prca.Category = db.Categories.ToList();
                return View(prca);
            }
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                file.SaveAs(path);
                prca.MyProduct.image = pic;
            }
            var CategDb = db.Categories.ToList().Single(c => c.id == prca.MyProduct.category_id);
            CategDb.number_of_products = CategDb.number_of_products+1;

            db.Products.Add(prca.MyProduct);
            db.SaveChanges();

            return RedirectToAction("ViewProduct");
        }
        
        // get product details
        [HttpGet]
        public ActionResult Details(int? ID)
        {
            if (ID == null || db.Products.ToList().Where(s=>s.id==ID).Count()==0) {

                return RedirectToAction("ViewProduct");

            }
            var product = db.Products.ToList().Single(c => c.id == ID);

            ProductCategory prca = new ProductCategory
            {
                MyProduct = product,
                MyCategory = db.Categories.ToList().Single(c => c.id == product.category_id),
            };
            return View(prca);
        }


        // get update product page 
        [HttpGet]
        public ActionResult Update(int? ID)
        {
            if (ID == null || db.Products.ToList().Where(s => s.id == ID).Count() == 0)
            {

                return RedirectToAction("ViewProduct");

            }

            var product = db.Products.ToList().Single(c => c.id == ID);

            ProductCategory prca = new ProductCategory
            {
                MyProduct = product,
                MyCategory = db.Categories.ToList().Single(c => c.id == product.category_id),
                Category = db.Categories.ToList()
            };


            return View(prca);
        }
        // Update proudct action button
        [HttpPost]
        public ActionResult Update(ProductCategory prca, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                var mcat = db.Categories.ToList();
                prca.Category = mcat;
                return View("Update", prca);
            }
            var productDb = db.Products.ToList().Single(c => c.id == prca.MyProduct.id);
           if (file!=null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                file.SaveAs(path);
                prca.MyProduct.image = pic;
                productDb.image = prca.MyProduct.image;
            }
          
            productDb.name = prca.MyProduct.name;
            productDb.price = prca.MyProduct.price;
            productDb.category_id = prca.MyProduct.category_id;
            productDb.description = prca.MyProduct.description;


            db.SaveChanges();

            return RedirectToAction("ViewProduct");
        }

        // Delete Product action button
        [HttpGet]
        public ActionResult Delete(int? ID)
        {
            if (ID == null || db.Products.ToList().Where(s => s.id == ID).Count() == 0) {

                return RedirectToAction("ViewProduct");

            }
                var product = db.Products.Single(c => c.id == ID);
            var CategDb = db.Categories.ToList().Single(c => c.id == product.category_id);

            CategDb.number_of_products = CategDb.number_of_products-1;

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("ViewProduct");
        }      
    }
}


        /**
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,image,description,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,image,description,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
}**/