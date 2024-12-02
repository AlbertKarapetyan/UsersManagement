using Domain.Commands;
using Domain.Interfaces;
using DTO;
using MediatR;

namespace Domain.Handlers.Commands
{
    public class UpdateUsersHandler : IRequestHandler<UpdateUsersCommand, IEnumerable<UserDTO>>
    {
        private readonly IUserService _userService;

        public UpdateUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDTO>> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateStateAsync(request.modifiedUsers);
        }
    }
}
