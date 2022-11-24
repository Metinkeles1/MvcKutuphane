using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblYazar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TblYazar t)
        {
            db.TblYazar.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TblYazar.Find(id);
            db.TblYazar.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarGetir(int id)
        {
            var yazar = db.TblYazar.Find(id);
            return View("YazarGetir", yazar);
        }
        public ActionResult YazarGuncelle(TblYazar p)
        {
            var yazar = db.TblYazar.Find(p.ID);
            yazar.Ad = p.Ad;
            yazar.Soyad = p.Soyad;
            yazar.Detay = p.Detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}