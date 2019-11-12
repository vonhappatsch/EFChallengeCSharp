using System.Collections.Generic;
using Codenation.Challenge.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext context;
        public CompanyService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return context.Accelerations.Where(a => a.Id == accelerationId).
                                        SelectMany(a => a.Candidates).
                                        Select(c => c.Company).
                                        Distinct().ToList();
        }

        public Company FindById(int id)
        {
            return context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return context.Candidates.Where(c => c.UserId == userId).
                                        Select(c => c.Company).
                                        Distinct().ToList();
        }

        public Company Save(Company company)
        {
            var state = company.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(company).State = state;
            context.SaveChanges();
            return company;
        }
    }
}