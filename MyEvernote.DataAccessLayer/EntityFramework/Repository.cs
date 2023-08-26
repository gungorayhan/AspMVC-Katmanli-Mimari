using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    //öncelikle modellerinizi oluşturuduk ve aralarındaki ilişkileri Entities katmanında tanımladık.
    //şimdi businesslayer katmanında oluştruduğumuz modeller ile iligli veri tabanı işlemleri yapacağız.
    //burada tüm tablolarda benzer işlemlerim olduğundan dolayı repository patern kullanıyorum
    //Repository de T değğişken şekilde model alacaileceiği ve modelleri where ile sadece class dan gelen modelleri alablieceği söylüyorum
    // class içerisine modeli aktrdıktan sonta istenilen sorgu kullanılarak model üzerinde işlmeler yapılmaktadır. 
    //ilgili işlmelerin test.class ımızda denelerini gerçekleştireceğiz.
    // yapılan işlemler node js express ile api geliştirme adımalrında MVc mimarisi kulanımına benzemektedir
    //node js controller adımlarında benzerlikleri belirtmek adına yanlarına işlemler yazılmaktadır.
    public class Repository<T>:RepositoryBase, IRepository<T> where T:class
    {
        //private DatabaseContext db = new DatabaseContext();
        //private DatabaseContext db; // singleton ile kullanılacak--> :RespositoryBase ile kalıtım alınması sonrasında yorum satırı yapıoyruz
        private DbSet<T> _objectSet;  //ObjectSet nesnesinden türüyor DbSet- DbSet nesnesini DataAccessLayer katmanında DbContext yapılanmasını oluştururkende kullanıyoruz

        public Repository() {
            //db = RepositoryBase.CreateContext(); // static bir yapılanma olmasından dolayı new lemeden kullanıyoruz. :RespositoryBase ile kalıtım alınması sonrasında yorum satırı yapıoyruz
            //RepositoryBase içerisinde constructor ı protected ile oluşturulduğundan dolayı new leyemeyeceğiz. DbContext bağlantısı(DataAccessLayer üzerindeiki :DbContext ten türeyen class a ulaşıyoruz ) için ;
            // node js ise mongoose ile bağlantımızı farklı bir .js dosyasında oluşturarak server.js dosyamıza çağırıyoruz. controllerımıza giderken çoktan mongodb bağlantısı kurulmuş oluyor
            // server.js mongoodb.js router.js controller.js asyncService.js model.js sonrasında modeller üzerinden repository patern metotlarıan benzer mongoose metotları uygulanır


            _objectSet = context.Set<T>(); //node js -> db.collection // :RepositoryBase den context parametresini miras alıyor ve burada kullanılıyor

        }

        public List<T> List() {
            return _objectSet.ToList();  // node js find({}); findById(objectId); findByIdAndUpdate({},{});  findOne();
        }

        //aşağıda yazdığımız metot ile tüm yük sql tarafından karşılanacak. 
        public IQueryable<T> ListQueryable() {
            return _objectSet.AsQueryable<T>(); //bu yazım şeklinde ListQuryable çağırıldığı yerde ToList(); kullanıldıktan sonra tetiklenecek.Tetiklenme ve işlemler Sql tarafında gerçekleşecek 
        }


        //Kullanıcı ToList(); i  nezaman çağırmak isterse ozaman çalıcak olan listelemme fonksiyonu
        //public IQueryable<T> List(Expression<Func<T, bool>> where)
        //{
        //    return _objectSet.Where(where);
        //}

        public List<T> List(Expression<Func<T,bool>> where) { // node js db.collection.find();
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj) {  // node js db.collection.insert(); insertMany();

            _objectSet.Add(obj);
            return Save();
        }

        public int Update(T obj) { // node js findByIdAndUpdate(filter,data);
            return Save();
        }

        public int Delete(T obj)// node js findByIdAndDelete(filter);
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save() { // node js collection.Save();
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T,bool>> where) { //node js findOne(); findById();
            return _objectSet.FirstOrDefault(where);
        }
    }
}
