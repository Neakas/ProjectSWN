﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWNAdmin.Utility
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Db1Entities : DbContext
    {
        public Db1Entities()
            : base("name=Db1Entities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advantages> Advantages { get; set; }
        public virtual DbSet<CharacterBonus> CharacterBonus { get; set; }
        public virtual DbSet<CharacterMalus> CharacterMalus { get; set; }
        public virtual DbSet<Characters> Characters { get; set; }
        public virtual DbSet<Disadvantages> Disadvantages { get; set; }
        public virtual DbSet<InnerMoonlets> InnerMoonlets { get; set; }
        public virtual DbSet<MajorMoons> MajorMoons { get; set; }
        public virtual DbSet<OuterMoonlets> OuterMoonlets { get; set; }
        public virtual DbSet<Planets> Planets { get; set; }
        public virtual DbSet<PName> PName { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<SatelliteBaseTypes> SatelliteBaseTypes { get; set; }
        public virtual DbSet<Stars> Stars { get; set; }
        public virtual DbSet<StarSystems> StarSystems { get; set; }
        public virtual DbSet<UsedBonus> UsedBonus { get; set; }
        public virtual DbSet<UsedMalus> UsedMalus { get; set; }
        public virtual DbSet<UDateTime> UDateTime { get; set; }
        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<SkillSpecialization> SkillSpecialization { get; set; }
        public virtual DbSet<Requirements> Requirements { get; set; }
        public virtual DbSet<Modifier> Modifier { get; set; }
        public virtual DbSet<StatGroup> StatGroup { get; set; }
        public virtual DbSet<StatSubGroup> StatSubGroup { get; set; }
    }
}
