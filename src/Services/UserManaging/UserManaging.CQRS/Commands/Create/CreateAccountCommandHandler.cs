using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManaging.Domain.Repository;
using AutoMapper;
using UserManaging.Domain;
using UserManaging.CQRS.Queries;
using UserManaging.CQRS.Exceptions;

namespace UserManaging.CQRS.Commands.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUserAccountQueries userAccountQueries;
        public CreateAccountCommandHandler(IUserRepository userRepository, 
                                            IMapper mapper,
                                            IUserAccountQueries userAccountQueries)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userAccountQueries = userAccountQueries;
        }
        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var existingUser=await userAccountQueries.GetUserAsync(request.UserId, cancellationToken);
            if (existingUser == null)
                throw new NotFoundException(nameof(existingUser), request.UserId.ToString());
            
            var accountRequest = mapper.Map<CreateAccountCommand, Account>(request);

            var accountEntity = userRepository.AddAccount(accountRequest);

            await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<Account, CreateAccountResponse>(accountEntity);
            return response;
        }
    }
}
