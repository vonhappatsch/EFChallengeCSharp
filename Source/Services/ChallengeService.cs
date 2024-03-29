using System.Collections.Generic;
using Codenation.Challenge.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext context;
        public ChallengeService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return context.Users.Where(u => u.Id == userId).
                                SelectMany(u => u.Candidates).
                                Where(c => c.AccelerationId == accelerationId).
                                Select(c => c.Acceleration.Challenge).
                                Distinct().ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            var state = challenge.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(challenge).State = state;
            context.SaveChanges();
            return challenge;
        }
    }
}