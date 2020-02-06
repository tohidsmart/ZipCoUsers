using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManaging.Domain;
using UserManaging.Domain.Repository;

namespace UserManaging.CQRS.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRequest = mapper.Map<CreateUserCommand, User>(request);

            var userEntity = userRepository.AddUser(userRequest);

            await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<User, CreateUserResponse>(userEntity);
            return response;

        }
    }
}
