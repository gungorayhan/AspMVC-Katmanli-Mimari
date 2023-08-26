using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //BusinessLayer.Test test = new BusinessLayer.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.CommentTest();
            //şu ana kadar yazılan metotlar test edildi ve çalışıyor.

            //Controller içerinden businesLayer katmanında oluşturduğumuz Note İş merkezine bağlanıyoruz
            //Hangi model ile çalışacak isek modeli kullanarak Repositoy nesnesi oluşturuyoruz ve modelimize  üzerinde işlem yapacağımız metot özellikleri kazandırıyoruz
            //metot özellikleri kazandırırken Repository nesnesini kullanıyor ve Repository nin kalıtım aldığı RepositoyBase ile Singleton Patern uygulaması üzerinden tek sefer veri tabanına bağlanıyoruz
            //Tabi ilgili Repository oluşturulurken absract class interfaceden türeterek oluşturuyoruz 
            //tüm bu özellikler bize ileride yapılacak gerek veri tabanı değişiklikleri olsun gerekse çalışma arkadaşı değişikliklerinde projenin yönetilebilir olmasını sağlamaktadır

            //CategoryController -> Select actiondan tempdata içerisinde yönderilerek gelen verilerin burada yakalanması ve kontrol edilmesi durumu
            //fakat biz bu durmu değiştirerek HomeControl altında ByCategory Action üzerinden işlemlerimizi yaptık
            //if (TempData["mm"]!=null) {
            //    return View(TempData["mm"] as List<Note>);
            //}


            NoteManager nm = new NoteManager();
           
            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList()); //BusineLayer ksımındaki metotdan geriye List<> aldığımızdan dolayı sonuna ToList() yazıyoruz
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList()); // ToList(); e kadar sorgu oluşacak ve ToList(); dedikten sonra sorgu gidip Sql de çalışacak
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ilgili verileri burada çağırarak sayfalarıma yönlendirebirlim
            //React.jd de Redux.jx veri yönetim tarafı gibi düşünebiliriz.
            //Busines layer tarafından verilerimizi aldık ve buradan view sayfalarımıza dağıtımı yapacağız

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value); //int? yazdığımızdan nullable olduğundan .Value yazarak değerini alıyoruz ve hatayı engellemiş oluyoruz
            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index", Home);
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        //Notlerımızın anasayfada son eklenenlerden başlayarak listelemesinide yaptık. Şimdi en çok bağanilenlerlin listelemesini yapacağız.ByCategory sonrası işlemler
        //Buradan Geriye Doğru düşünecek olursak
        //Öncelikle BusinesLayer Katmanında NoteManager.cs ile veri tabanı işlemlerimizi gerçekleştirdiğimiz alana veri almak için metot yazacağız
        //BusinessLayer ->katmanımızda veri alma işlemlerini model üzerinden gerçekleştirecek.
        //Modellerin listeleme, tekil listele, ekleme,silme güncelleme işlemlerini kazandırdığımız Repository patern uygulamasından nesne türetmesi gerekecek
        //nesne türetmesi sonucunda Modele ait moetotlar ve Singleton patern sayesinde veri tabanı bağlantı işlemleri yapılabilecek
        //Singleten patern uygulanana RepositoryBase.cs ise bağlantı ve veri taanı model bilgilerini( DbSet<ModelAdi> ) DatabaseContext.cs de alacak
        public ActionResult MostLiked()
        {
            //view ekranından Home/MostLiked ulaşırız
            NoteManager nm = new NoteManager();
            //burada tekrar bir View()  nesnesi oluşturmaktansa Index in View ına yönlendiriyoruz
            //Krştşk olan konu -> Index View da Note model aldığından kabul ediyor. gelen veriyi tür dönüşümü sıkıntısı çıkarmadan listeliyor
            return View("Index",nm.GetAllNote().OrderByDescending(x=>x.LikeCount).ToList()); // ToList() üzerine imleci getirdiğimizde istenen türü görebiliriz
        }

        //MostLiked işlemleri sonrasında 
        public ActionResult About() {
            return View();
        }


        //About Sonrasında 
        public ActionResult Login() {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model) {
            //Giriş kontrolü ve yönlendirme yapılacak
            //Session a kullanıcı bilgi saklama
            return View();
        }


        public ActionResult Register() {

            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            //aşağıda yazılan kodları yorum satırı  yapıyorum çünkü;
            //ilgili kodlar beni,m işlem basamaklarım bugün webapp katmanında yazıyorum ama yarın mobile katmanında lazım olabilir
            //web ve mobile ortak katman busines katmanı okcağından busines katmanına taşıyorum
            //BusinessLayer-> EvernoteUSerManager
            //if (ModelState.IsValid) {//model doğrulama belirtilen kuralları geçmiş ise
            //    if (model.Username == "aaa")
            //    {
            //        ModelState.AddModelError("", "Kullanıcı Adı Kullanılıyor.");
            //    }
            //    if (model.Email == "aaa")
            //    {
            //        ModelState.AddModelError("", "Eposta Adres Kullanılıyor.");
            //    }
            //    foreach (var item in ModelState) {
            //        if (item.Value.Errors.Count>0) {
            //            return View(model);
            //        }
            //    }
            //}
            //kullanıcı username kontrolü
            //kullanıcı e-posta kontrolü
            //kayıt işlemi
            //aktivasyon e-postası gönderimi

            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

            //tyrcatch örneği için 
            //EvernoteUser user = null;
            //try
            //{
            //    user = eum.RegisterUser(model);
            //}
            //catch (Exception ex)
            //{

            //    ModelState.AddModelError("",ex.Message);
            //}

            //--------BusinessLayer a geçtik
            if (ModelState.IsValid) {
                if (res.Errors.Count>0) {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
               
            }
            

            return RedirectToAction("RegisterOk");
        }
        public ActionResult RegisterOk()
        {

            return View();
        }
        public ActionResult UserActivate(Guid activated_id) {
            //kullanıcı aktivasyonu sağlanacak
            return View();
        }

        public ActionResult Logout() {

            return View();
        }

        



    }
}