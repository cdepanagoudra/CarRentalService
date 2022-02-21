using CAR_RENTAL_SERVICE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public interface IUserService
    {
        bool Authenticate(User item);

        Role GetUserRole(int id);
        List<User> GetAll();
        User GetDetails(int id);
    }
    public class UserService : IUserService
    {
        CarRentalContext _context;
        public UserService(CarRentalContext context) => _context = context;
        public bool Authenticate(User item)
        {
            //throw new NotImplementedException();
            var obj = _context.Users.FirstOrDefault(c=>c.UserName.Equals(item.UserName) && c.Password.Equals(item.Password));
            if(obj != null)
            {
                item.Id = obj.Id;
                return true;
            }
            else   
                return false;
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetDetails(int id)
        {
            throw new NotImplementedException();
            
        }

        public Role GetUserRole(int id)
        {
            //throw new NotImplementedException();
            var roles = _context.Roles.FromSqlRaw(
                $"SELECT RoleId, RoleName FROM Roles WHERE RoleId IN" + 
                $"(SELECT RoleID FROM UserRoles WHERE UserId={id})"
            );
            if(roles.Count()==0)
                return null;
            else    
                return roles.First();
        }
    }
}