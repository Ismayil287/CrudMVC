using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudMVC.Models.Entity;

namespace CrudMVC.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities DB = new NorthwindEntities();


        public ActionResult Index()
        {
            var model = DB.Orders.ToList();
            return View(model);
        }
        [HttpGet]
        //////Yeni bir action result yaradırıq və Bu action resulta uyğun view hissəsini əlavə edib 
        ///uyğun kodları yazırıq
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        /////bu hissədə biz əgər table miz uyğundursa əlavə edir yox deyilsə HttpNotFound Adlı erroru işə salır
        public ActionResult Create(Order order)
        {
            if (order.OrderID == 0) 
            {
                DB.Orders.Add(order);
            }
            else
            {
                var updateData = DB.Orders.Find(order.OrderID);
                if (updateData == null)
                {
                    return HttpNotFound();
                }
                updateData.ShipName = order.ShipName;
            }
            DB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        

        ///Burda isə uyğun idlə həmin datada dəyişiklik edə bilirik.
        public ActionResult Update(int id)
        {
            var model = DB.Orders.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("Create", model);
        }
    }
}