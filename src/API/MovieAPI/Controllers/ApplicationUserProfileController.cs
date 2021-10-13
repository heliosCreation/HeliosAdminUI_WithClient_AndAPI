//using API.Domain.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MovieAPI.Controllers
//{
//    public class ApplicationUserProfileController : ApiController
//    {
//        private readonly IApplicationUserProfileService _applicationUserProfileService;
//        private readonly IMapper _mapper;

//        public ApplicationUserProfilesController(
//            IApplicationUserProfileService applicationUserProfileService,
//            IMapper mapper)
//        {
//            _applicationUserProfileService = applicationUserProfileService ??
//                throw new ArgumentNullException(nameof(applicationUserProfileService));
//            _mapper = mapper ??
//                throw new ArgumentNullException(nameof(mapper));
//        }

//        //[Authorize(Policy = "SubjectMustMatchUser")]
//        [HttpGet("{subject}")]
//        public IActionResult GetApplicationUserProfile(string subject)
//        {
//            var applicationUserProfileFromRepo = _applicationUserProfileService.GetApplicationUserProfile(subject);

//            if (applicationUserProfileFromRepo == null)
//            {
//                applicationUserProfileFromRepo = new ApplicationUserProfile
//                {
//                    Subject = subject,
//                    SubscriptionLevel = "freeUser",
//                    Role = "user"
//                };
//                _applicationUserProfileService.AddApplicationUserProfile(applicationUserProfileFromRepo);
//            }

//            return Ok(_mapper.Map<ApplicationUserProfileModel>(applicationUserProfileFromRepo));
//        }

//        [HttpGet]
//        public IActionResult GetAllApplicationUserProfiles()
//        {
//            var applicationUserProfileFromRepo = _applicationUserProfileService.GetAllApplicationUserProfile();
//            return Ok(_mapper.Map<List<ApplicationUserProfileModel>>(applicationUserProfileFromRepo));
//        }
//    }
//}
