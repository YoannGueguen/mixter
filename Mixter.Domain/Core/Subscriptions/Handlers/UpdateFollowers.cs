using Mixter.Domain.Core.Subscriptions.Events;

namespace Mixter.Domain.Core.Subscriptions.Handlers
{
    [Handler]
    public class UpdateFollowers: IEventHandler<UserFollowed>
    {
        private readonly IFollowersRepository _repository;
            
        public UpdateFollowers(IFollowersRepository repo)
        {
            _repository = repo;
        }
            
        public void Handle(UserFollowed evt)
        {
            if (_repository == null)
            {
                return;
            }
                
            _repository.Save(new FollowerProjection(evt.SubscriptionId.Followee, evt.SubscriptionId.Follower));
        }
    }
}
