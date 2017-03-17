using Propose.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Propose.Features.Teams.AddOrUpdateTeamMemberCommand;
using static Propose.Features.Teams.GetTeamMembersQuery;
using static Propose.Features.Teams.GetTeamMemberByIdQuery;
using static Propose.Features.Teams.RemoveTeamMemberCommand;

namespace Propose.Features.Teams
{
    [Authorize]
    [RoutePrefix("api/teamMember")]
    public class TeamMemberController : ApiController
    {
        public TeamMemberController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTeamMemberResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTeamMemberRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTeamMemberResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTeamMemberRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTeamMembersResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTeamMembersRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTeamMemberByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTeamMemberByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTeamMemberResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTeamMemberRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
