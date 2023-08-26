using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    //Singleton yapılanması sabittir istenilen nesne tekil aşağıdaki gibi tekil hale getireilebilir
    //public class RepositoryBase
    //{

    //    private static DatabaseContext _db;
    //    private static object _lockSync = new object();

    //    //new lenmesini istemediğimizden dolayı protected diyoruz
    //    //ilgili nesnemiz new diyerek yeniden oluşturulamayacak
    //    //comment eklerken aldığımız her model farklı DbContext türemesi olayının önünü kapamış olacağız
    //    //new leyemeyeceğimiz iç,n static bir metot yazmamız gerekece.Çünkğ static metotlar new demeden de çalışıyor
    //    protected RepositoryBase() {

    //    }

    //    public static DatabaseContext CreateContext() {
    //        // boş ise yeni bağlantı oluşturacak değil ise var olanı dönecek
    //        // tek bir dbContext bağlantısı oluşturmuş olduk
    //        // Bundan sonra BusinessLayer üzerinden DataAccessLayer ile işlmeler esnasında tek bir bağlantı üzerinden çalışacağız
    //        // Çalışacağımız tekil bağlantı  Entities katmanındaki modelleri kullanarak oluşturduğumuz  yapıları-> tek bir bağlantıdan oluşturarak kendi aralarında işlemler yapabilir hale getirecek
    //        if(_db == null)
    //        {
    //            lock (_lockSync)
    //            {  //multi threading  yapılanmalarında bir iş halledilmeden başka bir işe geçilmemesi adına yapılan kilitlemelerde kullanılır
    //                if (_db == null)
    //                {
    //                    _db = new DatabaseContext(); //işi garantiye alıyoruz :D
    //                }
    //            }
                
    //        }

    //        return _db;
    //    }



    //}

    // miras vereceğimiz zaman aşağıdaki gibi yazabiliriz!

    public class RepositoryBase // güncel olarak burada yazılan singleton yapılanmasını kulllanıyoruz
    {
        protected static DatabaseContext context; //protected yaptığımızdan dolayı miras alınabilir hale geldi
        private static object _lockSync = new object();
        protected RepositoryBase() {
            CreateContext();
        }

        private static void CreateContext() { // artık hernagi bir şey dönmesine gerek yok ve private olması gerekiyor
            if (context == null) {
                lock (_lockSync)
                {
                    if (context == null) {
                        context = new DatabaseContext();
                    }
                }
            }
        }

    }
}
