using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    //WepApp katmanında başlatarak businelayer katmanına taşıdığımız kullanıcı işlemelrini yapacağız
    //öncelikle node js sayfasında ki controller.js olarak düşünürsek; metot yazmadan önce modelimiz çağırcağız
    //metot içerisine kullanıcadam gelen verileri olacağız(Request)
    // metot içerinde Repository nesnesi ile türettiğimiz modelimizi çağırıyoruz. 
    //node js -> controller.js -> const User = require("/path..."); 
    //export method(req,res){ daha önce kayıtlı user vr mı kontrolleri yapılır try{const user = await User.create(req.body)}catch{throw new Error("User oluşturudu")}}
    //.net public olarak belirledikten sonra dışarıdan ulaşabiliyoruz. node js de ise dılarıya export diyerek çağıralabilir halde yazıyoruz
    // .net de dönüş değerlerini belirtmeek gerekirken node js de typescript ile yazılan kodlarda dönüşdeğeri istememktedir.
    // yazılan metotlara asp mvc içerisinde contoller seviyesinde yani wepapp seviyesinden ulaşırız 
    // node js ise react içerinde yapılanması yağtığımız redux iöerinden ulaşarak uygulama içerinde store -> Provider state şeklinde bir dağıtım yaprak  page.js lerde kullanımı yapmaktayız
   public class EvernoteUserManager // class içerinde işlemlerimiz gerçekleitireceğiz node js->contoller.js
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        // modelimiz çağırıdığımıza göre metotları yazmaya başlayabiliriz

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data) {
            //kullanıcı user kontrolü
            //kullanıcı e-posta kontrolü
            //kayıt işlemi
            //aktivasyon e-posta gönderimi
            //javascript te biz burasını const user diyerek geçebşklşyoruz. fakat .net ve typscripte bir dönüş değeri geleceğinden dolayı ;
            //bizim dönüş değerimiz tanıyabilmiz adına string int gibi gelen ifadeyi tanımalayan bir değer ile karşılammmız lazım
           EvernoteUser user = repo_user.Find(x =>x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            if (user!=null) {
                //böyle bir dönüş sağlamamız durumdan controller actionda try catch bloğunda yakalamamız gerek
                // yakaladıktan sonra ModelState.AddError("",exMeesage) yapmamaız gerekiyor 
                //throw new Exception("Kayıtlı kullanıcı adı veya e-posta aderesi");
                //BusinesLayerREsult sonrası;

                //database den döenen nesne üzerinden sorgulamalar yapıyoruz
                if (user.Username == data.Username) {
                    layerResult.Errors.Add("Kullanıcı Adı Kayıtlı.");
                }
                if (user.Email==data.Email) {
                    //listeler de add metodunu kullanıyoruz
                    layerResult.Errors.Add("E-posta Adtresi Kayıtlı");
                }
                
            }
            else
            {
                //burada gelen insert ve find metotları repository metotlarından gelmektedir.
                //model ile kullanıldıkalrı anda veri tabanı işlemleri gerçekleştirilmekte
               int dbResult = repo_user.Insert(new EvernoteUser() {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUsername="system",
                    IsActive=false,
                    IsAdmin=false
                });

                if (dbResult>1) {
                    layerResult.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);
                    //TODO: aktivasyon mail ı atılacak
                    //layerResult.Result.Activate
                }
            }

            return layerResult;
            
        }

    }
}
