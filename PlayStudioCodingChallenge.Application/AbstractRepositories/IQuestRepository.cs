using PlayStudioCodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.AbstractRepository
{
    public interface IQuestRepository : IBaseRepository<Quest>
    {
        Task<Quest> GetQuestByPlayerId(string playerId);
    }
}
