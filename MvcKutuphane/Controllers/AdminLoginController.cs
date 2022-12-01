using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: AdminLogin
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TblAdmin p)
        {
            var bilgiler = db.TblAdmin.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici, false);
                Session["Kullanici"] = bilgiler.Kullanici.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
            return View();
            }
        }
    }
}