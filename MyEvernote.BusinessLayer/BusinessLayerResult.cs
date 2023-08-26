using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    // class a generic bir parametre geriyorum gönderdiğim T tipli parmetre ileri class iöerinde referans alabiliyor olacağım 
    //artık classımda dönene değerlere veya oluşturacağım değişkenlerime referans verebiliyor olacağım
    //bunude tek bir class dan gelen veya döenen obje nesnesine karşı oluşturmayacağım ggeneric olduğundan dolayı solid prensiplerine uygun olmuş oacak
    // tabi burada where T:class deiyerek sadece class bir generic akacağını söylüyorum
    //bana geriye bir obje dönmesini beliyorum
    //List<object> objeclist = new List<object>(); objectlist.Add(new object(){ object içerisindeki property ler ile where döngüsüinde her defeaasında dönerek obje sonrasında liste oluşturarak ekrana gönderebilirim })
    public class BusinessLayerResult<T> where T : class
    {
        public List<string> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult() {
            Errors = new List<string>();  //null hatası almamak adına başlangıç anında oluşturuyoruz
        }
    }
}
