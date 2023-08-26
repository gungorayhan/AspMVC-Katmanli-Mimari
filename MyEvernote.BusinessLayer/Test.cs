using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;


namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        //ilgli kısımları Repository den türetiyoruz ve generic bir tür giriyoruz.
        //girdiğimiz generic tür modelden geliyor. İlgili model DbContext te kullanılıyor.
        //artıık Repository sayesinde modelimize DbContext üzerinden işlemler yapabilmesi için;
        //listeleme,kısıtlara göre listeleme, ekleme, güncelleme ve silme işlevselliği katmış bulunuyoruz
        // node js de ise mongoose ile türetilen nesnelerimize bu özellik mongoose ile kalıtım olarak gelmektedir.
        //find(), findOne(), Save(), FindByIdAndUpdate(), FindByIdAndDelete(), FindById(), Insert(), InsertMany(); vb özellikler.

        //Singleton patern uygulaması sonrasında ilk repository new lenmesinden sonra repositorybase içerisinde CreateContext tetiklenecek içerisinde ki kurala göre tek sefer bağlantı oluşturacak
        //sonraki repository new lemelerinde repositorybase içerinde ki bağlantı tekrar oluşturulmayacak-> Singleton Patern
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();
         
        public Test()
        {
            //-------------------------------------------------------------
            // ilgili adımları uygulamamızı ilk çalıştırma esnasında fake data oluşturmak adına tanımladık.
            //DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();
            ////database yok ise oluştur.
            ////db.Database.CreateIfNotExists()
            ////örnek data oluşturmak için ilgili kodu çalıştırmak gerekecektir.
            //db.Categories.ToList();
            //------------------------------------------------------------
            //HomeControllerda Text.class ı nı new liyoruz yani bir nesne oluşturuyoruz.
            // Test classın nesnesi türetiliyor. costructor ında çalışmasını istediğimiz kodlar businessLayer katmanında Repository class ındaki işlemler datalayer katmanı ile  


            //Repository Patern uygulaması sonrasında yazılan kısımlar
            
            //repo.List(x => x.Id > 5); system.linq.Expression
           List<Category> categories= repo_category.List();
           //List<Category> categories_filtered = repo_category.List(x=>x.Id>5);
        }

        public void InsertTest() {
            
            int result = repo_user.Insert(new EvernoteUser()
            {
                Name = "aaa",
                Surname = "bbb",
                Email = "aaa@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "aabb",
                Password = "111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername="aabb"

            });

        }

        public void UpdateTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "aabb");
            if (user != null)
            {
                user.Username = "xxx";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "xxx");
            if (user != null) {
                int result =repo_user.Delete(user);
            }
        }

        public void CommentTest() {
            // Comment ekleyeceğiz ama öncelikle bu commenti kim ekleyecek ve hangi note altına eklenecek 
            // kullanıcıyı bulacağız ve notu bulacağız
            // comment oluşturarak ekleme işlemlerini yapmış olacağız
            //aşağıda işlem yaptığım nesnelerin herbiri Repository den çoğaltıldı. çağalırken DbContext nesnesine de her defasından yeniden oluşturuluyor.
            // bu durumda herbiri farklı DbContext nesnesinden geliyor anlamına geliyor. SaveChange yapacağımız zaman bir eşleştirme problemi yaşayacağız.
            //farklı IEntityChangeTracker lar tarından takipler olduğunun hatasını alacağız.
            //hepsini aynı DbContext ile takip etmeli ve oluşturmalıyız.
            //commentin içerisinde başka dbcontenxt nesneler olamayacağını söylüyor.
            //Bu problemin çözümü her yerde yeniden DbContext oluşumunu engellemektir. 
            //Singleton-> bir nesnenin sadece bir kopyası oluşsun.RepositoryBase oluşturuyoruz bu adımda 

            EvernoteUser user = repo_user.Find(x=>x.Id==1);
            Note note = repo_note.Find(x => x.Id == 3);

            Comment comment = new Comment()
            {
                Text = "Bu bir test'dir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "ayhangungor",
                Note = note,
                Owner = user
            };
            repo_comment.Insert(comment);

        }
    }
}
