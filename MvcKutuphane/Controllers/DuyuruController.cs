using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblDuyurular.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TblDuyurular p)
        {
            db.TblDuyurular.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TblDuyurular.Find(id);
            db.TblDuyurular.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(TblDuyurular p)
        {
            var duyuru = db.TblDuyurular.Find(p.Id);
            return View("DuyuruDetay", duyuru);
        }
        public ActionResult DuyuruGuncelle(TblDuyurular p)
        {
            var duyuru = db.TblDuyurular.Find(p.Id);
            duyuru.Icerik = p.Icerik;
            duyuru.Kategori = p.Kategori;
            duyuru.Tarih = p.Tarih;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}