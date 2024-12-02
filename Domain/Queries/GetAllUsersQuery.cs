using DTO;
using MediatR;

namespace Domain.Queries
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDTO>>;
}
