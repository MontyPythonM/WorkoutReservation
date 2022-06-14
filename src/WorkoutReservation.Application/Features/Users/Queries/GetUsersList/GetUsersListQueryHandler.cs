using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, 
                                                            List<UsersListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserRepository userRepository,
                                        IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UsersListDto>> Handle(GetUsersListQuery request, 
                                                     CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            if (!users.Any())
                throw new NotFoundException($"Users not found.");

            return _mapper.Map<List<UsersListDto>>(users);
        }
    }
}
