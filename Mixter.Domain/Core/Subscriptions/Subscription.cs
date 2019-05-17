using System.Collections.Generic;
using Mixter.Domain.Core.Messages;
using Mixter.Domain.Core.Messages.Events;
using Mixter.Domain.Core.Subscriptions.Events;
using Mixter.Domain.Identity;

namespace Mixter.Domain.Core.Subscriptions
{
    [Aggregate]
    public class Subscription
    {
        private static readonly DecisionProjection _projection = new DecisionProjection();

        public Subscription(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                _projection.Apply(@event);
            }
        }
        
        [Command]
        public static void FollowUser(IEventPublisher eventPublisher, UserId Follower, UserId Followee)
        {
            var subscription = new SubscriptionId(Follower, Followee);
            
            eventPublisher.Publish(new UserFollowed(subscription));
        }

        [Command]
        public void Unfollow(IEventPublisher eventPublisher)
        {
            eventPublisher.Publish(new UserUnfollowed(_projection.Id));
        }
        
        [Projection]
        private class DecisionProjection : DecisionProjectionBase
        {
            private readonly IList<SubscriptionId> _subscriptions = new List<SubscriptionId>();

            public SubscriptionId Id { get; private set; }

            public IEnumerable<SubscriptionId> Subscriptions
            {
                get { return _subscriptions; }
            }

            public DecisionProjection()
            {
                AddHandler<UserFollowed>(When);
                AddHandler<UserUnfollowed>(When);
            }

            private void When(UserFollowed evt)
            {
                Id = evt.SubscriptionId;
                
                _subscriptions.Add(evt.SubscriptionId);
            }

            private void When(UserUnfollowed evt)
            {
                
            }
        }
    }
}
