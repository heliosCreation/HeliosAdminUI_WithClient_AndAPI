using API.Application.Contracts.Identity;
using API.Application.Enum;
using API.Application.Response;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.ApplicationUserProfiles.Query.Get
{
    public class GetApplicationUserProfileQueryHandler : IRequestHandler<GetApplicationUserProfileQuery, ApiResponse<ApplicationUserProfileVm>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicatonUserProfileRepository _applicatonUserProfileRepository;

        public GetApplicationUserProfileQueryHandler(
            IMapper mapper,
            IApplicatonUserProfileRepository applicatonUserProfileRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _applicatonUserProfileRepository = applicatonUserProfileRepository ?? throw new ArgumentNullException(nameof(applicatonUserProfileRepository));
        }

        public async Task<ApiResponse<ApplicationUserProfileVm>> Handle(GetApplicationUserProfileQuery request, CancellationToken cancellationToken)
        {
            var entity = await _applicatonUserProfileRepository.GetApplicationUserProfileBySubject(request.Sub);
            if (entity == null)
            {
                entity = new ApplicationUserProfile
                {
                    Subject = request.Sub,
                    SubscriptionLevel = SubscriptionLevel.FreeUser.ToString(),
                    Role = Roles.Basic.ToString()
                };

                await _applicatonUserProfileRepository.AddAsync(entity);
            }

            return new ApiResponse<ApplicationUserProfileVm>
            {
                Data = _mapper.Map<ApplicationUserProfileVm>(entity)
            };
        }
    }
}
