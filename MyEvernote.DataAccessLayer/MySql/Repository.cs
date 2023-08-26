using MyEvernote.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.MySql
{
    // öncelikle iş BusinessLayer katmanındaki repository ve repositorybase yapılanmasını DataAccessLayer katmanına almak oldu
    //EntityFramework ile çalışan Repository ve RepostoriyBase yapılanmasını ayrı bir dosyalama yaptık EntitFramework ->Repository.cs.... using MyEvernote.DataAccessLayer.EntityFrameWork
    //Abstract class oluşturduk ve Repository e ve bundan sonra oluşturukacak Repository lere buradan bir interface vermiş olduk...
    //BusinessLayer katmanında kullandığımız Repository nesnemiz artık Abstract dan interface alıyor vede RepoStoryBase den kalıtım larak veri tabanına bağlanıyor.
    //ileride MySql ile çalışacağımı düşündüğümde. öncelikle DatabaseContext.cs gibi MysqlBağlantısı oluşturacağım.
    //Sonrasında RepositoryBase.cs ile Singleton patern uyguladığım bir db bağlantısı oluşturuyorum.
    //artık sıra BusinessLayer katmanında çalışacak Repository.cs oluşturmaya geldi bağlantı için RepositoryBase den kalıtım alacağım ve veri tabanı bağlantısı kuracağım
    //Yine oluşturulan tüm Repository katmanları için geçerli olmasını beklediğimiz Interface imizi MySql klasöründe-> oluşturulan Repository.cs de kullanıyoruz.
    //Sql ve Mysql kullanılan Repository.cs aynı interface i kullanmasından dolayı içerisindeki metot farklılıkları ortadan kalkmış oldu.
    //Şimdi ben BusinessLayer katmanında modellerimle işlem yaptığım Test.cs de Repository uzantımı using MyEvernote.DataAccessLayer.MySql yaparsam herhangi bir hata almayacağım
    //Hata almamakla birlikte using MyEvernoteDataAccessLayer.MySql altındaki Repository.cs RepositoryBase.cs den Mysql bağlantısı aldığından dolayı veri tabanımda değişmiş oldu
    //Böyle bir yapılanmada veri tabanını değiştirmiş olmam sadece DataAccessLayer katmanında gerçekleşti ve BusinessLayer Entities ve WebUygulamamı etkilemedi
    //Maliyet ve okunurluk artmış oldu.
   
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        //Metotların içerisini doldurarak standart bir yapılanma oluşturacağım
        public int Delete(T obj)
        {
            //modellerimden gelen objeler ile MySql veri tabanına kayıt işlemleri buradan gerçekleştirmiş olacağım
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ListQueryable()
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
