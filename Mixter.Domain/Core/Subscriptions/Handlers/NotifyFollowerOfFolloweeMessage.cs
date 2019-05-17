using Mixter.Domain.Core.Messages.Events;
using Mixter.Domain.Core.Subscriptions.Events;
using Mixter.Domain.Identity;

namespace Mixter.Domain.Core.Subscriptions.Handlers
{
    [Handler]
    public class NotifyFollowerOfFolloweeMessage : IEventHandler<MessageQuacked>, IEventHandler<MessageRequacked>
    {
        private IFollowersRepository _followersRepository;
        private ISubscriptionsRepository _subscriptionsRepository;
        private IEventPublisher _eventPublisher;

        public NotifyFollowerOfFolloweeMessage(IFollowersRepository followersRepository, ISubscriptionsRepository subscriptionsRepository, IEventPublisher eventPublisher)
        {
            _followersRepository = followersRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _eventPublisher = eventPublisher;
        }

        public void Handle(MessageQuacked evt)
        {
            foreach (var userId in _followersRepository.GetFollowers(evt.Author))
            {
                _eventPublisher.Publish(new FolloweeMessageQuacked(new SubscriptionId(userId, evt.Author), evt.Id));
            }
        }
        
        public void Handle(MessageRequacked evt)
        {
            foreach (var userId in _followersRepository.GetFollowers(evt.Requacker))
            {
                _eventPublisher.Publish(new FolloweeMessageQuacked(new SubscriptionId(userId, evt.Requacker), evt.Id));
            }
        }
    }
}
