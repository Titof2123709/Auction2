using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ICabinetService
    {
        IEnumerable<string> GetAllLotNameForUserId(int key);
        BllLot GetLotByName(string name);
        void CreateLot(BllLot e);
        void DeleteLot(string name);
        void UpdateLot(BllLot blllot);
        BllUser GetUserById(int id);
        void UpdateUser(BllUser blluser);
        int FindIdByName(string name);
        BllProfile FindUserProfile(string name);
        void UpdateProfile(BllProfile bllprofile);
        void CreateProfile(BllProfile bllprofile);
        bool UserHasProfile(string name);
        bool UserExist(string name);
        IEnumerable<string> GetAllCountries();
        string GetCountryNameById(int id);
        int GetIdByCountryName(string name);
        int GetUserIdByName(string name);
        void CreateImage(BllImage bllimage);
        BllImage GetImageById(int Id);
        IEnumerable<BllImage> GetImagesByLotId(int id);
        void DeleteImageById(int Id);
        BllLot GetLotById(int Id);
        string GetCathegoryNameById(int Id);
        IEnumerable<string> GetAllCathegoryNames();
        int GetCathegoryIdByName(string cathegory);
        int GetLotIdByName(string name);
    }
}
