//using LXP.Data.Repository;
//using System;
//using LXP.Common;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using LXP.Data.IRepository;
//using System.Threading.Tasks;
//using LXP.Data.Repository;
//using LXP.Common.ViewModels;
//using LXP.Data;
//using LXP.Core.IServices;
//using LXP.Common.Entities;

//namespace LXP.Core.Services
//{
//    public class LearnerService : ILearnerService
//    {
//        private readonly ILearnerRepository _learnerRepository;
//        private readonly IProfileRepository _profileRepository;

//        public LearnerService(ILearnerRepository learnerRepository,IProfileRepository profileRepository) { 

//        this._learnerRepository = learnerRepository;
//            this._profileRepository = profileRepository;

//        }

//        public async Task<bool> LearnerRegistration(RegisterUserViewModel registerUserViewModel) {

//            bool isLearnerExists = await _learnerRepository.AnyLearnerByEmail(registerUserViewModel.Email);
//            if (isLearnerExists)
//            {
//                return false;
//            }
//            else
//            {
//                Learner newlearner = new Learner()
//                {
//                    LearnerId = Guid.NewGuid(),
//                    Email = registerUserViewModel.Email,
//                    Password = registerUserViewModel.Password,
//                    Role = registerUserViewModel.Role,
//                    UnblockRequest = false,
//                    AccountStatus = true,
//                    UserLastLogin = DateTime.Now,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = registerUserViewModel.FirstName + " " + registerUserViewModel.LastName,
//                    ModifiedAt= DateTime.Now,
//                    ModifiedBy = registerUserViewModel.FirstName + " " + registerUserViewModel.LastName
//                };
//                _learnerRepository.AddLearner(newlearner);
//                Learner learner=_learnerRepository.GetLearnerByLearnerEmail(newlearner.Email);
//                LearnerProfile profile = new LearnerProfile()
//                {
//                    ProfileId = Guid.NewGuid(),
//                    FirstName = registerUserViewModel.FirstName,
//                    LastName = registerUserViewModel.LastName,
//                    Dob = registerUserViewModel.Dob,
//                    Gender = registerUserViewModel.Gender,
//                    ContactNumber = registerUserViewModel.ContactNumber,
//                    Stream = registerUserViewModel.Stream,
//                    ProfilePhoto = registerUserViewModel.ProfilePhoto,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = registerUserViewModel.FirstName + " " + registerUserViewModel.LastName,
//                    LearnerId= learner.LearnerId
//                };
//                _profileRepository.AddProfile(profile);
//                return true;

//            }

//         }

//        }

//    }


using LXP.Data.Repository;
using System;
using LXP.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LXP.Data.IRepository;
using System.Threading.Tasks;
using LXP.Data.Repository;
using LXP.Data.IRepository;
using LXP.Common.ViewModels;
using LXP.Data;
using LXP.Core.IServices;
using LXP.Common.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace LXP.Core.Services
{
    public class LearnerService : ILearnerService
    {
        private readonly ILearnerRepository _learnerRepository;
        private readonly IProfileRepository _profileRepository;
        private Mapper _learnerMapper;

        public LearnerService(ILearnerRepository learnerRepository, IProfileRepository profileRepository)
        {
            this._learnerRepository = learnerRepository;
            this._profileRepository = profileRepository;
            var _configCategory = new MapperConfiguration(cfg => cfg.CreateMap<Learner, GetLearnerViewModel>().ReverseMap());
            _learnerMapper = new Mapper(_configCategory);

        }

        public async Task<bool> LearnerRegistration(RegisterUserViewModel registerUserViewModel)
        {
            bool isLearnerExists = await _learnerRepository.AnyLearnerByEmail(registerUserViewModel.Email);
            if (isLearnerExists)
            {
                return false;
            }
            else
            {
                Learner newlearner = new Learner()
                {
                    LearnerId = Guid.NewGuid(),
                    Email = registerUserViewModel.Email,
                    Password = registerUserViewModel.Password,
                    Role = registerUserViewModel.Role,
                    UnblockRequest = false,
                    AccountStatus = true,
                    UserLastLogin = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    CreatedBy = $"{registerUserViewModel.FirstName} {registerUserViewModel.LastName}",
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = $"{registerUserViewModel.FirstName} {registerUserViewModel.LastName}"
                };
                _learnerRepository.AddLearner(newlearner);
                Learner learner = _learnerRepository.GetLearnerByLearnerEmail(newlearner.Email);
                LearnerProfile profile = new LearnerProfile()
                {
                    ProfileId = Guid.NewGuid(),
                    FirstName = registerUserViewModel.FirstName,
                    LastName = registerUserViewModel.LastName,
                    Dob = DateOnly.ParseExact(registerUserViewModel.Dob, "yyyy-MM-dd", null), // Correctly parse the date
                    Gender = registerUserViewModel.Gender,
                    ContactNumber = registerUserViewModel.ContactNumber,
                    Stream = registerUserViewModel.Stream,
                    ProfilePhoto = registerUserViewModel.ProfilePhoto,
                    CreatedAt = DateTime.Now,
                    CreatedBy = $"{registerUserViewModel.FirstName} {registerUserViewModel.LastName}",
                    LearnerId = learner.LearnerId
                };
                _profileRepository.AddProfile(profile);
                return true;
            }
        }

        public async Task<List<GetLearnerViewModel>> GetAllLearner()
        {
            List<GetLearnerViewModel> learner = _learnerMapper.Map<List<Learner>, List<GetLearnerViewModel>>(await _learnerRepository.GetAllLearner());
            return learner;
        }
        

    }
}
