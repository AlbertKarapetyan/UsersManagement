using API.Models;
using AutoMapper;
using DTO;
using Infrastructure.DataEntities;

namespace UsersManagement.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IUser, UserDTO>();
            CreateMap<UserUpdateStateModel, UserUpdateStateDTO>();
        }
    }
}
