using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{    
    public class PanelimController : Controller
    {
        // GET: Panelim
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        [HttpGet]        
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"];
            //var degerler = db.TblUyeler.FirstOrDefault(z => z.Mail == uyeMail);
            var degerler = db.TblDuyurular.ToList();
            var UyeAdSoyad = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Ad+" "+y.Soyad).FirstOrDefault();
            var UyeFoto = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Fotoğraf).FirstOrDefault();
            var UyeKullaniciAd = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.KullaniciAdi).FirstOrDefault();
            var UyeOkul = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Okul).FirstOrDefault();
            var UyeTelefon = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Telefon).FirstOrDefault();
            var UyeMail= db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Mail).FirstOrDefault();
            var UyeId = db.TblUyeler.Where(x => x.Mail == uyeMail).Select(y => y.Id).FirstOrDefault();
            var UyeHareket = db.TblHareket.Where(x=>x.Uye == UyeId).Count();
            var UyeGelenMesaj = db.TblMesajlar.Where(x => x.Alici == uyeMail).Count();
            var Duyurular = db.TblDuyurular.Count();
            ViewBag.UyeAdSoyad = UyeAdSoyad;
            ViewBag.UyeFoto = UyeFoto;
            ViewBag.UyeKullaniciAd = UyeKullaniciAd;
            ViewBag.UyeOkul = UyeOkul;
            ViewBag.UyeTelefon = UyeTelefon;
            ViewBag.UyeMail = UyeMail;
            ViewBag.UyeHareket = UyeHareket;
            ViewBag.UyeGelenMesaj = UyeGelenMesaj;
            ViewBag.Duyurular = Duyurular;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TblUyeler p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TblUyeler.FirstOrDefault(x => x.Mail == kullanici);
            uye.Sifre = p.Sifre;
            uye.Ad = p.Ad;
            uye.Soyad = p.Soyad;
            uye.KullaniciAdi = p.KullaniciAdi;
            uye.Fotoğraf = p.Fotoğraf;
            uye.Okul = p.Okul;
            db.SaveChanges();
            return  RedirectToAction("Index");
        }
       public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TblUyeler.Where(x => x.Mail == kullanici.ToString()).Select(z => z.Id).FirstOrDefault();
            var degerler = db.TblHareket.Where(x => x.Uye == id).ToList();
            return View(degerler);
        }
        public ActionResult Duyurular()
        {
            var duyuruListesi = db.TblDuyurular.ToList();
            return View(duyuruListesi);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult partial1()
        {
            return PartialView();
        }
        public PartialViewResult partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TblUyeler.Where(x => x.Mail == kullanici).Select(y => y.Id).FirstOrDefault();
            var uyebul = db.TblUyeler.Find(id);
            return PartialView("Partial2", uyebul);
        }
    }
}