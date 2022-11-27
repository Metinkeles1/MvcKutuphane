using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index()
        {
            var deger1 = db.TblUyeler.Count();
            var deger2 = db.TblKitap.Count();
            var deger3 = db.TblKitap.Where(x => x.Durum == false).Count();
            var deger4 = db.TblCezalar.Sum(x => x.Para);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyaYolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyaYolu);                
            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            var kitapSayisi = db.TblKitap.Count();
            var uyeSayisi = db.TblUyeler.Count();
            var kasadakiTutar = db.TblCezalar.Sum(x => x.Para);
            var oduncKitaplar = db.TblKitap.Where(x => x.Durum == false).Count();
            var kategoriSayisi = db.TblKategori.Count();
            var mesajSayisi = db.TblIletisim.Count();
            var enFazlaKitapYazar = db.EnFazlakitapYazar().FirstOrDefault();
            var enIyıYayinEvi = db.TblKitap.GroupBy(x => x.YayınEvi).OrderByDescending(z => z.Count()).Select(y => new { y.Key}).FirstOrDefault().Key;
            var enAktifUye = db.TblHareket.GroupBy(x => x.TblUyeler.Ad).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            var enCokOkunanKitap = db.TblHareket.GroupBy(x => x.TblKitap.Ad).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            var enAktifPersonel = db.TblHareket.GroupBy(x => x.TblPersonel.Personel).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            var bugunkuEmanetler = db.TblKitap.Where(x => x.Durum == false).Count();
            ViewBag.kitapSayisi = kitapSayisi;
            ViewBag.uyeSayisi = uyeSayisi;
            ViewBag.kasadakiTutar = kasadakiTutar;
            ViewBag.oduncKitaplar = oduncKitaplar;
            ViewBag.kategoriSayisi = kategoriSayisi;
            ViewBag.mesajSayisi = mesajSayisi;
            ViewBag.enFazlaKitapYazar = enFazlaKitapYazar;
            ViewBag.enIyiYayinEvi = enIyıYayinEvi;
            ViewBag.enAktifUye = enAktifUye;
            ViewBag.enCokOkunanKitap = enCokOkunanKitap;
            ViewBag.enAktifPersonel = enAktifPersonel;
            ViewBag.bugunkuEmanetler = bugunkuEmanetler;
            return View();
        }
    }
}