using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TblUyeler p)
        {
            db.TblUyeler.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}