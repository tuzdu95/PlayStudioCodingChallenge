using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Domain.Entities;
using PlayStudioCodingChallenge.Domain.ValueObjects;
using PlayStudioCodingChallenge.Persistence.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Persistence.Repositories
{
    public class ConfigurationRepository : BaseRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IOptions<MongoConfiguration> databaseSettings) : base(databaseSettings)
        {

        }

        public async Task<LevelBonusRate> GetBonusByLevel(int playerLevel)
        {
           var currentConfig =  (await _entityCollection.Find(Builders<Configuration>.Filter.Empty).ToListAsync()).FirstOrDefault();
           var playerLevelBonusRate = currentConfig.LevelBonusRates.Where(item => playerLevel >= item.Level).OrderByDescending(item=>item.Level).FirstOrDefault();
           return playerLevelBonusRate;
        }

        public async Task<RateFromBet> GetRateByAmountBet(double amountBet)
        {
            var currentConfig = (await _entityCollection.Find(Builders<Configuration>.Filter.Empty).ToListAsync()).FirstOrDefault();
            var amountBetRate = currentConfig.RateFromBets.Where(item => amountBet >= item.MinimumBet).OrderByDescending(item => item.MinimumBet).FirstOrDefault();
            return amountBetRate;
        }
    }
}
