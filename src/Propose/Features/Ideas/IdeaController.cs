using Propose.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Propose.Features.Ideas.AddOrUpdateIdeaCommand;
using static Propose.Features.Ideas.GetIdeasQuery;
using static Propose.Features.Ideas.GetIdeaByIdQuery;
using static Propose.Features.Ideas.RemoveIdeaCommand;
using static Propose.Features.Ideas.VoteIdeaCommand;

namespace Propose.Features.Ideas
{
    [Authorize]
    [RoutePrefix("api/idea")]
    public class IdeaController : ApiController
    {
        public IdeaController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateIdeaResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateIdeaRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            request.TenantId = user.TenantId;
            request.UserId = user.Id;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateIdeaResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateIdeaRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetIdeasResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetIdeasRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetIdeaByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetIdeaByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveIdeaResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveIdeaRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("vote")]
        [HttpPost]
        [ResponseType(typeof(VoteIdeaResponse))]
        public async Task<IHttpActionResult> Vote([FromUri]VoteIdeaRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            request.TenantId = (user).TenantId;
            request.UserId = user.Id;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
