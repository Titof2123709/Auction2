using BLL.Interface;
using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using WEB.Models;

namespace WEB.WebMappers
{
    internal static class Maper
    {
        #region User

        internal static BllUser ToBllUser(RegisterModel registermodel)
        {
            if (registermodel!=null) return new BllUser()
            {
                Email = registermodel.Email,
                Password = Crypto.HashPassword(registermodel.Password),
                Name = registermodel.Name,
            };
            return null;
        }
        internal static UserModel ToUserModel(BllUser blluser)
        {
            if(blluser!=null) return new UserModel()
            { 
                Id  = blluser.Id,
                Email = blluser.Email,
                Password = blluser.Password,
                Name = blluser.Name,
                TimeRegister = blluser.TimeRegister
            };
            return null;
        }
        internal static BllUser ToBllUserFromUserModel(UserModel usermodel)
        {
          if(usermodel!=null)  return new BllUser()
            {
                Id = usermodel.Id,
                Email = usermodel.Email,
                Password = usermodel.Password,
                Name = usermodel.Name,
                TimeRegister = usermodel.TimeRegister
            };
          return null;
        }
    
        #endregion

        #region Role
        internal static BllRole ToBllRole(RoleModel rolemodel)
        {
            if (rolemodel!=null) return new BllRole()
            {
                Id = rolemodel.Id,
                Name = rolemodel.Name,
                Description = rolemodel.Description
            };
            return null;
        }
        internal static RoleModel ToRoleModel(BllRole bllrole)
        {
           if(bllrole!=null) return new RoleModel()
            {
                Id = bllrole.Id,
                Name = bllrole.Name,
                Description = bllrole.Description
            };
           return null;
        }

        #endregion

        #region Lot

        internal static LotModel ToLotModel(BllLot blllot)
        {
          if(blllot!=null)  return new LotModel()
            {   
                Id = blllot.Id,
                TimeBegin = blllot.TimeBegin,
                Statys = (Statys)blllot.StatysId,
                StartPrice = blllot.StartPrice,
                Name = blllot.Name,
                EndPrice = blllot.EndPrice,
                Description = blllot.Description,
                DateBegin = blllot.DateBegin,
                BuyerName = blllot.BuyerName
            };
          return null;
        }
        internal static BllLot ToBllLot(LotModel lotmodel)
        {
          if(lotmodel!=null)  return new BllLot()
            {
                Id = lotmodel.Id,
                UserId = Convert.ToInt32(((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault()),
                TimeBegin = lotmodel.TimeBegin,
                StatysId = (int)lotmodel.Statys,
                StartPrice = lotmodel.StartPrice,
                Name = lotmodel.Name,
                EndPrice = lotmodel.EndPrice,
                Description = lotmodel.Description,
                DateBegin = lotmodel.DateBegin,
                BuyerName = lotmodel.BuyerName
            };
          return null;
        }

        #endregion

        #region Profile

        internal static BllProfile ToBllProfile(ProfileModel modelprofile)
        { 
           if(modelprofile!=null) return new BllProfile()
            {
                Id  = modelprofile.Id,
                Receiver = modelprofile.Receiver,
                City = modelprofile.City,
                Address = modelprofile.Address,
                Phone = modelprofile.Phone
            };
           return null;
        }

        internal static ProfileModel ToProfileModel(BllProfile bllprofile)
        {
            if(bllprofile!=null) return new ProfileModel()
            { 
                Id = bllprofile.Id,
                Receiver = bllprofile.Receiver,
                City = bllprofile.City,
                Address = bllprofile.Address,
                Phone = bllprofile.Phone
            };
         return null;
        }

        #endregion

        #region Image

        internal static ImageModel ToImageModel(BllImage bllImage)
        {
            if (bllImage != null) return new ImageModel()
            {
                Id = bllImage.Id,
                Image = bllImage.Image,
                MimeType = bllImage.MimeType,
                LotId = bllImage.LotId
            };
            return null;
        }

        #endregion
    }
}