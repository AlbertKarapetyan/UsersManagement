using Domain.Interfaces;
using Domain.Queries;
using DTO;
using MediatR;

namespace Domain.Handlers.Queries
{
    public class CheckUserHandler : IRequestHandler<CheckUserQuery, string?>
    {
        private readonly IUserService _userService;

        public CheckUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string?> Handle(CheckUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.AuthenticateAsync(request.username, request.password);
        }
    }
}
