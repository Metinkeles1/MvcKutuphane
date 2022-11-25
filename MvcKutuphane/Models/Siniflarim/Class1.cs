using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Models.Siniflarim
{
    public class Class1
    {
        public IEnumerable<TblKitap> Kitap{ get; set; }
        public IEnumerable<TblHakkimizda> hakkimda{ get; set; }
    }
}