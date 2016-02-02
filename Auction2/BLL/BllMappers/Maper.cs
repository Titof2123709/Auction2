using BLL.Interface;
using BLL.Interface.BLLModel;
using DAL.Interface;
using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllMappers
{
    internal static class Maper
    {
        #region User

        internal static BllUser ToBllUser(DalUser daluser)
        {
            if (daluser!=null) return new BllUser()
                {
                    Password = daluser.Password,
                    Name = daluser.Name,
                    Id = daluser.Id,
                    Email = daluser.Email,
                    TimeRegister = daluser.TimeRegister
                };
            return null;
        }

        internal static DalUser ToDalUser(BllUser blluser)
        {
            if (blluser != null) return new DalUser()
            {
                    Password = blluser.Password,
                    Name = blluser.Name,
                    Id = blluser.Id,
                    Email = blluser.Email,
                    TimeRegister = blluser.TimeRegister
                };
            return null;
        }

        #endregion

        #region Lot
        internal static BllLot ToBllLot(DalLot dallot)
        {
            if (dallot != null) return new BllLot()
            { 
                UserId = dallot.UserId,
                TimeBegin = dallot.TimeBegin,
                StatysId = dallot.StatysId,
                StartPrice = dallot.StartPrice,
                Name = dallot.Name,
                Id = dallot.Id,
                EndPrice = dallot.EndPrice,
                Description = dallot.Description,
                DateBegin = dallot.DateBegin,
                CathegoryId = dallot.CathegoryId,
                BuyerName = dallot.BuyerName
            };
            return null;
        }
        internal static DalLot ToDalLot(BllLot blllot)
        {
            if (blllot != null) return new DalLot()
            { 
                    UserId = blllot.UserId,
                    TimeBegin = blllot.TimeBegin,
                    StatysId = blllot.StatysId,
                    StartPrice = blllot.StartPrice,
                    Name = blllot.Name,
                    Id = blllot.Id,
                    EndPrice = blllot.EndPrice,
                    Description = blllot.Description,
                    DateBegin = blllot.DateBegin,
                    CathegoryId = blllot.CathegoryId,
                    BuyerName = blllot.BuyerName
                };
            return null;
        }

        #endregion

        #region Role

        internal static BllRole ToBllRole(DalRole dalrole)
        {
          if(dalrole!=null) return new BllRole()
            {
                Id = dalrole.Id,
                Name = dalrole.Name,
                Description = dalrole.Description
            };
          return null;
        }
        internal static DalRole ToDalRole(BllRole bllrole)
        {
          if(bllrole!=null) return new DalRole()
            {
                Id = bllrole.Id,
                Name = bllrole.Name,
                Description = bllrole.Description
            };
          return null;
        }

        #endregion

        #region Profile

        internal static BllProfile ToBllProfile(DalProfile dalprofile)
        {
            if (dalprofile!=null) return new BllProfile()
            { 
                Receiver = dalprofile.Receiver,
                CountryId = dalprofile.CountryId,
                City = dalprofile.City,
                Address = dalprofile.Address,
                Phone = dalprofile.Phone
            };
            return null;
        }

        internal static DalProfile ToDalProfile(BllProfile bllprofile)
        {
            if (bllprofile!=null) return new DalProfile()
            {
                Id = bllprofile.Id,
                Receiver = bllprofile.Receiver,
                CountryId = bllprofile.CountryId,
                City = bllprofile.City,
                Address = bllprofile.Address,
                Phone = bllprofile.Phone
            };
            return null;
        }

        #endregion

        #region Image

        internal static DalImage ToDalImage(BllImage bllimage)
        {
            if (bllimage != null) return new DalImage()
            {
                Id = bllimage.Id,
                Image = bllimage.Image,
                MimeType = bllimage.MimeType,
                LotId = bllimage.LotId
            };
            return null;
        }
        internal static BllImage ToBllImage(DalImage dalImage)
        {
            if (dalImage != null) return new BllImage()
            {
                Id = dalImage.Id,
                Image = dalImage.Image,
                MimeType = dalImage.MimeType,
                LotId = dalImage.LotId
            };
            return null;
        }

        #endregion
    }
}
