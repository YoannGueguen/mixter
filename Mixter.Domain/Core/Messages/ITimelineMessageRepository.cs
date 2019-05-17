using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mixter.Domain.Identity;

namespace Mixter.Domain.Core.Messages
{
    [Repository]
    public interface ITimelineMessageRepository
    {
        void Save(TimelineMessageProjection messageProjection);

        void Delete(MessageId msgId);

        [Query]
        IEnumerable<TimelineMessageProjection> GetMessagesOfUser(UserId userId);
    }
}