using System.Collections.Generic;
using Codenation.Challenge.Models;
using System.Linq;
using System;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext context;
        public CandidateService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return context.Candidates.Where(c => c.AccelerationId == accelerationId).
                                      ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return context.Candidates.Where(x => x.CompanyId == companyId).
                                      ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return context.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            var checking = context.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);
            if (checking == null)
            {
                context.Candidates.Add(candidate);
            }
            else
            {
                checking.Status = candidate.Status;
            }
            context.SaveChanges();
            return candidate;
        }
    }
}
