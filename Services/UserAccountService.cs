using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepository _repository = new();

        public List<UserAccount> GetAccounts => _repository.GetAccounts().ToList();

        public void AddAccount(UserAccount account)
        {
            _repository.AddUserAccount(account);
        }

        public UserAccount GetOne(string email, string pass)
        {
           return _repository.GetOne(email, pass);
        }

        public UserAccount GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }
    }
}
