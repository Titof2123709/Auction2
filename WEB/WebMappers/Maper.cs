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

        internal static BllUser ToBllUser(LoginModel loginmodel)
        {
            if (loginmodel!=null) return new BllUser()
            {
                Password = loginmodel.Password,
                Name = loginmodel.Name,
            };
            return null;
        }





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



        internal static IEnumerable<UserModel> ToUserModel(IEnumerable<BllUser> bllusers)
        {
            foreach(var blluser in bllusers)
            {
                yield return ToUserModel(blluser);
            }
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






        internal static BllCathegory ToBllCathegory(CathegoryModel cathegorymodel)
        {
           if(cathegorymodel!=null) return new BllCathegory()
            { 
                Id = cathegorymodel.Id,
                Name = cathegorymodel.Name            
            };
           return null;
        }

        internal static CathegoryModel ToCathegoryModel(BllCathegory bllcathegory)
        {
          if(bllcathegory!=null) return new CathegoryModel()
            { 
                Id = bllcathegory.Id,
                Name = bllcathegory.Name
            };
          return null;
        }






        internal static IEnumerable<RoleModel> ToRoleModel(this IEnumerable<BllRole> bllroles)
        {
            foreach (var bllrole in bllroles)
            {
                yield return bllrole.ToRoleModel();
            }
        }
   /*     public static IEnumerable<BllRole> ToBllRole(IEnumerable<RoleModel> rolemodels)
        {
            foreach (var rolemodel in rolemodels)
            {
                yield return rolemodel.ToBllRole();
            }
        }*/
        internal static BllRole ToBllRole(this RoleModel rolemodel)
        {
             return new BllRole()
            {
                Id = rolemodel.Id,
                Name = rolemodel.Name,
                Description = rolemodel.Description
            };
        }
        internal static RoleModel ToRoleModel(this BllRole bllrole)
        {
            return new RoleModel()
            {
                Id = bllrole.Id,
                Name = bllrole.Name,
                Description = bllrole.Description
            };
        }





        internal static IEnumerable<LotModel> ToLotModel(IEnumerable<BllLot> blllots)
        {
            foreach (var blllot in blllots)
            {
                yield return ToLotModel(blllot);
            }
        }

    /*    public static IEnumerable<BllLot> ToBllLot(IEnumerable<LotModel> lotmodels)
        {
            foreach (var lotmodel in lotmodels)
            {
                yield return ToBllLot(lotmodels);
            }
        }*/

        internal static LotModel ToLotModel(BllLot blllot)
        {
          if(blllot!=null)  return new LotModel()
            {   
                Id = blllot.Id,
                TimeBegin = blllot.TimeBegin,
                Statys = (Statys)blllot.StatysId,
                StartPrice = blllot.StartPrice,
                Name = blllot.Name,
                Image = blllot.Image,
                EndPrice = blllot.EndPrice,
                Description = blllot.Description,
                DateBegin = blllot.DateBegin,
              //  CathegoryId = blllot.CathegoryId,
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
                Image = lotmodel.Image,
                EndPrice = lotmodel.EndPrice,
                Description = lotmodel.Description,
                DateBegin = lotmodel.DateBegin,
              //  CathegoryId = lotmodel.CathegoryId,
                BuyerName = lotmodel.BuyerName
            };
          return null;
        }












        internal static IEnumerable<ProfileModel> ToProfileModel(IEnumerable<BllProfile> bllprofiles)
        {
            foreach (var bllprofile in bllprofiles)
            {
                yield return ToProfileModel(bllprofile);
            }
        }

        internal static BllProfile ToBllProfile(this ProfileModel modelprofile)
        { 
            return new BllProfile()
            {
                Id  = modelprofile.Id,// Convert.ToInt32(((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault()),
                Receiver = modelprofile.Receiver,
                City = modelprofile.City,
                Address = modelprofile.Address,
                Phone = modelprofile.Phone
            };
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










        internal static IEnumerable<CountryModel> ToCountryModel(IEnumerable<BllCountry> bllcountries)
        {
            foreach (var bllcountry in bllcountries)
            {
                yield return ToCountryModel(bllcountry);
            }
        }

        /*  public static BllCountry ToBllCountry(this CountryModel modelcountry)
          {
             if(modelcountry!=null) return new BllCountry()
              { 
                  Id = modelcountry.Id,
                  Name = modelcountry.Name
              };
          return null;
          }*/

        internal static CountryModel ToCountryModel(BllCountry bllcountry)
        {
           if(bllcountry!=null) return new CountryModel()
            {
                Id = bllcountry.Id,
                Name = bllcountry.Name
            };
           return null;
        }











        internal static BllImage ToBllImage(ImageModel imagemodel)
        {
            if (imagemodel != null) return new BllImage()
            {
                Id = imagemodel.Id,
                Image = imagemodel.Image,
                MimeType = imagemodel.MimeType,
                LotId = imagemodel.LotId
            };
            return null;
        }

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
    }
}