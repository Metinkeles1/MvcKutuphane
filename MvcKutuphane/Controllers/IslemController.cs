using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class IslemController : Controller
    {
        // GET: Islem
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TblHareket.Where(x => x.İslemDurum == true).ToList();
            var degerler = db.TblHareket.Where(x => x.İslemDurum == true).ToList().ToPagedList(sayfa, 15);
            return View(degerler);
        }
    }
}