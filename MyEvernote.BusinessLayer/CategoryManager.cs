using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
namespace MyEvernote.BusinessLayer
{
   public class CategoryManager
    {
        //işlem yapacağımız modele hangi veri tabanı ile çalışcaksak ilgili klasörde tanımlı repositoryden nesne oluşturuyoruz.
        // oluşan nesnemiz sayaseinde model üzerinde işlmeler yapabilir hale geliyoruz.
        //repo_category ile artık yaptığımız tüm işlemler Category modeli üzerinden Ms_sql veri tabanı üzerinden gerçekleşecek
        // verdiğimiz model db.Set<> ile node js deki db.collection formatına bürünüyor.
        // Repository ile mongoose ın bize getirdiği modelüzerinde işlem yapma yeteneklerini getirmiş oldu.
        //CategoryManager ise noje js->expressjs ile api geliştirmede kullandığımız controller yapılanmasına benzemektedir
        // Controller oluşturulur model çağırılır ve ilgili metotlar içerisnde model üzerinden işlemler gerçekleştirilir
        
          
        private Repository<Category> repo_category = new Repository<Category>(); 

        public List<Category> GetCategories()
        {
            return repo_category.List();
        }

        public Category GetCategoryById(int id) //ilgili Id view kısmından gelecek
        {
            // burada bize gelecek kısım category
            //category ile birlikte categry e ait notların hepsi Notes başlığı altında gelmiş olacak
            return repo_category.Find(x => x.Id == id);
        }
    }
}
