using AutoMapper;
using UserManaging.CQRS.Commands.Create;
using UserManaging.Domain;
using UserManaging.CQRS.Queries;

namespace UserManaging.CQRS.AutoMapperConfig
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountCommand, Account>();
            CreateMap<CreateUserCommand, User>();

            CreateMap<Account, UserAccountDto>();
            CreateMap<User, QueryUserResponse>()
                .ForMember(dto => dto.Account, opt => opt.MapFrom(x => x.Account));

            CreateMap<User, AccountUserDto>();
            CreateMap<Account, QueryAccountResponse>()
                .ForMember(dto => dto.User, opt => opt.MapFrom(x => x.User));

            CreateMap<User, CreateUserResponse>();
            CreateMap<Account, CreateAccountResponse>();
        }
    }
}
