﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LessonScheduler.AppFiles
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbSchedulerEntities : DbContext
    {
        public DbSchedulerEntities()
            : base("name=DbSchedulerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Days> Days { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<LessonsPlans> LessonsPlans { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}