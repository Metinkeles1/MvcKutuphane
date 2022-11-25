using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblHareket.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TblHareket p)
        {
            db.TblHareket.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult OduncIade(int id)
        {
            var odn = db.TblHareket.Find(id);
            return View("OduncIade", odn);
        }
        public ActionResult OduncGuncelle(TblHareket p)
        {
            var hareket = db.TblHareket.Find(p.Id);
            hareket.UyeGetirtarih = p.UyeGetirtarih;
            hareket.İslemDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}