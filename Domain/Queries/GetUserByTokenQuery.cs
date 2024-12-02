using DTO;
using MediatR;

namespace Domain.Queries
{
    public record GetUserByTokenQuery(string token) : IRequest<UserDTO?>;
}
