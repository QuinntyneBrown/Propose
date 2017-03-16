using Propose.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Propose.Features.Ideas.AddOrUpdateIdeaCommentCommand;
using static Propose.Features.Ideas.GetIdeaCommentsQuery;
using static Propose.Features.Ideas.GetIdeaCommentByIdQuery;
using static Propose.Features.Ideas.RemoveIdeaCommentCommand;

namespace Propose.Features.Ideas
{
    [Authorize]
    [RoutePrefix("api/ideaComment")]
    public class IdeaCommentController : ApiController
    {
        public IdeaCommentController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateIdeaCommentResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateIdeaCommentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateIdeaCommentResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateIdeaCommentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetIdeaCommentsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetIdeaCommentsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetIdeaCommentByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetIdeaCommentByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveIdeaCommentResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveIdeaCommentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
