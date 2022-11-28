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
        [Authorize]
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"];
            var degerler = db.TblUyeler.FirstOrDefault(z => z.Mail == uyeMail);
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
    }
}