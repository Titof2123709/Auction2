﻿using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DbInitializer : DropCreateDatabaseAlways/* CreateDatabaseIfNotExists*/<EntityModelContext>
    {
        protected override void Seed(EntityModelContext db)
        {
            db.Set<OrmUser>().Add(new OrmUser
            { 
                Id = 1,
                Name = "vadim",
                Email = "vadp@tut.by",
                Password = "AK3n/0C8hOx0WSsfoP72BmyxeeyzaAyEfzNrF+wrifsqcAG9sxedvtbf0PX708842Q==",
                TimeRegister = DateTime.Now ,
                OrmRoles = new List<OrmRole>() { new OrmRole() { Description = "This role can do anything", Id = 1, Name = "Admin" } }
            });

            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Все категории"});
            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Бытовая техника"});
            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Телефоны"});
            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Ноутбуки"});
            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Аудиотехника"});
            db.Set<OrmCathegory>().Add(new OrmCathegory{Name="Другое"});

            db.Set<OrmStatys>().Add(new OrmStatys { Name = "NotExpose" });
            db.Set<OrmStatys>().Add(new OrmStatys { Name = "InProcess" });
            db.Set<OrmStatys>().Add(new OrmStatys { Name = "Sold" });

            db.Set<OrmCountry>().Add(new OrmCountry { Name = "Belarus" });
            db.Set<OrmCountry>().Add(new OrmCountry { Name = "Russia" });
            db.Set<OrmCountry>().Add(new OrmCountry { Name = "Turkey" });
            db.Set<OrmCountry>().Add(new OrmCountry { Name = "America" });

            //только для проверки!!!!!!

            db.Set<OrmRole>().Add(new OrmRole() {  
                Id = 2, 
                Name = "User" ,
                Description = "This role is a visitor of auction",
                OrmUsers = new List<OrmUser>() 
                { 
                    new OrmUser() { Id=2, Name="vad", Email="tito@tut.by", Password="APqUWHeWTozEWzwv0SFfkXKGaavpRbijLRsmT/6ucSQ344UtCgqm5YOw74Bp4Z2rBg==",TimeRegister = DateTime.Now  },
                    new OrmUser() { Id=3, Name="den", Email="dido@tut.by", Password="AB0Iadga0E5kEfqsixnWhUXwxNwuMSFK1VgbmaiKX8xmy5U9vF/domKVudubHrKi5A==", TimeRegister = DateTime.Now },
                    new OrmUser() { Id=4, Name="lyk", Email="viva@tut.by", Password="APY3RxaLVA1goAJT/HmAbXKJ9uNBp4IYhbqKr+Z+Sui8y53B7jEgATvLAfzNuskH2g==", TimeRegister = DateTime.Now },  
                    new OrmUser() { Id=5, Name="grod", Email="kakao@tut.by", Password="AAuenWN+0VMsgUi5vy8bGTg5XCsoX8k0Anwvyjy1MXw/qSEs97KMR5Gx4l4JsLeO+g==", TimeRegister = DateTime.Now }, 
                    new OrmUser() { Id=6, Name="donkey", Email="mys@tut.by", Password="ANKiXTwQsf1M3WNRtoKJrmf92A3iugpCoU0rsdB96hftbQSTuZy0deHpgdGDel32sw==", TimeRegister = DateTime.Now },
                                       
                    new OrmUser() { Id=7, Name="vad1", Email="titoi@tut.by", Password="APqUWHeWTozEWzwv0SFfkXKGaavpRbijLRsmT/6ucSQ344UtCgqm5YOw74Bp4Z2rBg==",TimeRegister = DateTime.Now  },
                    new OrmUser() { Id=8, Name="den1", Email="didoi@tut.by", Password="AB0Iadga0E5kEfqsixnWhUXwxNwuMSFK1VgbmaiKX8xmy5U9vF/domKVudubHrKi5A==", TimeRegister = DateTime.Now },
                    new OrmUser() { Id=9, Name="lyk1", Email="vivai@tut.by", Password="APY3RxaLVA1goAJT/HmAbXKJ9uNBp4IYhbqKr+Z+Sui8y53B7jEgATvLAfzNuskH2g==", TimeRegister = DateTime.Now },  
                    new OrmUser() { Id=10, Name="grod1", Email="kakaoi@tut.by", Password="AAuenWN+0VMsgUi5vy8bGTg5XCsoX8k0Anwvyjy1MXw/qSEs97KMR5Gx4l4JsLeO+g==", TimeRegister = DateTime.Now }, 
                    new OrmUser() { Id=11, Name="donkey1", Email="mysi@tut.by", Password="ANKiXTwQsf1M3WNRtoKJrmf92A3iugpCoU0rsdB96hftbQSTuZy0deHpgdGDel32sw==", TimeRegister = DateTime.Now } 

                } 
            });


            //MANY TO MANY!!!
            //сначала добавляем несвязную не с чем роль!!
            db.Set<OrmRole>().Add(new OrmRole()
            {
                Id = 3,
                Name = "Moderator",
                Description = "This is role for test"
            });
            //сохраняем обязательно!!!!!
            db.SaveChanges();
            //получаем юзера из бд!!!а не создаем нового!!!иммено из бд!чтобы не было повторной записи!
            int f = 2;
            var dbuserr = db.Set<OrmUser>().FirstOrDefault(dbus=>dbus.Id==f);
            //добавляем юзера к роли!!!
            db.Set<OrmRole>().SingleOrDefault(dbr => dbr.Id == 3).OrmUsers.Add(dbuserr);



            db.Set<OrmProfile>().Add(new OrmProfile() { City = "Minsk", Id = 1, Address = "", OrmCountryId = 1, Phone = 2123709, Receiver = "Frensis" });
            db.Set<OrmProfile>().Add(new OrmProfile() { City = "Moskva", Id = 2, Address = "", OrmCountryId = 2, Phone = 2582769, Receiver = "Sayd" });
            db.Set<OrmProfile>().Add(new OrmProfile() { City = "Gomel", Id = 3, Address = "", OrmCountryId  = 1, Phone = 2788709, Receiver = "Grotov" });
            db.Set<OrmProfile>().Add(new OrmProfile() { City = "Tambov", Id = 4, Address = "", OrmCountryId  = 2, Phone = 1323489, Receiver = "Denisv" });

            db.Set<OrmLot>().Add(new OrmLot 
            {
                OrmUserId =1,
                TimeBegin =DateTime.Now,
                OrmStatysId =2,
                StartPrice =20,
                Name = "test1",
                Image = string.Empty,
                EndPrice = 0,
                Description =string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 6,
                BuyerName = "titof"
            });
            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test2",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 6,
                BuyerName = "titof"
            });
            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test3",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });
            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test4",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });
            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test5",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 4,
                BuyerName = "titof"
            });

            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test6",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });

            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 1,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test7",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });





            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 3,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test8",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 4,
                BuyerName = "titof"
            });

            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 4,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test9",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });

            db.Set<OrmLot>().Add(new OrmLot
            {
                OrmUserId = 2,
                TimeBegin = DateTime.Now,
                OrmStatysId = 2,
                StartPrice = 20,
                Name = "test10",
                Image = string.Empty,
                EndPrice = 0,
                Description = string.Empty,
                DateBegin = DateTime.Now,
                OrmCathegoryId = 5,
                BuyerName = "titof"
            });

            base.Seed(db);
            db.SaveChanges();
        }


    }
}
