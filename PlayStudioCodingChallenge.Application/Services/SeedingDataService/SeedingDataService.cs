using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Domain.Entities;
using PlayStudioCodingChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.Services
{
    public class SeedingDataService : ISeedingDataService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerRepository _playerRepository;
        public SeedingDataService(
            IConfigurationRepository configurationRepository,
            IQuestRepository questRepository,
            IPlayerRepository playerRepository)
        {
            _configurationRepository = configurationRepository;
            _questRepository = questRepository;
            _playerRepository = playerRepository;
        }
        public async Task InitializeData()
        {
            if (await IsDatabaseStateExists())
            {
                return;
            }
            var player = await InitPlayerData();
            await InitQuestData(player);
            await InitConfigurationData();
        }

        private async Task<bool> IsDatabaseStateExists()
        {
            var isConfigurationDbExists = await _configurationRepository.CheckDocumentExists();
            var isQuestDbExists = await _questRepository.CheckDocumentExists();
            var isPlayerDbExists = await _playerRepository.CheckDocumentExists();
            if (isConfigurationDbExists || isQuestDbExists || isPlayerDbExists) return true;
            return false;
        }

        private IEnumerable<RateFromBet> InitBetRateData()
        {
            var rateFromBets = new List<RateFromBet> {
                new RateFromBet
                {
                    MinimumBet = 1,
                    Rate = 100
                },
                new RateFromBet
                {
                    MinimumBet = 1000,
                    Rate = 110
                }, 
                new RateFromBet
                {
                    MinimumBet = 5000,
                    Rate = 120
                },
                new RateFromBet
                {
                    MinimumBet = 10000,
                    Rate = 130
                }
            };
            return rateFromBets;
        }

        private IEnumerable<LevelBonusRate> InitRateFromLevelData()
        {
            var levelBonusRates = new List<LevelBonusRate> {
                new LevelBonusRate
                {
                    Level = 0,
                    BonusRate = 0
                },
                new LevelBonusRate
                {
                    Level = 1,
                    BonusRate = 500
                },
                new LevelBonusRate
                {
                    Level = 2,
                    BonusRate = 1000
                },
                new LevelBonusRate
                {
                    Level = 3,
                    BonusRate = 2000
                }
            };
            return levelBonusRates;
        }
        private async Task<Quest> InitQuestData(Player player)
        {
            var quest = new Quest
            {
                Name = "Quest1",
                Milestones =  new List<Milestone> {
                new Milestone()
                {
                    Index=1,
                    Name = "milestone1",
                    PointToComplete = 10000,
                    Award = 1000
                }, new Milestone()
                {
                    Index=2,
                    Name = "milestone2",
                    PointToComplete = 15000,
                    Award = 1500
                },new Milestone()
                {
                    Index=3,
                    Name = "milestone3",
                    PointToComplete = 20000,
                    Award = 2000
                }
            },
                Player = player,
                IsComplete = false,
                IsActive = true,
                PointToComplete = 25000,
                NumberOfMilestones =3
            };
            var result = await _questRepository.AddAsync(quest);
            return result;
        }

        private async Task<Player> InitPlayerData()
        {
            var player = new Player
            {
                Name = "Tung Duong",
                Level = 1
            };
            var result = await _playerRepository.AddAsync(player);
            return result;
        }
        private async Task<string> InitConfigurationData()
        {
            var config = new Configuration
            {
                LevelBonusRates = InitRateFromLevelData(),
                RateFromBets = InitBetRateData()
            };
            var result = await _configurationRepository.AddAsync(config);
            return result.Id;
        }
    }
}
