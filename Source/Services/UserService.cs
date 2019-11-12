using System.Collections.Generic;
using Codenation.Challenge.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext context;
        public UserService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return context.Accelerations.Where(a => a.Name == name).
                                         SelectMany(a => a.Candidates).
                                         Select(c => c.User).
                                         Distinct().ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            return context.Candidates.Where(c => c.CompanyId == companyId).
                                        Select(c => c.User).
                                        Distinct().ToList();
        }

        public User FindById(int id)
        {
            return context.Users.Find(id);
        }

        public User Save(User user)
        {
            var state = user.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(user).State = state;
            context.SaveChanges();
            return user;
        }
    }
}
