using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.TblKitap select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(x => x.Ad.Contains(p));
            }
            //var kitaplar = db.TblKitap.ToList();
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> kategoriler = (from i in db.TblKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.ktgr= kategoriler;

            List<SelectListItem> yazarlar = (from i in db.TblYazar.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Ad + ' ' + i.Soyad,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.yzr = yazarlar;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TblKitap p)
        {
            var ktg = db.TblKategori.Where(k => k.ID == p.TblKategori.ID).FirstOrDefault();
            var yzr = db.TblYazar.Where(y => y.ID == p.TblYazar.ID).FirstOrDefault();
            p.TblKategori = ktg;
            p.TblYazar = yzr;
            db.TblKitap.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var Kitap = db.TblKitap.Find(id);
            db.TblKitap.Remove(Kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            List<SelectListItem> kategoriler = (from i in db.TblKategori.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.AD,
                                                    Value = i.ID.ToString()
                                                }).ToList();
            ViewBag.ktgr = kategoriler;

            List<SelectListItem> yazarlar = (from i in db.TblYazar.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Ad + ' ' + i.Soyad,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.yzr = yazarlar;
            var ktp = db.TblKitap.Find(id);
            return View("KitapGetir", ktp);
        }
        public ActionResult KitapGuncelle(TblKitap p)
        {
            var kitap = db.TblKitap.Find(p.Id);
            kitap.Ad = p.Ad;
            kitap.BasımYıl = p.BasımYıl;
            kitap.Sayfa = p.Sayfa;
            kitap.YayınEvi = p.YayınEvi;
            kitap.Durum = true;
            var ktg = db.TblKategori.Where(k => k.ID == p.TblKategori.ID).FirstOrDefault();
            var yzr = db.TblYazar.Where(k => k.ID == p.TblYazar.ID).FirstOrDefault();
            kitap.Kategori = ktg.ID;
            kitap.Yazar = yzr.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}