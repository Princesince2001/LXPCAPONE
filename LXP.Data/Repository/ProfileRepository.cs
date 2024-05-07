using LXP.Data.DBContexts;
using LXP.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LXP.Common.Entities;

namespace LXP.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly LXPDbContext _LXPDbContext;
        public ProfileRepository(LXPDbContext context)
        {
            _LXPDbContext = context;
        }
        public void AddProfile(LearnerProfile learnerprofile)
        {
            
            
            _LXPDbContext.LearnerProfiles.Add(learnerprofile);
            _LXPDbContext.SaveChanges();
        }

        public async Task<List<LearnerProfile>> GetAllLearnerProfile()
        {
            return _LXPDbContext.LearnerProfiles.ToList();
        }


    }
}