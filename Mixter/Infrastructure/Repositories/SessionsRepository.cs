﻿using System.Collections.Generic;
using Mixter.Domain.Identity;

namespace Mixter.Infrastructure.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly IDictionary<SessionId, SessionProjection> _projectionsById = new Dictionary<SessionId, SessionProjection>();

        public void Save(SessionProjection projection)
        {
            _projectionsById.Add(projection.SessionId, projection);
        }

        public void ReplaceBy(SessionProjection projection)
        {
            _projectionsById[projection.SessionId] = projection;
        }

        public UserId? GetUserIdOfSession(SessionId sessionId)
        {
            if (!_projectionsById.ContainsKey(sessionId))
            {
                return null;
            }

            var projectionOfSession = _projectionsById[sessionId];
            return projectionOfSession.SessionState == SessionState.Enabled
                ? projectionOfSession.UserId 
                : (UserId?) null;
        }
    }
}
