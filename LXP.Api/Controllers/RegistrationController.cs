using LXP.Common.ViewModels;
using LXP.Core.IServices;
using LXP.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LXP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly ILearnerService _learnerServices;
        private readonly IProfileService _profileService;
        public RegistrationController(ILearnerService learnerServices, IProfileService profileService)
        {
            _learnerServices=learnerServices;
            _profileService=profileService;
        }
        [HttpPost("/lxp/learner/registration")]
        public  async Task<IActionResult> Registration(RegisterUserViewModel learner)
        {
            bool learnerservices= await _learnerServices.LearnerRegistration(learner);
            if (learnerservices)
            {
                return Ok(CreateSuccessResponse(null));
            }
            else
            {
                return  Ok(CreateFailureResponse("Learner Already exists",200));
            }
        }

        [HttpGet("/lxp/view/learner")]
        public async Task<IActionResult> GetAllCategory()
        {
            List<GetLearnerViewModel> categories = await _learnerServices.GetAllLearner();
            return Ok(CreateSuccessResponse(categories));
        }


        [HttpGet("/lxp/view/learnerProfile")]
        public async Task<IActionResult> GetAllLearnerProfile()
        {
            List<GetProfileViewModel> LearnerProfileone = await _profileService.GetAllLearnerProfile();
            return Ok(CreateSuccessResponse(LearnerProfileone));
        }


    }
}
