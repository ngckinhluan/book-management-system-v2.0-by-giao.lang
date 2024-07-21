using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserAccountRepository()
    {
        // thằng này chứa các hàm crud trực tiếp table 
        // dĩ nhiên repo thì cần DBContext
        private readonly BookManagementDbContext _context = new(); // ko new ở 

        public List<UserAccount> GetAccounts()
        {
            return _context.UserAccounts.ToList();
        }

        public UserAccount GetOne(string email, string pass)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Email == email && u.Password == pass && u.Role != 3);
        }

        public UserAccount GetUserById(int id)
        {
            return _context.UserAccounts.Find(id);
        }

        public void AddUserAccount(UserAccount account)
        {
            _context.UserAccounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateUserAccount(UserAccount account)
        {
            _context.UserAccounts.Update(account);
            _context.SaveChanges();
        }

        public void DeleteUserAccount(UserAccount account)
        {
            _context.UserAccounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
