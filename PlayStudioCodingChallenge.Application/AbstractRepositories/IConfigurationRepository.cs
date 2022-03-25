using PlayStudioCodingChallenge.Domain.Entities;
using PlayStudioCodingChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.AbstractRepository
{
    public interface IConfigurationRepository : IBaseRepository<Configuration>
    {
        Task<RateFromBet> GetRateByAmountBet(double amountBet);
        Task<LevelBonusRate> GetBonusByLevel(int playerLevel);
    }
}
