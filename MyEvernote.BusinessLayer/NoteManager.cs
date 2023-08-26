using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class NoteManager // node js de controller ı temsil etmektedir 
    {
        private Repository<Note> repo_note = new Repository<Note>();

        public List<Note> GetAllNote(){ //controller içerisindeki metotlerı temsil etmektedir public ile dışarıya sunuyoruz. node js de module.export diyoruz
            // node js de-> mongoose ile oluşturduğumuz modele metotlar geldiğinden dolayı find(); yazarak buradaki List(); i yapmış oluyoruz
            // oluşturukan modele sonradan genel metotlar yazmak adına kullanıyoruz.
            // node js de de modellerimize özel metotlar yazabilmekteyi. model_name.methods.method_name=async function(){}
            return repo_note.List();  
        }

        //IQueryable kullanımı
        public IQueryable<Note> GetAllNoteQueryable() {
            return repo_note.ListQueryable();
        }
    }
}
