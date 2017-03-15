using Propose.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Propose.Features.Ideations.AddOrUpdateIdeationCommand;
using static Propose.Features.Ideations.GetIdeationsQuery;
using static Propose.Features.Ideations.GetIdeationByIdQuery;
using static Propose.Features.Ideations.RemoveIdeationCommand;

namespace Propose.Features.Ideations
{
    [Authorize]
    [RoutePrefix("api/ideation")]
    public class IdeationController : ApiController
    {
        public IdeationController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateIdeationResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateIdeationRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateIdeationResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateIdeationRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetIdeationsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetIdeationsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetIdeationByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetIdeationByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveIdeationResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveIdeationRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
