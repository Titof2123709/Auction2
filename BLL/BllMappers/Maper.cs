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
    //мы сюда приходим только при обрщаниии к элементу в контролере а не сразу!!
    //смысл yield
    //http://www.cyberforum.ru/csharp-beginners/thread451380.html
    public static class Maper
    {
        public static IEnumerable<BllUser> ToBllUser(IEnumerable<DalUser> dalusers)
        {
            foreach(var daluser in dalusers)
            {
                yield return ToBllUser(daluser);
            }
        }

        public static IEnumerable<DalUser> ToDalUser(IEnumerable<BllUser> bllusers)
        {
            foreach (var blluser in bllusers)
            {
                yield return ToDalUser(blluser);
            }
        }

        public static BllUser ToBllUser(DalUser daluser)
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

        public static DalUser ToDalUser(this BllUser blluser)
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







        public static IEnumerable<BllLot> ToBllLot(IEnumerable<DalLot> dallots)
        {
            foreach (var dallot in dallots)
            {
                yield return ToBllLot(dallot);
            }
        }
        public static IEnumerable<DalLot> ToDalLot(IEnumerable<BllLot> blllots)
        {
            foreach (var blllot in blllots)
            {
                yield return ToDalLot(blllot);
            }
        }
        public static BllLot ToBllLot(DalLot dallot)
        {
            if (dallot != null) return new BllLot()
            { 
                UserId = dallot.UserId,
                TimeBegin = dallot.TimeBegin,
                StatysId = dallot.StatysId,
                StartPrice = dallot.StartPrice,
                Name = dallot.Name,
                Image = dallot.Image,
                Id = dallot.Id,
                EndPrice = dallot.EndPrice,
                Description = dallot.Description,
                DateBegin = dallot.DateBegin,
                CathegoryId = dallot.CathegoryId,
                BuyerName = dallot.BuyerName
            };
            return null;
        }
        public static DalLot ToDalLot(BllLot blllot)
        {
            if (blllot != null) return new DalLot()
            { 
                    UserId = blllot.UserId,
                    TimeBegin = blllot.TimeBegin,
                    StatysId = blllot.StatysId,
                    StartPrice = blllot.StartPrice,
                    Name = blllot.Name,
                    Image = blllot.Image,
                    Id = blllot.Id,
                    EndPrice = blllot.EndPrice,
                    Description = blllot.Description,
                    DateBegin = blllot.DateBegin,
                    CathegoryId = blllot.CathegoryId,
                    BuyerName = blllot.BuyerName
                };
            return null;
        }





        public static IEnumerable<BllRole> ToBllRole(this IEnumerable<DalRole> dalroles)
        {
            foreach (var dalrole in dalroles)
            {
                yield return dalrole.ToBllRole();
            }
        }
        public static IEnumerable<DalRole> ToDalRole(this IEnumerable<BllRole> bllroles)
        {
            foreach (var bllrole in bllroles)
            {
                yield return bllrole.ToDalRole();
            }
        }
        public static BllRole ToBllRole(this DalRole dalrole)
        {
           return new BllRole()
            {
                Id = dalrole.Id,
                Name = dalrole.Name,
                Description = dalrole.Description
            };
        }
        public static DalRole ToDalRole(this BllRole bllrole)
        {
             return new DalRole()
            {
                Id = bllrole.Id,
                Name = bllrole.Name,
                Description = bllrole.Description
            };
        }








        public static IEnumerable<BllProfile> ToBllProfile(IEnumerable<DalProfile> dalprofiles)
        {
            foreach (var dalprofile in dalprofiles)
            {
                yield return ToBllProfile(dalprofile);
            }
        }

        public static IEnumerable<DalProfile> ToDalProfile(IEnumerable<BllProfile> bllprofiles)
        {
            foreach (var bllprofile in bllprofiles)
            {
                yield return ToDalProfile(bllprofile);
            }
        }

        public static BllProfile ToBllProfile(DalProfile dalprofile)
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

        public static DalProfile ToDalProfile(BllProfile bllprofile)
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






        public static BllCathegory ToBllCathegory(DalCathegory dalcathegory)
        {
            if (dalcathegory != null) return new BllCathegory()
            {
                Id = dalcathegory.Id,
                Name = dalcathegory.Name
            };
            return null;
        }
        public static DalCathegory ToDalCathegory(BllCathegory bllcathegory)
        {
            if (bllcathegory != null) return new DalCathegory()
            {
                Id = bllcathegory.Id,
                Name = bllcathegory.Name
            };
            return null;
        }






        public static IEnumerable<BllCountry> ToBllCountry(IEnumerable<DalCountry> dalcountries)
        {
            foreach (var dalcountry in dalcountries)
            {
                yield return ToBllCountry(dalcountry);
            }
        }

        public static BllCountry ToBllCountry(DalCountry dalcountry)
        {
            if (dalcountry!=null) return new BllCountry()
            {
                Id = dalcountry.Id,
                Name = dalcountry.Name
            };
            return null;
        }

     /*   public static DalCountry ToDalCountry(this BllCountry dalcountry)
        {
            return new DalCountry()
            {
                Id = dalcountry.Id,
                Name = dalcountry.Name
            };
        }*/








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
    }
}
