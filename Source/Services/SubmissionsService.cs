using System.Collections.Generic;
using Codenation.Challenge.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext context;

        public SubmissionService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return context.Candidates.Where(c => c.AccelerationId == accelerationId).
                                        Select(c => c.User).
                                        SelectMany(u => u.Submissions).
                                        Where(s => s.ChallengeId == challengeId).
                                        Distinct().ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return context.Submissions.Where(s => s.ChallengeId == challengeId).
                                        Max(s => s.Score);
        }

        public Submission Save(Submission submission)
        {
            var checking = context.Submissions.Find(submission.ChallengeId, submission.UserId);
            if (checking == null)
            {
                context.Submissions.Add(submission);
            }
            else
            {
                checking.Score = submission.Score;
            }
            context.SaveChanges();
            return submission;
        }
    }
}
