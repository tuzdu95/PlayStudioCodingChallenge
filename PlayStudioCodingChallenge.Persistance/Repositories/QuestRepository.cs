using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Domain.Entities;
using PlayStudioCodingChallenge.Persistence.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Persistence.Repositories
{
    public class QuestRepository : BaseRepository<Quest>, IQuestRepository
    {
        public QuestRepository(IOptions<MongoConfiguration> databaseSettings) : base(databaseSettings)
        {
            
        }

        public async Task<Quest> GetQuestByPlayerId(string playerId)
        {
            var quests = await _entityCollection.Find(item => item.Player.Id == playerId).ToListAsync();
            var activeQuest = quests.Where(quest => quest.IsActive == true).FirstOrDefault();
            return activeQuest;
        }
    }
}
