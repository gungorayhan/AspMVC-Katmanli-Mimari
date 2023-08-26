using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace MyEvernote.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category -> Home İndex e tempdata ile yönlendirme yaptığımız metot fakat sonrasında home controller içerisindeki bir action ile yönlendirme işlemi uyguladık
        //public ActionResult Select(int? id)
        //{
        //    if (id==null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //ilgili verileri burada çağırarak sayfalarıma yönlendirebirlim
        //    //React.jd de Redux.jx veri yönetim tarafı gibi düşünebiliriz.
        //    //Busines layer tarafından verilerimizi aldık ve buradan view sayfalarımıza dağıtımı yapacağız

        //    CategoryManager cm = new CategoryManager();
           
        //    Category cat = cm.GetCategoryById(id.Value); //int? yazdığımızdan nullable olduğundan .Value yazarak değerini alıyoruz ve hatayı engellemiş oluyoruz
        //    if (cat == null) {
        //        return HttpNotFound();
        //        //return RedirectToAction("Index", Home);
        //    }

        //    TempData["mm"] = cat.Notes;
        //    return RedirectToAction("Index", "Home");
        //}
    }
}