using DTO;
using MediatR;

namespace Domain.Commands
{
    public record UpdateUsersCommand(IEnumerable<UserUpdateStateDTO> modifiedUsers) : IRequest<IEnumerable<UserDTO>>;

}
