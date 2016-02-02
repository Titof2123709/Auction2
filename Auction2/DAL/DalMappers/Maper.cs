using DAL.Interface.DalModel;
using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalMappers
{
    internal static class Maper
    {
        #region User
        internal static DalUser ToDalUser(OrmUser ormuser)
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

        internal static void ToOrmUser(DalUser daluser, OrmUser inThisOrmUser)
        {
            inThisOrmUser.Password = daluser.Password;
            inThisOrmUser.Name = daluser.Name;
            inThisOrmUser.Id = daluser.Id;
            inThisOrmUser.Email = daluser.Email;
            inThisOrmUser.TimeRegister = daluser.TimeRegister;
        }

        internal static OrmUser ToOrmUser(DalUser daluser)
        {
            if (daluser != null) return new OrmUser()
            {
                Password = daluser.Password,
                Name = daluser.Name,
                Id = daluser.Id,
                Email = daluser.Email,
                TimeRegister = daluser.TimeRegister
            };
            return null;
        }

        #endregion

        #region Lot
        internal static DalLot ToDalLot(OrmLot ormlot)
        {
            if (ormlot!=null) return new DalLot()
            {
                UserId = ormlot.OrmUserId,
                TimeBegin = ormlot.TimeBegin,
                StatysId = ormlot.OrmStatysId,
                StartPrice = ormlot.StartPrice,
                Name = ormlot.Name,
                Id = ormlot.Id,
                EndPrice = ormlot.EndPrice,
                Description = ormlot.Description,
                DateBegin = ormlot.DateBegin,
                CathegoryId = ormlot.OrmCathegoryId,
                BuyerName = ormlot.BuyerName
            };
            return null;
        }

        internal static OrmLot ToOrmLot(this DalLot dallot)
        {
            if (dallot != null) return new OrmLot()
            {
                OrmUserId = dallot.UserId,
                TimeBegin = dallot.TimeBegin,
                OrmStatysId = dallot.StatysId,
                StartPrice = dallot.StartPrice,
                Name = dallot.Name,
                Id = dallot.Id,
                EndPrice = dallot.EndPrice,
                Description = dallot.Description,
                DateBegin = dallot.DateBegin,
                OrmCathegoryId = dallot.CathegoryId,
                BuyerName = dallot.BuyerName
            };
            return null;
        }

        internal static void ToOrmLot(DalLot dallot, OrmLot ormlot)
        { 
                ormlot.OrmUserId = dallot.UserId;
                ormlot.TimeBegin = dallot.TimeBegin;
                ormlot.OrmStatysId = dallot.StatysId;
                ormlot.StartPrice = dallot.StartPrice;
                ormlot.Name = dallot.Name;
                ormlot.Id = dallot.Id;
                ormlot.EndPrice = dallot.EndPrice;
                ormlot.Description = dallot.Description;
                ormlot.DateBegin = dallot.DateBegin;
                ormlot.OrmCathegoryId = dallot.CathegoryId;
                ormlot.BuyerName = dallot.BuyerName;
        }
        #endregion

        #region Role
        internal static DalRole ToDalRole(OrmRole ormrole)
        {
         if(ormrole!=null) return new DalRole()
            {
                Id = ormrole.Id,
                Name = ormrole.Name,
                Description = ormrole.Description
            };
         return null;
        }


        internal static OrmRole ToOrmRole(DalRole dalrole)
        {
          if(dalrole!=null) return new OrmRole()
            {
                Id = dalrole.Id,
                Name = dalrole.Name,
                Description = dalrole.Description
            };
          return null;
        }

        //для обновления
        internal static void ToOrmRole(DalRole dalrole, OrmRole inThisOrmRole)
        {
            inThisOrmRole.Name = dalrole.Name;
            inThisOrmRole.Description = dalrole.Description;
        }
        #endregion

        #region Profile

        internal static DalProfile ToDalProfile(OrmProfile ormprofile)
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

        internal static OrmProfile ToOrmProfile(DalProfile dalprofile)
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

        internal static void ToOrmProfile(DalProfile dalprofile, OrmProfile ormprofile)
        {
                ormprofile.Receiver = dalprofile.Receiver;
                ormprofile.OrmCountryId = dalprofile.CountryId;
                ormprofile.City = dalprofile.City;
                ormprofile.Address = dalprofile.Address;
                ormprofile.Phone = dalprofile.Phone;
        }
        #endregion

        #region Image
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
        #endregion
    }
}
