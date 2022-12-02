using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TblUyeler p)
        {
            var bilgiler = db.TblUyeler.FirstOrDefault(x => x.Mail == p.Mail && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();              
                return RedirectToAction("Index", "Istatistik");
            }
            else
            {
                return View();
            }            
        }
    }
}