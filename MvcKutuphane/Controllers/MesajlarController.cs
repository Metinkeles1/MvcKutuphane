using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            var mesajlar = db.TblMesajlar.Where(x => x.Alici == uyeMail.ToString()).ToList();
            return View(mesajlar);
        }
        public ActionResult GidenMesaj()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            var mesajlar = db.TblMesajlar.Where(x => x.Gonderen == uyeMail.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(TblMesajlar p)
        {
            var uyeMail = (string)Session["Mail"].ToString();
            p.Gonderen = uyeMail.ToString();
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblMesajlar.Add(p);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj", "Mesajlar");
        }        
        public PartialViewResult MesajlarMenu()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            var gelensayisi = db.TblMesajlar.Where(x => x.Alici == uyeMail).Count();
            ViewBag.gelenMesaj = gelensayisi;
            var gidensayisi = db.TblMesajlar.Where(x => x.Gonderen == uyeMail).Count();
            ViewBag.gidenMesaj = gidensayisi;
            return PartialView();
        }
    }
}