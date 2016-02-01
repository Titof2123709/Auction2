using DAL.Interface.DalModel;
using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalMappers
{
    public static class Maper
    {
        public static IEnumerable<DalUser> ToDalUser(IEnumerable<OrmUser> ormusers)
        {
            foreach (var ormuser in ormusers)
            {
                yield return ToDalUser(ormuser);
            }
        }

        public static DalUser ToDalUser(OrmUser ormuser)
        {
          if(ormuser!=null)  return new DalUser()
            { 
                Password = ormuser.Password,
                Name = ormuser.Name,
                Id = ormuser.Id,
                Email = ormuser.Email,
                TimeRegister = ormuser.TimeRegister
            };
          return null;
        }
        
        //без создания нового объекта 
        //вроде здесь на null не надо проверять!!
        public static void ToOrmUser(DalUser daluser, OrmUser inThisOrmUser)
        {
            inThisOrmUser.Password = daluser.Password;
            inThisOrmUser.Name = daluser.Name;
            inThisOrmUser.Id = daluser.Id;
            inThisOrmUser.Email = daluser.Email;
            inThisOrmUser.TimeRegister = daluser.TimeRegister;
        }







        public static IEnumerable<DalLot> ToDalLot(IEnumerable<OrmLot> ormlots)
        {
            foreach (var ormlot in ormlots)
            {
                yield return ToDalLot(ormlot);
            }
        }

        //неверно!!!
     /*   public static IEnumerable<OrmLot> ToOrmLot(this IEnumerable<DalLot> dallots)
        {
            foreach (var dallot in dallots)
            {
                yield return dallot.ToOrmLot();
            }
        }*/

        public static DalLot ToDalLot(OrmLot ormlot)
        {
            if (ormlot!=null) return new DalLot()
            {
                UserId = ormlot.OrmUserId,
                TimeBegin = ormlot.TimeBegin,
                StatysId = ormlot.OrmStatysId,
                StartPrice = ormlot.StartPrice,
                Name = ormlot.Name,
                Image = ormlot.Image,
                Id = ormlot.Id,
                EndPrice = ormlot.EndPrice,
                Description = ormlot.Description,
                DateBegin = ormlot.DateBegin,
                CathegoryId = ormlot.OrmCathegoryId,
                BuyerName = ormlot.BuyerName
            };
            return null;
        }

        //только для Create новые!!
        public static OrmLot ToOrmLot(this DalLot dallot)
        {
            if (dallot != null) return new OrmLot()
            {
                OrmUserId = dallot.UserId,
                TimeBegin = dallot.TimeBegin,
                OrmStatysId = dallot.StatysId,
                StartPrice = dallot.StartPrice,
                Name = dallot.Name,
                Image = dallot.Image,
                Id = dallot.Id,
                EndPrice = dallot.EndPrice,
                Description = dallot.Description,
                DateBegin = dallot.DateBegin,
                OrmCathegoryId = dallot.CathegoryId,
                BuyerName = dallot.BuyerName
            };
            return null;
        }

        //при update наверн!!
   /*     public static void ToOrmLot(DalLot dallot,OrmLot ormlot)
        {
                ormlot.OrmUserId = dallot.UserId;
                ormlot.TimeBegin = dallot.TimeBegin;
                ormlot.OrmStatysId = dallot.StatysId;
                ormlot.StartPrice = dallot.StartPrice;
                ormlot.Name = dallot.Name;
                ormlot.Image = dallot.Image;
                ormlot.Id = dallot.Id;
                ormlot.EndPrice = dallot.EndPrice;
                ormlot.Description = dallot.Description;
                ormlot.DateBegin = dallot.DateBegin;
                ormlot.OrmCathegoryId = dallot.CathegoryId;
                ormlot.BuyerName = dallot.BuyerName;
        }*/







        public static IEnumerable<DalRole> ToDalRole(this IEnumerable<OrmRole> ormroles)
        {
            foreach (var ormrole in ormroles)
            {
                yield return ormrole.ToDalRole();
            }
        }

        //для добавления нового объекта но не обновления!!
        public static IEnumerable<OrmRole> ToOrmRole(this IEnumerable<DalRole> dalroles)
        {
            foreach (var dalrole in dalroles)
            {
                yield return dalrole.ToOrmRole();
            }
        }

        public static DalRole ToDalRole(this OrmRole ormrole)
        {
          return new DalRole()
            {
                Id = ormrole.Id,
                Name = ormrole.Name,
                Description = ormrole.Description
            };
        }

        //для добавления нового объекта но не обновления!!
        public static OrmRole ToOrmRole(this DalRole dalrole)
        {
           return new OrmRole()
            {
                Id = dalrole.Id,
                Name = dalrole.Name,
                Description = dalrole.Description
            };
        }

        //для обновления
        public static void ToOrmRole(DalRole dalrole, OrmRole inThisOrmRole)
        {
            //эт лишнее!
           // inThisOrmRole.Id = dalrole.Id;
            inThisOrmRole.Name = dalrole.Name;
            inThisOrmRole.Description = dalrole.Description;

        }







        public static IEnumerable<DalProfile> ToDalProfile(IEnumerable<OrmProfile> ormprofiles)
        {
            foreach (var ormprofile in ormprofiles)
            {
                yield return ToDalProfile(ormprofile);
            }
        }

      /*  public static IEnumerable<OrmProfile> ToOrmProfile(IEnumerable<DalProfile> dalprofiles)
        {
            foreach (var dalprofile in dalprofiles)
            {
                yield return ToOrmProfile(dalprofile);
            }
        }*/

        public static DalProfile ToDalProfile(OrmProfile ormprofile)
        {
            if (ormprofile!=null) return new DalProfile()
            {
                Receiver = ormprofile.Receiver,
                CountryId = ormprofile.OrmCountryId,
                City = ormprofile.City,
                Address = ormprofile.Address,
                Phone = ormprofile.Phone
            };
            return null;
        }

        public static OrmProfile ToOrmProfile(DalProfile dalprofile)
        {
            if (dalprofile!=null) return new OrmProfile()
            {
                Id = dalprofile.Id,
                Receiver = dalprofile.Receiver,
                OrmCountryId = dalprofile.CountryId,
                City = dalprofile.City,
                Address = dalprofile.Address,
                Phone = dalprofile.Phone
            };
            return null;
        }

        public static void ToOrmProfile(DalProfile dalprofile,OrmProfile ormprofile)
        {
                ormprofile.Receiver = dalprofile.Receiver;
                ormprofile.OrmCountryId = dalprofile.CountryId;
                ormprofile.City = dalprofile.City;
                ormprofile.Address = dalprofile.Address;
                ormprofile.Phone = dalprofile.Phone;
        }





        public static IEnumerable<DalCountry> ToDalCountry(this IEnumerable<OrmCountry> ormcountries)
        {
            foreach (var ormcountry in ormcountries)
            {
                yield return ormcountry.ToDalCountry();
            }
        }

    /*    public static OrmCountry ToOrmCountry(this DalCountry dalcountry)
        {
            return new OrmCountry()
            {
                Id = dalcountry.Id,
                Name = dalcountry.Name
            };
        }*/

        public static DalCountry ToDalCountry(this OrmCountry ormcountry)
        {
            return new DalCountry()
            {
                Id = ormcountry.Id,
                Name = ormcountry.Name
            };
        }











        //не для обновления!!! 
        internal static OrmImage ToOrmImage(DalImage dalimage)
        {
            if (dalimage != null) return new OrmImage()
            {
                Id = dalimage.Id,
                Image = dalimage.Image,
                MimeType = dalimage.MimeType,
                OrmLotId = dalimage.LotId
            };
            return null;
        }


        internal static DalImage ToDalImage(OrmImage ormImage)
        {
            if (ormImage != null) return new DalImage()
            {
                Id = ormImage.Id,
                Image = ormImage.Image,
                MimeType = ormImage.MimeType,
                LotId= ormImage.OrmLotId 
            };
            return null;
        }
    }
}
