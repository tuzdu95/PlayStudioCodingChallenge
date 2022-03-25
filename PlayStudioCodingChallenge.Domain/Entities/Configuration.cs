using PlayStudioCodingChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Domain.Entities
{
    public class Configuration
    {
        public string Id { get; set; }
        public IEnumerable<RateFromBet> RateFromBets { get; set; }
        public IEnumerable<LevelBonusRate> LevelBonusRates { get; set; }
    }
}
