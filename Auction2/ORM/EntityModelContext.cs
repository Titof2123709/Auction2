using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public partial class EntityModelContext : DbContext
    {
        static EntityModelContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public EntityModelContext(): base("name=EntityModelContext")
        {
            Debug.WriteLine("Context creating!");
        }
        public virtual DbSet<OrmCathegory> OrmCathegories { get; set; }
        public virtual DbSet<OrmLot> OrmLots { get; set; }
        public virtual DbSet<OrmRole> OrmRoles { get; set; }
        public virtual DbSet<OrmStatys> OrmStatyses { get; set; }
        public virtual DbSet<OrmUser> OrmUsers { get; set; }
        public virtual DbSet<OrmImage> OrmImages { get; set; }

        //http://metanit.com/sharp/entityframework/6.2.php
        //ПРОЧИТАТЬ!!!
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //   base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //инфа здесь!
            //http://metanit.com/sharp/entityframework/6.2.php
            //CascadeOnDelete здесь!
            //http://professorweb.ru/my/entity-framework/6/level2/2_3.php

            modelBuilder.Entity<OrmUser>()
                .HasMany(e => e.OrmLots)
                .WithRequired(e => e.OrmUser)
                .HasForeignKey(e => e.OrmUserId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<OrmCathegory>()
                .HasMany(e => e.OrmLots)
                .WithRequired(e => e.OrmCathegory)
                .HasForeignKey(e => e.OrmCathegoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrmCountry>()
                .HasMany(e => e.OrmProfiles)
                .WithRequired(e => e.OrmCountry)
                .HasForeignKey(e => e.OrmCountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrmRole>()
                .HasMany(e => e.OrmUsers)
                .WithMany(e => e.OrmRoles)
                .Map(m => m.ToTable("UserRole")
                    .MapLeftKey("OrmRoleId")
                    .MapRightKey("OrmUserId"));

            modelBuilder.Entity<OrmStatys>()
                .HasMany(e => e.OrmLots)
                .WithRequired(e => e.OrmStatys)
                .HasForeignKey(e => e.OrmStatysId)
                .WillCascadeOnDelete(false);



         /*   modelBuilder.Entity<OrmProfile>()
                .HasRequired(p => p.OrmUser)
                .WithOptional(c => c.OrmProfile);*/

          /*  modelBuilder.Entity<OrmUser>()
                        .HasMany<OrmRole>(dbroles=>dbroles.OrmRoles)
                        .WithMany(dbusers=>dbusers.OrmUsers)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("UserId");
                            cs.MapRightKey("RoleId");
                            cs.ToTable("UserRole");
                        });*/


            modelBuilder.Entity<OrmUser>()
                .HasOptional(e => e.OrmProfile)
                .WithRequired(e => e.OrmUser)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<OrmLot>()
                .HasMany(e => e.OrmImages)
                .WithRequired(e => e.OrmLot)
                .HasForeignKey(e => e.OrmLotId)
                .WillCascadeOnDelete(true);

        }
        public void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("Context disposing!");
        }

    }
}
