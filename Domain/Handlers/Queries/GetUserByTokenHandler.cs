using Domain.Interfaces;
using Domain.Queries;
using DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Queries
{
    public class GetUserByTokenHandler : IRequestHandler<GetUserByTokenQuery, UserDTO?>
    {
        private readonly IUserService _userService;

        public GetUserByTokenHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDTO?> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAsync(request.token);
        }
    }
}
