﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Snackautomaten_Verwaltungsapp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SnackEmDBEntities : DbContext
    {
        public SnackEmDBEntities()
            : base("name=SnackEmDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUTOMATEN> AUTOMATENs { get; set; }
        public virtual DbSet<KATEGORIE> KATEGORIEs { get; set; }
        public virtual DbSet<KONTROLLE> KONTROLLEs { get; set; }
        public virtual DbSet<POSITION> POSITIONs { get; set; }
        public virtual DbSet<PRODUKT> PRODUKTs { get; set; }
        public virtual DbSet<STANDORT> STANDORTs { get; set; }
    }
}