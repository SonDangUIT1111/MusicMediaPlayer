﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicMediaPlayer.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicPlayerEntities : DbContext
    {
        public MusicPlayerEntities()
            : base("name=MusicPlayerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PlayList> PlayList { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
    }
}
