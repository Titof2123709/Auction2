using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Classes
{
    public interface IClaimsAutorization
    {
        void Autorization(LoginModel model, int id);
    }
}
