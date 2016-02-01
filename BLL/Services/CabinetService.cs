using AutoMapper;
using BLL.Interface;
using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BllMappers;
using BLL.Interface.BLLModel;

namespace BLL.Services
{
    public class CabinetService:ICabinetService
    {
        private readonly IUnitOfWork uow;
        private readonly ILotRepository lotRepository;
        private readonly IUserRepository userRepository;
        private readonly IProfileRepository profileRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IImageRepository imageRepository;

        public CabinetService(IUnitOfWork uow, ILotRepository lotRepository, IUserRepository userRepository, IProfileRepository profileRepository, ICountryRepository countryRepository, IImageRepository imageRepository)
        {
            this.uow = uow;
            this.lotRepository = lotRepository;
            this.userRepository = userRepository;
            this.profileRepository = profileRepository;
            this.profileRepository = profileRepository;
            this.countryRepository = countryRepository;
            this.imageRepository = imageRepository;
        }

        public IEnumerable<string> GetAllLotNameForUserId(int key)
        {
            return lotRepository.GetAllLotNameByUserId(key);
        }

        public BllLot GetLotByName(string name)
        {
            return Maper.ToBllLot(lotRepository.GetLotByName(name));
        }

        public void CreateLot(BllLot blllot)
        {
            lotRepository.Create(Maper.ToDalLot(blllot));
            //не надо!сначало image!
            uow.Commit();
        }

        public void UpdateLot(BllLot blllot)
        {
            lotRepository.Update(Maper.ToDalLot(blllot));
            //не надо!сначала image!
            uow.Commit();
        }

        public void DeleteLot(string name)
        {
            lotRepository.Delete(name);
            uow.Commit();
        }

        public BllUser GetUserById(int id)
        {
            return Maper.ToBllUser(userRepository.GetUserById(id));
        }

        public bool UserExist(string name)
        {
            return userRepository.UserExist(name);
        }

        public void UpdateUser(BllUser blluser)
        {
            userRepository.Update(blluser.ToDalUser());
            uow.Commit();
        }


        public int FindIdByName(string name)
        {
            return userRepository.GetIdByName(name);
        }


        public BllProfile FindUserProfile(string name)
        {
           return Maper.ToBllProfile(profileRepository.GetUserProfile(name));
        }

        public void CreateProfile(BllProfile bllprofile)
        {
            profileRepository.Create(Maper.ToDalProfile(bllprofile));
            uow.Commit();
        }

        public void UpdateProfile(BllProfile bllprofile)
        {
            profileRepository.Update(Maper.ToDalProfile(bllprofile));
            uow.Commit();
        }

        public bool UserHasProfile(string name)
        {
          return profileRepository.UserHasProfile(name);
        }

        public IEnumerable<string> GetAllCountries()
        {
            return countryRepository.GetAllCountries();
        }


        public string GetCountryNameById(int id)
        {
          return  countryRepository.GetCountryNameById(id);
        }


        public int GetIdByCountryName(string name)
        {
            return countryRepository.GetIdByCountryName(name);
        }


        public int GetUserIdByName(string name)
        {
            return userRepository.GetIdByName(name);
        }

        public void CreateImage(BllImage bllimage)
        {
           imageRepository.Create(Maper.ToDalImage(bllimage));
            //это ещё и за лот!
           uow.Commit();
        }


        public BllImage GetImageById(int Id)
        {
            return Maper.ToBllImage(imageRepository.GetById(Id));
        }


        public IEnumerable<BllImage> GetImagesByLotId(int id)
        {
            return imageRepository.GetImagesByLotId(id).Select(image => Maper.ToBllImage(image));
          //  return  Maper.ToBllImage(imageRepository.GetImagesByLotId(id));
        }
    }
}
