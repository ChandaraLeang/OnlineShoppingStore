using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //if (product.Category == null && product.Price > 0)
                //{
                //    TempData["message"] = string.Format("Category is required!");
                //}

                //else if (product.Price <= 0 && product.Category != null)
                //{
                //    TempData["message"] = string.Format("Price must be possitive!");
                //}

                //else if (product.Category == null && product.Price <= 0)
                //{
                //    TempData["message"] = string.Format("Price must be possitive & Category is required!");
                //}

                //there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {   
                //if(product.Category == null && product.Price > 0)
                //{
                //    TempData["message"] = string.Format("Category is required!");
                //}

                //else if(product.Price <= 0 && product.Category != null)
                //{
                //    TempData["message"] = string.Format("Price must be possitive!");
                //}

                //else if(product.Category == null && product.Price <= 0)
                //{
                //    TempData["message"] = string.Format("Price must be possitive & Category is required!");
                //}

                //there is something wrong with the data values
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if(deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}