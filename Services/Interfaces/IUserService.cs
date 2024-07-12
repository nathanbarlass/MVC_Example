using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        ApplicationUser? GetUserById(string userId);
        IEnumerable<ApplicationUser> GetAllUsers();
        void CreateUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(string id);
    }
}
