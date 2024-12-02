using DTO;
using MediatR;

namespace Domain.Queries
{
    public record CheckUserQuery(string username, string password) : IRequest<string?>;
}
