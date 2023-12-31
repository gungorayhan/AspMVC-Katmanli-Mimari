﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Categories")]
    public class Category:MyEntityBase
    {
        [Required,StringLength(100)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
        
        public virtual List<Note> Notes { get; set; }

        public Category() {
            //category oluştuğunda notes de oluşturuyotuz ki note eklemek istediğimiz de null hatasaına düşmemek adına
            Notes = new List<Note>();
        }
    }
}
