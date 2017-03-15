using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace Propose.Features.Ideas
{
    [HubName("ideaHub")]
    public class IdeaHub: Hub
    {
        public void VotedIdea(dynamic options)
        {
            options.eventType = IdeaEventType.VotedIdea;
            Clients.Others.votedIdea(options);
        }

        public void IdeaAddedOrUpdated(dynamic options)
        {
            options.eventType = IdeaEventType.IdeaAddedOrUpdated;
                
            Clients.Others.ideaAddedOrUpdated(options);
        }
    }

    public enum IdeaEventType {
        VotedIdea,
        IdeaAddedOrUpdated
    }
}
