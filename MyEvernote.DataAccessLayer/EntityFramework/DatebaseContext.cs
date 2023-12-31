﻿using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        //tablolarımıza karşılık gelen veri setlerimizi tanımlayacağız
        //Entities referans olarak eklenir
        //dikkat edilmesi gereken DbSet ile nesnelerimizi oluşturuyıryz
        public DbSet<EvernoteUser> EvernoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }


        public DatabaseContext() {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
