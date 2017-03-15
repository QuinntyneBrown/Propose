using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace Propose.Features.Ideas
{
    [HubName("ideaHub")]
    public class IdeaHub: Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void VotedIdea(dynamic options)
        {
            Clients.Others.votedIdea(options);
        }
    }
}
