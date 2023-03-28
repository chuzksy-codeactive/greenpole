using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Mapper
{
    public class UserMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, CreateUserResponseDto>();
            config.NewConfig<CreateUserDto, User>()
                .Map(dest => dest.Email, src => src.Email.ToLower().Trim());
            config.NewConfig<User, UserLoginResponseDto>();
            config.NewConfig<User, UserByIdResponseDto>();

        }
    }
}
