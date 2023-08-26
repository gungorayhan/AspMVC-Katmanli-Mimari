using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Notes")]
    public class Note:MyEntityBase
    {
        [Required,StringLength(120)]
        public string Title { get; set; }

        [Required,StringLength(2000)]
        public string Text { get; set; }

        public bool IsDraft { get; set; }

        public int LikeCount { get; set; }

        public int CategoryId { get; set; } //id eşitleme durumu için kullanılacak

        //bir notun sadece bir sahibi vardır
        public virtual EvernoteUser Owner { get; set; }
        //bir notun birden fazla yorumu vardır
        public virtual List<Comment> Comments { get; set; }
        //her notun bir kategorisi vardır
        public virtual Category Category { get; set; }
        //bir notun birden çok liked ı vardır
        public virtual List<Liked> Likes { get; set; }

        public Note() {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
