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
        [Authorize(Roles ="A")]
        public ActionResult Index()
        {
            var degerler = db.TblHareket.Where(x => x.İslemDurum == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> uyeler = (from x in db.TblUyeler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Ad +" "+ x.Soyad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            List<SelectListItem> kitaplar = (from x in db.TblKitap.Where(x=>x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Ad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            List<SelectListItem> personeller = (from x in db.TblPersonel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Personel,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.uyeler = uyeler;
            ViewBag.kitaplar = kitaplar;
            ViewBag.personeller = personeller;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TblHareket p)
        {
            var uye = db.TblUyeler.Where(x => x.Id == p.TblUyeler.Id).FirstOrDefault();
            var kitap = db.TblKitap.Where(x => x.Id == p.TblKitap.Id).FirstOrDefault();
            var personel = db.TblPersonel.Where(x => x.Id == p.TblPersonel.Id).FirstOrDefault();
            p.TblUyeler = uye;
            p.TblKitap = kitap;
            p.TblPersonel = personel;
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