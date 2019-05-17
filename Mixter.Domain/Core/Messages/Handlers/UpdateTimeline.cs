using System.Xml.Schema;
using Mixter.Domain.Core.Messages.Events;

namespace Mixter.Domain.Core.Messages.Handlers
{
    [Handler]
    public class UpdateTimeline : 
        IEventHandler<MessageQuacked>, IEventHandler<MessageDeleted>
    {
        private readonly ITimelineMessageRepository _repository;
        
        public UpdateTimeline(ITimelineMessageRepository repo)
        {
            _repository = repo;
        }
        
        public void Handle(MessageQuacked evt)
        {
            if (_repository == null)
            {
                return;
            }
            
            _repository.Save(new TimelineMessageProjection(evt.Author, evt.Author, evt.Content, evt.Id));
        }
        
        public void Handle(MessageDeleted evt)
        {
            if (_repository == null)
            {
                return;
            }
            
            _repository.Delete(evt.MessageId);
        }
    }
}
