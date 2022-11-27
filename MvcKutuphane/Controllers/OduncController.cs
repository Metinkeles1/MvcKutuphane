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
            var degerler = db.TblHareket.Where(x => x.İslemDurum == false).ToList();
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
            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(TblHareket p)
        {
            var odn = db.TblHareket.Find(p.Id);
            DateTime d1 = DateTime.Parse(odn.IadeTarih.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
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