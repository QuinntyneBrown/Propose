using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Threading.Tasks;

namespace Propose.Features.Users
{
    public class ConfirmRegistrationCommand
    {
        public class ConfirmRegistrationRequest : IRequest<ConfirmRegistrationResponse>
        {
            public string Token { get; set; }
        }

        public class ConfirmRegistrationResponse
        {
            public ConfirmRegistrationResponse()
            {

            }
        }

        public class ConfirmRegistrationHandler : IAsyncRequestHandler<ConfirmRegistrationRequest, ConfirmRegistrationResponse>
        {
            public ConfirmRegistrationHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<ConfirmRegistrationResponse> Handle(ConfirmRegistrationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}
