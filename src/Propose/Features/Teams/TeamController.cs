using Propose.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Propose.Features.Teams.AddOrUpdateTeamCommand;
using static Propose.Features.Teams.GetTeamsQuery;
using static Propose.Features.Teams.GetTeamByIdQuery;
using static Propose.Features.Teams.RemoveTeamCommand;

namespace Propose.Features.Teams
{
    [Authorize]
    [RoutePrefix("api/team")]
    public class TeamController : ApiController
    {
        public TeamController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTeamResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTeamRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTeamResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTeamRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTeamsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTeamsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTeamByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTeamByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTeamResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTeamRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
